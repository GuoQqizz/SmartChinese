using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response.School;
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
    public class StudentApplyBll : BllBase
    {
        public StudentApplyBll() : base()
        {
        }

        #region bll
        private StudentBll studentBll;
        public StudentBll StudentBll
        {
            get
            {
                if (studentBll == null)
                {
                    studentBll = new StudentBll();
                }
                return studentBll;
            }
        }

        private SchoolBll schoolBll;
        public SchoolBll SchoolBll
        {
            get
            {
                if (schoolBll == null)
                {
                    schoolBll = new SchoolBll();
                }
                return schoolBll;
            }
        }
        private SchoolTeacherBll schoolTeacherBll;
        public SchoolTeacherBll SchoolTeacherBll
        {
            get
            {
                if (schoolTeacherBll == null)
                {
                    schoolTeacherBll = new SchoolTeacherBll();
                }
                return schoolTeacherBll;
            }
        }
        #endregion
        #region repository
        private IStudentApplySchoolRepository studentApplySchoolRepository;
        public IStudentApplySchoolRepository StudentApplySchoolRepository
        {
            get
            {
                if (studentApplySchoolRepository == null)
                {
                    studentApplySchoolRepository = new StudentApplySchoolRepository();
                }
                return studentApplySchoolRepository;
            }
        }
        #endregion

        #region select
        /// <summary>
        /// 学生入校申请列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<DtoStudentApplySchool> GetToDoApplyList(DtoApplyStudentSearch search)
        {
            return StudentApplySchoolRepository.GetToDoApplyList(search);
        }
        /// <summary>
        /// 查找学生最近一条申请信息
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public DtoStudentApplySchool GetApplyByStudentId(int studentId)
        {
            return StudentApplySchoolRepository.GetApplyByStudentId(studentId);
        }
        #endregion

        #region update
        /// <summary>
        /// 更新学生申请状态
        /// </summary>
        /// <param name="yayId"></param>
        /// <param name="toStatus"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public bool UpdateApplyStatus(int yayId, int toStatus, int fromStatus, int oper)
        {
            bool result = false;
            DtoStudentApplySchool model = StudentApplySchoolRepository.GetById(yayId);
            if (model == null)
            {
                return false;
            }
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var updateApply = StudentApplySchoolRepository.UpdateApplyStatus(yayId, toStatus, fromStatus, oper);
                    var updateStudent = true;
                    if (updateApply && toStatus == (int)ApplyStatusEnum.同意)
                    {
                        updateStudent = StudentBll.UpdateSchool(model.Yay_StudentId, model.Yay_SchoolId);
                    }
                    if (updateApply && updateStudent)
                    {
                        scope.Complete();
                        result = true;
                    }
                    else
                    {
                        RollbackTran();
                    }

                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw;
                }
            }
            return result;
        }


        private bool CloseApplyStatus(int studentId, int oper)
        {
            return StudentApplySchoolRepository.UpdateApplyStatusByStudentId(studentId, (int)ApplyStatusEnum.关闭, (int)ApplyStatusEnum.申请, oper);
        }
        #endregion



        #region insert
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertApply(Yw_StudentApplySchool entity)
        {
            return StudentApplySchoolRepository.Insert(entity);
        }
        /// <summary>
        /// 学生申请
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="teacherPhone"></param>
        /// <returns></returns>
        public bool StudentApply(int studentId, string teacherPhone, int oper = 0)
        {
            var result = false;
            var schoolTeacher = SchoolTeacherBll.GetSchoolTeacherByPhone(teacherPhone);
            if (schoolTeacher != null)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        var apply = GetApplyByStudentId(studentId);
                        if (apply == null || apply.Yay_Status != (int)ApplyStatusEnum.同意)
                        {
                            CloseApplyStatus(studentId, oper);
                            Yw_StudentApplySchool model = new Yw_StudentApplySchool();
                            model.Yay_OperateTime = DateTime.Now;
                            model.Yay_Operator = studentId;
                            model.Yay_SchoolId = schoolTeacher.Yoh_SchoolId;
                            model.Yay_Status = (int)ApplyStatusEnum.申请;
                            model.Yay_StudentId = studentId;
                            model.Yay_TeacherId = schoolTeacher.Yoh_Id;
                            model.Yay_ApplyTime = DateTime.Now;
                            result = InsertApply(model) > 0;
                            if (result)
                            {
                                scope.Complete();
                            }
                            else
                            {
                                RollbackTran();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        RollbackTran();
                        throw ex;
                    }

                }

            }
            return result;
        }
        #endregion
    }
}
