﻿using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}