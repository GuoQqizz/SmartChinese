using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbhsChinese.Repository.Repository
{
    public class RoleRepository : RepositoryBase<Bas_Role>, IRoleRepository
    {
        public RoleRepository() : base(AppSetting.ConnString) { }

        public Bas_Role GetEntityRole(int broId)
        {
            return base.GetEntity(broId);
        }
        public int Add(Bas_Role bas_Role)
        {
            return base.InsertEntity(bas_Role);
        }
        public bool Update(Bas_Role bas_Role)
        {
            return base.UpdateEntity(bas_Role);
        }

        public List<Bas_Role> GetPaging(PagingObject paging, string broCode)
        {
            var strWhere = new StringBuilder();
            strWhere.Append(@"Bas_Role where 1=1 ");
            return base.QueryPaging<Bas_Role>("*", strWhere.ToString(), "Bro_Id", paging).ToList();
        }
        public List<Bas_Role> GetRoleList()
        {
            var strWhere = new StringBuilder();
            strWhere.Append("select * from Bas_Role");
            return base.Query<Bas_Role>(strWhere.ToString()).ToList();
        }
        public Bas_Role GetEntityRoleByCode(string broCode)
        {
            var strWhere = new StringBuilder();
            strWhere.AppendFormat("select * from Bas_Role where Bro_code =@Bro_code");
            return base.Query<Bas_Role>(strWhere.ToString(), new { Bro_code = broCode }).ToList()[0];
        }
    }

}
