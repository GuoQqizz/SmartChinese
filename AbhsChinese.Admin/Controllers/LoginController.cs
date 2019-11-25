using AbhsChinese.Admin.Helpers;
using AbhsChinese.Admin.Models;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string redirect = "")
        {
            ViewBag.Redirect = redirect;
            return View(redirect);
        }

        [HttpPost]
        public ActionResult Login(string account, string pwd)
        {
            bool success = false;
            string msg = "";

            EmployeeBll employeeBll = new EmployeeBll();

            DtoEmployee employee = employeeBll.Login(account, pwd);
            CookieUserModel user = null;
            if (employee != null)
            {
                success = true;
                msg = "登录成功";
                user = new CookieUserModel();
                user.UserId = employee.Bem_Id;
                user.UserName = employee.Bem_Name;
                user.RoleId = employee.Bro_Id;
                user.RoleName = employee.Bro_Name;
                user.Grades = employee.Bem_Grades;
                user.GradesList = CustomEnumHelper.ParseBinaryAnd(typeof(GradeEnum), employee.Bem_Grades).Keys.ToList();
            }
            else
            {
                msg = "登录失败";
            }
            LoginCookieHelper.SetCurrentUser(user);

            return Json(new JsonSimpleResponse() { State = success, ErrorMsg = msg });
        }


        public ActionResult SignOut()
        {
            LoginCookieHelper.SetCurrentUser(null);
            return RedirectToAction("/Index");
        }

        public ActionResult FindPwd()
        {
            return View();
        }
    }
}