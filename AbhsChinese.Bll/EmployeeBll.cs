using System;
using System.Collections.Generic;
using System.Transactions;
using AbhsChinese.Repository.Repository;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.Dto.Request.Teachers;
using AbhsChinese.Domain.Dto.Response.Teachers;

namespace AbhsChinese.Bll
{
    public class EmployeeBll : BllBase
    {
        #region repository
        private IEmployeeRepository employeeRepository;
        public IEmployeeRepository BllEmployeeRepository
        {
            get
            {
                if (employeeRepository == null)
                {
                    employeeRepository = new EmployeeRepository();
                }
                employeeRepository.TranId = tranId;
                return employeeRepository;
            }
        }



        private IRoleRepository roleRepository;
        public IRoleRepository BllRoleRepository
        {
            get
            {
                if (roleRepository == null)
                {
                    roleRepository = new RoleRepository();
                }
                roleRepository.TranId = tranId;
                return roleRepository;
            }
        }

        private IEmployeeRoleRepository employeeRoleRepository;
        public IEmployeeRoleRepository EmployeeRoleRepository
        {
            get
            {
                if (employeeRoleRepository == null)
                {
                    employeeRoleRepository = new EmployeeRoleRepository();
                }
                employeeRoleRepository.TranId = tranId;
                return employeeRoleRepository;
            }
        }
        #endregion

        #region select
        public Bas_Employee GetEmployeeEntity(int bem_id)
        {
            return BllEmployeeRepository.GetEntity(bem_id);
        }


        public List<DtoEmployee> GetPagingEmployee(PagingObject paging, string accountOrNameOrPhone, int bro_id, int status)
        {

            return BllEmployeeRepository.GetPagingEmployee(paging, accountOrNameOrPhone, bro_id, status);
        }

        public List<Bas_Role> GetRoleList()
        {
            return BllRoleRepository.GetRoleList();
        }

        public int CheckUniqueAccount(string account)
        {
            return BllEmployeeRepository.CheckUniqueAccount(account);
        }

        public IList<DtoTeacher> GetEmployees(DtoEmployeeSearch search)
        {
            return BllEmployeeRepository.GetEmployees(search);
        }
        #endregion


        public DtoEmployee Login(string account, string pwd)
        {
            DtoEmployee result = null;
            var model = BllEmployeeRepository.GetByAccount(account, (int)StatusEnum.有效);
            if (model != null)
            {
                if (model.Bem_Password._EqualsByOrdinalIgnoreCase(Encrypt.GetMD5Pwd(pwd)))
                {
                    result = model;
                    try
                    {
                        string ip = "";
                        string cityName = "";
                        ip = clsCommon.GetIP();
                        if (ip.HasValue())
                        {
                            cityName = clsCommon.CityName_ByBaidu(ip);
                        }
                        if (ip.HasValue() || cityName.HasValue())
                        {
                            var old = BllEmployeeRepository.GetEntity(model.Bem_Id);
                            old.Bem_LastLoginArea = cityName;
                            old.Bem_LastLoginIp = ip;
                            old.Bem_LastLoginTime = DateTime.Now;
                            BllEmployeeRepository.Update(old);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return result;
        }


        #region save
        public bool AddEmployee(Bas_Employee employee, Bas_EmployeeRole bas_EmployeeRole)
        {
            bool result = false;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    BeginTran();
                    Bas_Role bas_Role = BllRoleRepository.GetEntityRoleByCode(employee.Bem_Roles);
                    int value = BllEmployeeRepository.Add(employee);
                    result = value > 0;
                    if (result)
                    {
                        bas_EmployeeRole.Ber_EmployeeId = value;
                        bas_EmployeeRole.Ber_RoleId = bas_Role.Bro_Id;
                        result = EmployeeRoleRepository.Add(bas_EmployeeRole) > 0;
                    }
                    if (result)
                    {
                        scope.Complete();
                    }
                    else
                    {
                        RollbackTran();
                    }
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    result = false;
                    throw ex;
                }
            }
            return result;
        }

        public bool UpdateEmployee(Bas_Employee employee, Bas_EmployeeRole bas_EmployeeRole)
        {
            bool result = false;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    BeginTran();
                    Bas_Role bas_Role = BllRoleRepository.GetEntityRoleByCode(employee.Bem_Roles);
                    result = BllEmployeeRepository.Update(employee);
                    if (result)
                    {
                        Bas_EmployeeRole bas_EmployeeRoleData = EmployeeRoleRepository.GetEmployeeRoleByEmployeeId(employee.Bem_Id);
                        bas_EmployeeRole.ber_Id = bas_EmployeeRoleData.ber_Id;
                        bas_EmployeeRole.Ber_EmployeeId = employee.Bem_Id;
                        bas_EmployeeRole.Ber_RoleId = bas_Role.Bro_Id;
                        result = EmployeeRoleRepository.Update(bas_EmployeeRole);
                    }
                    if (result)
                    {
                        scope.Complete();
                    }
                    else
                    {
                        RollbackTran();
                    }


                }
                catch (Exception ex)
                {
                    RollbackTran();
                    result = false;
                }
            }
            return result;
        }

        public IList<Bas_Employee> GetEmployees(IEnumerable<int> employeeIds)
        {
            return BllEmployeeRepository.GetEntities(employeeIds);
        }

        #endregion
    }
}
