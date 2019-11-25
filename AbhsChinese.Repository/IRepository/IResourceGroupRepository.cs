using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;

namespace AbhsChinese.Repository.IRepository
{
    public interface IResourceGroupRepository : IRepositoryBase<Yw_ResourceGroup>
    {
        int AddResourceGroup(Yw_ResourceGroup entity);

        bool Update(Yw_ResourceGroup entity);

        List<Yw_ResourceGroup> GetPagingResourceGroup(PagingObject paging, int id, string name, int grade, int status);
        
        bool Delete(Yw_ResourceGroup entity);

        Yw_ResourceGroup GetResourceGroup(int id);
    }
}
