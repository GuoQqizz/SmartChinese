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
    public class CoursePriceAuditRepository : RepositoryBase<Yw_CoursePriceAudit>, ICoursePriceAuditRepository
    {
        public CoursePriceAuditRepository() : base(AppSetting.ConnString) { }
    }
}
