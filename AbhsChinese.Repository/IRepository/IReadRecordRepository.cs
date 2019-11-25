using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IReadRecordRepository : IRepositoryBase<Yw_ReadRecord>
    {
        void Add(Yw_ReadRecord entity);

        void Update(Yw_ReadRecord entity);

        Yw_ReadRecord Get(int unitId, string actionId, int studentId);

        int CalculateRankPercent(int unitId, string actionId, int score);
    }
}
