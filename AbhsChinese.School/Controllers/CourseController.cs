using AbhsChinese.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.School.Controllers
{
    public class CourseController : BaseController
    {
        // GET: Course

        public ActionResult GetPrice(int courseId)
        {

            CourseBll bll = new CourseBll();
            decimal price = 0;
            var priceModel = bll.GetCoursePrice(courseId, CurrentUser.School.Bsl_Level);
            if (priceModel != null)
            {
                price = priceModel.Yce_Price;
            }
            return Json(price, JsonRequestBehavior.AllowGet);
        }
    }
}