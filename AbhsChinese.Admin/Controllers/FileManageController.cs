using Abhs.Service.Client;
using AbhsChinese.Admin.Helpers;
using AbhsChinese.Admin.Models;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{

    public class FileManageController : Controller
    {


        [HttpPost]
        public ActionResult UEditorUpload(HttpPostedFileBase file)
        {
            string url = SaveFile(file);

            UEditorUploadResponse response = new UEditorUploadResponse
            {
                State = "SUCCESS",
                Url = url,
                OriginalName = file.FileName,
                Size = file.ContentLength
            };

            return Content(JsonConvert.SerializeObject(response));
        }
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
                string directory = $"SmartChinese/{fileTypeStr}";
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

        [HttpPost]
        public ActionResult UploadBase64(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return HttpNotFound();
            }

            byte[] buffer = Convert.FromBase64String(content);
            MemoryStream ms = new MemoryStream(buffer);
            var path = Server.MapPath("~/Files");
            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var fullName = Path.Combine(path, fileName);
            using (Bitmap bmp = new Bitmap(ms))
            {
                bmp.Save(fullName, ImageFormat.Jpeg);
            }
            return Json("../../Files/" + fileName);
        }

        private string SaveFile(HttpPostedFileBase file)
        {
            var path = Server.MapPath("~/Files");

            file.SaveAs(path + "\\" + file.FileName);
            return "../../Files/" + file.FileName;
        }

        /// <summary>
        /// 为html2canvas提供代理
        /// </summary>
        /// <param name="url"></param>
        /// <param name="responseType"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<ActionResult> GetUrlFile(string url, string responseType)
        {
            HttpClient hc = new HttpClient();
            var res = hc.GetStreamAsync(url);
            await res;
            return new FileStreamResult(res.Result, responseType);
        }
    }
}