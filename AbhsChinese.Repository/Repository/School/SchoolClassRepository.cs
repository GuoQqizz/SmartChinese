using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository.School;
using AbhsChinese.Repository.RepositoryBase;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository.School
{
    public class SchoolClassRepository : RepositoryBase<Yw_SchoolClass>, ISchoolClassRepository
    {
        public SchoolClassRepository() : base(AppSetting.ConnString)
        {
        }

        public List<DtoSchoolClass> GetSchoolClassList(DtoSchoolClassSearch search)
        {
            if (search == null)
            {
                return null;
            }
            var strWhere = new StringBuilder();
            var fields = "sc.*, st.Yoh_Name AS ClassMasterName ,c.Ycs_Name AS CourseName";
            var orderBy = "sc.Ycc_Id DESC ";
            var parameters = new DynamicParameters();

            strWhere.Append($@"dbo.Yw_SchoolClass sc
                                    JOIN dbo.Yw_SchoolTeacher st ON sc.Ycc_ClassMaster=st.Yoh_Id
                                    JOIN dbo.Yw_Course c ON sc.Ycc_CourseId=c.Ycs_Id
                                    WHERE 1=1");

            if (search.SchoolId > 0)
            {
                strWhere.Append(" AND sc.Ycc_SchoolId=@Ycc_SchoolId ");
                parameters.Add("Ycc_SchoolId", search.SchoolId);
            }
            if (search.ClassId > 0)
            {
                strWhere.Append(" AND sc.Ycc_Id=@Ycc_Id");
                parameters.Add("Ycc_Id", search.ClassId);
            }
            else
            {
                if (search.CourseType > 0)
                {
                    strWhere.Append(" AND sc.Ycc_CourseType=@Ycc_CourseType");
                    parameters.Add("Ycc_CourseType", search.CourseType);
                }
                if (search.Status > 0)
                {
                    strWhere.Append(" AND sc.Ycc_Status=@Ycc_Status");
                    parameters.Add("Ycc_Status", search.Status);
                }

                if (search.Grade > 0)
                {
                    strWhere.Append(" AND sc.Ycc_Grade=@Ycc_Grade");
                    parameters.Add("Ycc_Grade", search.Grade);
                }
                if (search.SearchStr.HasValue())
                {
                    strWhere.Append(" AND (sc.Ycc_Name LIKE @Ycc_Name OR c.Ycs_Name LIKE @Ycs_Name OR st.Yoh_Name LIKE @Yoh_Name ) ");
                    parameters.Add("Ycc_Name", $"%{search.SearchStr}%");
                    parameters.Add("Ycs_Name", $"%{search.SearchStr}%");
                    parameters.Add("Yoh_Name", $"%{search.SearchStr}%");
                }
            }

            var list = base.QueryPaging<DtoSchoolClass>(fields, strWhere._ToString(), orderBy, search.Pagination, parameters).ToList();



            return list;
        }

        public List<Yw_SchoolClass> GetSchoolClassByCourseId(int courseId, int schoolId)
        {
            string sql = " SELECT * FROM dbo.Yw_SchoolClass WHERE Ycc_SchoolId=@Ycc_SchoolId AND Ycc_CourseId=@Ycc_CourseId  AND Ycc_Status<>@Ycc_Status";
            return Query(sql, new { Ycc_CourseId = courseId, Ycc_SchoolId = schoolId, Ycc_Status = (int)SchoolClassEnum.已结课 }).ToList();
        }



        public bool IncrementStudentCount(int classId, int count, int oper)
        {
            string sql = "UPDATE dbo.Yw_SchoolClass SET Ycc_StudentCount=Ycc_StudentCount+@Count,Ycc_UpdateTime=GETDATE(),Ycc_Editor=@Ycc_Editor WHERE Ycc_Id=@Ycc_Id";
            return Execute(sql, new
            {
                Count = count,
                Ycc_Id = classId,
                Ycc_Editor = oper
            }) > 0;
        }

        #region basecrud
        public Yw_SchoolClass Get(int id)
        {
            var result = base.GetEntity(id);
            if (result != null)
            {
                result.EnableAudit();
            }
            return result;
        }

        public bool Update(Yw_SchoolClass entity)
        {
            return base.UpdateEntity(entity);
        }

        public int Insert(Yw_SchoolClass obj)
        {
            return base.InsertEntity(obj);
        }
        #endregion
    }
}
