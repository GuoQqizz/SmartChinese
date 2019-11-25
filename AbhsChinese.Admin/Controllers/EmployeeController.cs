using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AbhsChinese.Bll;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Code.Common;
using AbhsChinese.Admin.Models.Employee;
using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Code.Extension;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.AbhsResource;

namespace AbhsChinese.Admin.Controllers
{
    public class EmployeeController : BaseController
    {
        EmployeeBll employeeBll = new EmployeeBll();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EditEmployee(int bem_id)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.RuleSource = employeeBll.GetRoleList().Select(s =>
            {
                return new DtoKeyValue<string, string>() { key = s.Bro_code, value = s.Bro_Name };
            }).ToList();

            if (bem_id > 0)
            {
                var model = employeeBll.GetEmployeeEntity(bem_id);

                #region
                if (model != null)
                {
                    employeeViewModel.Id = model.Bem_Id;
                    employeeViewModel.Account = model.Bem_Account;
                    employeeViewModel.Password = model.Bem_Password;
                    employeeViewModel.Name = model.Bem_Name;
                    employeeViewModel.Sex = model.Bem_Sex;
                    employeeViewModel.Phone = model.Bem_Phone;
                    employeeViewModel.Email = model.Bem_Email;
                    employeeViewModel.Roles = model.Bem_Roles;
                    employeeViewModel.Status = model.Bem_Status;
                    employeeViewModel.Remark = model.Bem_Remark;
                    employeeViewModel.TotalGrades = model.Bem_Grades;
                }

                #endregion


            }
            return View(employeeViewModel);
        }
        [HttpPost]
        public ActionResult AddOrUpdateEmployee(EmployeeViewModel employeeViewModel)
        {
            bool result = false;
            var msg = "";
            try
            {
                #region Bas_Employee
                Bas_Employee bas_Employee = null;
                if (employeeViewModel.Id > 0)
                {
                    bas_Employee = employeeBll.GetEmployeeEntity(employeeViewModel.Id);
                    if (bas_Employee == null)
                    {
                        throw new AbhsException(ErrorCodeEnum.ParameterInvalid, AbhsErrorMsg.ConstParameterInvalid);
                    }
                }
                if (bas_Employee == null)
                {
                    bas_Employee = new Bas_Employee();
                }
                bas_Employee.Bem_Id = employeeViewModel.Id;
                bas_Employee.Bem_Account = employeeViewModel.Account;
                if (employeeViewModel.Password.HasValue())
                {
                    bas_Employee.Bem_Password = Encrypt.GetMD5Pwd(employeeViewModel.Password);
                }
                bas_Employee.Bem_Name = employeeViewModel.Name;
                bas_Employee.Bem_Sex = employeeViewModel.Sex;
                bas_Employee.Bem_Phone = employeeViewModel.Phone;
                bas_Employee.Bem_Email = employeeViewModel.Email;
                bas_Employee.Bem_Roles = employeeViewModel.Roles;
                bas_Employee.Bem_Status = employeeViewModel.Status;
                bas_Employee.Bem_Remark = employeeViewModel.Remark;
                bas_Employee.Bem_Grades = employeeViewModel.Grades.Sum();
                #endregion

                Bas_EmployeeRole bas_EmployeeRole = new Bas_EmployeeRole();
                if (bas_Employee.Bem_Id == 0)
                {
                    bas_Employee.Bem_CreateTime = DateTime.Now;
                    bas_Employee.Bem_Editor = CurrentUserID;
                    bas_Employee.Bem_LastLoginTime = DateTimeExtensions.DefaultDateTime;
                    bas_EmployeeRole.Ber_CreateTime = DateTime.Now;
                    bas_EmployeeRole.Ber_Creator = CurrentUserID;
                    result = employeeBll.AddEmployee(bas_Employee, bas_EmployeeRole);
                }
                else
                {
                    bas_Employee.Bem_UpdateTime = DateTime.Now;
                    bas_Employee.Bem_Editor = CurrentUserID;
                    bas_EmployeeRole.Ber_CreateTime = DateTime.Now;
                    bas_EmployeeRole.Ber_Creator = CurrentUserID;
                    result = employeeBll.UpdateEmployee(bas_Employee, bas_EmployeeRole);
                }
            }
            catch (Exception ex)
            {
                msg = "异常，请重试";
                //return Json(new JsonSimpleResponse() { State = false, ErrorMsg = ex.Message });
            }
            msg = result ? "操作成功" : "操作失败";
            return Json(new JsonSimpleResponse() { State = result, ErrorMsg = msg });
        }
        [HttpGet]
        public ActionResult GetEmployees(EmployeeSearch employeeSearch)
        {
            try
            {
                List<DtoEmployee> employeeList = employeeBll.GetPagingEmployee(employeeSearch.Pagination, employeeSearch.AccountOrNameOrPhone, employeeSearch.Role, employeeSearch.Status);
                var table = AbhsTableFactory.Create(employeeList, employeeSearch.Pagination.TotalCount);
                return Json(table, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonSimpleResponse() { State = false, ErrorMsg = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult CheckUniqueAccount(string accont)
        {
            try
            {
                var count = employeeBll.CheckUniqueAccount(accont);
                var result = count == 0;
                return Json(new JsonSimpleResponse() { State = result, ErrorMsg = count.ToString() });
            }
            catch (Exception ex)
            {
                return Json(new JsonSimpleResponse() { State = false, ErrorMsg = ex.Message });
            }
        }



    }
}