using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Dto.Request.StudentWrong;
using Dapper;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Code.Extension;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity.StudentWrong;

namespace AbhsChinese.Repository.Repository
{
    //
    public class StudentWrongBookRepository : RepositoryBase<Yw_StudentWrongBook>, IStudentWrongBookRepository
    {
        public StudentWrongBookRepository() : base(AppSetting.ConnString)
        {
        }
        #region select

        public Yw_StudentWrongBook GetByStudentAndStudy(int studentId, int courseId, int lessonId, int progressId, int taskId)
        {
            string sql = $"SELECT * FROM dbo.Yw_StudentWrongBook WHERE Ywb_StudentId={studentId} AND Ywb_CourseId={courseId} AND Ywb_LessonId={lessonId} AND Ywb_LessonProgressId={progressId} AND Ywb_StudyTaskId={taskId}";

            var res = Query(sql).FirstOrDefault();
            if (res != null)
            {
                res.EnableAudit();
            }
            return res;

        }


        public List<DtoStudentWrongBookInfo> GetBookList(DtoStudentWrongSearch search)
        {
            if (search == null)
            {
                return null;
            }
            var strWhere = new StringBuilder();
            var fields = "swb.*,c.Ycs_Name AS CourseName,cl.Ycl_Name AS CourseLessonName";
            var orderBy = "Yws_CreateTime DESC  ";
            var parameters = new DynamicParameters();

            strWhere.Append($@"dbo.Yw_StudentWrongBook swb
                                 INNER JOIN dbo.Yw_Course c ON swb.Ywb_CourseId=c.Ycs_Id
                                 LEFT JOIN dbo.Yw_CourseLesson cl ON cl.Ycl_Id=swb.Ywb_LessonId WHERE 1=1 AND swb.Yws_Status<>{(int)StudyWrongStatusEnum.已消除}");
            if (search.StudentId > 0)
            {
                strWhere.Append(" AND swb.Ywb_StudentId=@Ywb_StudentId ");
                parameters.Add("Ywb_StudentId", search.StudentId);
            }
            if (search.BookId > 0)
            {
                strWhere.Append(" AND swb.Ywb_Id=@Ywb_Id ");
                parameters.Add("Ywb_Id", search.BookId);
            }
            else
            {

                if (!search.BeginDate.IsDefaultTime())
                {
                    //strWhere.Append(" AND swb.Yws_CreateTime>@beginDate ");
                    strWhere.Append(" AND DATEDIFF(DAY,swb.Yws_CreateTime,@beginDate)<=0");
                    parameters.Add("beginDate", search.BeginDate);
                }
                if (!search.EndDate.IsDefaultTime())
                {
                    strWhere.Append(" AND DATEDIFF(DAY,swb.Yws_CreateTime,@endDate)>=0");
                    parameters.Add("endDate", search.EndDate);
                }
            }

            return base.QueryPaging<DtoStudentWrongBookInfo>(fields, strWhere._ToString(), orderBy, search.Pagination, parameters).ToList();
        }
        #endregion
        #region update
        public bool RefreshStatus(int bookId)
        {
            string sql = $@"DECLARE @wrongCount INT;
                        SELECT  @wrongCount = COUNT(1)
                        FROM    dbo.Yw_StudentWrongSubject
                        WHERE   Yws_WrongBookId = @Yws_WrongBookId
                                AND Yws_Status <>{(int)StudyWrongStatusEnum.已消除};
                        UPDATE  dbo.Yw_StudentWrongBook
                        SET     Yws_RemoveCount = ( SELECT  COUNT(1)
                                                    FROM    dbo.Yw_StudentWrongSubject
                                                    WHERE   Yws_WrongBookId = @Yws_WrongBookId
                                                            AND Yws_Status = {(int)StudyWrongStatusEnum.已消除}
                                                    ) ,
                                Yws_WrongKnowledgeCount = ( SELECT  COUNT(DISTINCT Yws_KnowledgeId)
                                                            FROM    dbo.Yw_StudentWrongSubject
                                                            WHERE   Yws_WrongBookId = @Yws_WrongBookId
                                                                    AND Yws_Status <> {(int)StudyWrongStatusEnum.已消除}
                                                                    AND Yws_KnowledgeId > 0 
                                                            ) ,
                                Yws_Status = ( CASE WHEN @wrongCount = 0 THEN {(int)StudyWrongStatusEnum.已消除}
                                                    ELSE {(int)StudyWrongStatusEnum.消除中}
                                                END ) ,
                                Yws_UpdateTime = GETDATE()
                        WHERE   Ywb_Id = @Yws_WrongBookId  ";

            return Execute(sql, new { Yws_WrongBookId = bookId }) > 0;
        }
        #endregion

        #region basecrud

        public Yw_StudentWrongBook Get(int id)
        {
            var res = base.GetEntity(id);
            if (res != null)
            {
                res.EnableAudit();
            }
            return res;
        }
        public int Insert(Yw_StudentWrongBook entity)
        {
            return base.InsertEntity(entity);
        }

        public bool Update(Yw_StudentWrongBook entity)
        {
            return base.UpdateEntity(entity);
        }


        #endregion
    }
}
