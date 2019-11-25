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
    public class SchoolRepository : RepositoryBase<Bas_School>, ISchoolRepository
    {
        public SchoolRepository() : base(AppSetting.ConnString)
        {
        }


        #region select
        public List<DtoSchool> GetSchoolList(DtoSchoolSearch search)
        {
            if (search == null)
            {
                return null;
            }
            var strWhere = new StringBuilder();
            var fields = "s.*,sl.Bhl_Name AS LevelName,sl.Bhl_DividePercent,r.Reg_FullName AS RegionName ,st.Yoh_Phone AS LoginPhone ";
            var orderBy = "s.Bsl_Id DESC ";
            var parameters = new DynamicParameters();

            strWhere.Append($@"dbo.Bas_School s
                                    INNER JOIN dbo.Bas_SchoolLevel sl ON s.Bsl_Level=sl.Bhl_Id AND sl.Bhl_Status = {(int)StatusEnum.有效}
                                    INNER JOIN dbo.Bas_Region r ON s.Bsl_County = r.Reg_ID
                                    LEFT JOIN dbo.Yw_SchoolTeacher st ON s.Bsl_SchoolMasterId=st.Yoh_Id AND st.Yoh_Status  = {(int)StatusEnum.有效}
                                    WHERE 1=1");

            if (search.SchoolId > 0)
            {
                strWhere.Append(" AND s.Bsl_Id=@Bsl_Id ");
                parameters.Add("Bsl_Id", search.SchoolId);
            }
            else
            {
                if (search.Status > 0)
                {
                    strWhere.Append(" AND s.Bsl_Status=@Bsl_Status ");
                    parameters.Add("Bsl_Status", search.Status);
                }
                if (search.CountyId > 0)
                {
                    strWhere.Append(" AND s.Bsl_County=@CountyId ");
                    parameters.Add("CountyId", search.CountyId);
                }
                else if (search.CityId > 0)
                {
                    strWhere.Append(" AND s.Bsl_City=@CityId ");
                    parameters.Add("CityId", search.CityId);
                }
                else if (search.ProvId > 0)
                {
                    strWhere.Append(" AND s.Bsl_Province=@ProvId ");
                    parameters.Add("ProvId", search.ProvId);
                }
                if (search.SearchStr.HasValue())
                {
                    strWhere.Append(" AND (s.Bsl_MasterName LIKE @Bsl_MasterName OR s.Bsl_MasterPhone LIKE @Bsl_MasterPhone OR s.Bsl_SchoolName=@Bsl_SchoolName) ");
                    parameters.Add("Bsl_MasterName", $"%{search.SearchStr}%");
                    parameters.Add("Bsl_MasterPhone", $"%{search.SearchStr}%");
                    parameters.Add("Bsl_SchoolName", $"%{search.SearchStr}%");
                }
            }

            return base.QueryPaging<DtoSchool>(fields, strWhere._ToString(), orderBy, search.Pagination, parameters).ToList();
        }

        public Bas_School GetSchoolByStudent(int studentId)
        {
            string sql = @"select a.* from Bas_School a inner join Bas_Student b on a.Bsl_Id=b.Bst_SchoolId 
                          where b.bst_id = @StudentId";
            return Query(sql, new { StudentId = studentId }).FirstOrDefault();
        }

        public List<Bas_School> GetSchoolByIdOrName(PagingObject paging, string idOrName)
        {
            var sql = "Bas_School where 1=1";
            var parameters = new DynamicParameters();
            if (idOrName.IsNumberic())
            {
                sql += " and Bsl_Id=@Id";
                parameters.Add("Id", idOrName);
            }
            else
            {
                sql += " and Bsl_SchoolName like @SchoolName";
                parameters.Add("SchoolName", "%" + idOrName + "%");
            }

            var list = QueryPaging<Bas_School>("*", sql, "Bsl_Id", paging, parameters).ToList();
            return list;
        }
        #endregion

        #region update
        public bool UpdateMasterId(int id, int masterId)
        {
            string sql = "UPDATE dbo.Bas_School SET Bsl_SchoolMasterId=@Bsl_SchoolMasterId,Bsl_UpdateTime=GETDATE() WHERE Bsl_Id=@Bsl_Id ";
            return Execute(sql, new
            {
                Bsl_SchoolMasterId = masterId,
                Bsl_Id = id
            }) > 0;
        }


        #endregion
        #region basecrud
        public bool Update(Bas_School entity)
        {
            return base.UpdateEntity(entity);
        }

        public int Insert(Bas_School obj)
        {
            return base.InsertEntity(obj);
        }

        public Bas_School Get(int id)
        {
            var result = base.GetEntity(id);
            if (result != null)
            {
                result.EnableAudit();
            }
            return result;
        }
        #endregion
    }
}
