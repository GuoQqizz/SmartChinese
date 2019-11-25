using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Web.Models;
using AbhsChinese.Web.Models.Common;
using AbhsChinese.Web.Models.StudentInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Web.Controllers
{
    public class OwnCashVoucherController : BaseController
    {
        public ActionResult OwnCashVoucherList()
        {
            StudentInfoBll studentBll = new StudentInfoBll();
            var studentInfo = studentBll.GetStudentInfo(GetCurrentUser().StudentId);
            var viewModel = studentInfo.ConvertTo<StudentInfoViewModel>();
            return View(viewModel);
        }
        
        public ActionResult GetOwnCashVoucher(StuOwnVoucherSearch search)
        {
            StudentInfoBll studentInfoBll = new StudentInfoBll();
            var device = "";
            device = clsCommon.CheckAgent();
            PagingObject paging = new PagingObject();
            paging.PageIndex = search.PageIndex;
            if (device.Contains("iPad"))
            {
                paging.PageSize = 6;
            }
            else
            {
                paging.PageSize = 9;
            }
            var studentCashVoucher = studentInfoBll.GetOwnCashVoucher(paging, GetCurrentUser().StudentId, search.Status);
            List<StudentCashVoucherViewModel> viewModels = studentCashVoucher.ConvertTo<List<StudentCashVoucherViewModel>>();
            return Json(new { Data = viewModels, PageSize = paging.PageSize, TotalRecord = paging.TotalCount }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询已领取注册券
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public ActionResult GetHaveRegisterVoucher()
        {
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var studentCashVoucher = cashVoucherBll.GetHaveRegisterVoucher(GetCurrentUser().StudentId);
            return Json(new JsonResponse<List<DtoOwnCashVoucher>>() { Data = studentCashVoucher, State = true }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 领取注册券部分视图
        /// </summary>
        /// <returns></returns>
        public ActionResult GetHaveRegisterVoucherView()
        {
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var studentCashVoucher = cashVoucherBll.GetHaveRegisterVoucher(GetCurrentUser().StudentId);
            studentCashVoucher = studentCashVoucher.OrderByDescending(s => s.Ycv_Amount).Take(4).ToList();
            return PartialView(studentCashVoucher);
        }
        /// <summary>
        /// 查询课程可使用的所有现金券
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public ActionResult GetAllCashVoucher(StudentCashVoucherSearch search)
        {
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var studentCashVoucher = cashVoucherBll.GetStudentUseCashVoucher(GetCurrentUser().StudentId, search.OrderAmountLimit, search.Grade, search.CourseType, search.CourseId);
            return Json(new JsonResponse<List<DtoOwnCashVoucher>>() { Data = studentCashVoucher, State = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 领取现金券（添加）
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="cashVoucherId"></param>
        /// <param name="gotType">现金券获取方式</param>
        /// <param name="gotReferId">现金券获取关联Id</param>
        [HttpPost]
        public ActionResult TakeStudentCashVoucher(StudentCashVoucherInputModel inputModel)
        {
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var studentCashVoucher = cashVoucherBll.TakeStudentCashVoucher(GetCurrentUser().StudentId, inputModel.CashVoucherId, inputModel.GotType, inputModel.GotReferId);
            return Json(new JsonResponse<bool>() { Data = studentCashVoucher, State = true });
        }

        /// <summary>
        /// 查询已下单的现金券
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ActionResult GetCashVoucherByOrderId(int orderId)
        {
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            var cashVoucher = cashVoucherBll.GetCashVoucherByOrderId(GetCurrentUser().StudentId, orderId);
            return Json(new JsonResponse<List<DtoOwnCashVoucher>>() { Data = cashVoucher, State = true },JsonRequestBehavior.AllowGet);
        }
    }
}