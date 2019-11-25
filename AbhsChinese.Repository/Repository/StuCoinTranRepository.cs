using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class StuCoinTranRepository : RepositoryBase<Yw_StudentCoinTransaction>, IStuCoinTranRepository
    {
        public StuCoinTranRepository() : base(AppSetting.ConnString)
        {
        }

        public void Add(Yw_StudentCoinTransaction entity)
        {
            InsertEntity(entity);
        }
    }
}
