using AbhsChinese.Bll.WechatPay;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using AbhsChinese.Domain.AbhsResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WenDu.ZhiBo.Infrastructure;
using AbhsChinese.Domain.Dto.Response;
using System.Threading;
using AbhsChinese.Code.Common;
using WenDu.ZhiBo.BLL.WechatPay;

namespace AbhsChinese.Bll
{
    public class StudentOrderBll : BllBase
    {
        /// <summary>
        /// 商户订单号中订单号和批次号的分隔符
        /// </summary>
        private static readonly string ORDERNO_BATCHID_SEPARATOR = "-";
        private IStudentOrderPayRepository service;
        public IStudentOrderPayRepository Service
        {
            get
            {
                if (service == null)
                {
                    service = new StudentOrderPayRepository();
                }

                service.TranId = tranId;
                return service;
            }
        }

        private IStudentOrderRepository orderService;
        public IStudentOrderRepository OrderService
        {
            get
            {
                if (orderService == null)
                {
                    orderService = new StudentOrderRepository();
                }

                orderService.TranId = tranId;
                return orderService;
            }
        }

        private IPayLogRepository payLogService;
        public IPayLogRepository PayLogService
        {
            get
            {
                if (payLogService == null)
                {
                    payLogService = new PayLogRepository();
                }

                payLogService.TranId = tranId;
                return payLogService;
            }
        }

        public Yw_StudentOrder GetStudentOrder(int orderId)
        {
            return OrderService.Get(orderId);
        }

        public Yw_StudentOrder GetStudentOrder(string orderNo)
        {
            return OrderService.Get(orderNo);
        }

        public Yw_StudentOrder GetFinishOrder(int studentId, int courseId)
        {
            return OrderService.GetFinishOrder(studentId, courseId);
        }
        /// <summary>
        /// 学员下订单
        /// </summary>
        public int MakeOrder(int studentId, int courseId)
        {
            CourseBll courseBll = new CourseBll();
            SchoolBll schoolBll = new SchoolBll();

            Yw_Course course = courseBll.GetCourse(courseId);
            if (course == null || course.Ycs_Status != (int)CourseStatusEnum.已上架)
            {
                throw new AbhsException(ErrorCodeEnum.CourseCanNotBuy, AbhsErrorMsg.ConstCourseCanNotBuy);
            }

            Bas_School school = schoolBll.GetSchoolByStudent(studentId);
            if (school == null)
            {
                throw new AbhsException(ErrorCodeEnum.StudentNotBindSchool, AbhsErrorMsg.ConstStudentNotBindSchool);
            }

            Yw_CoursePrice price = courseBll.GetCoursePrice(courseId, school.Bsl_Level);
            Yw_StudentOrder order = new Yw_StudentOrder();
            order.Yod_CourseId = courseId;
            order.Yod_OrderNo = GenerateOrderNo(courseId, studentId);
            order.Yod_OrderTime = DateTime.Now;
            order.Yod_OrderType = (int)OrderTypeEnum.订单;
            order.Yod_PayBatchId = 0;
            order.Yod_ReferOrderId = 0;
            order.Yod_PayTime = new DateTime(1900, 1, 1);
            order.Yod_Status = (int)StudentOrderStatus.待支付;
            order.Yod_StudentId = studentId;
            order.Yod_UpdateTime = DateTime.Now;
            order.Yod_Amount = price.Yce_Price;
            OrderService.Add(order);
            return order.Yod_Id;
        }

        /// <summary>
        /// 订单支付，该方法暂时不用
        /// </summary>
        public void OrderPay(int orderId, int studentId, int[] voucherIds, CashPayTypeEnum payType, out decimal amount, out string orderNo)
        {
            int batchId = Service.PreOrderPay(orderId, studentId, voucherIds, payType);
            Service.OrderPay(orderId, studentId, voucherIds, payType, batchId, out amount, out orderNo);
            Service.OrderPayOver(orderNo);
        }

        /// <summary>
        /// 支付回调
        /// </summary>
        /// <param name="orderNo">经过编码的商户订单号</param>
        /// <param name="transactionId">交易流水号（微信支付订单号）</param>
        /// <param name="payType">付款方式</param>
        /// <param name="amount">实付金额（元）</param>
        public void OrderPayCallback(string orderNo, string transactionId, CashPayTypeEnum payType, decimal amount)
        {
            //如果是微信支付需要解析出真正的订单号
            if (CashPayTypeEnum.微信.Equals(payType))
            {
                orderNo = GetOrderNoAndBatchIdByOutTradeNo(orderNo).Item1;
            }
            Service.OrderPayCallback(orderNo, transactionId, payType, amount);
        }

        /// <summary>
        /// 调用微信统一下单
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <param name="studentId">学生Id</param>
        /// <param name="voucherIds">本次支付使用的优惠券Id列表</param>
        /// <param name="ip">用户IP</param>
        /// <param name="image">支付二维码图片</param>
        /// <param name="isFreeOrder">是否免费订单，如为0元购返回true</param>
        /// <returns>统一下单处理结果。</returns>
        public bool WechatOrderPay(int orderId, int studentId, int[] voucherIds, string ip, ref byte[] image, ref bool isFreeOrder)
        {
            decimal amount = 0;
            string orderNo = "";
            int batchId = Service.PreOrderPay(orderId, studentId, voucherIds, CashPayTypeEnum.微信);
            Service.OrderPay(orderId, studentId, voucherIds, CashPayTypeEnum.微信, batchId, out amount, out orderNo);

            if (batchId >= 1)//如果之前已经发起过微信支付，需要关闭之前的微信支付订单。如果关闭之前订单失败，本次支付仍然继续执行。
            {
                try
                {
                    WeChatApi weChatApi = new WeChatApi();
                    weChatApi.CloseOrder(orderId, GetOutTradeNo(orderNo, batchId));
                }
                catch (Exception ex)
                {
                    LogHelper.ErrorLog("", ex);
                }
            }

            if (amount > 0 && !string.IsNullOrEmpty(orderNo))
            {
                WechatPreOrderResponse response = new WeChatApi().CreatePreOrder(orderId, "大语文订单", GetOutTradeNo(orderNo, batchId + 1), amount, ip);
                image = QRCodeUtil.MakeImage(response.CodeUrl);
                return true;
            }
            else if (amount == 0 && !string.IsNullOrEmpty(orderNo))//如果本次不需要微信支付（课程价格是0或者使用的现金券金额>=课程金额）
            {
                Service.OrderPayOver(orderNo);
                isFreeOrder = true;
                return false;
            }

            return false;
        }


        public DtoPreOrderPay PreOrderPay(int orderId, int studentId, int[] voucherIds, string ip, int paytype)
        {
            DtoPreOrderPay response = new DtoPreOrderPay();
            DtoSelectCenterOrderResult courseOrderResult = GetOrderInfoForPayment(studentId, orderId);
            if (courseOrderResult.OrderStatus.Equals((int)StudentOrderStatus.待支付) || courseOrderResult.OrderStatus.Equals((int)StudentOrderStatus.支付中))
            {
                //查询用户是否已购买此课程
                var oriOrder = GetFinishOrder(studentId, courseOrderResult.CourseId);
                if (oriOrder != null && oriOrder.Yod_Id > 0)
                {
                    //已购买，不再生成付款码
                    response.State = false;
                    response.ErrorMsg = "你已购买此课程。";
                }
                else
                {
                    if (CashPayTypeEnum.微信.Equals((CashPayTypeEnum)paytype))
                    {
                        //支付二维码图片
                        byte[] qrImage = null;
                        bool isFreeOrder = false;
                        bool unifiedOrderResult = WechatOrderPay(orderId, studentId, voucherIds, ip, ref qrImage, ref isFreeOrder);
                        response.State = true;
                        response.IsFreeOrder = isFreeOrder;
                        if (unifiedOrderResult)
                        {
                            response.Base64Img = Convert.ToBase64String(qrImage);
                        }
                        else if (!isFreeOrder)
                        {
                            throw new AbhsException(ErrorCodeEnum.WechatPrePayError, AbhsErrorMsg.ConstWechatPrePayError);
                        }
                    }
                    else
                    {
                        throw new AbhsException(ErrorCodeEnum.PayTypeError, AbhsErrorMsg.PayTypeError);
                    }
                }
            }
            else if (courseOrderResult.OrderStatus.Equals((int)StudentOrderStatus.已支付))
            {
                response.State = false;
                response.ErrorMsg = "此订单已支付。";
            }
            else
            {
                response.State = false;
                response.ErrorMsg = "此订单已超时。";
            }
            return response;
        }


        /// <summary>
        /// 交易查询
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <param name="payType">支付方式</param>
        /// <param name="studentId">下单的学生Id</param>
        /// <returns>如果订单支付成功返回true，否则返回false</returns>
        public bool OrderQuery(int orderId, CashPayTypeEnum payType, int studentId)
        {
            Yw_StudentOrder order = GetStudentOrder(orderId);
            if (order.Yod_StudentId.Equals(studentId))
            {
                bool result = false;
                //switch (payType)
                //{
                //    case CashPayTypeEnum.微信:
                //        WeChatApi weChatApi = new WeChatApi();
                //        string outTradeNo = GetOutTradeNo(order.Yod_OrderNo, order.Yod_PayBatchId);
                //        WechatQueryOrderResponse orderQueryResponse = weChatApi.OrderQuery("", orderId, outTradeNo);

                //        if ("SUCCESS".Equals(orderQueryResponse.TradeState))
                //        {
                //            //以接收微信支付通知为准，此处不再触发支付回调
                //            //OrderPayCallback(outTradeNo, payType, (decimal)(orderQueryResponse.TotalFee * 1.0 / 100));
                //            result = true;
                //        }
                //        break;
                //}
                result = order.Yod_Status.Equals((int)StudentOrderStatus.已支付);
                return result;

            }
            else
            {
                throw new AbhsException(ErrorCodeEnum.NoAuthorize, AbhsErrorMsg.ConstNoAuthorizeViewOrder);
            }
        }
        /// <summary>
        /// 接收微信支付成功通知
        /// </summary>
        /// <param name="xmlContent">微信通知内容</param>
        /// <returns>通知处理结果</returns>
        public string WechatOrderPayNotify(string xmlContent)
        {
            string return_code = "", return_msg = "";
            try
            {
                WeChatApi weChatApi = new WeChatApi();
                WechatPayNotifyResponse response = weChatApi.PayNotify(xmlContent);
                if (response.IsSuccess)
                {
                    return_code = "SUCCESS";
                    return_msg = "OK";
                }
                else
                {
                    return_code = "FAIL";
                    return_msg = "FAIL";
                }
            }
            catch (Exception)
            {
                return_code = "FAIL";
                return_msg = "FAIL";
            }
            //返回通知处理结果
            return string.Format("<xml><return_code><![CDATA[{0}]]></return_code><return_msg><![CDATA[{1}]]></return_msg></xml>", return_code, return_msg);
        }


        /// <summary>
        /// 过期取消订单，如果失败会抛出异常
        /// </summary>
        public void CancelOrder(int orderId)
        {
            OrderService.CancelOrder(orderId);
        }

        /// <summary>
        /// 系统自动取消过期订单
        /// </summary>
        public void CancelOrderAuto()
        {
            List<Yw_StudentOrder> orders = OrderService.GetOverdueOrder();
            if (orders.Count > 0)
            {
                for (int index = 0; index < orders.Count; index++)
                {
                    if (index > 0 && index % 10 == 0)
                    {
                        Thread.Sleep(100);
                    }

                    OrderService.CancelOrder(orders[index].Yod_Id);
                }
            }
        }

        /// <summary>
        /// 记录支付日志
        /// </summary>
        public void PayLog(Yw_PayLog log)
        {
            PayLogService.Add(log);
        }

        private string GenerateOrderNo(int courseId, int userId)
        {
            return OrderService.GenerateOrderNo(courseId, userId, OrderTypeEnum.订单);
        }

        public List<DtoStudentOrder> GetStudentOrderByStudentId(PagingObject paging, int studentId)
        {
            return OrderService.GetStudentOrderByStudentId(paging, studentId);
        }


        /// <summary>
        /// 选课中心订单详情页-支付页
        /// </summary>
        /// <param name="studentId">学生Id</param>
        /// <param name="orderId">订单Id</param>
        /// <returns></returns>
        public DtoSelectCenterOrderResult GetOrderInfoForPayment(int studentId, int orderId)
        {
            //获取订单信息
            //验证订单是否属于当前登录用户

            //根据订单Id查询课程信息（名称、图片、年级、课程类型、课时、描述、教材版本）
            //课程价格显示订单原始金额

            Yw_StudentOrder order = GetStudentOrder(orderId);

            if (order.Yod_StudentId.Equals(studentId))
            {
                CourseBll courseBll = new CourseBll();
                Yw_Course course = courseBll.GetCourse(order.Yod_CourseId);

                DtoSelectCenterOrderResult result = new DtoSelectCenterOrderResult();
                result.StudentId = order.Yod_StudentId;
                result.CourseId = course.Ycs_Id;
                result.CourseName = course.Ycs_Name;
                result.CourseImage = course.Ycs_CoverImage.ToOssPath();
                result.Grade = course.Ycs_Grade;
                result.CourseType = course.Ycs_CourseType;
                result.LessonCount = course.Ycs_LessonCount;
                result.Description = course.Ycs_Description;
                result.OrderId = order.Yod_Id;
                result.OrderAmount = order.Yod_Amount;
                result.OrderStatus = order.Yod_Status;
                result.OrderTime = order.Yod_OrderTime;
                result.OrderNo = order.Yod_OrderNo;
                return result;
            }
            else
            {
                throw new AbhsException(ErrorCodeEnum.NoAuthorize, AbhsErrorMsg.ConstNoAuthorizeViewOrder);
            }

        }

        public DtoSelectCenterOrderResult GetOrderInfoForDetail(int studentId, int orderId)
        {
            Yw_StudentOrder order = GetStudentOrder(orderId);

            if (order.Yod_StudentId.Equals(studentId))
            {
                CourseBll courseBll = new CourseBll();
                Yw_Course course = courseBll.GetCourse(order.Yod_CourseId);

                DtoSelectCenterOrderResult result = new DtoSelectCenterOrderResult();
                result.StudentId = order.Yod_StudentId;
                result.CourseId = course.Ycs_Id;
                result.CourseName = course.Ycs_Name;
                result.CourseImage = course.Ycs_CoverImage.ToOssPath();
                result.Grade = course.Ycs_Grade;
                result.CourseType = course.Ycs_CourseType;
                result.LessonCount = course.Ycs_LessonCount;
                result.Description = course.Ycs_Description;
                result.OrderId = order.Yod_Id;
                result.OrderAmount = order.Yod_Amount;
                result.PayAmount = order.Yod_PayAmount;
                result.OrderStatus = order.Yod_Status;
                result.OrderTime = order.Yod_OrderTime;
                result.OrderNo = order.Yod_OrderNo;

                List<DtoStudentOrderPay> orderPayList = Service.GetStudentOrderPay(order.Yod_Id, order.Yod_PayBatchId);
                result.OrderCashPay = orderPayList.FirstOrDefault(p => p.PayType < 200);
                result.OrderVoucherPayList = orderPayList.Where(p => p.PayType > 200).ToList();

                if (result.OrderCashPay != null)
                {
                    result.PayType = result.OrderCashPay.PayType - 100;
                    result.PayTime = result.OrderCashPay.PayTime;
                }

                return result;
            }
            else
            {
                throw new AbhsException(ErrorCodeEnum.NoAuthorize, AbhsErrorMsg.ConstNoAuthorizeViewOrder);
            }
        }

        /// <summary>
        /// 通过订单号和批次号组合成商户订单号
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="batchId">批次号</param>
        /// <returns>商户订单号</returns>
        private string GetOutTradeNo(string orderNo, int batchId)
        {
            return string.Format("{0}{1}{2}", orderNo, ORDERNO_BATCHID_SEPARATOR, batchId);
        }
        /// <summary>
        /// 通过商户订单号解析出订单号和批次号
        /// </summary>
        /// <param name="outTradeNo">商户订单号</param>
        /// <returns>订单号,批次号元组</returns>
        private Tuple<string, int> GetOrderNoAndBatchIdByOutTradeNo(string outTradeNo)
        {
            string orderNo = "";
            int batchId = 0;
            string[] arr = outTradeNo.Split(new String[] { ORDERNO_BATCHID_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 2)
            {
                orderNo = arr[0];
                int.TryParse(arr[1], out batchId);
            }
            else
            {
                orderNo = outTradeNo;
            }
            return Tuple.Create(orderNo, batchId);
        }
    }
}
