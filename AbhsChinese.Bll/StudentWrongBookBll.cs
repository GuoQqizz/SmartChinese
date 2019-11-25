using AbhsChinese.Code.Extension;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Dto.Request.StudentWrong;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.StudentWrong;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AbhsChinese.Bll
{
    public class StudentWrongBookBll : BllBase
    {

        #region repository
        private IStudentWrongBookRepository studentWrongBookRepository;
        public IStudentWrongBookRepository StudentWrongBookRepository
        {
            get
            {
                if (studentWrongBookRepository == null)
                {
                    studentWrongBookRepository = new StudentWrongBookRepository();
                }
                return studentWrongBookRepository;
            }
        }

        private IStudentWrongSubjectRepository studentWrongSubjectRepository;
        public IStudentWrongSubjectRepository StudentWrongSubjectRepository
        {
            get
            {
                if (studentWrongSubjectRepository == null)
                {
                    studentWrongSubjectRepository = new StudentWrongSubjectRepository();
                }
                return studentWrongSubjectRepository;
            }
        }
        #endregion
        #region bll

        private StudentStudyBll studentStudyBll;
        public StudentStudyBll StudentStudyBll
        {
            get
            {
                if (studentStudyBll == null)
                {
                    studentStudyBll = new StudentStudyBll();
                }
                return studentStudyBll;
            }
        }

        private SubjectBll subjectBll;
        public SubjectBll SubjectBll
        {
            get
            {
                if (subjectBll == null)
                {
                    subjectBll = new SubjectBll();
                }
                return subjectBll;
            }
        }
        #endregion
        #region select

        public List<DtoStudentWrongBookInfo> GetBookList(DtoStudentWrongSearch search)
        {
            return StudentWrongBookRepository.GetBookList(search);
        }
        public Yw_StudentWrongBook GetBookByStudentAndStudy(int studentId, int courseId, int lessonId, int progressId, int taskId)
        {
            return StudentWrongBookRepository.GetByStudentAndStudy(studentId, courseId, lessonId, progressId, taskId);
        }

        public List<Yw_StudentWrongSubject> GetSubjectByBookId(int bookId)
        {
            return StudentWrongSubjectRepository.GetByBookId(bookId);
        }

        public List<int> GetSubjectIdsByBookId(int studentId, int bookId)
        {
            return StudentWrongSubjectRepository.GetIdsByBookId(studentId, bookId);
        }


        public DtoStudentWrongSubjectInfo GetWrongSubject(int wrongSubjectId, bool getIds = false)
        {
            var dto = StudentWrongSubjectRepository.GetDto(wrongSubjectId);
            if (dto != null && getIds)
            {
                dto.WrongSubjectIds = GetSubjectIdsByBookId(dto.Yws_StudentId, dto.Yws_WrongBookId);
            }
            return dto;
        }

        public Tuple<StudentAnswerBase, Yw_SubjectContent, Yw_Subject, Yw_StudentWrongSubject> GetWorngSubjectVm(int studentId, int wrongSubjectId)
        {

            Yw_StudentWrongSubject wrongSubject = StudentWrongSubjectRepository.Get(wrongSubjectId);
            if (wrongSubject == null || wrongSubject.Yws_StudentId != studentId)
            {
                wrongSubject = null;
            }
            if (wrongSubject != null)
            {
                StudentAnswerBase answer = (wrongSubject as Yw_StudentWrongSubjectExt).Yws_Answer_Obj;
                Yw_SubjectContent subjectContent = SubjectBll.GetSubjectContent(wrongSubject.Yws_WrongSubjectId);
                Yw_Subject subject = SubjectBll.GetSubject(wrongSubject.Yws_WrongSubjectId);
                return new Tuple<StudentAnswerBase, Yw_SubjectContent, Yw_Subject, Yw_StudentWrongSubject>(answer, subjectContent, subject, wrongSubject);
            }
            return null;
        }
        #endregion
        #region save
        public bool SaveWrongBook(List<StudentAnswerCard> answerCard, DtoStudentWrongBook wrongBook)
        {
            bool result = false;
            var studyProcess = StudentStudyBll.GetProgressByStudentCourse(wrongBook.StudentId, wrongBook.CourseId);
            int schoolId = studyProcess == null ? 0 : studyProcess.Yps_SchoolId;
            int classId = studyProcess == null ? 0 : studyProcess.Yps_ClassId;
            List<StudentAnswerBase> answers = answerCard.SelectMany(s => s.AnswerCollection).ToList();
            List<Yw_StudentWrongSubject> listSubject = new List<Yw_StudentWrongSubject>();
            if (answers.Any(s => s.SubjectId <= 0))
            {
                throw new AbhsException(ErrorCodeEnum.ParameterInvalid, AbhsErrorMsg.ConstParameterInvalid);
            }
            answers.ForEach(a =>
            {
                if (CheckAnswerNeedRecordWorng(a))
                {
                    Yw_StudentWrongSubjectExt tmp = new Yw_StudentWrongSubjectExt();
                    tmp.Yws_CourseId = wrongBook.CourseId;
                    tmp.Yws_CreateTime = DateTime.Now;
                    tmp.Yws_KnowledgeId = a.KnowledgeId;
                    tmp.Yws_LessonId = wrongBook.LessonId;
                    tmp.Yws_RemoveTryCount = 0;
                    tmp.Yws_Source = (int)wrongBook.Source;
                    tmp.Yws_Status = (int)StudyWrongStatusEnum.未消除;
                    tmp.Yws_Answer_Obj = a;
                    tmp.Yws_StudentId = wrongBook.StudentId;
                    tmp.Yws_SubjectType = a.Type;
                    tmp.Yws_UpdateTime = DateTime.Now;
                    tmp.Yws_WrongBookId = 0;
                    tmp.Yws_WrongSubjectId = a.SubjectId;

                    listSubject.Add(tmp);
                }
            });
            if (listSubject.Count == 0) return true;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Yw_StudentWrongBook bookModel = GetBookByStudentAndStudy(wrongBook.StudentId, wrongBook.CourseId, wrongBook.LessonId, wrongBook.LessonProgressId, wrongBook.StudyTaskId);
                    bool saveSubject = false;
                    bool saveBook = false;
                    int WrongCount = 0;
                    int WrongKnowledgeCount = 0;

                    #region 获取要插入的错题及错题数的计算
                    if (bookModel != null)
                    {
                        List<Yw_StudentWrongSubject> oldSubjectList = GetSubjectByBookId(bookModel.Ywb_Id);
                        listSubject = listSubject.ExceptExt(oldSubjectList, l => l.Yws_WrongSubjectId)
                                       //.Where(s =>
                                       // {
                                       //     return !oldSubjectList.Exists(o => o.Yws_WrongSubjectId == s.Yws_WrongSubjectId);
                                       // })
                                       .Select(s => s)
                                       .ToList();
                        WrongCount = bookModel.Yws_WrongCount + listSubject.Select(s => s.Yws_WrongSubjectId).Distinct().Count();
                        WrongKnowledgeCount = (from o in oldSubjectList
                                               where o.Yws_Status == (int)StudyWrongStatusEnum.未消除 && o.Yws_KnowledgeId > 0
                                               select o.Yws_KnowledgeId)
                                               .Concat(from n in listSubject where n.Yws_KnowledgeId > 0 select n.Yws_KnowledgeId)
                                               .Distinct().Count();
                    }
                    else
                    {
                        WrongCount = listSubject.Select(s => s.Yws_WrongSubjectId).Distinct().Count();
                        WrongKnowledgeCount = listSubject.Where(s => s.Yws_KnowledgeId > 0).Select(s => s.Yws_KnowledgeId).Distinct().Count();
                    }
                    #endregion

                    #region save错题本
                    if (bookModel != null)
                    {
                        bookModel.Yws_WrongCount = WrongCount;
                        bookModel.Yws_WrongKnowledgeCount = WrongKnowledgeCount;
                        bookModel.Yws_UpdateTime = DateTime.Now;
                        saveBook = StudentWrongBookRepository.Update(bookModel);
                    }
                    else
                    {
                        bookModel = new Yw_StudentWrongBook();
                        bookModel.Ywb_CourseId = wrongBook.CourseId;
                        bookModel.Ywb_LessonId = wrongBook.LessonId;
                        bookModel.Ywb_LessonProgressId = wrongBook.LessonProgressId;
                        bookModel.Ywb_StudentId = wrongBook.StudentId;
                        bookModel.Ywb_StudyTaskId = wrongBook.StudyTaskId;
                        bookModel.Yws_ClassId = classId;
                        bookModel.Yws_CreateTime = DateTime.Now;
                        bookModel.Yws_RemoveCount = 0;
                        bookModel.Yws_SchoolId = schoolId;
                        bookModel.Yws_Source = (int)wrongBook.Source;
                        bookModel.Yws_Status = (int)StudyWrongStatusEnum.未消除;
                        bookModel.Yws_UpdateTime = DateTime.Now;
                        bookModel.Yws_WrongCount = WrongCount;
                        bookModel.Yws_WrongKnowledgeCount = WrongKnowledgeCount;
                        bookModel.Ywb_Id = StudentWrongBookRepository.Insert(bookModel);
                        saveBook = bookModel.Ywb_Id > 0;
                    }
                    listSubject.ForEach(s => s.Yws_WrongBookId = bookModel.Ywb_Id);
                    #endregion

                    #region save错题详情
                    saveSubject = InsertSubjectList(listSubject);
                    #endregion
                    result = saveBook && saveSubject;
                    if (result)
                    {
                        scope.Complete();
                    }
                    else
                    {
                        RollbackTran();
                    }
                }
                catch (Exception)
                {
                    RollbackTran();
                    throw;
                }
            }

            return result;
        }

        public bool ClearWrongSubject(int bookId, int wrongSubjectId, StudyWrongStatusEnum toStatus)
        {
            bool result = false;
            if (toStatus == StudyWrongStatusEnum.消除中 || toStatus == StudyWrongStatusEnum.已消除 || toStatus == StudyWrongStatusEnum.已做对)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        bool clearSubject = StudentWrongSubjectRepository.ClearSubject(wrongSubjectId, toStatus);
                        bool clearBook = StudentWrongBookRepository.RefreshStatus(bookId);
                        //if (toStatus == StudyWrongStatusEnum.已消除)
                        //{
                        //    clearBook = StudentWrongBookRepository.RefreshStatus(bookId);
                        //}
                        result = clearSubject && clearBook;
                        if (result)
                        {
                            scope.Complete();
                        }
                        else
                        {
                            RollbackTran();
                        }
                    }
                    catch (Exception)
                    {
                        RollbackTran();
                        throw;
                    }
                }
            }
            else
            {
                result = StudentWrongSubjectRepository.IncrementRemoveCount(wrongSubjectId, 1);
            }
            return result;
        }
        private bool CheckAnswerNeedRecordWorng(StudentAnswerBase answer)
        {
            bool res = false;
            try
            {
                SubjectTypeEnum type = (SubjectTypeEnum)answer.Type;
                switch (type)
                {
                    case SubjectTypeEnum.选择题:
                    case SubjectTypeEnum.判断题:
                        res = !answer.IsRight;
                        break;
                    case SubjectTypeEnum.填空题:
                    case SubjectTypeEnum.选择填空:
                    case SubjectTypeEnum.连线题:
                    case SubjectTypeEnum.主观题:
                        res = answer.ResultStars <= 2;
                        break;
                    case SubjectTypeEnum.圈点批注标色:
                    case SubjectTypeEnum.圈点批注断句:
                        res = false;
                        break;
                    default:
                        res = false;
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        #endregion

        #region insert
        public bool InsertSubjectList(List<Yw_StudentWrongSubject> list)
        {
            if (list.Select(s => s.Yws_WrongBookId).Distinct().Count() > 1)
            {
                return false;
            }
            return StudentWrongSubjectRepository.InsertList(list);
        }
        #endregion

        #region delete

        #endregion
    }
}
