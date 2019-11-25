using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AbhsChinese.Bll
{
    public class CashVoucherBll : BllBase
    {
        public CashVoucherBll() : base()
        {

        }

        #region Repository
        private ICashVoucherRepository cashVoucherRepository;
        public ICashVoucherRepository CashVoucherRepository
        {
            get
            {
                if (cashVoucherRepository == null)
                {
                    cashVoucherRepository = new CashVoucherRepository();
                }

                return cashVoucherRepository;
            }
        }

        private ICashVoucherIndexRepository cashVoucherIndexRepository;
        public ICashVoucherIndexRepository CashVoucherIndexRepository
        {
            get
            {
                if (cashVoucherIndexRepository == null)
                {
                    cashVoucherIndexRepository = new CashVoucherIndexRepository();
                }

                return cashVoucherIndexRepository;
            }
        }

        private IStudentCashVoucherRepository studentCashVoucherRepository;
        public IStudentCashVoucherRepository StudentCashVoucherRepository
        {
            get
            {
                if (studentCashVoucherRepository == null)
                {
                    studentCashVoucherRepository = new StudentCashVoucherRepository();
                }

                return studentCashVoucherRepository;
            }
        }

        private IStudentGetCashVoucherRepository studentGetCashVoucherRepository;
        public IStudentGetCashVoucherRepository StudentGetCashVoucherRepository
        {
            get
            {
                if (studentGetCashVoucherRepository == null)
                {
                    studentGetCashVoucherRepository = new StudentGetCashVoucherRepository();
                }

                return studentGetCashVoucherRepository;
            }
        }
        #endregion

        #region 现金券管理
        public List<DtoCashVoucher> GetPagingCashVoucher(PagingObject paging, int id, string name, int status, int voucherType)
        {
            return CashVoucherRepository.GetPagingCashVoucher(paging, id, name, status, voucherType);
        }
        public List<DtoCashVoucher> GetPagingCashVoucherForSchool(PagingObject paging, int id, string name, int status, int schoolId)
        {
            return CashVoucherRepository.GetPagingCashVoucherForSchool(paging, id, name, status, schoolId);
        }

        public int Add(DtoCashVoucherRequest request)
        {
            Yw_CashVoucher cashVoucher = new Yw_CashVoucher()
            {
                Ycv_Name = request.Name,
                Ycv_VoucherType = request.VoucherType,
                Ycv_SchoolId = request.SchoolId,
                Ycv_PublishCount = request.PublishCount,
                Ycv_Amount = request.Amount,
                Ycv_LimitByPerson = request.LimitByPerson,
                Ycv_OrderAmountLimit = request.OrderAmountLimit,
                Ycv_ExpireType = request.ExpireType,
                Ycv_ExpireDate = request.ExpireDate.CompareTo(Convert.ToDateTime("1900-01-01")) == 0 ? request.ExpireDate : Convert.ToDateTime(request.ExpireDate.ToString("yyyy-MM-dd 23:59:59")),
                Ycv_ExpireDayCount = request.ExpireDay,
                Ycv_ApplyScopeType = request.ApplyScopeType,
                Ycv_ApplyGrade = request.Grade,
                Ycv_CourseType = request.CourseType,
                Ycv_CourseId = request.CourseId,
                Ycv_RelatedCourseId = request.RelatedCourseId,
                Ycv_UseWithVoucherType = request.UseWithVoucherType,
                Ycv_Remark = request.Remark,
                Ycv_Status = (int)CashVoucherStatusEnum.未启用,
                Ycv_CreateTime = DateTime.Now,
                Ycv_Creator = request.Creator,
                Ycv_UpdateTime = DateTime.Now,
                Ycv_Editor = request.Editor
            };
            return CashVoucherRepository.Add(cashVoucher);
        }

        public DtoCashVoucher GetVoucherById(int id)
        {
            return CashVoucherRepository.GetVoucherById(id);
        }

        public bool Update(DtoCashVoucherRequest request)
        {
            var cashVoucher = CashVoucherRepository.Get(request.Id);
            if (cashVoucher != null)
            {
                cashVoucher.EnableAudit();
                cashVoucher.Ycv_Name = request.Name;
                cashVoucher.Ycv_SchoolId = request.SchoolId;
                cashVoucher.Ycv_PublishCount = request.PublishCount;
                cashVoucher.Ycv_Amount = request.Amount;
                cashVoucher.Ycv_LimitByPerson = request.LimitByPerson;
                cashVoucher.Ycv_OrderAmountLimit = request.OrderAmountLimit;
                cashVoucher.Ycv_ExpireType = request.ExpireType;
                cashVoucher.Ycv_ExpireDate = request.ExpireDate;
                cashVoucher.Ycv_ExpireDayCount = request.ExpireDay;
                cashVoucher.Ycv_ApplyScopeType = request.ApplyScopeType;
                cashVoucher.Ycv_ApplyGrade = request.Grade;
                cashVoucher.Ycv_CourseType = request.CourseType;
                cashVoucher.Ycv_CourseId = request.CourseId;
                cashVoucher.Ycv_RelatedCourseId = request.RelatedCourseId;
                cashVoucher.Ycv_UseWithVoucherType = request.UseWithVoucherType;
                cashVoucher.Ycv_Remark = request.Remark;
                cashVoucher.Ycv_UpdateTime = DateTime.Now;
                cashVoucher.Ycv_Editor = request.Editor;
                return CashVoucherRepository.Update(cashVoucher);
            }
            return false;
        }

        public bool UpdateStatus(int id, int status)
        {
            var cashVoucher = CashVoucherRepository.Get(id);
            if (cashVoucher != null)
            {
                cashVoucher.EnableAudit();
                cashVoucher.Ycv_Status = status;
                return CashVoucherRepository.Update(cashVoucher);
            }
            return false;
        }
        #endregion

        #region 学生现金券（现金券明细）
        public List<DtoStudentCashVoucher> GetPagingStudentCashVoucher(PagingObject paging, int cashVoucherId, int status, string usedReferNo)
        {
            return StudentCashVoucherRepository.GetPagingStudentCashVoucher(paging, cashVoucherId, status, usedReferNo);
        }

        public DtoCashVoucher GetCashVoucherDetail(int cashVoucherId)
        {
            return CashVoucherRepository.GetCashVoucherDetail(cashVoucherId);
        }
        #endregion

        #region 领取现金券
        /// <summary>
        /// 查询可领取注册券
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public List<DtoOwnCashVoucher> GetAvailableRegisterVoucher(int studentId)
        {
            StudentBll studentBll = new StudentBll();
            var student = studentBll.StudentRepository.Get(studentId);
            var ids = CashVoucherRepository.GetAvailableRegisterVoucherId(studentId, student.Bst_SchoolId).Select(s => s.Ycv_Id).ToList();
            return CashVoucherRepository.GetAvailableRegisterVoucherByIds(ids);
        }

        /// <summary>
        /// 查询已领取注册券
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public List<DtoOwnCashVoucher> GetHaveRegisterVoucher(int studentId)
        {
            StudentBll studentBll = new StudentBll();
            var student = studentBll.StudentRepository.Get(studentId);
            var ids = StudentCashVoucherRepository.GetHaveRegisterVoucherId(studentId).Select(s => s.Ysv_CashVoucherId).ToList();
            return CashVoucherRepository.GetAvailableRegisterVoucherByIds(ids);
        }

        /// <summary>
        /// 获取学生该课的可使用的现金券
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="amount"></param>
        /// <param name="grade"></param>
        /// <param name="courseType"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<DtoOwnCashVoucher> GetStudentUseCashVoucher(int studentId, int amount, int grade, int courseType, int courseId)
        {
            List<DtoOwnCashVoucher> cashVoucherList = new List<DtoOwnCashVoucher>();
            StudentBll studentBll = new StudentBll();
            var student = studentBll.StudentRepository.Get(studentId);

            //获取所有的现金券的Id和类型
            var allCashVoucher = CashVoucherRepository.GetCashVoucherId(
                new DtoSimpleCourse { CourseId = courseId, Grade = grade, Amount = amount, CourseType = courseType },
                studentId,
                student.Bst_SchoolId);

            //根据id查询所有符合条件的现金券的基本信息
            cashVoucherList = CashVoucherRepository.GetAvailableCashVoucherByIds(allCashVoucher.Select(s => s.VoucherId).ToList());

            //获取学生已拥有的现金券Id
            var haveIds = allCashVoucher.Where(s => s.IsTaken == 1).Select(s => s.VoucherId).ToList();

            //查询学生现金券的信息
            var studentCashVouchers = StudentCashVoucherRepository.GetStudentCashVoucherByStudentIdAndVoucherId(studentId, haveIds);

            //给学生已拥有现金券赋值
            foreach (var item in cashVoucherList)
            {
                var studentCashVoucher = studentCashVouchers.Where(s => s.Ysv_CashVoucherId == item.Ycv_Id).FirstOrDefault();
                if (studentCashVoucher != null)
                {
                    item.Ysv_Id = studentCashVoucher.Ysv_Id;
                    item.Ysv_ExpireDate = studentCashVoucher.Ysv_ExpireDate;
                    item.Ysv_TakenTime = studentCashVoucher.Ysv_TakenTime;
                    item.IsAvailable = (int)IsAvailableEnum.学生已拥有且可用的;
                }
                else
                {
                    item.IsAvailable = (int)IsAvailableEnum.学生可领取且可用的;
                }
            }
            return cashVoucherList;
        }

        /// <summary>
        /// 领取现金券（添加）
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="cashVoucherId"></param>
        /// <param name="gotType">现金券获取方式</param>
        /// <param name="gotReferId">现金券获取关联Id</param>
        /// <returns></returns>
        public bool TakeStudentCashVoucher(int studentId, int cashVoucherId, VoucherGotTypeEnum gotType, int gotReferId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    StudentBll studentBll = new Bll.StudentBll();
                    var student = studentBll.StudentRepository.Get(studentId);
                    if (student != null)
                    {
                        //查询学生是否已经拥有该现金券
                        if (!StudentCashVoucherRepository.StudentIsHaveCashVoucher(studentId, cashVoucherId))
                        {
                            //修改现金券已领取数量
                            int result = CashVoucherRepository.UpdateCashVoucherTakenCount(cashVoucherId, student.Bst_SchoolId);
                            if (result > 0)
                            {
                                //添加学生现金券
                                int studentCashVoucherId = AddStudentCashVoucher(studentId, cashVoucherId, gotType, gotReferId);
                                //添加现金券领取记录
                                // AddStudentGetCashVoucher(studentId, cashVoucherId);
                            }
                            else
                            {
                                throw new AbhsException(ErrorCodeEnum.NotCashVoucherCount, AbhsErrorMsg.ConstNotCashVoucherCount);
                            }
                        }
                        else
                        {
                            throw new AbhsException(ErrorCodeEnum.AlreadyHaveCashVoucher, AbhsErrorMsg.ConstAlreadyHaveCashVoucher);
                        }
                    }
                    scope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw ex;
                }
            }

        }

        /// <summary>
        /// 添加学生现金券
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="cashVoucherId"></param>
        /// <param name="gotType"></param>
        /// <param name="gotReferId"></param>
        /// <returns></returns>
        private int AddStudentCashVoucher(int studentId, int cashVoucherId, VoucherGotTypeEnum gotType, int gotReferId)
        {
            var cashVoucher = CashVoucherRepository.Get(cashVoucherId);
            Yw_StudentCashVoucher studentCashVoucher = new Yw_StudentCashVoucher();
            studentCashVoucher.Ysv_CashVoucherId = cashVoucher.Ycv_Id;
            studentCashVoucher.Ysv_StudentId = studentId;
            studentCashVoucher.Ysv_VoucherNo = GetVoucherNo(cashVoucher.Ycv_Id, studentId, cashVoucher.Ycv_VoucherType);
            studentCashVoucher.Ysv_VoucherType = cashVoucher.Ycv_VoucherType;
            if (cashVoucher.Ycv_ExpireType == (int)ExpireTypeEnum.截止日期)
            {
                studentCashVoucher.Ysv_ExpireDate = cashVoucher.Ycv_ExpireDate;
            }
            else if (cashVoucher.Ycv_ExpireType == (int)ExpireTypeEnum.固定天数)
            {
                studentCashVoucher.Ysv_ExpireDate = DateTime.Now.AddDays(cashVoucher.Ycv_ExpireDayCount);
            }
            else if (cashVoucher.Ycv_ExpireType == (int)ExpireTypeEnum.长期有效)
            {
                studentCashVoucher.Ysv_ExpireDate = "3000-01-01"._ToDateTime();
            }

            studentCashVoucher.Ysv_GotType = (int)gotType;
            studentCashVoucher.Ysv_GotReferId = gotReferId;
            studentCashVoucher.Ysv_UsedType = 0;
            studentCashVoucher.Ysv_UsedReferId = 0;
            studentCashVoucher.Ysv_UsedReferNo = "";
            studentCashVoucher.Ysv_TakenTime = DateTime.Now;
            studentCashVoucher.Ysv_Status = (int)StudentCashVoucherStatusEnum.未使用;
            studentCashVoucher.Ysv_UsedTime = "1900-01-01"._ToDateTime();
            studentCashVoucher.Ysv_UpdateTime = DateTime.Now;
            return StudentCashVoucherRepository.Add(studentCashVoucher);
        }

        /// <summary>
        /// 添加学生现金券领取记录
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="maxCashVoucherId"></param>
        /// <returns></returns>
        private int AddStudentGetCashVoucher(int studentId, int maxCashVoucherId)
        {
            Yw_StudentGetCashVoucher studentGetCashVoucher = new Yw_StudentGetCashVoucher()
            {
                Ygv_StudentId = studentId,
                Ygv_MaxCashVoucherId = maxCashVoucherId,
                Ysv_UpdateTime = DateTime.Now
            };
            return StudentGetCashVoucherRepository.Add(studentGetCashVoucher);
        }

        /// <summary>
        /// 获取现金券编号
        /// </summary>
        /// <param name="cashCoucherId"></param>
        /// <param name="studentId"></param>
        /// <param name="voucherType"></param>
        /// <returns></returns>
        private string GetVoucherNo(int cashCoucherId, int studentId, int voucherType)
        {
            return StudentCashVoucherRepository.GetVoucherNo(cashCoucherId, studentId, voucherType);
        }

        /// <summary>
        /// 查询已下单的现金券
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<DtoOwnCashVoucher> GetCashVoucherByOrderId(int studentId, int orderId)
        {
            return CashVoucherRepository.GetCashVoucherByOrderId(studentId, orderId);
        }
        #endregion

        /// <summary>
        /// 获取学生对于指定课程（1-N）能使用的最大面值的现金券
        /// </summary>
        public List<DtoVoucherForUserCourse> GetBestVoucherForUserCourse(List<DtoSimpleCourse> simpleCourses, int userId, int schoolId)
        {
            return StudentCashVoucherRepository.GetBestVoucherForUserCourse(simpleCourses, userId, schoolId);
        }
    }
}
