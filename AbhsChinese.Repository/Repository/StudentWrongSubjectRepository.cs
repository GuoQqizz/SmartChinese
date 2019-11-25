using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Dto.Request.StudyTask;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity.StudentWrong;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonTranslator;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    //Yw_StudentWrongSubject
    public class StudentWrongSubjectRepository : RepositoryBase<Yw_StudentWrongSubject>, IStudentWrongSubjectRepository
    {
        public StudentWrongSubjectRepository() : base(AppSetting.ConnString)
        {
        }



        #region select
        public Yw_StudentWrongSubject Get(int id)
        {
            var entity = base.GetEntity(id);
            Yw_StudentWrongSubjectExt realObj = TranslatorFactory.Translator<Yw_StudentWrongSubject, Yw_StudentWrongSubjectExt>(entity);
            if (realObj != null)
            {
                realObj.EnableAudit();
            }
            return realObj;

        }

        public DtoStudentWrongSubjectInfo GetDto(int id)
        {
            string sql = @"SELECT  sws.* ,
                                c.Ycs_Name AS CourseName ,
                                cl.Ycl_Name AS CourseLessonName ,
                                swb.Yws_CreateTime AS BookCreateTime
                        FROM    dbo.Yw_StudentWrongSubject sws
                                INNER JOIN dbo.Yw_StudentWrongBook swb ON sws.Yws_WrongBookId = swb.Ywb_Id
                                INNER JOIN dbo.Yw_Course c ON sws.Yws_CourseId = c.Ycs_Id
                                LEFT JOIN dbo.Yw_CourseLesson cl ON cl.Ycl_Id = sws.Yws_LessonId
                        WHERE   1 = 1
                                AND sws.Yws_Id = @Yws_Id";
            var entity = Query<DtoStudentWrongSubjectInfo>(sql, new { Yws_Id = id }).FirstOrDefault();

            return entity;
        }
        public List<Yw_StudentWrongSubject> GetByBookId(int bookId)
        {
            string sql = "SELECT * FROM dbo.Yw_StudentWrongSubject WHERE Yws_WrongBookId=@Yws_WrongBookId";
            return Query(sql, new { Yws_WrongBookId = bookId }).ToList();
        }

        public List<int> GetIdsByBookId(int studentId, int bookId)
        {
            string sql = "SELECT Yws_Id FROM dbo.Yw_StudentWrongSubject WHERE Yws_StudentId=@Yws_StudentId AND Yws_WrongBookId=@Yws_WrongBookId AND Yws_Status<>@Yws_Status ORDER BY Yws_CreateTime ";
            return Query<int>(sql, new { Yws_StudentId = studentId, Yws_WrongBookId = bookId, Yws_Status = (int)StudyWrongStatusEnum.已消除 }).ToList();
        }
        #endregion

        #region insert
        public bool InsertList(List<Yw_StudentWrongSubject> list)
        {

            bool checkParam = true;
            if (list == null
                || list.Count == 0
                || list.Select(s => s.Yws_StudentId).Distinct().Count() > 1
                || list.Select(s => s.Yws_WrongBookId).Distinct().Count() > 1
                || list.Select(s => s.Yws_WrongSubjectId).Distinct().Count() != list.Count
                )
            {
                checkParam = false;
            }
            if (!checkParam)
            {
                throw new AbhsException(ErrorCodeEnum.ParameterInvalid, AbhsErrorMsg.ConstParameterInvalid);
            }
            return base.InsertListEntity(list) > 0;
        }

        #endregion
        #region update


        public bool IncrementRemoveCount(int wrongSubjectId, int count)
        {
            string sql = $"UPDATE dbo.Yw_StudentWrongSubject SET Yws_RemoveTryCount=Yws_RemoveTryCount+{count},Yws_UpdateTime=GETDATE() WHERE Yws_Id={wrongSubjectId}";

            return Execute(sql) > 0;
        }

        public bool ClearSubject(int wrongSubjectId, StudyWrongStatusEnum toStatus)
        {
            if (toStatus == StudyWrongStatusEnum.已消除 || toStatus == StudyWrongStatusEnum.已做对)
            {
                string sql = $"UPDATE dbo.Yw_StudentWrongSubject SET Yws_RemoveTryCount=Yws_RemoveTryCount+1,Yws_UpdateTime=GETDATE(),Yws_Status={(int)toStatus} WHERE Yws_Id={wrongSubjectId}";
                return Execute(sql) > 0;
            }
            return true;

        }
        #endregion


    }
}
