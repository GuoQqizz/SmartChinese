using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IAdvertisingPosRepository : IRepositoryBase<Bas_AdvertisingPos>
    {
        Bas_AdvertisingPos Get(int id);
        List<DtoAdvertisingPos> GetPagingAdvertisingPos(PagingObject paging, int advPosType, int advStatus);
        List<DtoAdvertisingPos> GetPagingAdvertisingHistory(PagingObject paging, int advPosId);
        bool EditAdertisingPosStatus(int bapId, int bapStatus, int editor);
        List<DtoAdvertisingIndex> GetPagingAdvertisingForIndex(string codePre);
      
    }
}
