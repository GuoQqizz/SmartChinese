using AbhsChinese.Bll.WechatPay;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.AbhsResource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using WenDu.ZhiBo.BLL.WechatPay;
using AbhsChinese.Code.Common;

namespace AbhsChinese.Bll.WechatPay
{
    public class WeChatApi
    {
        /// <summary>
        /// 统一支付接口
        /// 统一支付接口，可接受JSAPI/NATIVE/APP 下预支付订单，返回预支付订单号。NATIVE 支付返回二维码code_url。
        /// </summary>
        /// <param name="data">微信支付需要post的xml数据</param>
        /// <returns></returns>
        public string Unifiedorder(string data)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            return WebHelper.HttpWebRequest(urlFormat, data);
        }

        /// <summary>
        /// 微信扫码支付/微信H5支付时调用同一下单接口
        /// </summary>
        /// <param name="body"></param>
        /// <param name="outTradeNo"></param>
        /// <param name="amount"></param>
        /// <param name="payMethod"></param>
        /// <returns></returns>
        public WechatPreOrderResponse CreatePreOrder(int orderId, string body, string outTradeNo, decimal amount, string userIp)
        {
            Yw_PayLog log = new Yw_PayLog();
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic["appid"] = WechatPayAppSetting.WeiXinPayAppId;
                dic["mch_id"] = WechatPayAppSetting.WeiXinPayMchId;
                dic["nonce_str"] = GetNoncestr();
                dic["body"] = body;
                dic["out_trade_no"] = outTradeNo;
                dic["total_fee"] = ((int)(amount * 100)).ToString();
                dic["spbill_create_ip"] = userIp;
                dic["notify_url"] = WechatPayAppSetting.WeiXinPayNotifyUrl;
                dic["trade_type"] = "NATIVE";
                dic["product_id"] = outTradeNo;
                #region useless
                //if (!string.IsNullOrEmpty(WeiXinSetting.TestInternetIPAddress))//仅测试环境应该进入该路径
                //{
                //    userIp = WeiXinSetting.TestInternetIPAddress;
                //}
                //else 
                //if (HttpContext.Current != null)
                //{
                //    userIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                //}
                //if (!string.IsNullOrEmpty(userIp))
                //{
                //    if (userIp.Contains(","))
                //    {
                //        userIp = userIp.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                //    }
                //}
                #endregion

                string sign = CreateMd5Sign(dic, WechatPayAppSetting.WeiXinPaySecret);
                dic["sign"] = sign;

                string data = ParseXML(dic);

                BeginLog(log, orderId, "", PayLogStepEnum.生成微信预订单, data);
                string returnData = Unifiedorder(data);
                EndLog(log, returnData, 1);
                SaveLog(log);

                Dictionary<string, string> notify = ParseXml(returnData);
                WechatPreOrderResponse response = new WechatPreOrderResponse();
                response.ReturnCode = notify.ContainsKey("return_code") ? notify["return_code"] : "";
                response.ReturnMsg = notify.ContainsKey("return_msg") ? notify["return_msg"] : "";
                response.AppId = notify.ContainsKey("appid") ? notify["appid"] : "";
                response.MchId = notify.ContainsKey("mch_id") ? notify["mch_id"] : "";
                response.NonceStr = notify.ContainsKey("nonce_str") ? notify["nonce_str"] : "";
                response.Sign = notify.ContainsKey("sign") ? notify["sign"] : "";
                response.ResultCode = notify.ContainsKey("result_code") ? notify["result_code"] : "";
                response.ErrCode = notify.ContainsKey("err_code") ? notify["err_code"] : "";
                response.ErrCodeDes = notify.ContainsKey("err_code_des") ? notify["err_code_des"] : "";
                response.TradeType = notify.ContainsKey("trade_type") ? notify["trade_type"] : "";
                response.PrepayId = notify.ContainsKey("prepay_id") ? notify["prepay_id"] : "";
                response.CodeUrl = notify.ContainsKey("code_url") ? notify["code_url"] : "";
                response.MWebUrl = notify.ContainsKey("mweb_url") ? notify["mweb_url"] : "";

                if (response.ReturnCode == "SUCCESS" && CheckSign(notify))//通信标志成功时才能对比校验签名信息
                {
                    if (response.ResultCode == "SUCCESS")//支付业务成功时才返回
                    {
                        return response;
                    }
                }
                throw new AbhsException(ErrorCodeEnum.WechatPrePayError, AbhsErrorMsg.ConstWechatPrePayError);
            }
            catch (Exception ex)
            {
                EndLog(log, "Fail," + ex.Message + "|" + ex.StackTrace, 0);
                SaveLog(log);
                throw;
            }
        }

        public WechatPayNotifyResponse PayNotify(string xmlContent)
        {
            WechatPayNotifyResponse response = new WechatPayNotifyResponse();
            Yw_PayLog log = new Yw_PayLog();

            try
            {
                BeginLog(log, 0, "", PayLogStepEnum.微信支付回调, xmlContent);
                Dictionary<string, string> notify = ParseXml(xmlContent);

                response.IsSuccess = false;

                response.ReturnCode = notify.ContainsKey("return_code") ? notify["return_code"] : "";
                response.ReturnMsg = notify.ContainsKey("return_msg") ? notify["return_msg"] : "";
                response.AppId = notify.ContainsKey("appid") ? notify["appid"] : "";
                response.MchId = notify.ContainsKey("mch_id") ? notify["mch_id"] : "";
                response.ResultCode = notify.ContainsKey("result_code") ? notify["result_code"] : "";
                response.ErrCode = notify.ContainsKey("err_code") ? notify["err_code"] : "";
                response.ErrCodeDes = notify.ContainsKey("err_code_des") ? notify["err_code_des"] : "";
                response.TradeType = notify.ContainsKey("trade_type") ? notify["trade_type"] : "";
                response.OpenId = notify.ContainsKey("openid") ? notify["openid"] : "";
                response.TotalFee = int.Parse(notify["total_fee"]);
                response.TransactionId = notify.ContainsKey("transaction_id") ? notify["transaction_id"] : "";
                response.OutTradeNo = notify.ContainsKey("out_trade_no") ? notify["out_trade_no"] : "";

                BeginLog(log, 0, response.OutTradeNo, PayLogStepEnum.微信支付回调, xmlContent);

                if (CheckSign(notify))
                {
                    if (response.ReturnCode == "SUCCESS" && response.ResultCode == "SUCCESS")
                    {
                        StudentOrderBll bll = new StudentOrderBll();
                        bll.OrderPayCallback(response.OutTradeNo, response.TransactionId, CashPayTypeEnum.微信, (decimal)(response.TotalFee * 1.0 / 100));
                        response.IsSuccess = true;
                        EndLog(log, "Success", 1);
                        SaveLog(log);
                    }
                    return response;
                }

                throw new AbhsException(ErrorCodeEnum.WechatPayNotifyError, AbhsErrorMsg.ConstWechatPayNotifyError, 0);
            }
            catch (Exception ex)
            {
                EndLog(log, "Fail," + ex.Message + "|" + ex.StackTrace, 0);
                SaveLog(log);
                throw;
            }
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="orderId">商户订单Id（记录日志用）</param>
        /// <param name="outTradeNo">商户订单号</param>
        /// <returns>关闭订单结果</returns>
        public WechatCloseOrderResponse CloseOrder(int orderId, string outTradeNo)
        {
            Yw_PayLog log = new Yw_PayLog();
            try
            {
                string url = "https://api.mch.weixin.qq.com/pay/closeorder";
                //检测必填参数
                if (string.IsNullOrEmpty(outTradeNo))
                {
                    throw new AbhsException(ErrorCodeEnum.WechatPayCloseOrderError, AbhsErrorMsg.ConstWechatPayCloseOrderError);
                }

                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic["appid"] = WechatPayAppSetting.WeiXinPayAppId;
                dic["mch_id"] = WechatPayAppSetting.WeiXinPayMchId;
                dic["nonce_str"] = GetNoncestr();
                dic["out_trade_no"] = outTradeNo;
                string sign = CreateMd5Sign(dic, WechatPayAppSetting.WeiXinPaySecret);
                dic["sign"] = sign;

                string data = ParseXML(dic);

                BeginLog(log, orderId, "", PayLogStepEnum.微信关闭订单, data);

                string responseData = WebHelper.HttpWebRequest(url, data);

                EndLog(log, responseData, 1);
                SaveLog(log);


                Dictionary<string, string> result = ParseXml(responseData);

                WechatCloseOrderResponse response = new WechatCloseOrderResponse();
                response.ReturnCode = result.ContainsKey("return_code") ? result["return_code"] : "";
                response.ReturnMsg = result.ContainsKey("return_msg") ? result["return_msg"] : "";
                response.AppId = result.ContainsKey("appid") ? result["appid"] : "";
                response.MchId = result.ContainsKey("mch_id") ? result["mch_id"] : "";
                response.NonceStr = result.ContainsKey("nonce_str") ? result["nonce_str"] : "";
                response.Sign = result.ContainsKey("sign") ? result["sign"] : "";
                response.ResultCode = result.ContainsKey("result_code") ? result["result_code"] : "";
                response.ResultMsg = result.ContainsKey("result_msg") ? result["result_msg"] : "";
                response.ErrCode = result.ContainsKey("err_code") ? result["err_code"] : "";
                response.ErrCodeDes = result.ContainsKey("err_code_des") ? result["err_code_des"] : "";

                if (response.ReturnCode == "SUCCESS" && CheckSign(result))//通信标志成功时才能对比校验签名信息
                {
                    if (response.ResultCode == "SUCCESS")//支付业务成功时才返回
                    {
                        return response;
                    }
                }
                throw new AbhsException(ErrorCodeEnum.WechatPayCloseOrderError, AbhsErrorMsg.ConstWechatPayCloseOrderError);
            }
            catch (Exception ex)
            {
                EndLog(log, "Fail," + ex.Message + "|" + ex.StackTrace, 0);
                SaveLog(log);
                throw;
            }
        }

        /// <summary>
        /// 该接口提供所有微信支付订单的查询，商户可以通过查询订单接口主动查询订单状态，完成下一步的业务逻辑。
        /// 需要调用查询接口的情况：
        /// ◆ 当商户后台、网络、服务器等出现异常，商户系统最终未接收到支付通知；
        /// ◆ 调用支付接口后，返回系统错误或未知交易状态情况；
        /// ◆ 调用付款码支付API，返回USERPAYING的状态；
        /// ◆ 调用关单或撤销接口API之前，需确认支付状态；
        /// </summary>
        /// <param name="transaction_id">微信的订单号，建议优先使用</param>
        /// <param name="orderId">商户订单Id（记录日志用）</param>
        /// <param name="outTradeNo">商户订单号</param>
        /// <returns>订单交易信息，通过返回信息的TradeState获取订单交易状态。</returns>
        public WechatQueryOrderResponse OrderQuery(string transaction_id, int orderId, string outTradeNo = "")
        {
            Yw_PayLog log = new Yw_PayLog();
            try
            {
                string url = "https://api.mch.weixin.qq.com/pay/orderquery";
                //检测必填参数
                if (string.IsNullOrEmpty(transaction_id) && string.IsNullOrEmpty(outTradeNo))
                {
                    throw new AbhsException(ErrorCodeEnum.WechatPayQueryOrderError, AbhsErrorMsg.ConstWechatPayQueryOrderError);
                }

                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic["appid"] = WechatPayAppSetting.WeiXinPayAppId;
                dic["mch_id"] = WechatPayAppSetting.WeiXinPayMchId;
                dic["transaction_id"] = transaction_id;
                dic["out_trade_no"] = outTradeNo;
                dic["nonce_str"] = GetNoncestr();
                string sign = CreateMd5Sign(dic, WechatPayAppSetting.WeiXinPaySecret);
                dic["sign"] = sign;

                string data = ParseXML(dic);

                BeginLog(log, orderId, "", PayLogStepEnum.微信交易查询, data);

                string responseData = WebHelper.HttpWebRequest(url, data);

                EndLog(log, responseData, 1);
                SaveLog(log);


                Dictionary<string, string> result = ParseXml(responseData);

                WechatQueryOrderResponse response = new WechatQueryOrderResponse();

                response.ReturnCode = result.ContainsKey("return_code") ? result["return_code"] : "";
                response.ReturnMsg = result.ContainsKey("return_msg") ? result["return_msg"] : "";

                response.AppId = result.ContainsKey("appid") ? result["appid"] : "";
                response.MchId = result.ContainsKey("mch_id") ? result["mch_id"] : "";
                response.NonceStr = result.ContainsKey("nonce_str") ? result["nonce_str"] : "";
                response.Sign = result.ContainsKey("sign") ? result["sign"] : "";
                response.ResultCode = result.ContainsKey("result_code") ? result["result_code"] : "";
                response.ErrCode = result.ContainsKey("err_code") ? result["err_code"] : "";
                response.ErrCodeDes = result.ContainsKey("err_code_des") ? result["err_code_des"] : "";

                response.DeviceInfo = result.ContainsKey("device_info") ? result["device_info"] : "";
                response.OpenId = result.ContainsKey("openid") ? result["openid"] : "";
                response.IsSubscribe = result.ContainsKey("is_subscribe") ? result["is_subscribe"] : "";
                response.TradeType = result.ContainsKey("trade_type") ? result["trade_type"] : "";
                response.TradeState = result.ContainsKey("trade_state") ? result["trade_state"] : "";
                response.BankType = result.ContainsKey("bank_type") ? result["bank_type"] : "";
                response.TotalFee = result.ContainsKey("total_fee") ? int.Parse(result["total_fee"]) : 0;
                response.SettlementTotalFee = result.ContainsKey("settlement_total_fee") ? int.Parse(result["settlement_total_fee"]) : 0;
                response.FeeType = result.ContainsKey("fee_type") ? result["fee_type"] : "";
                response.CashFee = result.ContainsKey("cash_fee") ? int.Parse(result["cash_fee"]) : 0;
                response.CashFeeType = result.ContainsKey("cash_fee_type") ? result["cash_fee_type"] : "";
                response.CouponFee = result.ContainsKey("coupon_fee") ? int.Parse(result["coupon_fee"]) : 0;
                response.CouponCount = result.ContainsKey("coupon_count") ? int.Parse(result["coupon_count"]) : 0;
                response.TransactionId = result.ContainsKey("transaction_id") ? result["transaction_id"] : "";
                response.OutTradeNo = result.ContainsKey("out_trade_no") ? result["out_trade_no"] : "";
                response.Attach = result.ContainsKey("attach") ? result["attach"] : "";
                response.TimeEnd = result.ContainsKey("time_end") ? result["time_end"] : "";
                response.TradeStateDesc = result.ContainsKey("trade_state_desc") ? result["trade_state_desc"] : "";
                
                if (response.ReturnCode == "SUCCESS" && CheckSign(result))//通信标志成功时才能对比校验签名信息
                {
                    if (response.ResultCode == "SUCCESS")//支付业务成功时才返回
                    {
                        return response;
                    }
                }
                throw new AbhsException(ErrorCodeEnum.WechatPayQueryOrderError, AbhsErrorMsg.ConstWechatPayQueryOrderError);
            }
            catch (Exception ex)
            {
                EndLog(log, "Fail," + ex.Message + "|" + ex.StackTrace, 0);
                SaveLog(log);
                throw;
            }
        }


        public string CreateMd5Sign(Dictionary<string, string> dic, string payApiSecret = null)
        {
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(dic.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)dic[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            sb.Length -= 1;
            if (!string.IsNullOrEmpty(payApiSecret))
            {
                sb.Append("&key=" + payApiSecret);
            }

            string sign = MD5(sb.ToString()).ToUpper();
            return sign;
        }

        private string MD5(string text)
        {
            byte[] result = Encoding.UTF8.GetBytes(text);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }

        private string GetNoncestr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        private string ParseXML(Dictionary<string, string> dic)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            foreach (string k in dic.Keys)
            {
                string v = (string)dic[k];
                if (Regex.IsMatch(v, @"^[0-9.]$"))
                {

                    sb.Append("<" + k + ">" + v + "</" + k + ">");
                }
                else
                {
                    sb.Append("<" + k + "><![CDATA[" + v + "]]></" + k + ">");
                }

            }
            sb.Append("</xml>");
            return sb.ToString();
        }

        private Dictionary<string, string> ParseXml(string content)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);
            xmlDocument.DocumentElement.ChildNodes.Cast<XmlNode>().ToList().ForEach(item => dic[item.Name] = item.InnerText);

            return dic;
        }

        public bool CheckSign(Dictionary<string, string> dic)
        {
            string paySign = CreateMd5Sign(dic, WechatPayAppSetting.WeiXinPaySecret);

            if (dic["sign"] == paySign)
            {
                return true;
            }

            return false;
        }

        private bool CheckSign(string content)
        {
            Dictionary<string, string> dic = ParseXml(content);
            return CheckSign(dic);
        }

        private void BeginLog(Yw_PayLog log, int orderId, string orderNo, PayLogStepEnum step, string data)
        {
            log.Ypg_OrderId = orderId;
            log.Ypg_OrderNo = orderNo;
            log.Ypg_PayStep = (int)step;
            log.Ypg_RequestData = data;
            log.Ypg_RequestTime = DateTime.Now;
        }

        private void EndLog(Yw_PayLog log, string data, int status)
        {
            log.Ypg_ResponseData = (log.Ypg_ResponseData ?? "") + data;
            log.Ypg_ResponseTime = DateTime.Now;
            log.Ypg_Status = status;
        }

        private void SaveLog(Yw_PayLog log)
        {
            if (log != null)
            {
                StudentOrderBll orderBll = new StudentOrderBll();
                orderBll.PayLog(log);
            }
        }
    }
}
