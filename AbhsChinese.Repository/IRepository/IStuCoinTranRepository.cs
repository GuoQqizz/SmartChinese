using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IStuCoinTranRepository : IRepositoryBase<Yw_StudentCoinTransaction>
    {
        void Add(Yw_StudentCoinTransaction entity);
    }
}
