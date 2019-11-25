using Abhs.Service.Client;
using Abhs.Service.Client.Results;
using AbhsChinese.Admin.Helpers;
using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Resource;
using AbhsChinese.Bll;
using AbhsChinese.Bll.BaiduApi;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class PrologueController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetPrologueList(int pageIndex,int pageSize)
        {
            ResourceBll resourceBll = new ResourceBll();
            PagingObject paging = new PagingObject(pageIndex, pageSize);
            var viewModels = resourceBll.GetXiaoAiBianOrPrologue(paging, (int)MediaResourceTypeEnum.开场语);
            var table = AbhsTableFactory.Create(viewModels, paging.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPrologues(ResourceSearch search)
        {
            ResourceBll resourceBll = new ResourceBll();
            var resourceList = resourceBll.GetPagingMediaResource(search.Pagination, search.Id, search.NameOrKey, search.Grade,
                search.MediaType);
            List<MediaResourceViewModel> list = resourceList.ConvertTo<List<MediaResourceViewModel>>();
            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddPrologue(int? id)
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModel = resourceBll.GetMediaResource(id ?? 0);
            return View(viewModel);
        }

        public ActionResult AddPrologueDo(int Id,string description)
        {
            ResourceBll resourceBll = new ResourceBll();
            if (Id>0)
            {
                var audio = BaiduApiBll.GetAudio(description);
                var response = Upload(audio);
                if (response.State)
                {
                    resourceBll.UpdatePropogue(Id, description,response.FileUrl);
                    return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "操作成功" });
                }
            }
            else
            {
                var audio = BaiduApiBll.GetAudio(description);
                var response = Upload(audio);
                if (response.State)
                {
                    resourceBll.AddMediaResource(new DtoResourceRequest { Description = description, Url = response.FileUrl, MediaType = MediaResourceTypeEnum.开场语, MediaObjectType = MediaObjectTypeEnum.音频, State = 1, IsStatus = true });
                    return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "操作成功" });
                }
            }
            return Json(new JsonSimpleResponse() { State = false, ErrorMsg = "操作失败" });
        }

        private FileUploadResult Upload(byte[] audio)
        {
            var fileType = FileTypeEnum.Audio.ToString();
            var fileName = Guid.NewGuid().ToString("N") + ".mp3";
            string directory = $"Resource/{fileType}";
            Stream stream = new MemoryStream(audio);
            FileManageClient client = new FileManageClient(ConfigurationManager.AppSettings["uploadUrl"]);
            var response = client.Upload(fileName, stream, ConfigurationManager.AppSettings["OssSubject"], $"{directory}", true);
            if (response.Code == FileManageStatusCode.Success)
            {
                response.FileUrl = $"/{directory}/{response.FileName}";
            }
            return response;
        }
    }
}