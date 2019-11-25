using Abhs.Service.Client;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.School.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.School.Controllers
{
    public class FileManageController : Controller
    {
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            //string url = SaveFile(file);

            //return Json(new { url = url });

            var fileTypeStr = "";
            var type = FileTypeEnum.UnKnow;
            try
            {
                type = FileHelper.CheckFile(file);
                if (type != FileTypeEnum.UnKnow)
                {
                    fileTypeStr = type.ToString();
                }
                else
                {
                    return Json(new JsonSimpleResponse() { State = false, ErrorMsg = "选择的文件类型有错误" });
                }
                string directory = $"SchoolMgr/{fileTypeStr}";
                FileManageClient client = new FileManageClient(ConfigurationManager.AppSettings["uploadUrl"]);
                var response = client.Upload(file.FileName, file.InputStream, ConfigurationManager.AppSettings["OssSubject"], directory, true);
                if (response.Code == Abhs.Service.Client.Results.FileManageStatusCode.Success)
                {
                    response.FileUrl = $"/{directory}/{response.FileName}";
                }
                return Json(response);
            }
            catch (Exception ex)
            {
                var msg = "服务器异常";
                if (ex is AbhsException)
                {
                    msg = ex.Message;
                }
                return Json(new JsonSimpleResponse() { State = false, ErrorMsg = msg });
            }

        }

    }
}