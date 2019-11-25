using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
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
    public class CourseRepository : RepositoryBase<Yw_Course>, ICourseRespository
    {
        public CourseRepository() : base(AppSetting.ConnString)
        {
        }

        #region select

        public IList<DtoCourseListItem> GetByPage(DtoCurriculumSearch search)
        {
            Check.IfNull(search, nameof(search));

            string fields = "t.*, e.bem_Name AS Ycs_OwnerName";
            StringBuilder builder = new StringBuilder();
            builder.Append("Yw_Course as t");
            builder.Append(" LEFT JOIN Bas_Employee AS e ON t.Ycs_Owner = e.bem_Id");
            builder.Append(" WHERE Ycs_Status != 6");//只要未删除的数据
            if (search.Id >= 10000)
            {
                builder.Append(" AND t.Ycs_Id = @Id");
            }
            if (search.Name.HasValue())
            {
                builder.Append(" AND t.Ycs_Name like @Name");
            }
            if (search.OwnerName.HasValue())
            {
                builder.Append(" AND e.bem_Name = @Owner");
            }
            if (search.Status != null && search.Status.Count > 0)
            {
                builder.Append(" AND t.Ycs_Status in @Status");
            }
            if (search.Grade > 0)
            {
                builder.Append(" AND t.Ycs_Grade = @Grade");
            }
            int courseType = -1;
            if (search.CourseType.HasValue)
            {
                courseType = (int)search.CourseType.Value;
                builder.Append(" AND t.Ycs_CourseType = @CourseType");
            }
            string where = builder.ToString();
            string orderby = "t.Ycs_UpdateTime DESC";
            object param = new
            {
                Id = search.Id,
                Name = $"%{search.Name}%",
                Owner = search.OwnerName,
                Status = search.Status,
                Grade = search.Grade,
                CourseType = courseType
            };
            return base.QueryPaging<DtoCourseListItem>(
                fields,
                where,
                orderby,
                search.Pagination,
                param).ToList();
        }

        public Yw_Course GetCourse(int id)
        {
            return GetEntity(id);
        }

        public List<Yw_Course> GetCourseByIdOrName(string idOrName)
        {
            var sql = $"SELECT * FROM dbo.Yw_Course WHERE Ycs_Status = {(int)CourseStatusEnum.已上架} AND  Ycs_Id LIKE @Ycs_Id  OR Ycs_Name LIKE @Ycs_Name";

            var list = base.Query(sql, new
            {
                Ycs_Id = $"{idOrName}%",
                Ycs_Name = $"%{idOrName}%"
            }).ToList();
            return list;
        }
        public List<Yw_Course> GetCourseByTypeAndGrade(int type, int grade)
        {
            var sql = $"SELECT * FROM dbo.Yw_Course WHERE Ycs_CourseType=@Ycs_CourseType AND Ycs_Grade  = @Ycs_Grade AND Ycs_Status = {(int)CourseStatusEnum.已上架}";

            var list = base.Query(sql, new
            {
                Ycs_CourseType = type,
                Ycs_Grade = grade
            }).ToList();
            return list;
        }

        public void InsertCourse(Yw_Course entity)
        {
            base.InsertEntity(entity);
        }

        /// <summary>
        /// 选课中心课程列表,包含价格
        /// </summary>
        public List<DtoSelectCenterCourseObject> GetCourseWithPrice(DtoCourseSelectCondition condition, PagingObject paging)
        {
            string field = $@"a.Ycs_Id as CourseId,a.Ycs_Name as CourseName,a.Ycs_CoverImage as CourseImage,
                            b.Yce_Price as CoursePrice,a.Ycs_SellCount as SellCount,
                            a.Ycs_Grade as Grade,a.Ycs_CourseType as CourseType,a.Ycs_LessonCount as LessonCount";
            string where = $@"yw_course a inner join yw_courseprice b 
                            on a.ycs_id = b.Yce_CourseId and b.Yce_SchoolLevelId = @SchoolLevelId 
                            where a.ycs_status = 3 and isnull(b.Yce_Price,0)>0 {condition.GradeExpression} {condition.CourseTypeExpression}";
            string orderBy = condition.OrderExpression;

            return QueryPaging<DtoSelectCenterCourseObject>(field, where, orderBy, paging, new { SchoolLevelId = condition.SchoolLevel, Grade = condition.Grade, CourseType = condition.CourseType }).ToList();
        }

        /// <summary>
        /// 选课中心课程列表,不包含价格
        /// </summary>
        public List<DtoSelectCenterCourseObject> GetCourseWithoutPrice(DtoCourseSelectCondition condition, PagingObject paging)
        {
            string field = $@"a.Ycs_Id as CourseId,a.Ycs_Name as CourseName,a.Ycs_CoverImage as CourseImage,
                              a.Ycs_SellCount as SellCount,a.Ycs_Grade as Grade,a.Ycs_CourseType as CourseType,a.Ycs_LessonCount as LessonCount";
            string where = $@"yw_course a
                             where a.ycs_status = 3 {condition.GradeExpression} {condition.CourseTypeExpression}";
            string orderBy = condition.OrderExpression;

            return QueryPaging<DtoSelectCenterCourseObject>(field, where, orderBy, paging, new { SchoolLevelId = condition.SchoolLevel, Grade = condition.Grade, CourseType = condition.CourseType }).ToList();
        }

        public DtoSelectCenterCourseDetailObject GetCourseDetailWithPrice(int courseId, int schoolLevelId, bool isPreview = false)
        {
            string previewCondition = isPreview ? "" : " AND a.Ycs_Status = @CourseStatus";
            string sql = $@"SELECT  a.Ycs_Id AS CourseId ,
            a.Ycs_Name AS CourseName ,
            a.Ycs_CoverImage AS CourseImage ,
            b.Yce_Price AS CoursePrice ,
            a.Ycs_SellCount AS SellCount ,
            a.Ycs_Grade AS Grade ,
            a.Ycs_CourseType AS CourseType ,
            a.Ycs_LessonCount AS LessonCount ,
            a.Ycs_Description AS [Description]
    FROM    Yw_Course (NOLOCK) a
            INNER JOIN Yw_CoursePrice b ON a.Ycs_Id = b.Yce_CourseId
                                           AND b.Yce_SchoolLevelId = @SchoolLevelId
    WHERE   a.Ycs_Id = @CourseId
            {previewCondition}";
            return Query<DtoSelectCenterCourseDetailObject>(sql, new { SchoolLevelId = schoolLevelId, CourseId = courseId, CourseStatus = (int)CourseStatusEnum.已上架 }).FirstOrDefault();
        }

        public DtoSelectCenterCourseDetailObject GetCourseDetailWithoutPrice(int courseId, bool isPreview = false)
        {
            string previewCondition = isPreview ? "" : " AND a.Ycs_Status = @CourseStatus";
            string sql = $@"SELECT  a.Ycs_Id AS CourseId ,
            a.Ycs_Name AS CourseName ,
            a.Ycs_CoverImage AS CourseImage ,
            a.Ycs_SellCount AS SellCount ,
            a.Ycs_Grade AS Grade ,
            a.Ycs_CourseType AS CourseType ,
            a.Ycs_LessonCount AS LessonCount ,
            a.Ycs_Description AS [Description]
    FROM    Yw_Course (NOLOCK) a            
    WHERE   a.Ycs_Id = @CourseId
            {previewCondition}";
            return Query<DtoSelectCenterCourseDetailObject>(sql, new { CourseId = courseId, CourseStatus = (int)CourseStatusEnum.已上架 }).FirstOrDefault();
        }
        
        public List<Yw_Course> GetPagingCourseByIdOrName(PagingObject paging, string idOrName)
        {
            var sql = $"Yw_Course WHERE Ycs_Status = {(int)CourseStatusEnum.已上架}";
            DynamicParameters parameters = new DynamicParameters();
            if (idOrName.IsNumberic())
            {
                sql += " AND  Ycs_Id=@Ycs_Id";
                parameters.Add("Ycs_Id", idOrName);
            }
            else
            {
                sql += " AND Ycs_Name like @Ycs_Name";
                parameters.Add("Ycs_Name", "%" + idOrName + "%");
            }
            var list = QueryPaging<Yw_Course>("*", sql, "Ycs_Id", paging, parameters).ToList();
            return list;
        }

        public void Update(Yw_Course entity)
        {
            UpdateEntity(entity);
        }

        #endregion select
    }
}