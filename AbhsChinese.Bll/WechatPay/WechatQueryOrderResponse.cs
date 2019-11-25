using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll.WechatPay
{
    public class WechatQueryOrderResponse
    {
        /// <summary>
        /// 返回状态码
        /// SUCCESS/FAIL
        /// 此字段是通信标识，非交易标识，交易是否成功需要查看trade_state来判断
        /// </summary>
        public string ReturnCode
        {
            set;
            get;
        }
        /// <summary>
        /// 返回信息
        /// 当return_code为FAIL时返回信息为错误原因 ，例如：签名失败、参数格式校验错误
        /// </summary>
        public string ReturnMsg
        {
            set;
            get;
        }
        /// <summary>
        /// 公众账号ID
        /// 微信分配的公众账号ID
        /// </summary>
        public string AppId
        {
            set;
            get;
        }
        /// <summary>
        /// 商户号
        /// 微信支付分配的商户号
        /// </summary>
        public string MchId
        {
            set;
            get;
        }
        /// <summary>
        /// 随机字符串，不长于32位。
        /// </summary>
        public string NonceStr
        {
            set;
            get;
        }
        /// <summary>
        /// 签名
        /// </summary>
        public string Sign
        {
            set;
            get;
        }
        /// <summary>
        /// 业务结果
        /// SUCCESS/FAIL
        /// </summary>
        public string ResultCode
        {
            set;
            get;
        }
        //public string ResultMsg
        //{
        //    set;
        //    get;
        //}
        /// <summary>
        /// 错误代码
        /// 当result_code为FAIL时返回错误代码，详细参见错误列表
        /// </summary>
        public string ErrCode
        {
            set;
            get;
        }
        /// <summary>
        /// 错误代码描述
        /// 当result_code为FAIL时返回错误描述，详细参见错误列表
        /// </summary>
        public string ErrCodeDes
        {
            set;
            get;
        }
        /// <summary>
        /// 设备号
        /// 微信支付分配的终端设备号
        /// 非必填
        /// </summary>
        public string DeviceInfo { get; set; }
        /// <summary>
        /// 用户标识
        /// 用户在商户appid下的唯一标识
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 是否关注公众账号
        /// 用户是否关注公众账号，Y-关注，N-未关注
        /// </summary>
        public string IsSubscribe { get; set; }
        /// <summary>
        /// 交易类型
        /// 调用接口提交的交易类型，取值如下：JSAPI，NATIVE，APP，MICROPAY，详细说明见参数规定
        /// </summary>
        public string TradeType { get; set; }
        /// <summary>
        /// 交易状态
        /// SUCCESS—支付成功
        /// REFUND—转入退款
        /// NOTPAY—未支付
        /// CLOSED—已关闭
        /// REVOKED—已撤销（付款码支付）
        /// USERPAYING--用户支付中（付款码支付）
        /// PAYERROR--支付失败(其他原因，如银行返回失败)
        /// 支付状态机请见下单API页面
        /// </summary>
        public string TradeState { get; set; }
        /// <summary>
        /// 付款银行
        /// 银行类型，采用字符串类型的银行标识
        /// </summary>
        public string BankType { get; set; }
        /// <summary>
        /// 标价金额
        /// 订单总金额，单位为分
        /// </summary>
        public int TotalFee { get; set; }
        /// <summary>
        /// 应结订单金额
        /// 当订单使用了免充值型优惠券后返回该参数，应结订单金额=订单金额-免充值优惠券金额。
        /// </summary>
        public int SettlementTotalFee { get; set; }
        /// <summary>
        /// 标价币种
        /// 货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY，其他值列表详见货币类型
        /// </summary>
        public string FeeType { get; set; }
        /// <summary>
        /// 现金支付金额
        /// 现金支付金额订单现金支付金额，详见支付金额
        /// </summary>
        public int CashFee { get; set; }
        /// <summary>
        /// 现金支付币种
        /// 货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY，其他值列表详见货币类型
        /// </summary>
        public string CashFeeType { get; set; }
        /// <summary>
        /// 代金券金额
        /// “代金券”金额&lt;=订单金额，订单金额-“代金券”金额=现金支付金额，详见支付金额
        /// </summary>
        public int CouponFee { get; set; }
        /// <summary>
        /// 代金券使用数量
        /// </summary>
        public int CouponCount { get; set; }

        //coupon_type_$n
        //coupon_id_$n
        //coupon_fee_$n

        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string TransactionId { get; set; }
        /// <summary>
        /// 商户订单号
        /// 商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 附加数据，原样返回
        /// </summary>
        public string Attach { get; set; }
        /// <summary>
        /// 支付完成时间
        /// 订单支付时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。其他详见时间规则
        /// </summary>
        public string TimeEnd { get; set; }
        /// <summary>
        /// 交易状态描述
        /// 对当前查询订单状态的描述和下一步操作的指引
        /// </summary>
        public string TradeStateDesc { get; set; }
    }
}
