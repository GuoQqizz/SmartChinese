using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Dto.Request.Subject;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbhsChinese.Repository.Repository
{
    public class SubjectRepository : RepositoryBase<Yw_Subject>,
        ISubjectRepository
    {
        public SubjectRepository() : base(AppSetting.ConnString)
        {
        }

        public Yw_Subject Get(int id)
        {
            return base.GetEntity(id);
        }

        public List<Yw_Subject> GetByIds(List<int> ids)
        {
            string sql = "select * from Yw_Subject where ysj_Id in @Ids";
            return Query(sql, new { Ids = ids }).ToList();
        }

        public IList<Yw_Subject> GetByPage(DtoQuestionSearch search)
        {
            string fields = "*";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Yw_Subject");
            stringBuilder.Append(" Where 1 = 1");
            int difficulty = -1;
            if (search.Difficulty.HasValue)
            {
                difficulty = (int)search.Difficulty.Value;
                stringBuilder.Append(" AND Ysj_Difficulty = @difficulty");
            }
            int status = -1;
            if (search.SubjectStatus.HasValue)
            {
                status = (int)search.SubjectStatus.Value;
                if (status > 0)
                {
                    stringBuilder.Append(" AND Ysj_Status = @status");
                }
            }
            else
            {
                status = (int)SubjectStatusEnum.合格;
                stringBuilder.Append(" AND Ysj_Status != @status");
            }

            int subjectType = -1;
            if (search.SubjectType.HasValue)
            {
                subjectType = (int)search.SubjectType.Value;
                if (subjectType > 0)
                {
                    stringBuilder.Append(" AND Ysj_SubjectType = @subjectType");
                }
            }

            string where = stringBuilder.ToString();

            string orderby = "Ysj_CreateTime DESC";
            return base.QueryPaging<Yw_Subject>(
                fields,
                where,
                orderby,
                search.Pagination,
                new
                {
                    difficulty = difficulty,
                    status = status,
                    subjectType = subjectType
                }).ToList();
        }

        public IList<Yw_Subject> GetSubjectsByIds(IEnumerable<int> ids)
        {
            /* 让in关键字按(1,1588,15782,9887,54)排序查询
             * Select * From Product Where id in (1,1588,15782,9887,54)  Order By charindex(','+ id +',', ',1,1588,15782,9887,54,')*/
            if (ids == null || !ids.Any())
            {
                return new List<Yw_Subject>();
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT * FROM Yw_Subject AS s");
            stringBuilder.Append(" Where s.Ysj_Id IN @ids");
            string orderbyids = "," + string.Join(",", ids) + ",";
            string orderby = " ORDER BY CHARINDEX(','+  convert(varchar(20),s.Ysj_Id) +',', @orderbyids)";
            stringBuilder.Append(orderby);
            string sql = stringBuilder.ToString();
            return Query<Yw_Subject>(sql, new { ids = ids, orderbyids = orderbyids }).ToList();
        }
        public IList<Yw_Subject> GetSubjectsById(DtoQuestionSearch search)
        {
            int id = search.Id;
            if (id < 10000 && id != 0)
            {
                throw new AbhsException(ErrorCodeEnum.IdOutOfRange, AbhsErrorMsg.IdOutOfRange);
            }
            string fields = "*";
            string where = "Yw_Subject Where Ysj_Id = @id";
            if (search.SubjectType.HasValue)
            {
                int subjectType = (int)search.SubjectType.Value;
                if (subjectType > 0)
                {
                    where += " AND Ysj_SubjectType = " + subjectType.ToString();
                }
            }
            int status = -1;
            if (search.SubjectStatus.HasValue)
            {
                status = (int)search.SubjectStatus.Value;
                if (status > 0)
                {
                    where += " AND Ysj_Status = " + status.ToString();
                }
            }
            else
            {
                status = (int)SubjectStatusEnum.合格;
                where += " AND Ysj_Status != " + status.ToString();
            }
            string orderby = "Ysj_CreateTime DESC";
            return base.QueryPaging<Yw_Subject>(
                fields,
                where,
                orderby,
                search.Pagination,
                new { id = id }).ToList();
        }

        public Yw_Subject Insert(Yw_Subject entity)
        {
            base.InsertEntity(entity);
            return entity;
        }

        public void Update(Yw_Subject entity)
        {
            base.UpdateEntity(entity);
        }

        public IEnumerable<Yw_Subject> GetPagingSubjectForResourceGroup(PagingObject paging, int grade, int subjectType, int subjectId)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append("Yw_Subject where Ysj_Status=3");
            if (subjectId > 0)
            {
                strSql.Append(" and Ysj_Id=@Id");
                parameters.Add("Id", subjectId);
            }
            else
            {
                if (grade > 0)
                {
                    strSql.Append(" and Ysj_Grade=@Grade");
                    parameters.Add("Grade", grade);
                }
                if (subjectType > 0)
                {
                    strSql.Append(" and Ysj_SubjectType=@SubjectType");
                    parameters.Add("SubjectType", subjectType);
                }
            }
            return QueryPaging<Yw_Subject>("*", strSql._ToString(), "Ysj_Id", paging, parameters).ToList();
        }

        public List<DtoSubjectKnowledge> GetTaskSubjectsAutoForKnowledges(int lessonId, List<int> knowledgeIds)
        {
            List<DtoSubjectKnowledge> result = new List<DtoSubjectKnowledge>();
            if (lessonId > 0 && knowledgeIds != null && knowledgeIds.Count > 0)
            {
                string knowIds = string.Join(",", knowledgeIds);
                string sql = @"SELECT * 
                                FROM   (SELECT Row_number() 
                                                 OVER( 
                                                   partition BY c.id 
                                                   ORDER BY Newid()) number, 
                                               a.ysj_id              AS SubjectId, 
                                               c.id                  AS KnowledgeId, 
                                               a.ysj_subjecttype     AS SubjectType, 
                                               a.ysj_difficulty      AS Difficulty 
                                        FROM   yw_subject a 
                                               INNER JOIN yw_subjectknowledge b 
                                                       ON a.ysj_id = b.ysw_subjectid and b.ysw_ismain = 1
                                               INNER JOIN Fn_toidtable(@Ids) 
c                                                        ON b.ysw_knowledgeid = c.id
                                               INNER JOIN (SELECT DISTINCT ysr_subjectid 
                                                           FROM   yw_coursesubjectrelation 
                                                           WHERE  ysr_lessonid = @LessonId 
                                                                  AND ysr_isdirect = 0 
                                                                  AND ysr_subjectid NOT IN (SELECT ysr_subjectid 
                                                                                            FROM 
                                                                      yw_coursesubjectrelation 
                                                                                            WHERE 
                                                                      ysr_lessonid = @LessonId 
                                                                      AND ysr_isdirect = 1)) d 
                                                       ON d.ysr_subjectid = a.ysj_id 
                                        WHERE  a.ysj_status = 3) x 
                                WHERE  number <= 10 order by KnowledgeId,SubjectType";
                result = Query<DtoSubjectKnowledge>(sql, new { Ids = knowIds, LessonId = lessonId }).ToList();
            }
            return result;
        }

        public List<DtoLesTaskSubject> GetLessonTaskSubject(int lessonId, List<Tuple<int, int>> errorSubjects)
        {
            List<DtoLesTaskSubject> subjects = new List<DtoLesTaskSubject>();
            if (errorSubjects.Count > 0)
            {
                string errorSubjectStrs = string.Join(";", errorSubjects.Select(x => x.Item1 + "," + x.Item2));
                var parems = new DynamicParameters();
                parems.Add("@LessonId", lessonId);
                parems.Add("@ErrorSubject", errorSubjectStrs);
                subjects = QueryProc<DtoLesTaskSubject>("P_getLessonTaskSubjects", parems).ToList();
            }

            return subjects;
        }

        public List<Yw_Subject> GetSubjectForPractice(int studentId, int courseId, int lessonIndex, int returnCount)
        {
            string sql = $@"SELECT top (@TopCount) * 
                            FROM   (SELECT b.*, 
                                           CASE 
                                             WHEN c.yrs_id IS NULL THEN 0 
                                             WHEN c.Yrs_IsLastest=0 THEN 1
                                             ELSE 2 
                                           END                   num1, 
                                           Row_number() 
                                             OVER( 
                                               partition BY b.ysj_subjecttype 
                                               ORDER BY Newid()) num2 
                                    FROM   (select distinct ysr_subjectid from yw_coursesubjectrelation where ysr_courseid = @CourseId 
                                           AND ysr_lessonindex < @LessonIndex 
                                           AND ysr_isdirect = 0) a
                                           INNER JOIN yw_subject b 
                                                   ON a.ysr_subjectid = b.ysj_id and b.Ysj_Status=3 
                                           LEFT JOIN yw_studentrecentsubject c 
                                                  ON b.ysj_id = c.yrs_subjectid 
                                                     AND yrs_studentid = @StudentId 
                                            )x 
                            ORDER  BY num1, 
                                      num2, 
                                      newId() ";

            return Query(sql, new { StudentId = studentId, CourseId = courseId, LessonIndex = lessonIndex, TopCount = returnCount }).ToList();
        }

        public List<DtoMediaResourceToCourse> GetSubjectToCourse(PagingObject paging, int courseId, int resourceType, string nameOrkey)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            var fields = "Ysj_Id as MediaID,Ysj_Name as MediaName";
            strSql.Append("Yw_Subject where Ysj_Id in (select a.Ygi_ResourceId from((select Ygi_ResourceId from Yw_Course join Yw_ResourceGroupItem on Ygi_GroupId=Ycs_ResourceGroupId  where Ycs_Id=@CourseId and Ygi_ResourceType=3");
            parameters.Add("CourseId", courseId);
            if (resourceType > 0)
            {
                strSql.Append(" and Ygi_ResourceType=@ResourceType");
                parameters.Add("ResourceType", resourceType);
            }
            strSql.Append(")a inner join (select Ysi_SubjectId from Yw_SubjectIndex where 1=1");
            if (!string.IsNullOrEmpty(nameOrkey))
            {
                strSql.Append(" and Ysi_Keyword=@Keyword");
                parameters.Add("Keyword", nameOrkey);
            }
            strSql.Append(")b on a.Ygi_ResourceId=b.Ysi_SubjectId b.Ysj_Status=3))");
            return QueryPaging<DtoMediaResourceToCourse>(fields, strSql.ToString(), "Ysj_CreateTime DESC", paging, parameters).ToList();
        }

        public List<DtoMediaResourceToCourse> GetSubjectToCourseById(PagingObject paging, int subjectType, int courseId, int resourceId)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            var fields = "Ysj_Id as MediaID,Ysj_Name as MediaName";
            strSql.Append("Yw_Subject where Ysj_Status=3 and Ysj_Id in (select a.Ygi_ResourceId from(select Ygi_ResourceId from Yw_Course join Yw_ResourceGroupItem on Ygi_GroupId=Ycs_ResourceGroupId  where Ycs_Id=@CourseId and Ygi_ResourceType=3");//Ysj_Status=3
            parameters.Add("CourseId", courseId);
            if (resourceId > 0)
            {
                strSql.Append(" and Ygi_ResourceId=@ResourceId");
                parameters.Add("ResourceId", resourceId);
            }
            strSql.Append(")a)");
            if (subjectType > 0)
            {
                strSql.Append(" and Ysj_SubjectType=@SubjectType");
                parameters.Add("SubjectType", subjectType);
            }

            return QueryPaging<DtoMediaResourceToCourse>(fields, strSql.ToString(), "Ysj_CreateTime DESC", paging, parameters).ToList();
        }

        public List<DtoResourceGroupItem> GetSubjectForGroupItem(PagingObject paging, List<int> ids)
        {
            var fields = "Ysj_Id as Id,Ysj_Name as Name,Ysj_SubjectType as SubjectType,Ysj_Grade as Grade,Ysj_Difficulty as Difficulty,Ysj_Keywords as Keywords,Ysj_GroupItemCount as GroupItemCount,Ysj_Status as Status,Ysj_CreateTime as CreateTime,Ysj_Creator as Creator,Ysj_UpdateTime as UpdateTime,Ysj_Editor as Editor";
            var sql = "Yw_Subject where Ysj_Id in @Ids";
            return QueryPaging<DtoResourceGroupItem>(fields, sql, "Ysj_CreateTime DESC", paging, new { Ids = ids }).ToList();
        }

        public List<Yw_Subject> GetSubjectRelationBySubjectId(int subjectId)
        {
            string sql = @"SELECT TOP 100 sb.* 
                            FROM    dbo.Yw_Subject sb
                                    JOIN ( SELECT   * 
                                           FROM     dbo.fn_ToIdtable(( SELECT   Ysg_RelSubjectId
                                                                       FROM     dbo.Yw_SubjectGroup
                                                                       WHERE    Ysg_SubjectId = @SubjectId
                                                                     ))
                                         ) sg ON sb.Ysj_Id = sg.id
                            WHERE   sb.Ysj_Status = @Status
                            ORDER BY NEWID()";
            return Query(sql, new { SubjectId = subjectId, Status = (int)SubjectStatusEnum.合格 }).ToList();
        }
    }
}