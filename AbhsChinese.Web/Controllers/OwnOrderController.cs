using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Web.Models.OwnOrder;
using AbhsChinese.Web.Models.StudentInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Web.Controllers
{
    public class OwnOrderController : BaseController
    {
        public ActionResult OwnOrderList()
        {
            StudentInfoBll studentBll = new StudentInfoBll();
            var studentInfo = studentBll.GetStudentInfo(GetCurrentUser().StudentId);
            var viewModel = studentInfo.ConvertTo<StudentInfoViewModel>();
            return View(viewModel);
        }

        public ActionResult GetStudentOrder(int pageIndex, int status = 1)
        {
            StudentOrderBll studentOrderBll = new StudentOrderBll();
            var device = "";
            device = clsCommon.CheckAgent();
            PagingObject paging = new PagingObject();
            paging.PageIndex = pageIndex;
            paging.PageSize = 7;
            //if (device.Contains("iPad"))
            //{
            //    paging.PageSize = 6;
            //}
            //else
            //{
            //    paging.PageSize = 7;
            //}
            var studentOrder = studentOrderBll.GetStudentOrderByStudentId(paging, GetCurrentUser().StudentId);
            var viewModels = studentOrder.ConvertTo<List<StudentOrderViewModel>>();
            return Json(new { Data = viewModels, PageSize = paging.PageSize, TotalRecord = paging.TotalCount }, JsonRequestBehavior.AllowGet);
        }

        private string defaultCourseImg = "/Images/Course/defaultimg.jpg";
        /// <summary>
        /// 订单详情页
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <returns></returns>
        public ActionResult OrderDetail(int id = 0)
        {
            if (id == 0)
            {
                //跳转到订单列表页
                return RedirectToAction("OwnOrderList");
            }
            int studentId = GetCurrentUser().StudentId;

            StudentOrderBll studentOrderBll = new StudentOrderBll();
            DtoSelectCenterOrderResult courseOrderResult = studentOrderBll.GetOrderInfoForDetail(studentId, id);

            OrderDetailViewModel model = courseOrderResult.ConvertTo<OrderDetailViewModel>();
            decimal? baseVoucherAmount = model.OrderVoucherPayList?.FirstOrDefault(v => v.PayType != (int)StudentOrderPaymnentTypeEnum.优惠券_校区券_支付)?.Amount;
            model.BaseVoucherAmount = baseVoucherAmount.HasValue ? baseVoucherAmount.Value : 0;
            decimal? schoolVoucherAmount = model.OrderVoucherPayList?.FirstOrDefault(v => v.PayType == (int)StudentOrderPaymnentTypeEnum.优惠券_校区券_支付)?.Amount;
            model.SchoolVoucherAmount = schoolVoucherAmount.HasValue ? schoolVoucherAmount.Value : 0;

            //防止用户未点击支付按钮，订单表没计算应付金额
            if (model.PayAmount == 0)
            {
                model.PayAmount = model.OrderAmount - model.BaseVoucherAmount - model.SchoolVoucherAmount;
                model.PayAmount = model.PayAmount > 0 ? model.PayAmount : 0;
            }

            if (model.OrderStatus == (int)StudentOrderStatus.待支付 || model.OrderStatus == (int)StudentOrderStatus.支付中 || model.OrderStatus == (int)StudentOrderStatus.已支付)
            {
                model.IsHaveThisCourse = true;
            }
            else
            {
                //查询用户是否已购买此课程
                var oriOrder = studentOrderBll.GetFinishOrder(studentId, model.CourseId);
                if (oriOrder != null && oriOrder.Yod_Id > 0)
                {
                    model.IsHaveThisCourse = true;
                }
                else
                {
                    model.IsHaveThisCourse = false;
                }
            }

            StudentInfoBll studentInfoBll = new StudentInfoBll();
            DtoStudentInfo student = studentInfoBll.GetStudentInfo(studentId);
            Regex regex = new Regex(@"(\d{3})\d{4}(\d{4})");
            model.StudentAccount = regex.Replace(student.Bst_Phone, "$1****$2");
            model.SchoolName = student.SchoolName;
            if (string.IsNullOrEmpty(model.CourseImage))
            {
                model.CourseImage = defaultCourseImg;
            }

            return View(model);
        }

        #region 生成订单、获取支付二维码、交易查询
        /// <summary>
        /// 学生下单（课程详情页点击立即购买）
        /// </summary>
        /// <param name="id">课程Id</param>
        /// <returns></returns>
        public ActionResult MakeOrder(int id = 0, int aid = 0)
        {
            //生成订单，如果生成成功，跳转到订单页
            int studentId = GetCurrentUser().StudentId;

            StudentOrderBll studentOrderBll = new StudentOrderBll();
            int orderId = studentOrderBll.MakeOrder(studentId, id);
            try
            {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    AdvertisingBll advertisingBll = new AdvertisingBll();
                    advertisingBll.SendOrderMessage(aid);
                });
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Payment", new { id = orderId });
        }

        /// <summary>
        /// 订单详情页-付款页
        /// 如果订单超时未支付，再次购买时，不用考虑上个订单是不是从点击广告来的。
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <returns></returns>
        public ActionResult Payment(int id = 0)
        {
            if (id == 0)
            {
                //跳转到订单列表页
                return RedirectToAction("OwnOrderList");
            }
            int studentId = GetCurrentUser().StudentId;

            StudentOrderBll studentOrderBll = new StudentOrderBll();
            DtoSelectCenterOrderResult courseOrderResult = studentOrderBll.GetOrderInfoForPayment(studentId, id);

            if (courseOrderResult.OrderStatus.Equals((int)StudentOrderStatus.待支付) || courseOrderResult.OrderStatus.Equals((int)StudentOrderStatus.支付中))
            {
                PaymentViewModel model = courseOrderResult.ConvertTo<PaymentViewModel>();
                if (string.IsNullOrEmpty(model.CourseImage))
                {
                    model.CourseImage = defaultCourseImg;
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("OrderDetail", new { id = courseOrderResult.OrderId });
            }
        }

        /// <summary>
        /// 获取支付二维码
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <param name="voucherIds">支付使用的优惠券Id</param>
        /// <param name="paytype">支付渠道（支付宝、微信）</param>
        /// <returns></returns>
        public JsonResult GetWechatPayQR(int orderId = 0, int[] voucherIds = null, int paytype = 1)
        {
            int studentId = 0;
            string ip = "";
            //当前登录用户
            LoginInfo studentInfo = GetCurrentUser();
            if (studentInfo != null)
            {
                studentId = studentInfo.StudentId;
                ip = studentInfo.LastLoginIP;
            }
            StudentOrderBll studentOrderBll = new StudentOrderBll();
            DtoPreOrderPay response = studentOrderBll.PreOrderPay(orderId, studentId, voucherIds, ip, paytype);
            return Json(new JsonResponse<dynamic>
            {
                State = response.State,
                ErrorCode = 0,
                ErrorMsg = response.ErrorMsg,
                Data = new
                {
                    QR = response.Base64Img,
                    IsFreeOrder = response.IsFreeOrder
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OrderQuery(int orderId, CashPayTypeEnum payType)
        {
            //当前登录用户
            LoginInfo studentInfo = GetCurrentUser();

            StudentOrderBll studentOrderBll = new StudentOrderBll();
            bool result = studentOrderBll.OrderQuery(orderId, payType, studentInfo.StudentId);

            return Json(new JsonResponse<bool> { State = true, ErrorCode = 0, ErrorMsg = "", Data = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}