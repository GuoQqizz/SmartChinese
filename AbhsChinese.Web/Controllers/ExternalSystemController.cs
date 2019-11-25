using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Web.Controllers
{
    /// <summary>
    /// 为第三方系统提供交互接口
    /// </summary>
    public class ExternalSystemController : Controller
    {
        /// <summary>
        /// 接收微信支付通知接口
        /// </summary>
        /// <returns>通知处理结果</returns>
        [AllowAnonymous]
        public ContentResult WechatPayNotify()
        {
            //接收从微信后台POST过来的数据
            System.IO.Stream s = Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();

            LogHelper.WriteLog("微信支付通知数据 : " + builder.ToString());
            StudentOrderBll studentOrderBll = new StudentOrderBll();
            string result = studentOrderBll.WechatOrderPayNotify(builder.ToString());
            LogHelper.WriteLog("微信支付通知处理结果 : " + result);
            return Content(result);
        }
    }
}