using AbhsChinese.Admin.Models.Advertising;
using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class AdvertisingController : BaseController
    {
        // GET: Advertising
        public ActionResult Index()
        {
            AdvertisingSearch search = new AdvertisingSearch();
            return View(search);
        }

        public ActionResult History(int badPosId)
        {
            AdvertisingSearch search = new AdvertisingSearch();
            search.AdvStatus = 0;
            search.AdvPosId = badPosId;
            return View(search);
        }
        public ActionResult Edit(int badPosId, int badId)
        {
            AdvertingInputModel viewModel;
            Bas_Advertising dbModel = null;
            if (badId > 0)
            {
                dbModel = new AdvertisingBll().GetAdvertisingById(badId);
            }
            if (dbModel == null)
            {
                viewModel = new AdvertingInputModel(badPosId);
                viewModel.Bad_Url = "";
            }
            else
            {
                viewModel = new AdvertingInputModel(dbModel);
            }
            var bapModel = new AdvertisingBll().GetAdvertisingPosById(badPosId);
            if (bapModel != null)
            {
                viewModel.Bap_Code = bapModel.Bap_Code;
            }
            return View(viewModel);
        }


        /// <summary>
        /// 获取推荐楼层信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPagingAdvertisingPos(AdvertisingSearch search)
        {
            var list = new AdvertisingBll().GetPagingAdvertisingPos(search.Pagination, search.AdvPosType, search.AdvStatus);
            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取推荐历史记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPagingAdvertisingHistory(AdvertisingSearch search)
        {
            var list = new AdvertisingBll().GetPagingAdvertisingHistory(search.Pagination, search.AdvPosId);
            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 保存推荐位推荐
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveAdertising(AdvertingInputModel model)
        {
            var bll = new AdvertisingBll();
            Bas_Advertising dbModel = null;
            if (model.Bad_Id > 0)
            {
                dbModel = bll.GetAdvertisingById(model.Bad_Id);
                var modelByPos = bll.GetAdvertisingByPosId(model.Bad_PosId);
                if (dbModel == null || modelByPos == null || modelByPos.Bad_Id != dbModel.Bad_Id)
                {
                    throw new AbhsException(ErrorCodeEnum.ParameterInvalid, AbhsErrorMsg.ConstParameterInvalid);
                }
            }
            //dbModel = model.ToAdvertisingDbModel();
            //dbModel.Bad_Creator = CurrentUserID;
            dbModel = model.ConvertTo<Bas_Advertising>(dbModel);
            bool success = bll.SaveAdertising(dbModel);
            var msg = success ? "保存成功" : "保存失败";
            return Json(new JsonSimpleResponse() { State = success, ErrorMsg = msg });
        }
        /// <summary>
        /// 修改推荐位楼层状态
        /// </summary>
        /// <param name="advPosId"></param>
        /// <param name="toStatus"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditAdertisingPosStatus(int advPosId, int toStatus)
        {
            bool success = new AdvertisingBll().EditAdertisingPosStatus(advPosId, toStatus, CurrentUserID);
            var msg = success ? "保存成功" : "保存失败";
            return Json(new JsonSimpleResponse() { State = success, ErrorMsg = msg });
        }
        /// <summary>
        /// 根据ID,名称搜索课程名称（后期迁移走？）
        /// </summary>
        /// <param name="idOrName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCourse(string idOrName)
        {
            var dic = new CourseBll().GetCourseByIdOrName(idOrName);
            return Json(dic, JsonRequestBehavior.AllowGet);

        }
    }
}