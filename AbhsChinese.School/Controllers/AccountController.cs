using AbhsChinese.Bll.Account;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.School.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        AccountBll accountBll = new AccountBll();
        [HttpPost]
        public ActionResult CheckAccount(string phone, AccountVerifyTypeEnum type)
        {
            bool result = accountBll.CheckPhone(phone, type);
            var msg = result ? "校验成功" : "校验失败";
            return SimpleResult(result);
        }
    }
}