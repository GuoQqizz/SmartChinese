using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using AbhsChinese.Code.Common;

namespace AbhsChinese.Repository.Repository
{
    public class EmployeeRoleRepository : RepositoryBase<Bas_EmployeeRole>, IEmployeeRoleRepository
    {
        public EmployeeRoleRepository() : base(AppSetting.ConnString) { }

        public Bas_EmployeeRole GetEmployeeRoleEntity(int Id)
        {
            return base.GetEntity(Id);
        }
        public int Add(Bas_EmployeeRole bas_EmployeeRole)
        {
            return base.InsertEntity(bas_EmployeeRole);
        }
        public bool Update(Bas_EmployeeRole bas_EmployeeRole)
        {
            return base.UpdateEntity(bas_EmployeeRole);
        }
        public Bas_EmployeeRole GetEmployeeRoleByEmployeeId(int employeeId)
        {
            var strWhere = new StringBuilder();
            strWhere.AppendFormat("select * from Bas_EmployeeRole  where Ber_EmployeeId = {0}", employeeId);
            var result= base.Query<Bas_EmployeeRole>(strWhere.ToString()).ToList()[0];
            if (result != null)
            {
                result.EnableAudit();
            }
            return result;
        }

        public Bas_EmployeeRole Get(int bemId)
        {
            var result = base.GetEntity(bemId);
            if (result != null)
            {
                result.EnableAudit();
            }
            return result;
        }
    }
}
