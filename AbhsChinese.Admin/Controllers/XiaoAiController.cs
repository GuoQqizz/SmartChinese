using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class XiaoAiController : BaseController
    {
        public ActionResult XiaoAiList()
        {
            ResourceBll resourceBll = new ResourceBll();
            var viewModels = resourceBll.GetXiaoAiBianOrPrologue(new PagingObject(1,50),(int)MediaResourceTypeEnum.小艾变);
            return View(viewModels);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult AddXiaoAiDo(DtoResourceRequest model)
        {
            ResourceBll resourceBll = new ResourceBll();
            if (model.Id == 0)
            {
                model.State = 1;
                model.IsStatus = true;
                model.Creator = CurrentUserID;
                model.Editor = CurrentUserID;
                resourceBll.AddMediaResource(model);
                return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "添加成功" });
            }
            else
            {
                model.Editor = CurrentUserID;
                resourceBll.UpdateMediaResource(model);
                return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "修改成功" });
            }
        }
    }
}