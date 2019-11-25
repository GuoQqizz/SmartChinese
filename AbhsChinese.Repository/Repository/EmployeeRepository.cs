using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using AbhsChinese.Code.Common;
using Dapper;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.Dto.Request.Teachers;
using AbhsChinese.Domain.Dto.Response.Teachers;

namespace AbhsChinese.Repository.Repository
{
    public class EmployeeRepository : RepositoryBase<Bas_Employee>, IEmployeeRepository
    {
        public EmployeeRepository() : base(AppSetting.ConnString) { }

        public Bas_Employee GetEntity(int bemId)
        {
            return base.GetEntity(bemId);
        }

        public int Add(Bas_Employee entity)
        {
            return base.InsertEntity(entity);
        }

        public bool Update(Bas_Employee entity)
        {
            return base.UpdateEntity(entity);
        }

        public List<DtoEmployee> GetPagingEmployee(PagingObject paging, string accountOrNameOrPhone, int broId, int status)
        {
            var strWhere = new StringBuilder();
            var parameters = new DynamicParameters();
            strWhere.Append($@"Bas_Employee bm 
left join Bas_EmployeeRole ber on ber.Ber_EmployeeId = bm.Bem_Id 
join Bas_Role br on  br.Bro_Id = ber.Ber_RoleId where 1=1 AND bm.Bem_Status <> {(int)StatusEnum.删除}");
            if (!string.IsNullOrEmpty(accountOrNameOrPhone))
            {
                var tmp = accountOrNameOrPhone.Trim();
                strWhere.AppendFormat(" and (bm.Bem_Account like @Bem_Account or bm.Bem_Name like @Bem_Name Or bm.Bem_Phone = @Bem_Phone)");
                parameters.Add("Bem_Account", $"%{tmp}%");
                parameters.Add("Bem_Name", $"%{tmp}%");
                parameters.Add("Bem_Phone", tmp);


            }
            if (broId > 0)
            {
                strWhere.AppendFormat(" and ber.Ber_RoleId = @Ber_RoleId");
                parameters.Add("Ber_RoleId", broId);

            }
            if (status > 0)
            {
                strWhere.AppendFormat(" and bm.Bem_Status = @Bem_Status");
                parameters.Add("Bem_Status", status);

            }
            return base.QueryPaging<DtoEmployee>("bm.*,br.Bro_Name", strWhere.ToString(), "bm.Bem_Id", paging, parameters).ToList();
        }
        public int CheckUniqueAccount(string account)
        {
            string sql = $"select COUNT(1) from Bas_Employee where Bem_Account = @Bem_Account AND Bem_Status <> {(int)StatusEnum.删除}";
            return base.ExecuteScalar<int>(sql.ToString(), new { Bem_Account = account });

        }

        public DtoEmployee GetByAccount(string account, int status = 0)
        {
            string sql = $@" SELECT bm.* ,br.Bro_Id , br.Bro_Name
                             FROM dbo.Bas_Employee bm 
left join Bas_EmployeeRole ber on ber.Ber_EmployeeId = bm.Bem_Id 
join Bas_Role br on  br.Bro_Id = ber.Ber_RoleId
                            WHERE  Bem_Account = @Bem_Account ";
            if (status == 0)
            {
                sql += $" AND Bem_Status <> {(int)StatusEnum.删除} ";
            }
            else
            {
                sql += $" AND Bem_Status ={status} ";
            }
            return base.Query<DtoEmployee>(sql, new { Bem_Account = account }).FirstOrDefault();
        }

        /// <summary>
        /// 根据id集合批量获取用户名
        /// </summary>
        /// <param name="ids">用户id集合</param>
        /// <returns>返回key:id,value:name的字典</returns>
        public Dictionary<int, string> GetEmployeeNameByIds(List<int> ids)
        {
            if (ids.Count > 0)
            {
                return base.Query($"select  [Bem_Id],[Bem_Name] from [Bas_Employee] where Bem_Id in ({string.Join(",", ids.ToArray())})").ToDictionary(s => s.Bem_Id, a => a.Bem_Name);
            }
            else
            {
                return new Dictionary<int, string>();
            }
        }

        public IList<Bas_Employee> GetEntities(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new List<Bas_Employee>();
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT * FROM Bas_Employee");
            stringBuilder.Append(" Where Bem_Id IN @ids");

            string orderbyParameters = string.Empty;
            string orderby = SqlBuilder.GenerateOrderBy(ids, "Bem_Id", out orderbyParameters);
            stringBuilder.Append(orderby);
            string sql = stringBuilder.ToString();
            return Query<Bas_Employee>(sql, new { ids = ids, OrderbyParameters = orderbyParameters }).ToList();
        }

        public IList<DtoTeacher> GetEmployees(DtoEmployeeSearch search)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(@"SELECT e.Bem_Id AS Id,
                                       e.Bem_Name AS Name 
                                      FROM Bas_Role AS r
                                INNER JOIN Bas_EmployeeRole AS er on er.Ber_RoleId = r.Bro_Id
                                INNER JOIN Bas_Employee AS e on er.Ber_EmployeeId = e.Bem_Id");
            sqlBuilder.Append(" WHERE e.Bem_Status = @Status");
            if (search.RoleCode.HasValue())
            {
                sqlBuilder.Append(" AND r.Bro_code = @RoleCode");
            }
            if (search.Grade > 0)
            {
                sqlBuilder.Append(" AND (e.Bem_Grades & @Grade) > 0");
            }

            return Query<DtoTeacher>(
                sqlBuilder.ToString(),
                search).ToList();
        }
    }
}
