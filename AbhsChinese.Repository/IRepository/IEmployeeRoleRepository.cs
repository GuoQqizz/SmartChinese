using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Repository.RepositoryBase;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Code.Common;

namespace AbhsChinese.Repository.IRepository
{
    public interface IEmployeeRoleRepository : IRepositoryBase<Bas_EmployeeRole>
    {
        Bas_EmployeeRole Get(int bemId);
        int Add(Bas_EmployeeRole entity);
        bool Update(Bas_EmployeeRole entity);
        Bas_EmployeeRole GetEmployeeRoleByEmployeeId(int employeeId);
    }
}
