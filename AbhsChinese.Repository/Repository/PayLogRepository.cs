using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;

namespace AbhsChinese.Repository.Repository
{
    public class PayLogRepository : RepositoryBase<Yw_PayLog>, IPayLogRepository
    {
        public PayLogRepository() : base(AppSetting.ConnString)
        {
        }

        public void Add(Yw_PayLog log)
        {
            InsertEntity(log);
        }
    }
}
