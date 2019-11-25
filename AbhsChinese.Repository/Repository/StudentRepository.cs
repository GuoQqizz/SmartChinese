using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class StudentRepository : RepositoryBase<Bas_Student>, IStudentRepository
    {
        public StudentRepository() : base(AppSetting.ConnString)
        {
        }

        public Bas_Student Get(int id)
        {
            var entity = GetEntity(id);
            if (entity != null)
            {
                entity.EnableAudit();
            }
            return entity;
        }

        #region select
        /// <summary>
        /// 学校学生列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<DtoSchoolStudent> GetSchoolStudentList(DtoSchoolStudentSearch search)
        {
            if (search == null)
            {
                return null;
            }
            var strWhere = new StringBuilder();
            var fields = "s.*,sp.Bsp_PassportKey AS StudentAccount,sc.Bsl_SchoolName AS SchoolName,ss.Sst_OwnCourseCount";
            var orderBy = "s.Bst_Id DESC ";
            var parameters = new DynamicParameters();
            strWhere.Append(@"dbo.Bas_Student s 
                                 JOIN dbo.Bas_StudentPassport sp ON s.Bst_Id = sp.Bsp_StudentId
                                 JOIN dbo.Bas_School sc ON s.Bst_SchoolId = sc.Bsl_Id
                                 JOIN dbo.Sum_Student ss ON s.Bst_Id = ss.Sst_StudentId
                                 WHERE 1 = 1 ");

            if (search.SchoolId > 0)
            {
                strWhere.Append("AND s.Bst_SchoolId=@Bst_SchoolId ");
                parameters.Add("Bst_SchoolId", search.SchoolId);
            }
            strWhere.Append(" AND sp.Bsp_PassportType=@Bsp_PassportType ");
            parameters.Add("Bsp_PassportType", (int)StudentAccountSourceEnum.手机);
            if (search.StudentId > 0)
            {
                strWhere.Append(" AND s.Bst_Id=@Bst_Id ");
                parameters.Add("Bst_Id", search.StudentId);
            }
            else
            {
                if (search.Grade > 0)
                {
                    strWhere.Append(" AND s.Bst_Grade=@Bst_Grade ");
                    parameters.Add("Bst_Grade", search.Grade);
                }

                if (search.Status > 0)
                {
                    strWhere.Append(" AND s.Bst_Status=@Bst_Status ");
                    parameters.Add("Bst_Status", search.Status);
                }
                if (search.Bst_No.HasValue())
                {
                    strWhere.Append(" AND s.Bst_No=@Bst_No ");
                    parameters.Add("Bst_No", search.Bst_No);
                }
                if (search.AccountType > 0)
                {
                    if (search.AccountType == (int)StudentAccountTypeEnum.客户)
                    {
                        strWhere.Append(" AND ss.Sst_OwnCourseCount>0 ");
                    }
                    else if (search.AccountType == (int)StudentAccountTypeEnum.用户)
                    {
                        strWhere.Append(" AND ss.Sst_OwnCourseCount=0 ");
                    }
                }
                if (search.SearchStr.HasValue())
                {
                    strWhere.Append(" AND (s.Bst_Phone LIKE @Bst_Phone OR s.Bst_NickName LIKE @Bst_NickName OR sp.Bsp_PassportKey LIKE @Bsp_PassportKey) ");
                    parameters.Add("Bst_Phone", $"%{search.SearchStr}%");
                    parameters.Add("Bst_NickName", $"%{search.SearchStr}%");
                    parameters.Add("Bsp_PassportKey", $"%{search.SearchStr}%");
                }
            }



            return base.QueryPaging<DtoSchoolStudent>(fields, strWhere._ToString(), orderBy, search.Pagination, parameters).ToList();
        }
        #endregion

        #region update
        public bool UpdateSchool(int studentId, int schoolId)
        {
            string sql = "  UPDATE dbo.Bas_Student SET Bst_SchoolId=@Bst_SchoolId ,Bst_UpdateTime=GETDATE() WHERE Bst_Id=@Bst_Id";
            return Execute(sql, new
            {
                Bst_SchoolId = schoolId,
                Bst_Id = studentId
            }) > 0;
        }
        #endregion


        #region 学生信息
        public string GetStudentNo()
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@StudentNo", "", DbType.String, direction: ParameterDirection.Output);
            ExecuteProc("P_CreateStudentNo", parameter);
            return parameter.Get<string>("@StudentNo");
        }

        public int Add(Bas_Student entity)
        {
            return InsertEntity(entity);
        }

        public DtoSumStudentTip GetSumStudentTip(int studentId)
        {
            string sql = "select Sst_StudentId as StudentId,Sst_FirstOrderTime as FirstOrderTime,Sst_LastLoginIp as LastLoginIP,Sst_LastLoginArea as LastLoginArea,Sst_LastLoginTime as LastLoginTime,Sst_OwnCourseCount as OwnCourseCount,Sst_ExperiencePoints as ExperiencePoints,Sst_Coins as Coins,Sst_StudyDayCount as StudyDayCount,Bst_Name as Name,Bst_Avatar,Bst_Sex as Sex,Bst_Phone as Phone,Bsl_SchoolName as School from Sum_Student inner join Bas_Student on Sst_StudentId=Bst_Id left join Bas_School on Bst_SchoolId=Bsl_Id where Sst_StudentId=@StudentId";
            return QueryObject<DtoSumStudentTip>(sql, new { StudentId = studentId }).FirstOrDefault();
        }

        public DtoStudentInfo GetStudentInfoById(int id)
        {
            string sql = "select Bas_Student.*,Bsl_SchoolName as SchoolName from Bas_Student left join Bas_School on Bst_SchoolId=Bsl_Id where Bst_Id=@Id";
            return QueryObject<DtoStudentInfo>(sql, new { Id = id }).FirstOrDefault();
        }
        public DtoStudentInfo GetStudentInfoByAccount(string account)
        {
            string sql = @"SELECT  s.* ,
                                    Bsl_SchoolName AS SchoolName
                            FROM    Bas_Student s
                                    LEFT JOIN Bas_School sc ON s.Bst_SchoolId = sc.Bsl_Id
                                    JOIN dbo.Bas_StudentPassport sp ON s.Bst_Id = sp.Bsp_StudentId
                            WHERE   sp.Bsp_PassportKey = @Bsp_PassportKey";
            return QueryObject<DtoStudentInfo>(sql, new { Bsp_PassportKey = account }).FirstOrDefault();
        }

        public bool Update(Bas_Student entity)
        {
            return UpdateEntity(entity);
        }
        #endregion

        public Bas_Student GetBySchoolId()
        {
            string sql = "select top 1 * from Bas_Student where Bst_SchoolId=10000 and Bst_Status=1";
            return Query(sql).FirstOrDefault();
        }
    }
}
