using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IRegionRepository : IRepositoryBase<Bas_Region>
    {

        List<Bas_Region> GetRegionList(int parentId);

        List<DtoRegion> GetRegionByIds(List<int> ids, RegionTypeEnum type);
    }
}
