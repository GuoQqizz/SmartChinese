using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Request.School;
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
    public class StudentBll : BllBase
    {
        public StudentBll() : base()
        {
        }

        #region repository
        private IStudentRepository studentRepository;
        public IStudentRepository StudentRepository
        {
            get
            {
                if (studentRepository == null)
                {
                    studentRepository = new StudentRepository();
                }
                return studentRepository;
            }
        }

        private IStuRecSubjectRepository stuRecSubjectRepo;
        public IStuRecSubjectRepository StuRecSubjectRepo
        {
            get
            {
                if (stuRecSubjectRepo == null)
                {
                    stuRecSubjectRepo = new StuRecSubjectRepository();
                }
                return stuRecSubjectRepo;
            }
        }
        public IStudentPassportRepository studentPassportRepository;
        public IStudentPassportRepository StudentPassportRepository
        {
            get
            {
                if (studentPassportRepository == null)
                {
                    studentPassportRepository = new StudentPassportRepository();
                }
                return studentPassportRepository;
            }
        }
        #endregion

        #region bll
        private StudentApplyBll studentApplyBll;
        public StudentApplyBll StudentApplyBll
        {
            get
            {
                if (studentApplyBll == null)
                {
                    studentApplyBll = new StudentApplyBll();
                }
                return studentApplyBll;
            }
        }
        #endregion

        public Bas_Student GetStudent(int id)
        {
            return StudentRepository.Get(id);
        }

        public void RefreshStudentRecentSubject(int studentId, List<int> newSubjectIds)
        {
            if (newSubjectIds == null || newSubjectIds.Count == 0)
            {
                return;
            }

            List<int> newSubIds = newSubjectIds.Distinct().ToList();

            List<Yw_StudentRecentSubject> subjects = StuRecSubjectRepo.GetByStudent(studentId);

            List<int> subjectIds = subjects.Select(x => x.Yrs_SubjectId).ToList();
            List<int> addIds = newSubIds.Except(subjectIds).ToList();
            List<int> deleteIds = new List<int>();

            if (addIds.Count > 0)
            {
                int totalCount = subjectIds.Count + addIds.Count;
                int exceedCount = totalCount - 100;
                if (exceedCount > 0)
                {
                    for (int index = 0; index < subjectIds.Count; index++)
                    {
                        if (!newSubIds.Contains(subjectIds[index]))
                        {
                            deleteIds.Add(subjectIds[index]);
                            if (deleteIds.Count >= exceedCount)
                            {
                                break;
                            }
                        }
                    }
                }

                List<DtoSimpleStuRecentSub> objs = new List<DtoSimpleStuRecentSub>();
                for (int index = 0; index < addIds.Count; index++)
                {
                    objs.Add(new DtoSimpleStuRecentSub() { StudentId = studentId, SubjectId = addIds[index] });
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        if (deleteIds.Count > 0)
                        {
                            StuRecSubjectRepo.DeleteByStudent(studentId, deleteIds);
                        }

                        StuRecSubjectRepo.AddBatch(objs);
                        StuRecSubjectRepo.UpdateToLastestforStudent(studentId, newSubIds);
                        scope.Complete();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        public List<DtoSchoolStudent> GetSchoolStudentList(DtoSchoolStudentSearch search)
        {
            return StudentRepository.GetSchoolStudentList(search);

        }
        public int CheckUniqueAccount(string account)
        {
            return StudentPassportRepository.CheckUniqueAccount(account);
        }

        #region update
        public bool UpdateSchool(int studentId, int schoolId)
        {
            return StudentRepository.UpdateSchool(studentId, schoolId);
        }

        public bool UnBindSchool(int studentId, int schoolId, int oper)
        {
            bool result = false;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    bool updateSchool = UpdateSchool(studentId, 0);
                    Yw_StudentApplySchool apply = new Yw_StudentApplySchool()
                    {
                        Yay_ApplyTime = DateTime.Now,
                        Yay_OperateTime = DateTime.Now,
                        Yay_Operator = oper,
                        Yay_SchoolId = schoolId,
                        Yay_Status = (int)ApplyStatusEnum.解绑,
                        Yay_StudentId = studentId,
                        Yay_TeacherId = oper
                    };
                    bool insertApply = StudentApplyBll.InsertApply(apply) > 0;
                    if (updateSchool && insertApply)
                    {
                        scope.Complete();
                        result = true;
                    }
                    else
                    {
                        RollbackTran();
                        result = false;
                    }
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw ex;
                }
            }
            return result;
        }
        #endregion
    }
}
