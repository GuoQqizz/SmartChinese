using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Filter
{
    public class CrossDomainAttribute : ActionFilterAttribute
    {
        

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
                var res = filterContext.HttpContext.Response;
                res.Headers.Add("Access-Control-Allow-Origin", "*");
                res.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                res.Headers.Add("Access-Control-Allow-Headers", "x-requested-with");
                base.OnActionExecuting(filterContext);
          
            
        }
    }
}