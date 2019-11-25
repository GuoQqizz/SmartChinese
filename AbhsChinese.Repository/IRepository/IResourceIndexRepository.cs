using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Repository.IRepository
{
    public interface IResourceIndexRepository : IRepositoryBase<Yw_ResourceIndex>
    {
        int Add(Yw_ResourceIndex entity);

        List<Yw_ResourceIndex> GetResourceIndices(int resourceId, int resourceCategory);

        int Delete(int[] ids);

        List<int> GetResourceIndexIds(PagingObject paging, int grade, int mediaType, int textType, string nameOrKey, ResourceTypeEnum resourceType);

        int UpdateByIds(List<int> ids, int resourceType, int grade);

        List<int> GetMediaIndexIds(PagingObject paging, int grade, int mediaType, string nameOrKey);
    }
}
