using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll
{
    public class LessonBll : BllBase
    {

        private ICourseLessonRepository lessonServer;

        public ICourseLessonRepository LessonServer
        {
            get
            {
                if (lessonServer == null)
                {
                    lessonServer = new CourseLessonRepository();
                }

                lessonServer.TranId = tranId;
                return lessonServer;
            }
        }


        private ICourseLessonProcessRepository lessonProcessServer;

        public ICourseLessonProcessRepository LessonProcessServer
        {
            get
            {
                if (lessonProcessServer == null)
                {
                    lessonProcessServer = new CourseLessonProcessRepository();
                }

                lessonProcessServer.TranId = tranId;
                return lessonProcessServer;
            }
        }


        private IEmployeeRepository employeeserver;
        public IEmployeeRepository EmployeeServer
        {
            get
            {
                if (employeeserver == null)
                {
                    employeeserver = new EmployeeRepository();
                }
                return employeeserver;
            }
        }
        /// <summary>
        /// 根据课时id查询课时信息,(含审批id)
        /// </summary>
        /// <param name="lessonid"></param>
        /// <returns></returns>
        public DtoLesson Select(int lessonid)
        {
            return LessonServer.SelectByLessonId(lessonid);
        }

        public Yw_CourseLesson GetLesson(int lessonId)
        {
            return LessonServer.Select(lessonId);
        } 

        /// <summary>
        /// 根据课程id获取课时信息
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<DtoLesson> GetLessons(int courseId)
        {
            var list = LessonServer.GetLessonsWithProcessInfo(courseId).ToList();
            #region 循环获取所有的id
            List<int> ids = new List<int>();
            foreach (var lesson in list)
            {
                if (!ids.Contains(lesson.Producer))
                {
                    ids.Add(lesson.Producer);
                }
                if (!ids.Contains(lesson.Approver))
                {
                    ids.Add(lesson.Approver);
                }
                if (!ids.Contains(lesson.Creator))
                {
                    ids.Add(lesson.Creator);
                }
                if (!ids.Contains(lesson.Editor))
                {
                    ids.Add(lesson.Editor);
                }
            }
            #endregion
            #region 循环给人员名称赋值
            Dictionary<int, string> names = EmployeeServer.GetEmployeeNameByIds(ids);
            foreach (var lesson in list)
            {
                if (names.ContainsKey(lesson.Producer))
                {
                    lesson.ProducerName = names[lesson.Producer];
                }
                if (names.ContainsKey(lesson.Approver))
                {
                    lesson.ApproverName = names[lesson.Approver];
                }
                if (names.ContainsKey(lesson.Creator))
                {
                    lesson.CreatorName = names[lesson.Creator];
                }
                if (names.ContainsKey(lesson.Editor))
                {
                    lesson.EditorName = names[lesson.Editor];
                }
            }
            #endregion
            return list;
        }
        /// <summary>
        /// 根据课程id获取课时数量
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public int GetLessonsCountByCourse(int courseId)
        {
            return LessonServer.SelectCountByCourse(courseId);
        }

        /// <summary>
        /// 更新课程状态
        /// </summary>
        /// <param name="lessonid">课程id</param>
        /// <param name="status">课程状态</param>
        /// <param name="editor">操作人</param>
        /// <returns>返回课时操作流程id(如果为0表示不能修改)</returns>
        public int UpdateLessonStatus(int lessonid, LessonStatusEnum status, int editor)
        {
            var lesson = LessonServer.Select(lessonid);
            if (lesson != null)
            {
                LessonStatusEnum oldStatus = (LessonStatusEnum)lesson.Ycl_Status;
                LessonStatusActionEnum action = 0;//枚举值定义为0表示不能修改

                //未编辑只能改为制作中,动作为编辑
                if (oldStatus == LessonStatusEnum.未编辑 && status == LessonStatusEnum.制作中)
                {
                    action = LessonStatusActionEnum.编辑;
                    lesson.Ycl_Producer = editor;//记录制作人
                }
                //制作中只能改为待审批,动作为完成编辑
                else if (oldStatus == LessonStatusEnum.制作中 && status == LessonStatusEnum.待审批)
                {
                    action = LessonStatusActionEnum.完成编辑;
                    lesson.Ycl_Producer = editor;//记录制作人
                    lesson.Ycl_Approver = 0;//清空审批人
                }
                //待审批只能改为审批中,动作为审批
                else if (oldStatus == LessonStatusEnum.待审批 && status == LessonStatusEnum.审批中)
                {
                    action = LessonStatusActionEnum.审批;
                }
                //审批中能改为合格与不合格,动作为审批
                else if (oldStatus == LessonStatusEnum.审批中 && (status == LessonStatusEnum.合格 || status == LessonStatusEnum.不合格))
                {
                    action = LessonStatusActionEnum.审批;
                    lesson.Ycl_Approver = editor;//记录审批人
                    if (status == LessonStatusEnum.合格)//如果是合格状态,就会修改题目数据
                    {
                        List<int> questionids = new LessonUnitBll().SelectLessonQuestions(lesson.Ycl_Id);//查询出本课时中所有的题目
                        new CourseSubjectRelBll().AddSubjectToLesson(lesson, questionids);//将题目id添加到课程题目对应表
                    }
                }
                //合格和不合格只能改为制作中,动作重新编辑
                else if ((oldStatus == LessonStatusEnum.合格 || oldStatus == LessonStatusEnum.不合格) && status == LessonStatusEnum.制作中)
                {
                    action = LessonStatusActionEnum.重新编辑;
                    lesson.Ycl_Producer = editor;//记录制作人
                    lesson.Ycl_Approver = 0;//清空审批人
                }

                if ((int)action != 0)//如果枚举值不为0 则表示可以进行修改
                {
                    //修改课时状态并更新
                    lesson.Ycl_Status = (int)status;
                    lesson.Ycl_Editor = editor;
                    lesson.Ycl_UpdateTime = DateTime.Now;
                    LessonServer.Update(lesson);

                    //创建审批对象
                    Yw_CourseLessonProcess lessonPorcess = new Yw_CourseLessonProcess
                    {
                        Ylp_CourseId = lesson.Ycl_CourseId,
                        Ylp_LessonId = lesson.Ycl_Id,
                        Ylp_Status = lesson.Ycl_Status,
                        Ylp_Action = (int)action,
                        Ylp_CreateTime = DateTime.Now,
                        Ylp_Operator = editor,
                        Ylp_Remark = $"课程“{lesson.Ycl_Name}”状态从 {oldStatus} 改为 {status}"
                    };
                    //添加课时操作流程
                    LessonProcessServer.Insert(lessonPorcess);
                    //返回课时操作流程id
                    return lessonPorcess.Ylp_Id;
                }
            }
            return 0;
        }

        /// <summary>
        /// 根据条件及分页信息查询审批过程中的课时信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<DtoLessonApprove> GetLessonApproveByPage(DtoApproveLessonSearch search)
        {
            return LessonServer.GetApproveLessonByPage(search).ToList();
        }
        /// <summary>
        /// 根据课程id及分页信息获取日志数据
        /// </summary>
        /// <param name="courseid"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<DtoLessonLog> GetLessonLogByPage(int courseid, DtoSearch search)
        {
            var list = LessonProcessServer.SelectProcessByCourseByPage(courseid, search);
            var emp = EmployeeServer.GetEmployeeNameByIds(list.Select(l => l.Ylp_Operator).Distinct().ToList());
            return list.Select(s => new DtoLessonLog
            {
                ID = s.Ylp_Id,
                Operator = s.Ylp_Operator,
                OperatorName = emp.ContainsKey(s.Ylp_Operator) ? emp[s.Ylp_Operator] : "",
                Remark = s.Ylp_Remark,
                Status = s.Ylp_Status,
                CreateTime = s.Ylp_CreateTime
            }).ToList();
        }

    }
}
