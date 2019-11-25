using Abhs.Service.Client;
using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Resource;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class ResourceController : BaseController
    {
        #region 文本资源

        public ActionResult AddAndUpdateTextResource(int id)
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModel = resourceBll.GetDtoTextResource(id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddTextResourceDo(DtoResourceRequest model)
        {
            try
            {
                ResourceBll resourceBll = new ResourceBll();
                if (model.Id == 0)
                {
                    model.Creator = CurrentUserID;
                    model.Editor = CurrentUserID;
                    resourceBll.AddTextResource(model);
                }
                else
                {
                    model.Editor = CurrentUserID;
                    resourceBll.UpdateTextResource(model);
                }
                return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "操作成功" });
            }
            catch (Exception ex)
            {
                return Json(new JsonSimpleResponse() { State = false, ErrorMsg = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetTextResources(ResourceSearch search)
        {
            ResourceBll resourceBll = new ResourceBll();
            var resourceList = resourceBll.GetPagingTextResource(search.Pagination, search.Id, search.NameOrKey, search.Grade,
                search.TextType);
            List<TextResourceViewModel> viewModels = resourceList.ConvertTo<List<TextResourceViewModel>>();
            var table = AbhsTableFactory.Create(viewModels, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TextResource()
        {
            return View();
        }

        #endregion 文本资源

        #region 多媒体资源

        public ActionResult AddImageResource(int? id)
        {
               ResourceBll resourceBll = new ResourceBll();
            var viewModel = resourceBll.GetMediaResource(id ?? 0);
            return View(viewModel);
        }

        public ActionResult AddAudioResource(int? id)
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModel = resourceBll.GetAudioMedia(id ?? 0);
            return View(viewModel);
        }

        public ActionResult AddVideoResource(int? id)
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModel = resourceBll.GetMediaResource(id ?? 0);
            return View(viewModel);
        }

        public ActionResult AddXiaoAiResource(int? id)
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModel = resourceBll.GetMediaResource(id ?? 0);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddMediaResourceDo(DtoResourceRequest model)
        {
            try
            {
                ResourceBll resourceBll = new ResourceBll();
                if (model.Id == 0)
                {
                    model.Creator = CurrentUserID;
                    model.Editor = CurrentUserID;
                    resourceBll.AddMediaResource(model);
                }
                else
                {
                    model.Editor = CurrentUserID;
                    resourceBll.UpdateMediaResource(model);
                }
                return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "操作成功" });
            }
            catch (Exception ex)
            {
                return Json(new JsonSimpleResponse() { State = false, ErrorMsg = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetPagingMediaResource(ResourceSearch search)
        {
            ResourceBll resourceBll = new ResourceBll();
            var resourceList = resourceBll.GetPagingMediaResource(search.Pagination, search.Id, search.NameOrKey, search.Grade,
                search.MediaType);
            List<MediaResourceViewModel> list = resourceList.ConvertTo<List<MediaResourceViewModel>>();
            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MediaResource()
        {
            return View();
        }
        
        public ActionResult ChooseImg()
        {
            return View();
        }

        public ActionResult GetImgList(PagingObject paging, int? grade, string nameOrkey)
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModel = resourceBll.GetImgList(paging,grade._ToInt32(),nameOrkey);
            return Json(new { Code = (int)ResourceTypeEnum.多媒体资源, Data = viewModel }, JsonRequestBehavior.AllowGet);
        }
        #endregion 多媒体资源


        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            var fileType = "";
            if (extension == ".jpg" || extension == ".png" || extension == ".gif" || extension == ".bmp")
            {
                fileType = "images";
            }
            else if (extension == ".mp4")
            {
                fileType = "video";
            }
            else if (extension == ".mp3")
            {
                fileType = "audio";
            }
            else
            {
                return Json(new JsonSimpleResponse() { State = false, ErrorMsg = "选择的文件类型有错误" });
            }
            FileManageClient client = new FileManageClient(ConfigurationManager.AppSettings["uploadUrl"]);
            var response = client.Upload(file.FileName, file.InputStream, ConfigurationManager.AppSettings["OssSubject"], $"SmartChinese/{fileType}", true);
            return Json(response);
        }
    }
}