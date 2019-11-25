using AbhsChinese.Code.Common;
using AbhsChinese.Code.Json;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace AbhsChinese.Bll
{
    public class CourseBll : BllBase
    {
        //Yw_Course
        public CourseBll() : base()
        {
        }

        #region repository

        private ICourseIntroductionRespository courseIntroductionRespository;
        private ICourseLessonRepository courseLessonRepository;
        private ICoursePriceRepository coursePriceRepository;
        private ICourseRespository courseRepository;


        private IEmployeeRepository employeeRepository;

        public ICourseIntroductionRespository CourseIntroductionRespository
        {
            get
            {
                if (courseIntroductionRespository == null)
                {
                    courseIntroductionRespository = new CourseIntroductionRespository();
                }
                return courseIntroductionRespository;
            }
        }

        private ICourseProcessRepository courseProcessRepository;

        public ICourseProcessRepository CourseProcessRepository
        {
            get
            {
                if (courseProcessRepository == null)
                {
                    courseProcessRepository = new CourseProcessRepository();
                }
                return courseProcessRepository;
            }
        }


        public ICourseLessonRepository CourseLessonRespository
        {
            get
            {
                if (courseLessonRepository == null)
                {
                    courseLessonRepository = new CourseLessonRepository();
                }
                return courseLessonRepository;
            }
        }

        public ICoursePriceRepository CoursePriceRepository
        {
            get
            {
                if (coursePriceRepository == null)
                {
                    coursePriceRepository = new CoursePriceRepository();
                }
                return coursePriceRepository;
            }
        }

        public ICourseRespository CourseRepository
        {
            get
            {
                if (courseRepository == null)
                {
                    courseRepository = new CourseRepository();
                }
                return courseRepository;
            }
        }

        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                if (employeeRepository == null)
                {
                    employeeRepository = new EmployeeRepository();
                }
                return employeeRepository;
            }
        }

        public IList<DtoCourseListItem> GetCourses(DtoCurriculumSearch search)
        {
            return CourseRepository.GetByPage(search);
        }

        public IList<DtoCourseListItem> GetManageCourses(DtoCurriculumSearch search)
        {
            IList<DtoCourseListItem> items = GetCourses(search);
            IEnumerable<int> courseIds = items.Select(c => c.Ycs_Id);
            var prices = CoursePriceRepository.GetEntities(courseIds);
            foreach (var item in items)
            {
                if (item.Ycs_Price == null)
                {
                    item.Ycs_Price = new Dictionary<string, decimal>();
                }
                prices.Where(p => p.Yce_CourseId == item.Ycs_Id).ToList()
                    .ForEach(p => item.Ycs_Price.Add(p.Yce_SchoolLevelName, p.Yce_Price));
            }
            return items;
        }

        #endregion repository

        #region select

        public List<DtoKeyValue<int, string>> GetCourseByIdOrName(string idOrName)
        {
            var list = CourseRepository.GetCourseByIdOrName(idOrName);
            return list.Select(s => { return new DtoKeyValue<int, string>() { key = s.Ycs_Id, value = s.Ycs_Name }; }).ToList();
        }

        public List<DtoKeyValue<int, string>> GetCourseByTypeAndGrade(int type, int grade)
        {
            var list = CourseRepository.GetCourseByTypeAndGrade(type, grade);
            return list.Select(s => { return new DtoKeyValue<int, string>() { key = s.Ycs_Id, value = s.Ycs_Name }; }).ToList();
        }

        #endregion select

        public void AddCourse(AbhsChinese.Domain.Dto.Request.DtoCourse course)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var entity = course.ConvertTo<Yw_Course>(PropertyNamePrefixAction.Add);
                    entity.Ycs_CreateTime = Clock.Now;
                    entity.Ycs_Creator = course.CurrentUser;
                    entity.Ycs_UpdateTime = Clock.Now;
                    entity.Ycs_Editor = course.CurrentUser;
                    entity.Ycs_PublishTime = Clock.MinValue;
                    CourseRepository.InsertCourse(entity);
                    CourseLessonRespository.Insert(
                        entity.Ycs_Id,
                        course.Lessons.ToList(),
                        1,//序号从1开始添加
                        course.CurrentUser);

                    UpdateProcess(
                        new DtoCoursePricing { CourseId = entity.Ycs_Id, NextStatus = CourseStatusEnum.未定价 },
                        course.CurrentUser,
                        CourseActionEnum.添加课程);

                    scope.Complete();
                }
                catch
                {
                    RollbackTran();
                    throw;
                }
            }
        }

        public Yw_Course GetCourse(int courseId)
        {
            return CourseRepository.GetCourse(courseId);
        }

        private IList<Yw_CourseProcess> GetLastestProcess(int courseId)
        {
            var processes = CourseProcessRepository.GetLastestEntity(courseId);
            return processes;
        }
        public void ReopenCourse(int courseId, int currentUser)
        {
            var course = GetCourse(courseId);
            if (course.Ycs_Status != (int)CourseStatusEnum.已关闭)
            {
                throw new AbhsException(ErrorCodeEnum.CanNotReopen,
                    AbhsErrorMsg.CanNotReopen);
            }
            Yw_CourseProcess[] processes = GetLastestProcess(courseId).ToArray();

            CourseStatusEnum repenStatus = CourseStatusEnum.待上架;
            if (processes[1].Ycp_Status == (int)CourseStatusEnum.未定价)
            {
                repenStatus = CourseStatusEnum.未定价;
            }

            UpdateStatus(
                courseId,
                repenStatus,
                CourseActionEnum.重新打开,
                currentUser);
        }

        public void UpdateStatus(
            int courseId,
            CourseStatusEnum status,
            CourseActionEnum action,
            int currentUser)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    var pricing = new DtoCoursePricing
                    {
                        CourseId = courseId,
                        NextStatus = status
                    };
                    UpdateProcess(pricing, currentUser, action);
                    UpdateStatus(pricing, currentUser);

                    scope.Complete();
                }
                catch
                {
                    RollbackTran();
                    throw;
                }
            }
        }

        public void Pricing(DtoCoursePricing price, int currentUser)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    SaveAllPrice(price, currentUser);
                    SaveIntroduction(price, currentUser);
                    UpdateProcess(price, currentUser, CourseActionEnum.定价);
                    UpdateStatus(price, currentUser);

                    scope.Complete();
                }
                catch
                {
                    RollbackTran();
                    throw;
                }
            }
        }

        private void UpdateStatus(DtoCoursePricing price, int currentUser)
        {
            var course = CourseRepository.GetCourse(price.CourseId);
            if (course == null)
            {
                throw new AbhsException(
                    ErrorCodeEnum.NotFoundEntity,
                    AbhsErrorMsg.NotFoundEntity);
            }
            else
            {
                course.EnableAudit();
                course.Ycs_Status = (int)price.NextStatus;
                course.Ycs_UpdateTime = Clock.Now;
                course.Ycs_Editor = currentUser;
                CourseRepository.Update(course);
            }
        }

        private void UpdateProcess(
            DtoCoursePricing price,
            int currentUser,
            CourseActionEnum action)
        {
            CourseProcessRepository.Insert(new Yw_CourseProcess
            {
                Ycp_CourseId = price.CourseId,
                Ycp_Action = (int)action,
                Ycp_Status = (int)price.NextStatus,
                Ycp_Operator = currentUser,
                Ycp_Remark = "",
                Ycp_CreateTime = Clock.Now
            });
        }

        private void SaveAllPrice(DtoCoursePricing price, int currentUser)
        {
            if (price.Pricings != null)
            {
                foreach (var pricing in price.Pricings)
                {
                    SavePrice(price.CourseId, pricing.Price, pricing.SchoolLevelId, currentUser);
                }
            }
        }
        private void SavePrice(int courseId, decimal price, int level, int currentUser)
        {
            if (price >= 0)
            {
                bool exist = false;
                var result = CoursePriceRepository.GetEntity(courseId, level);
                if (result == null)
                {
                    result = new Yw_CoursePrice
                    {
                        Yce_CourseId = courseId,
                        Yce_SchoolLevelId = level,
                        Yce_CreateTime = Clock.Now,
                        Yce_Creator = currentUser
                    };
                }
                else
                {
                    exist = true;
                    result.EnableAudit();
                }
                result.Yce_Price = price;
                result.Yce_UpdateTime = Clock.Now;
                result.Yce_Editor = currentUser;
                if (exist)
                {
                    CoursePriceRepository.Update(result);
                }
                else
                {
                    CoursePriceRepository.Insert(result);
                }
            }
        }

        private void SaveIntroduction(
            DtoCoursePricing price,
            int currentUser)
        {
            bool exist = false;
            var introduction = CourseIntroductionRespository.GetCourseIntroduction(
                        price.CourseId);
            if (introduction != null)
            {
                introduction.EnableAudit();
                exist = true;
            }
            else
            {
                introduction = new Yw_CourseIntroduction();
                introduction.Yci_CourseId = price.CourseId;
                introduction.Yci_CreateTime = Clock.Now;
                introduction.Yci_Creator = currentUser;
            }

            introduction.Yci_Introduction = price.Introduction;
            introduction.Yci_Arrange = price.Arrange;
            introduction.Yci_Editor = currentUser;
            introduction.Yci_UpdateTime = Clock.Now;
            if (exist)
            {
                CourseIntroductionRespository.Update(introduction);
            }
            else
            {
                CourseIntroductionRespository.Insert(introduction);
            }
        }

        public Domain.Dto.Response.DtoCourse GetCourseInfo(int courseId)
        {
            Yw_Course c = CourseRepository.GetCourse(courseId);
            if (c != null)
            {
                //获取人员id
                List<int> ids = new List<int>();

                #region 计算课程中所有的人员id

                if (c.Ycs_Owner != 0)
                {
                    ids.Add(c.Ycs_Owner);
                }
                if (c.Ycs_Creator != 0)
                {
                    ids.Add(c.Ycs_Creator);
                }
                if (c.Ycs_Editor != 0)
                {
                    ids.Add(c.Ycs_Editor);
                }
                foreach (string id in c.Ycs_Employees.Split(','))
                {
                    int i = 0;
                    if (int.TryParse(id, out i))
                    {
                        ids.Add(i);
                    }
                }

                #endregion 计算课程中所有的人员id

                Dictionary<int, string> names = EmployeeRepository.GetEmployeeNameByIds(ids);

                #region 计算姓名

                string ownerName = names.ContainsKey(c.Ycs_Owner) ? names[c.Ycs_Owner] : "";
                string employeesName = string.Join(",", names.Where(s => c.Ycs_Employees.Split(',').Contains(s.Key.ToString())).Select(s => s.Value).ToArray());
                string creatorName = names.ContainsKey(c.Ycs_Creator) ? names[c.Ycs_Creator] : "";
                string editorName = names.ContainsKey(c.Ycs_Editor) ? names[c.Ycs_Editor] : "";

                #endregion 计算姓名

                return new Domain.Dto.Response.DtoCourse
                {
                    Id = c.Ycs_Id,
                    Name = c.Ycs_Name,
                    CourseType = c.Ycs_CourseType,
                    Grade = c.Ycs_Grade,
                    CoverImage = c.Ycs_CoverImage,
                    LessonCount = c.Ycs_LessonCount,
                    Owner = c.Ycs_Owner,
                    OwnerName = ownerName,
                    Employees = c.Ycs_Employees,
                    EmployeesName = employeesName,
                    ResourceGroupId = c.Ycs_ResourceGroupId,
                    ResourceGroupName = "",//待实现
                    Description = c.Ycs_Description,
                    SellCount = c.Ycs_SellCount,
                    Status = c.Ycs_Status,
                    PublishTime = c.Ycs_PublishTime,
                    CreateTime = c.Ycs_CreateTime,
                    Creator = c.Ycs_Creator,
                    CreatorName = creatorName,
                    Editor = c.Ycs_Editor,
                    EditorName = editorName
                };
            }
            else
            {
                return null;
            }
        }

        public void AddCoverImage(int courseId, string coverImage)
        {
            EditCoverImage(courseId, coverImage);
        }

        public void RemoveCoverImage(int courseId)
        {
            EditCoverImage(courseId, string.Empty);
        }

        private void EditCoverImage(int courseId, string coverImage = "")
        {
            var course = CourseRepository.GetCourse(courseId);
            if (course != null)
            {
                course.EnableAudit();
                course.Ycs_CoverImage = coverImage;
                CourseRepository.Update(course);
            }
        }

        public Yw_CoursePrice GetCoursePrice(int courseId, int schoolLevelId)
        {
            return CoursePriceRepository.GetPrice(courseId, schoolLevelId);
        }

        /// <summary>
        /// 课程上下架详情
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public DtoCourseDetails GetDetailOfManagements(int courseId)
        {
            Yw_Course entity = GetCourse(courseId);

            DtoCourseDetails details = entity.ConvertTo<DtoCourseDetails>(
                PropertyNamePrefixAction.Remove);

            details.OwnerName = new EmployeeBll().GetEmployeeEntity(entity.Ycs_Owner)?.Bem_Name;

            var introduction =
                CourseIntroductionRespository.GetCourseIntroduction(courseId);
            details.Introduction = introduction?.Yci_Introduction;
            details.Arrange = introduction?.Yci_Arrange;

            var prices = CoursePriceRepository.GetEntities(new List<int> { courseId });
            foreach (var item in prices)
            {
                details.Pricings.Add(new DtoPricing
                {
                    Price = item.Yce_Price,
                    SchoolLevelId = item.Yce_SchoolLevelId
                });
            }

            var lessons = new LessonBll().GetLessons(courseId);
            int approvedStatus = (int)LessonStatusEnum.合格;
            var indexes = lessons.Where(l => l.Status == approvedStatus).Select(l => l.Index);
            details.ApprovedLessons = indexes.ToList();

            return details;
        }

        /// <summary>
        /// 课程管理详情
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public DtoCourseDetails GetDetails(int courseId)
        {
            Yw_Course entity = GetCourse(courseId);

            DtoCourseDetails details = entity.ConvertTo<DtoCourseDetails>(
                PropertyNamePrefixAction.Remove);
            details.ResourceGroupName = new ResourceBll().GetResourceGroupName(
                entity.Ycs_ResourceGroupId);

            List<int> employeeIds = new List<int>();
            employeeIds.Add(entity.Ycs_Owner);//负责人
            var teacherIds = entity.Ycs_Employees.Split(',').Select(e => e._ToInt32());
            employeeIds.AddRange(teacherIds);
            IList<Bas_Employee> teachers = new EmployeeBll().GetEmployees(employeeIds);
            string ownerName = teachers.FirstOrDefault(
                t => t.Bem_Id == entity.Ycs_Owner)?.Bem_Name;
            details.OwnerName = ownerName;
            var teacherNames = teachers.Where(t => t.Bem_Id != entity.Ycs_Owner)
                                       .Select(t => t.Bem_Name);
            details.Teachers = string.Join(",", teacherNames);

            return details;
        }

        public IList<DtoLesson> GetLessons(int courseId)
        {
            return CourseLessonRespository.GetLessonsWithProcessInfo(courseId);
        }

        public IList<DtoLesson> GetLogs(int courseId)
        {
            return CourseLessonRespository.GetLessonsWithProcessInfo(courseId);
        }

        public List<DtoKeyValue<int, string>> GetPagingCourseByIdOrName(PagingObject paging, string idOrName)
        {
            var list = CourseRepository.GetPagingCourseByIdOrName(paging, idOrName);
            return list.Select(s => { return new DtoKeyValue<int, string>() { key = s.Ycs_Id, value = s.Ycs_Name }; }).ToList();
        }

        #region 选课中心

        /// <summary>
        /// 选课中心课程详情
        /// </summary>
        /// <param name="courseId">课程Id</param>
        /// <param name="studentId">学生Id</param>
        /// <returns></returns>
        public DtoSelectCenterCourseDetailResult GetCourseDetailForSelectCenter(int courseId, int studentId)
        {
            DtoSelectCenterCourseDetailResult result = new DtoSelectCenterCourseDetailResult();
            DtoCourseSelectCondition condition = new DtoCourseSelectCondition();
            condition.StudentId = studentId;

            DtoSelectCenterCourseDetailObject course = null;
            Dictionary<string, decimal> voucherDic = null;
            Yw_StudentOrder order = null;
            DtoStudentApplySchool applyRecord = null;
            SchoolBll schBll = new SchoolBll();
            Bas_School school = schBll.GetSchoolByStudent(condition.StudentId);
            if (school != null)
            {
                condition.SetSchoolLevel(school.Bsl_Level);
                condition.SetSchoolId(school.Bsl_Id);
                course = CourseRepository.GetCourseDetailWithPrice(courseId, condition.SchoolLevel);
                if (course != null && course.CourseId > 0)
                {
                    DtoSimpleCourse simpleCourse = new DtoSimpleCourse() { CourseId = course.CourseId, Amount = course.CoursePrice, CourseType = course.CourseType, Grade = course.Grade };
                    voucherDic = GetVoucherDicForUserCourse(new List<DtoSimpleCourse>() { simpleCourse }, condition);

                    //查询用户是否已购买此课程
                    StudentOrderBll studentOrderBll = new StudentOrderBll();
                    order = studentOrderBll.GetFinishOrder(condition.StudentId, course.CourseId);
                }
            }
            else
            {
                course = CourseRepository.GetCourseDetailWithoutPrice(courseId);
                StudentApplyBll studentApplyBll = new StudentApplyBll();
                applyRecord = studentApplyBll.GetApplyByStudentId(condition.StudentId);
            }
            if (course != null)
            {
                Yw_CourseIntroduction introduction = CourseIntroductionRespository.GetCourseIntroduction(course.CourseId);
                result = CreateSelectCenterCourseDetailResultItem(course, introduction, school, applyRecord, voucherDic, order);
            }
            else
            {
                throw new AbhsException(ErrorCodeEnum.CourseNotExists, AbhsErrorMsg.ConstCourseNotExists);
            }

            return result;
        }
        /// <summary>
        /// 选课中心课程详情-预览功能
        /// </summary>
        /// <param name="courseId">课程Id</param>
        /// <param name="studentId">学生Id</param>
        /// <returns></returns>
        public DtoSelectCenterCourseDetailResult GetCourseDetailForPreview(int courseId, int studentId)
        {
            DtoSelectCenterCourseDetailResult result = new DtoSelectCenterCourseDetailResult();
            DtoCourseSelectCondition condition = new DtoCourseSelectCondition();
            condition.StudentId = studentId;

            DtoSelectCenterCourseDetailObject course = null;
            Dictionary<string, decimal> voucherDic = null;
            Yw_StudentOrder order = null;
            DtoStudentApplySchool applyRecord = null;
            SchoolBll schBll = new SchoolBll();
            Bas_School school = schBll.GetSchoolByStudent(condition.StudentId);
            if (school != null)
            {
                condition.SetSchoolLevel(school.Bsl_Level);
                condition.SetSchoolId(school.Bsl_Id);
                course = CourseRepository.GetCourseDetailWithPrice(courseId, condition.SchoolLevel, true);
                if (course != null && course.CourseId > 0)
                {
                    DtoSimpleCourse simpleCourse = new DtoSimpleCourse() { CourseId = course.CourseId, Amount = course.CoursePrice, CourseType = course.CourseType, Grade = course.Grade };
                    voucherDic = GetVoucherDicForUserCourse(new List<DtoSimpleCourse>() { simpleCourse }, condition);

                    //查询用户是否已购买此课程
                    StudentOrderBll studentOrderBll = new StudentOrderBll();
                    order = studentOrderBll.GetFinishOrder(condition.StudentId, course.CourseId);
                }
            }
            else
            {
                course = CourseRepository.GetCourseDetailWithoutPrice(courseId, true);
                StudentApplyBll studentApplyBll = new StudentApplyBll();
                applyRecord = studentApplyBll.GetApplyByStudentId(condition.StudentId);
            }
            if (course != null)
            {
                Yw_CourseIntroduction introduction = CourseIntroductionRespository.GetCourseIntroduction(course.CourseId);
                result = CreateSelectCenterCourseDetailResultItem(course, introduction, school, applyRecord, voucherDic, order);
            }
            else
            {
                throw new AbhsException(ErrorCodeEnum.CourseNotExists, AbhsErrorMsg.ConstCourseNotExists);
            }

            return result;
        }

        /// <summary>
        /// 选课中心课程列表(不再使用)
        /// </summary>
        public List<DtoSelectCenterResult> GetCourseForSelectCenter(DtoCourseSelectCondition condition, PagingObject paging)
        {
            SchoolBll schBll = new SchoolBll();
            Bas_School school = schBll.GetSchoolByStudent(condition.StudentId);
            if (school != null)
            {
                return GetCourseWithPriceForSelectCenter(school, condition, paging);
            }
            else
            {
                return GetCourseWithoutPriceForSelectCenter(condition, paging);
            }
        }

        /// <summary>
        /// 选课中心课程列表
        /// </summary>
        public List<DtoSelectCenterResult> GetCourseForSelectCenter(DtoCourseSelectCondition condition, PagingObject paging, out bool includePrice)
        {
            SchoolBll schBll = new SchoolBll();
            Bas_School school = schBll.GetSchoolByStudent(condition.StudentId);
            if (school != null)
            {
                includePrice = true;
                return GetCourseWithPriceForSelectCenter(school, condition, paging);
            }
            else
            {
                includePrice = false;
                return GetCourseWithoutPriceForSelectCenter(condition, paging);
            }
        }

        private DtoSelectCenterCourseDetailResult CreateSelectCenterCourseDetailResultItem(DtoSelectCenterCourseDetailObject obj, Yw_CourseIntroduction introduction, Bas_School school, DtoStudentApplySchool applySchoolRecord = null, Dictionary<string, decimal> voucherDic = null, Yw_StudentOrder order = null)
        {
            DtoSelectCenterCourseDetailResult result = new DtoSelectCenterCourseDetailResult();
            result.IsBindSchool = (school != null && school.Bsl_Id > 0) ? true : false;

            result.CourseId = obj.CourseId;
            result.CourseImage = obj.CourseImage.ToOssPath();
            result.CourseName = obj.CourseName;
            result.CourseType = obj.CourseType;
            result.Grade = obj.Grade;
            result.LessonCount = obj.LessonCount;
            result.Description = obj.Description;
            result.Introduction = introduction?.Yci_Introduction;
            result.Arrange = introduction?.Yci_Arrange;

            if (result.IsBindSchool)
            {
                result.SellCount = obj.SellCount;
                result.CoursePrice = obj.CoursePrice;
                result.BaseVoucherPrice = voucherDic.ContainsKey(obj.CourseId + "_" + 1) ? voucherDic[obj.CourseId + "_" + 1] : 0;
                result.SchoolVoucherPrice = voucherDic.ContainsKey(obj.CourseId + "_" + 2) ? voucherDic[obj.CourseId + "_" + 2] : 0;
            }

            if (applySchoolRecord != null && applySchoolRecord.Yay_Id > 0)
            {
                result.StudentApplySchoolStatus = applySchoolRecord.Yay_Status;
                result.StudentApplySchoolName = applySchoolRecord.SchoolName;
            }
            if (order != null && order.Yod_Id > 0)
            {
                result.IsHaveThisCourse = true;
            }
            return result;
        }

        private DtoSelectCenterResult CreateSelectCenterResultItem(DtoSelectCenterCourseObject obj, Dictionary<string, decimal> voucherDic)
        {
            DtoSelectCenterResult result = new DtoSelectCenterResult();
            result.CourseId = obj.CourseId;
            result.CoursePrice = obj.CoursePrice;
            result.CourseImage = obj.CourseImage.ToOssPath();
            result.CourseName = obj.CourseName;
            result.CourseType = obj.CourseType;
            result.Grade = obj.Grade;
            result.BaseVoucherPrice = voucherDic.ContainsKey(obj.CourseId + "_" + 1) ? voucherDic[obj.CourseId + "_" + 1] : 0;
            result.SchoolVoucherPrice = voucherDic.ContainsKey(obj.CourseId + "_" + 2) ? voucherDic[obj.CourseId + "_" + 2] : 0;
            result.LessonCount = obj.LessonCount;
            result.SellCount = obj.SellCount;
            return result;
        }

        private DtoSelectCenterResult CreateSelectCenterResultItem(DtoSelectCenterCourseObject obj)
        {
            return CreateSelectCenterResultItem(obj, new Dictionary<string, decimal>());
        }

        /// <summary>
        /// 选课中心课程列表不包含价格（学生没有绑定校区）
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        private List<DtoSelectCenterResult> GetCourseWithoutPriceForSelectCenter(DtoCourseSelectCondition condition, PagingObject paging)
        {
            List<DtoSelectCenterResult> results = new List<DtoSelectCenterResult>();

            List<DtoSelectCenterCourseObject> courseList = CourseRepository.GetCourseWithoutPrice(condition, paging);

            if (courseList.Count > 0)
            {
                foreach (DtoSelectCenterCourseObject obj in courseList)
                {
                    results.Add(CreateSelectCenterResultItem(obj));
                }
            }

            return results;
        }

        /// <summary>
        /// 选课中心课程列表包含价格（学生已经绑定校区）
        /// </summary>
        private List<DtoSelectCenterResult> GetCourseWithPriceForSelectCenter(Bas_School school, DtoCourseSelectCondition condition, PagingObject paging)
        {
            List<DtoSelectCenterResult> results = new List<DtoSelectCenterResult>();

            condition.SetSchoolLevel(school.Bsl_Level);
            condition.SetSchoolId(school.Bsl_Id);

            List<DtoSelectCenterCourseObject> courseList = CourseRepository.GetCourseWithPrice(condition, paging);

            if (courseList.Count > 0)
            {
                List<DtoSimpleCourse> simpleCourses = courseList.Select(
                    x => new DtoSimpleCourse() { CourseId = x.CourseId, Amount = x.CoursePrice, CourseType = x.CourseType, Grade = x.Grade })
                    .ToList();

                Dictionary<string, decimal> voucherDic = GetVoucherDicForUserCourse(simpleCourses, condition);

                foreach (DtoSelectCenterCourseObject obj in courseList)
                {
                    results.Add(CreateSelectCenterResultItem(obj, voucherDic));
                }
            }
            return results;
        }

        private Dictionary<string, decimal> GetVoucherDicForUserCourse(List<DtoSimpleCourse> simpleCourses, DtoCourseSelectCondition condition)
        {
            CashVoucherBll voucherBll = new CashVoucherBll();
            List<DtoVoucherForUserCourse> vouchers = voucherBll.GetBestVoucherForUserCourse(simpleCourses, condition.StudentId, condition.SchoolId);
            Dictionary<string, decimal> voucherDic = new Dictionary<string, decimal>();
            foreach (DtoVoucherForUserCourse voucher in vouchers)
            {
                voucherDic[voucher.CourseId + "_" + voucher.VoucherType] = voucher.Amount;
            }

            return voucherDic;
        }

        #endregion 选课中心
    }
}