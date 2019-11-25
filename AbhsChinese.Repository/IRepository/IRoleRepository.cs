using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IRoleRepository : IRepositoryBase<Bas_Role>
    {
        Bas_Role GetEntityRole(int broId);
        int Add(Bas_Role entity);
        bool Update(Bas_Role entity);
        List<Bas_Role> GetRoleList();
        List<Bas_Role> GetPaging(PagingObject paging, string broCode);
        Bas_Role GetEntityRoleByCode(string broCode);
    }
}
