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
    public class CashVoucherIndexRepository : RepositoryBase<Yw_CashVoucherIndex>, ICashVoucherIndexRepository
    {
        public CashVoucherIndexRepository() : base(AppSetting.ConnString) { }
        
        public List<Yw_CashVoucherIndex> GetCashVoucherIndeies()
        {
            throw new NotImplementedException();
        }
    }
}
