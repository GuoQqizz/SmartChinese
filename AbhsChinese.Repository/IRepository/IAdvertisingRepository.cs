using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IAdvertisingRepository : IRepositoryBase<Bas_Advertising>
    {
         Bas_Advertising Get(int id);
        int Insert(Bas_Advertising obj);

        bool Update(Bas_Advertising obj);
        bool UpdateStatusByBabPosId(int bad_PosId, int fromStatus, int toStatus, DateTime endTime);

        bool IncrementHitCount(int badId, int count);
        bool IncrementValidCount(int badId, int count);
        int InsertList(List<Bas_Advertising> list);
        Bas_Advertising GetByPosId(int bapId);
    }
}
