using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Enum;
using AbhsChinese.School.Models.CashVoucher;
using AbhsChinese.School.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.School.Controllers
{
    public class CashVoucherController : BaseController
    {
        // GET: CashVoucher
        public ActionResult Index()
        {
            CashVoucherSearch search = new CashVoucherSearch();
            search.SchoolId = CurrentUser.Teacher.Yoh_SchoolId;
            return View(search);
        }
        public ActionResult GetCashVoucheres(CashVoucherSearch search)
        {
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var viewModels = cashVoucherBll.GetPagingCashVoucherForSchool(search.Pagination, search.Id, search.Name, search.Status, search.SchoolId);
            var table = AbhsTableFactory.Create(viewModels, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id > 0)//禁止编辑
            {
                return RedirectToAction("Index");
            }
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            CourseBll bll = new CourseBll();
            DtoCashVoucher voucher = new DtoCashVoucher();
            decimal CashVoucherLimit = -1;
            if (id > 0)
            {
                voucher = cashVoucherBll.GetVoucherById(id);
                if (voucher != null)
                {
                    var priceModel = bll.GetCoursePrice(voucher.Ycv_CourseId, CurrentUser.School.Bsl_Level);
                    if (priceModel != null)
                    {
                        CashVoucherLimit = Math.Floor(priceModel.Yce_Price * (100 - CurrentUser.School.Bhl_DividePercent) / 100);
                    }
                }
            }
            ViewBag.CashVoucherLimit = CashVoucherLimit;
            ViewBag.DividePercent = CurrentUser.School.Bhl_DividePercent;
            return View(voucher);
        }

        [HttpPost]
        public ActionResult Save(CashVoucherInputModel inputModel)
        {
            inputModel.SchoolId = CurrentUser.Teacher.Yoh_SchoolId;
            inputModel.SchoolType = 1;
            inputModel.ExpireType = (int)ExpireTypeEnum.截止日期;
            inputModel.ApplyScopeType = (int)CashApplyScopeTypeEnum.指定课程;
            inputModel.VoucherType = (int)VoucherTypeEnum.校区券;
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var request = inputModel.ConvertTo<DtoCashVoucherRequest>();
            request.Grade = 0;
            request.CourseType = 0;
            var success = false;
            if (inputModel.Id > 0)
            {
                success = cashVoucherBll.Update(request);
            }
            else
            {
                request.Creator = CurrentUser.Teacher.Yoh_Id;
                request.Editor = CurrentUser.Teacher.Yoh_Id;
                success = cashVoucherBll.Add(request) > 0;
            }
            return SimpleResult(success);
        }

        /// <summary>
        /// 查看—现金券明细
        /// </summary>
        /// <returns></returns>
        public ActionResult CashVoucherDetails(int cashVoucherId = 0)
        {
            if (cashVoucherId == 0)
            {
                return View();
            }
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var viewModel = cashVoucherBll.GetCashVoucherDetail(cashVoucherId);
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
        public ActionResult UpdateStatus(int id, int status)
        {
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var result = cashVoucherBll.UpdateStatus(id, status);
            return SimpleResult(result);
        }
    }
}