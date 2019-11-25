using Abhs.Service.Client;
using Abhs.Service.Client.Results;
using AbhsChinese.Admin.Helpers;
using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Resource;
using AbhsChinese.Admin.Models.ResourceGroup;
using AbhsChinese.Bll;
using AbhsChinese.Bll.BaiduApi;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class ResourceGroupController : BaseController
    {
        #region Index
        [HttpGet]
        public ActionResult GetResourceGroups(ResourceSearch search)
        {
            ResourceBll resourceBll = new ResourceBll();
            var resourceList = resourceBll.GetPagingResourceGroup(search.Pagination, search.Id, search.Name, search.Grade,
                search.GroupType);
            var viewModels = resourceList.ConvertTo<List<ResourceGroupViewModel>>();
            var table = AbhsTableFactory.Create(viewModels, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AddResourceGroup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddResourceGroupDo(ResourceGroupInputModel inputModel)
        {
            ResourceBll resourceBll = new ResourceBll();
            var resourceGroup = inputModel.ConvertTo<Yw_ResourceGroup>();
            resourceGroup.Yrg_Creator = CurrentUserID;
            resourceGroup.Yrg_CreateTime = DateTime.Now;
            resourceBll.AddResourceGroup(resourceGroup);
            return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "操作成功" });
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id, ResourceTypeEnum active =ResourceTypeEnum.多媒体资源)
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModel = resourceBll.GetResourceGroup(id);
            var model = viewModel.ConvertTo<ResourceGroupEditViewModel>();
            model.Active = active;
            return View(model);
        }

        public ActionResult GetResourceGroupItem(ResourceGroupItemSearch search)
        {
            ResourceBll resourceBll = new ResourceBll();
            PagingObject paging = new PagingObject(search.PageIndex,search.PageSize);
            var viewModels = resourceBll.GetResourceGroupItem(paging, search.Id, search.ResourceType);
            var table = AbhsTableFactory.Create(viewModels, paging.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(int resourceId, int resourceType, int groupId)
        {
            ResourceBll resourceBll = new ResourceBll();
            resourceBll.Delete(groupId, resourceType, resourceId);
            return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "移除成功" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateStatus(int id, int status)
        {
            ResourceBll resourceBll = new ResourceBll();
            resourceBll.UpdateStatus(id, status);
            return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "修改成功" });
        }
        #endregion

        #region Detail
        [HttpGet]
        public ActionResult GetResourceList(PagingObject paging, int? grade, int? type, string nameOrkey, int resourceType)
        {
            ResourceBll resourceBll = new ResourceBll();
            if (resourceType == (int)ResourceTypeEnum.文本资源)
            {
                var viewModel = resourceBll.GetTextList(paging, grade._ToInt32(), type._ToInt32(), nameOrkey);
                return Json(new { Code = (int)ResourceTypeEnum.文本资源, Data = viewModel }, JsonRequestBehavior.AllowGet);
            }
            else if (resourceType == (int)ResourceTypeEnum.多媒体资源)
            {
                var viewModel = resourceBll.GetMediaList(paging, grade._ToInt32(), type._ToInt32(), nameOrkey);
                return Json(new { Code = (int)ResourceTypeEnum.多媒体资源, Data = viewModel }, JsonRequestBehavior.AllowGet);
            }
            else if (resourceType == (int)ResourceTypeEnum.题目)
            {
                var viewModel = resourceBll.GetSubjectList(paging, grade._ToInt32(), type._ToInt32(), nameOrkey);
                return Json(new { Code = (int)ResourceTypeEnum.题目, Data = viewModel }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Code = 0 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int id, int resourceType)
        {
            ResourceBll resourceBll = new ResourceBll();
            SubjectBll subjectBll = new SubjectBll();
            if (resourceType == (int)ResourceTypeEnum.文本资源)
            {
                var detail = resourceBll.GetDtoTextResource(id);
                return Json(new JsonResponse<DtoTextResource>() { Data = detail, ErrorCode = (int)ResourceTypeEnum.文本资源 }, JsonRequestBehavior.AllowGet);
            }
            else if (resourceType == (int)ResourceTypeEnum.多媒体资源)
            {
                var detail = resourceBll.GetMediaResourceGroup(id);
                return Json(new JsonResponse<DtoMediaResourceToCourse>() { Data = detail, ErrorCode = (int)ResourceTypeEnum.多媒体资源 }, JsonRequestBehavior.AllowGet);
            }
            else if (resourceType == (int)ResourceTypeEnum.题目)
            {
                var detail = subjectBll.GetSubject(id);
                return Json(new JsonResponse<Yw_Subject>() { Data = detail, ErrorCode = (int)ResourceTypeEnum.题目 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonResponse<Yw_TextResource>() { ErrorCode = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddResourceGroupItemDo(ResourceGroupItemInputModel inputModel)
        {
            try
            {
                ResourceBll resourceBll = new ResourceBll();
                var groupItem = inputModel.ConvertTo<Yw_ResourceGroupItem>();
                groupItem.Ygi_CreateTime = DateTime.Now;
                groupItem.Ygi_Creator = CurrentUserID;
                groupItem.Ygi_UpdateTime = DateTime.Now;
                groupItem.Ygi_Editor = CurrentUserID;
                resourceBll.AddResourceGroupItem(groupItem);
                return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "添加成功" });
            }
            catch(Exception ex)
            {
                return Json(new JsonSimpleResponse() { State = false, ErrorMsg = ex.Message });
            }
        }

        public ActionResult ShowMedia(int groupId)
        {
            return View(groupId);
        }

        public ActionResult ShowText(int groupId)
        {
            return View(groupId);
        }

        public ActionResult ShowSubject(int groupId)
        {
            return View(groupId);
        }
        #endregion

        #region 查看
        public ActionResult SubjectView(ResourceIdAndType resource)
        {
            return View(resource);
        }

        public ActionResult TextView(int id)
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModel = resourceBll.GetDtoTextResource(id);
            return View(viewModel);
        }

        public ActionResult MediaView(int id)
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModel = resourceBll.GetAudioMedia(id);
            return View(viewModel);
        }
        #endregion

        #region 课程制作
        /// <summary>
        /// 多媒体资源列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult GetMediaToCourse(ResourceToCourseSearch search)
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModels = resourceBll.GetMediaToCourse(search.Pagination, search.CourseId, search.MediaType, search.NameOrKey);
            var table = AbhsTableFactory.Create(viewModels, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 多媒体资源查看
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetMediaDetailToCourse(int id)
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModel = resourceBll.GetMediaDetailToCourse(id);
            return Json(new JsonResponse<DtoMediaResourceToCourse>() { Data = viewModel });
        }

        /// <summary>
        /// 文本资源列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult GetTextToCourse(ResourceToCourseSearch search)
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModels = resourceBll.GetTextToCourse(search.Pagination, search.CourseId, search.TextType, search.NameOrKey);
            var table = AbhsTableFactory.Create(viewModels, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 多媒体资源列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult GetSubjectToCourse(ResourceToCourseSearch search)
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModels = resourceBll.GetSubjectToCourse(search.Pagination, search.CourseId, search.SubjectType, search.NameOrKey);
            var table = AbhsTableFactory.Create(viewModels, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 文本资源查看
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetTextDetailToCourse(int id)
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModel = resourceBll.GetTextDetailToCourse(id);
            return Json(new JsonResponse<DtoMediaResourceToCourse>() { Data = viewModel });
        }

        /// <summary>
        /// 查询开场白
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public ActionResult GetPrologues(string description)
        {
            ResourceBll resourceBll = new ResourceBll();
            var prologues = resourceBll.GetPrologues(description);
            return Json(new JsonResponse<List<DtoMediaResourceToCourse>>() { Data = prologues });
        }

        /// <summary>
        /// 自定义开场白添加
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public ActionResult AddPrologues(string description)
        {
            ResourceBll resourceBll = new ResourceBll();
            var audio = BaiduApiBll.GetAudio(description);
            var response = Upload(audio);
            if (response.State)
            {
                var id = resourceBll.AddMediaObject((int)MediaObjectTypeEnum.音频, response.FileUrl, 0, 0, description, CurrentUserID);
                return Json(new JsonResponse<int>() { State = true, Data = id, ErrorMsg = "操作成功" });
            }
            return Json(new JsonResponse<int>() { State = false, Data = 0, ErrorMsg = "操作失败" });
        }

        /// <summary>
        /// 查询开场白
        /// </summary>
        /// <param name="prologueId"></param>
        /// <returns></returns>
        public ActionResult GetPrologueById(int prologueId)
        {
            ResourceBll resourceBll = new ResourceBll();
            var prologue = resourceBll.GetPrologueById(prologueId);
            return Json(new JsonResponse<DtoMediaResourceToCourse>() { Data = prologue });
        }

        /// <summary>
        /// 文本对象添加
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]//不做html验证
        public ActionResult AddTextObject(string content)
        {
            ResourceBll resourceBll = new ResourceBll();
            var result = resourceBll.AddTextObject(content, 1, CurrentUserID);
            if (result > 0)
            {
                return Json(new JsonResponse<int>() { State = true, Data = result, ErrorMsg = "操作成功" });
            }
            return Json(new JsonResponse<int>() { State = false, Data = 0, ErrorMsg = "操作失败" });
        }
        #endregion
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