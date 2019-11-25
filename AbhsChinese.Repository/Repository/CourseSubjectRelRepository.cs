using AbhsChinese.Domain.Dto.Response.StudentPractice;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class CourseSubjectRelRepository : RepositoryBase<Yw_CourseSubjectRelation>, ICourseSubjectRelRepository
    {
        public CourseSubjectRelRepository() : base(AppSetting.ConnString)
        {
        }

        public Yw_CourseSubjectRelation GetDirectRelation(int lessonId, int subjectId)
        {
            string sql = "select * from Yw_CourseSubjectRelation where Ysr_LessonId=@LessonId and Ysr_SubjectId=@SubjectId and Ysr_IsDirect=1";
            return Query(sql, new { LessonId = lessonId, SubjectId = subjectId }).FirstOrDefault();
        }

        public List<Yw_CourseSubjectRelation> GetDirectRelationsOfLesson(int lessonId)
        {
            string sql = "select * from Yw_CourseSubjectRelation where Ysr_LessonId=@LessonId and Ysr_IsDirect=1";
            return Query(sql, new { LessonId = lessonId }).ToList();
        }

        public List<Yw_CourseSubjectRelation> GetDirectRelation(int subjectId)
        {
            string sql = "select * from Yw_CourseSubjectRelation where Ysr_SubjectId=@SubjectId and Ysr_IsDirect=1";
            return Query(sql, new { SubjectId = subjectId }).ToList();
        }

        public void Add(Yw_CourseSubjectRelation relation)
        {
            InsertEntity(relation);
        }

        public void AddBatch(DataTable table)
        {
            BulkCopy(table);
        }

        public void CreateInDirectRelation(int relationId, int[] subjectIds)
        {
            if (subjectIds != null && subjectIds.Length > 0)
            {
                string subjectIdsArray = string.Join(",", subjectIds);
                string sql = $@"INSERT INTO yw_coursesubjectrelation 
                                            (ysr_courseid, 
                                             ysr_lessonid, 
                                             ysr_lessonindex, 
                                             ysr_subjectid, 
                                             ysr_subjecttype, 
                                             ysr_isdirect, 
                                             ysr_directsubjectid, 
                                             ysr_createtime) 
                                SELECT a.ysr_courseid, 
                                       a.ysr_lessonid, 
                                       a.ysr_lessonindex, 
                                       c.ysj_id, 
                                       c.ysj_subjecttype, 
                                       0, 
                                       a.ysr_subjectid, 
                                       Getdate() 
                                FROM   (SELECT * 
                                        FROM   yw_coursesubjectrelation a 
                                        WHERE  ysr_id = @RelationId) a 
                                       CROSS JOIN Fn_toidtable('{subjectIdsArray}') b 
                                       INNER JOIN yw_subject c 
                                               ON b.id = c.ysj_id ";
                Execute(sql, new { RelationId = relationId });
            }
        }

        public void AddSubjectToGroup(int subjectId, int subjectType, int targetSubjectId)
        {
            string sql = @"INSERT INTO yw_coursesubjectrelation 
                                        (ysr_courseid, 
                                         ysr_lessonid, 
                                         ysr_lessonindex, 
                                         ysr_subjectid, 
                                         ysr_subjecttype, 
                                         ysr_isdirect, 
                                         ysr_directsubjectid, 
                                         ysr_createtime) 
                            SELECT a.ysr_courseid, 
                                   a.ysr_lessonid, 
                                   a.ysr_lessonindex, 
                                   @SubjectId, 
                                   @SubjectType, 
                                   0, 
                                   a.ysr_subjectid, 
                                   Getdate() 
                            FROM   yw_coursesubjectrelation a 
                            WHERE  ysr_subjectid = @TargetSubjectId 
                                   AND ysr_isdirect = 1";

            Execute(sql, new { SubjectId = subjectId, SubjectType = subjectType, TargetSubjectId = targetSubjectId });

        }

        public void DeleteDirectRelation(int lessonId, int subjectId)
        {
            string sql = @"delete from Yw_CourseSubjectRelation 
                           where ysr_LessonId=@LessonId and Ysr_DirectSubjectId=@SubjectId";
            Execute(sql, new { LessonId = lessonId, SubjectId = subjectId });
        }

        public void DeleteDirectRelationOfLesson(int lessonId, List<int> subjectIds)
        {
            string sql = @"delete from Yw_CourseSubjectRelation 
                           where ysr_LessonId=@LessonId and Ysr_DirectSubjectId in @SubjectIds";
            Execute(sql, new { LessonId = lessonId, SubjectIds = subjectIds });
        }

        public void DeleteInDirectRelation(int directSubjectId, int subjectId)
        {
            string sql = @"delete from Yw_CourseSubjectRelation 
                           where ysr_SubjectId=@SubjectId and Ysr_DirectSubjectId=@DirectSubjectId and Ysr_IsDirect=0";
            Execute(sql, new { SubjectId = subjectId, DirectSubjectId = directSubjectId });
        }

        //public IList<int> GetSubjectIdsOfStudyTask(int lessonId, int lessonIndex)
        //{
        //    StringBuilder sqlBuilder = new StringBuilder();
        //    sqlBuilder.Append("SELECT DISTINCT(Ysr_DirectSubjectId) FROM Yw_CourseSubjectRelation");
        //    sqlBuilder.Append(" WHERE Ysr_LessonIndex < @lessonIndex");
        //    sqlBuilder.Append(" AND Ysr_IsDirect = 0");//学习课程中以外的题
        //    sqlBuilder.Append(" AND Ysr_LessonId = @lessonId");
        //    return Query<int>(sqlBuilder.ToString(), new { lessonId = lessonId, lessonIndex = lessonIndex }).ToList();
        //}

        /// <summary>
        /// 获取课后任务中的某个课程每个课时有多少练习题
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="targetSubjectId"></param>
        public IList<DtoSubjectCountToPractice> GetSubjectIdsOfStudyTask(
            IEnumerable<Tuple<int, int>> where)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'");
            foreach (var item in where)
            {
                sb.Append(item.Item1.ToString());
                sb.Append(",");
                sb.Append(item.Item2.ToString());
                sb.Append(";");
            }
            string strWhere = sb.ToString();
            strWhere = strWhere.TrimEnd(';');
            strWhere += "'";

            string sql = @"SELECT DISTINCT ysr_subjectid as SubjectId, r.Ysr_CourseId as CourseId
                            FROM   yw_coursesubjectrelation AS r 
                                   INNER JOIN [dbo].[Fn_to2columntable](" + strWhere + @") AS p 
                                           ON p.item1 = r.ysr_courseid 
                            WHERE  r.ysr_lessonindex < p.item2 
                                   AND r.ysr_isdirect = 0";
            return Query<DtoSubjectCountToPractice>(sql).ToList();
        }
    }
}
