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
    public class SumStuDailyRepository : RepositoryBase<Sum_StudentDaily>, ISumStuDailyRepository
    {
        public SumStuDailyRepository() : base(AppSetting.ConnString)
        {
        }

        public void Add(Sum_StudentDaily entity)
        {
            InsertEntity(entity);
        }

        public void Update(Sum_StudentDaily entity)
        {
            UpdateEntity(entity);
        }
    }
}