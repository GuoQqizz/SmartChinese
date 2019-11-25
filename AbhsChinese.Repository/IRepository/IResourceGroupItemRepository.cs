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
    public interface IResourceGroupItemRepository : IRepositoryBase<Yw_ResourceGroupItem>
    {
        int AddResourceGroupItem(Yw_ResourceGroupItem entity);

        int Delete(int groupId,int resourceType,int resourceId);

        Yw_ResourceGroupItem GetResourceGroupItem(int groupId, int resourceType, int resourceId);

        int GetResourceCount(int groupId,int resourceType);

        List<Yw_ResourceGroupItem> GetGroupIdAndResourceType(int courseId);

        List<Yw_ResourceGroupItem> GetResourceIdByTypeAndGroupId(int groupId, int resourceType);
    }
}
