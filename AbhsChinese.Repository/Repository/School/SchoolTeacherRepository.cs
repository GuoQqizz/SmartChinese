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
    public class SchoolTeacherRepository : RepositoryBase<Yw_SchoolTeacher>, ISchoolTeacherRepository
    {
        public SchoolTeacherRepository() : base(AppSetting.ConnString)
        {
        }

        #region select
        public List<DtoSchoolTeacher> GetSchoolTeacherList(DtoSchoolTeacherSearch search)
        {
            if (search == null)
            {
                return null;
            }
            var strWhere = new StringBuilder();
            var fields = "st.*,sc.Bsl_SchoolName";
            var orderBy = "st.Yoh_Id DESC ";
            var parameters = new DynamicParameters();

            strWhere.Append($@"dbo.Yw_SchoolTeacher st JOIN dbo.Bas_School sc ON st.Yoh_SchoolId=sc.Bsl_Id  WHERE 1=1 AND Yoh_Status <>${(int)StatusEnum.删除} AND Yoh_IsSchoolMaster = 0 ");

            if (search.SchoolId > 0)
            {
                strWhere.Append(" AND Yoh_SchoolId=@Yoh_SchoolId ");
                parameters.Add("Yoh_SchoolId", search.SchoolId);
            }
            if (search.TeacherId > 0)
            {
                strWhere.Append(" AND Yoh_Id=@Yoh_Id");
                parameters.Add("Yoh_Id", search.TeacherId);
            }
            else
            {
                if (search.Status > 0)
                {
                    strWhere.Append(" AND Yoh_Status=@Yoh_Status");
                    parameters.Add("Yoh_Status", search.Status);
                }

                if (search.Grade > 0)
                {
                    strWhere.Append(" AND Yoh_Grade & @Yoh_Grade=@Yoh_Grade");
                    parameters.Add("Yoh_Grade", search.Grade);
                }
                if (search.SearchStr.HasValue())
                {
                    strWhere.Append(" AND (Yoh_Name LIKE @Yoh_Name OR Yoh_Phone LIKE @Yoh_Phone ) ");
                    parameters.Add("Yoh_Name", $"%{search.SearchStr}%");
                    parameters.Add("Yoh_Phone", $"%{search.SearchStr}%");
                }
            }

            return base.QueryPaging<DtoSchoolTeacher>(fields, strWhere._ToString(), orderBy, search.Pagination, parameters).ToList();
        }

        public int GetCountByPhone(string phone)
        {
            string sql = $"SELECT COUNT(1) FROM dbo.Yw_SchoolTeacher WHERE Yoh_Phone=@Yoh_Phone AND Yoh_Status <>${(int)StatusEnum.删除}";

            return Query<int>(sql, new { Yoh_Phone = phone }).FirstOrDefault();
        }

        public DtoSchoolTeacher GetSchoolTeacherByPhone(string phone)
        {
            string sql = $@"SELECT  st.* ,
                                    sc.Bsl_SchoolName
                            FROM    Yw_SchoolTeacher st
                                    JOIN dbo.Bas_School sc ON st.Yoh_SchoolId = sc.Bsl_Id
                            WHERE   Yoh_Phone = @Yoh_Phone  AND Yoh_Status =${(int)StatusEnum.有效}";
            return Query<DtoSchoolTeacher>(sql, new { Yoh_Phone = phone }).FirstOrDefault();
        }

        public List<Yw_SchoolTeacher> GetTeacherByGrade(int grade, int schoolId)
        {
            string sql = $"SELECT * FROM dbo.Yw_SchoolTeacher WHERE Yoh_Grade & @Yoh_Grade = @Yoh_Grade AND Yoh_SchoolId=@Yoh_SchoolId AND Yoh_Status =${(int)StatusEnum.有效}";

            return Query<Yw_SchoolTeacher>(sql, new { Yoh_Grade = grade, Yoh_SchoolId = schoolId }).ToList();
        }
        #endregion

        #region update

        public bool UpdatePwd(int id, string pwd, int edit)
        {
            string sql = "UPDATE dbo.Yw_SchoolTeacher SET Yoh_Password =@Yoh_Password ,Yoh_UpdateTime=GETDATE() ";
            if (edit >= 10000)
            {
                sql += ",Yoh_Editor=@Yoh_Editor ";
            }
            sql += " WHERE Yoh_Id=@Yoh_Id";

            return Execute(sql, new
            {
                Yoh_Password = pwd,
                Yoh_Editor = edit,
                Yoh_Id = id
            }) > 0;
        }

        public bool UpdateSchoolId(int id, int schoolId, int edit)
        {
            string sql = "UPDATE dbo.Yw_SchoolTeacher SET Yoh_SchoolId=@Yoh_SchoolId,Yoh_UpdateTime=GETDATE(),Yoh_Editor=@Yoh_Editor WHERE Yoh_Id=@Yoh_Id ";
            return Execute(sql, new
            {
                Yoh_SchoolId = schoolId,
                Yoh_Editor = edit,
                Yoh_Id = id
            }) > 0;
        }


        public bool UpdateStatus(int id, int toStatus, int edit)
        {
            string sql = "UPDATE dbo.Yw_SchoolTeacher SET Yoh_Status =@toStatus ,Yoh_UpdateTime=GETDATE(),Yoh_Editor=@Yoh_Editor WHERE Yoh_Id=@Yoh_Id ";
            return Execute(sql, new
            {
                Yoh_Id = id,
                Yoh_Editor = edit,
                toStatus = toStatus,
            }) > 0;
        }






        #endregion

        #region basecrud


        public Yw_SchoolTeacher Get(int id)
        {
            var result = base.GetEntity(id);
            if (result != null)
            {
                result.EnableAudit();
            }
            return result;
        }
        public bool Update(Yw_SchoolTeacher entity)
        {
            return base.UpdateEntity(entity);
        }
        public int Insert(Yw_SchoolTeacher entity)
        {
            return base.InsertEntity(entity);
        }
        #endregion
    }
}
