using AbhsChinese.Admin.Models.CashVoucher;
using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Knowledge;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class CashVoucherController : BaseController
    {
        public ActionResult CashVoucherList()
        {
            return View();
        }

        public ActionResult GetCashVoucheres(CashVoucherSearch search)
        {
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var viewModels = cashVoucherBll.GetPagingCashVoucher(search.Pagination, search.Id, search.Name, search.Status, search.CashType);
            var table = AbhsTableFactory.Create(viewModels, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 全场券
        /// </summary>
        /// <returns></returns>
        public ActionResult AllVoucher(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return View();
            }
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var voucher = cashVoucherBll.GetVoucherById(id.Value);
            return View(voucher);
        }

        /// <summary>
        /// 成单券
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderVoucher(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return View();
            }
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var voucher = cashVoucherBll.GetVoucherById(id.Value);
            return View(voucher);
        }
        
        /// <summary>
        /// 注册券
        /// </summary>
        /// <returns></returns>
        public ActionResult RegisterVoucher(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return View();
            }
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var voucher = cashVoucherBll.GetVoucherById(id.Value);
            return View(voucher);
        }

        [HttpPost]
        public ActionResult AddVoucher(CashVoucherInputModel inputModel)
        {
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var request = inputModel.ConvertTo<DtoCashVoucherRequest>();
            var result = 0;
            if (inputModel.Id>0)
            {
                cashVoucherBll.Update(request);
            }
            else
            {
                request.Creator = CurrentUserID;
                request.Editor = CurrentUserID;
                result = cashVoucherBll.Add(request);
            }
            return Json(new SuccessJsonResponse(result));
        }

        /// <summary>
        /// 查看—现金券明细
        /// </summary>
        /// <returns></returns>
        public ActionResult CashVoucherDetails(int? cashVoucherId)
        {
            if(cashVoucherId==null || cashVoucherId.Value==0)
            {
                return View();
            }
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var viewModel = cashVoucherBll.GetCashVoucherDetail(cashVoucherId.Value);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetStudentCashVoucheres(CahsVoucherDetailsSearch search)
        {
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var viewModels = cashVoucherBll.GetPagingStudentCashVoucher(search.Pagination, search.CashVoucherId, search.Status, search.UsedReferNo);
            var table = AbhsTableFactory.Create(viewModels, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateStatus(int id,int status)
        {
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var result = cashVoucherBll.UpdateStatus(id, status);
            return Json(new SuccessJsonResponse(result));
        }

        public ActionResult GetSchool(int pageNumber, int pageSize, string name)
        {
            SchoolBll schoolBll = new SchoolBll();
            PagingObject paging = new PagingObject(pageNumber, pageSize);
            var schools = schoolBll.GetSchoolByIdOrName(paging, name);
            SelectPageModel model = new SelectPageModel();
            List<Dictionary<string, object>> listDic = new List<Dictionary<string, object>>();
            foreach (var item in schools)
            {
                var option = new Dictionary<string, object>()
                {
                    {"name", item.value},
                    {"id",item.key }
                };
                listDic.Add(option);
            }
            model.list = listDic;
            model.totalRow = paging.TotalCount;
            return Json(new { Data = model });
            //var schools = schoolBll.GetSchoolByIdOrName(paging,perName).ToDictionary(k => k.key, v => v.value);
            //var result = OptionFactory.CreateOptions(schools);
            //return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCourses(int pageNumber, int pageSize, string name)
        {
            CourseBll courseBll = new CourseBll();
            PagingObject paging = new PagingObject(pageNumber, pageSize);
            var courses = courseBll.GetPagingCourseByIdOrName(paging,name);
            SelectPageModel model = new SelectPageModel();
            List<Dictionary<string, object>> listDic = new List<Dictionary<string, object>>();
            foreach (var item in courses)
            {
                var option = new Dictionary<string, object>()
                {
                    {"name", item.value},
                    {"id",item.key }
                };
                listDic.Add(option);
            }
            model.list = listDic;
            model.totalRow = paging.TotalCount;
            return Json(new { Data = model });
        }

        protected JsonResult Select2(object data, JsonRequestBehavior behavior)
        {
            return Json(new { results = data }, null, null, behavior);
        }
    }
}