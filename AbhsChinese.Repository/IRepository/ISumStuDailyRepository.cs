using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface ISumStuDailyRepository : IRepositoryBase<Sum_StudyFactDaily>
    {
        void Add(Sum_StudyFactDaily entity);

        void Update(Sum_StudyFactDaily entity);
    }
}
