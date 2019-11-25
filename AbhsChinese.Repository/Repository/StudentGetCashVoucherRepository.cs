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
    public class StudentGetCashVoucherRepository : RepositoryBase<Yw_StudentGetCashVoucher>, IStudentGetCashVoucherRepository
    {
        public StudentGetCashVoucherRepository() : base(AppSetting.ConnString)
        {
        }
        public int Add(Yw_StudentGetCashVoucher entity)
        {
            return InsertEntity(entity);
        }
    }
}
