using AbhsChinese.Bll;
using AbhsChinese.Bll.Account;
using AbhsChinese.Code.Common;
using AbhsChinese.Code.Common.SMS;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Domain.Enum;
using AbhsChinese.School.Helpers;
using AbhsChinese.School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.School.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string redirect = "")
        {
            //ViewBag.Redirect = redirect;
            return View(redirect);
        }

        [HttpPost]
        public ActionResult Login(string account, string pwd)
        {
            bool success = false;
            string msg = "";
            SchoolBll schoolBll = new SchoolBll();
            SchoolTeacherBll schoolTeacherBll = new SchoolTeacherBll();

            DtoSchoolTeacher schoolTeacher = schoolTeacherBll.Login(account, pwd);
            DtoSchool school = null;
            if (schoolTeacher != null && schoolTeacher.Yoh_Status == (int)StatusEnum.有效)
            {
                school = schoolBll.GetSchoolDto(schoolTeacher.Yoh_SchoolId);
                if (schoolTeacher != null && school != null && school.Bsl_Status != (int)SchoolStatusEnum.合同到期 && school.Bsl_IsValid)
                {
                    CookieUserModel user = new CookieUserModel();
                    user.Teacher = schoolTeacher.ConvertTo<CookieTeacher>(); ;
                    user.School = school.ConvertTo<CookieSchool>();
                    LoginCookieHelper.SetCurrentUser(user);
                    success = true;
                    msg = "登录成功";
                }
                else
                {
                    msg = "当前校区不允许登录";
                }
            }
            else
            {
                msg = "登录失败";
            }
            if (!success)
            {
                LoginCookieHelper.SetCurrentUser(null);
            }

            return Json(new JsonSimpleResponse() { State = success, ErrorMsg = msg });
        }


        public ActionResult SignOut()
        {
            LoginCookieHelper.SetCurrentUser(null);
            return RedirectToAction("/Index");
        }

        [HttpGet]
        public ActionResult FindPwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FindPwd(string phone, string pwd, string repwd, string code)
        {
            AccountBll accountBll = new AccountBll();
            SchoolTeacherBll schoolTeacherBll = new SchoolTeacherBll();
            bool success = false;
            string msg = "";
            bool checkCode = false;
            bool checkPhone = false;
            bool checkPwd = false;
            DtoSchoolTeacher teacher = null;
            try
            {
                checkCode = SmsCookie.GetSmsCode.Check(phone, code);
            }
            catch (Exception)
            {
                checkCode = false;
            }
            msg = checkCode ? "" : "验证码错误";
            if (checkCode)
            {
                teacher = schoolTeacherBll.GetSchoolTeacherByPhone(phone);
                if (teacher != null)
                {
                    checkPhone = true;
                    checkPwd = !string.IsNullOrEmpty(pwd) && pwd == repwd;
                    msg = checkPwd ? "" : "密码错误";
                }
                else
                {
                    msg = "账号不存在";
                    checkPhone = false;
                }
            }
            if (checkCode && checkPhone && checkPwd)
            {
                success = schoolTeacherBll.UpdatePwd(teacher.Yoh_Id, Encrypt.GetMD5Pwd(pwd));
                msg = success ? "修改成功" : "修改失败";
            }
            return Json(new JsonSimpleResponse() { State = success, ErrorMsg = msg });

        }
        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(VerifyCode.GetVerifyCode(), @"image/Gif");
        }
    }
}