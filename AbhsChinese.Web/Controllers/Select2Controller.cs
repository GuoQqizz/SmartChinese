using AbhsChinese.Bll;
using AbhsChinese.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Web.Controllers
{
    public class Select2Controller : BaseController
    {
        public ActionResult GetRegion(int parentId = 0)
        {
            RegionBll regionBll = new RegionBll();
            var list = regionBll.GetRegionList(parentId).ToDictionary(k => { return k.Reg_ID; }, v => { return v.Reg_Name; });
            var result = OptionFactory.CreateOptions(list);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }
        protected JsonResult Select2(
            object data,
            JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { results = data }, null, null, behavior);
        }
    }
}