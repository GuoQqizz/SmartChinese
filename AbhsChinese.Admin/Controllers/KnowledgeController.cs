using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Knowledge;
using AbhsChinese.Admin.Models.Resource;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class KnowledgeController : BaseController
    {
        public ActionResult Add(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return View();
            }
            KnowledgeBll knowledgeBll = new KnowledgeBll();
            var viewModel = knowledgeBll.GetKnowledge(id.Value);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddKnowledge(DtoKnowledgeRequest inputModel)
        {
            try
            {
                KnowledgeBll knowledgeBll = new KnowledgeBll();
                //var knowledge = inputModel.ConvertTo<Yw_Knowledge>();
                if (inputModel.Ykl_Id > 0)
                {
                    inputModel.Ykl_Editor = CurrentUserID;
                    knowledgeBll.Update(inputModel);
                }
                else
                {
                    inputModel.Ykl_Creator = CurrentUserID;
                    inputModel.Ykl_Editor = CurrentUserID;
                    knowledgeBll.Add(inputModel);
                }
                return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "操作成功" });
            }
            catch(Exception ex)
            {
                return Json(new JsonSimpleResponse() { State = false, ErrorMsg = "请选择父知识点" });
            }
        }

        public ActionResult GetKnowledges(KnowledgeSearch search)
        {
            KnowledgeBll knowledgeBll = new KnowledgeBll();
            var list = knowledgeBll.GetPagingKnowledge(search.Pagination, search.Id, search.NameOrKey, search.KnowledgeType);
            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult KnowledgeTree(KnowledgeTree knowledgeTree)
        {
            return View(knowledgeTree);
        }

        public ActionResult Tree(KnowledgeTree knowledgeTree)
        {
            return View(knowledgeTree);
        }

        public ActionResult GetKnowledgeTree(KnowledgeTree knowledgeTree)
        {
            KnowledgeBll knowledgeBll = new KnowledgeBll();
            var tree = knowledgeBll.GetKnowledgeTree(knowledgeTree.ParentId, knowledgeTree.ChildId);
            List<TreeViewModel> trees = new List<TreeViewModel>();

            trees = tree.Where(l => l.Ykl_ParentId == 0).OrderBy(l => l.Ykl_Id)
                .Select(l => new TreeViewModel {
                    nodeid = l.Ykl_Id.ToString(),
                    text = l.Ykl_Name,
                    hasChildrenField=l.Ykl_IsLeaf,
                    nodes = GetChildren(tree,l.Ykl_Id)
                }).ToList();

            trees = GetTrees(trees);

            return Json(new { Data = trees }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LazyGet(int parentId)
        {
            KnowledgeBll knowledgeBll = new KnowledgeBll();
            var source = knowledgeBll.GetLazyTree(parentId);
            List<TreeViewModel> trees = new List<TreeViewModel>();

            foreach (var item in source)
            {
                TreeViewModel model = new TreeViewModel();
                model.nodeid = item.Ykl_Id.ToString();
                model.text = item.Ykl_Name;
                model.hasChildrenField = item.Ykl_IsLeaf;
                model.nodes = item.Ykl_IsLeaf ? new List<TreeViewModel>() : null;
                trees.Add(model);
            }

            trees = GetTrees(trees);
            return Json(new { Data = trees }, JsonRequestBehavior.AllowGet);
        }

        private List<TreeViewModel> GetTrees(List<TreeViewModel> treeList)
        {
            if (treeList != null)
            {
                foreach (var item in treeList)
                {
                    if (!item.hasChildrenField)
                    {
                        item.nodes = null;
                    }
                    else
                    {
                        GetTrees(item.nodes);
                    }
                }
            }
            return treeList;
        }

        private List<TreeViewModel> GetChildren(List<Yw_Knowledge> source, int parentId)
        {
            return source.Where(l => l.Ykl_ParentId == parentId).OrderBy(l => l.Ykl_Id)
                .Select(l => new TreeViewModel
                {
                    nodeid = l.Ykl_Id.ToString(),
                    text = l.Ykl_Name,
                    hasChildrenField = l.Ykl_IsLeaf,
                    nodes = GetChildren(source,l.Ykl_Id)
                }).ToList();
        }

        public ActionResult GetAudioAndVideo(string name,int pageNumber = 1, int pageSize = 10)
        {
            ResourceBll resourceBll = new ResourceBll();
            PagingObject paging = new PagingObject(pageNumber, pageSize);
            var list = resourceBll.GetAudioAndVideo(paging, name);
            SelectPageModel model = new SelectPageModel();
            List<Dictionary<string, object>> listDic = new List<Dictionary<string, object>>();
            foreach (var item in list)
            {
                var mediaType = CustomEnumHelper.Parse(typeof(MediaResourceTypeEnum), item.Ymr_MediaType);
                var option = new Dictionary<string, object>()
                {
                    {"name", "("+mediaType+")"+item.Ymr_Name},
                    {"id",item.Ymr_Id }
                };
                listDic.Add(option);
            }
            model.list = listDic;
            model.totalRow = paging.TotalCount;
            return Json(new { Data = model });
        }

        [HttpPost]
        public ActionResult GetParentNode(int? pageNumber, int? pageSize, string name, int? level, string searchValue)
        {
            SelectPageModel model = new SelectPageModel();
            List<Dictionary<string, object>> listDic = new List<Dictionary<string, object>>();
            KnowledgeBll knowledgeBll = new KnowledgeBll();
            if (!string.IsNullOrEmpty(searchValue))
            {
                var entity = knowledgeBll.Get(searchValue._ToInt32());
                if (entity != null)
                {
                    var option = new Dictionary<string, object>()
                    {
                        {"name", entity.Ykl_Name},
                        {"id",entity.Ykl_Id }
                    };
                    listDic.Add(option);
                    model.list = listDic;
                }
                return Json(new { Data = model });
            }

            PagingObject paging = new PagingObject(pageNumber.Value, pageSize.Value);
            var list = knowledgeBll.GetKnowledgeParentNode(paging, level.Value, name);

            foreach (var item in list)
            {
                var option = new Dictionary<string, object>()
                {
                    {"name", item.Ykl_Name},
                    {"id",item.Ykl_Id }
                };
                listDic.Add(option);
            }
            model.list = listDic;
            model.totalRow = paging.TotalCount;
            return Json(new { Data = model });
        }
    }
}