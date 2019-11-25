using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Enum
{
    public enum OrderTypeEnum
    {
        订单 = 1,
        退订单 = 2
    }

    public enum StudentOrderStatus
    {
        待支付 = 1,
        支付中 = 2,
        已支付 = 3,
        已取消 = 4,
        退款中 = 5,
        已退款 = 6,
        拒绝退款 = 7
    }

    public enum StudentOrderPayStatus
    {
        未支付 = 1,
        已支付 = 2,
        系统取消 = 3
    }

    public enum StudentOrderActionEnum
    {
        创建订单 = 1,
        订单支付 = 2,
        重新支付 = 3,
        过期取消 = 4,
        支付完成 = 5,
        发起退款 = 6,
        退款完成 = 7,
        拒绝退款 = 8
    }

    /// <summary>
    /// 优惠券类型
    /// </summary>
    public enum VoucherTypeEnum
    {
        全场券 = 1,
        成单券 = 2,
        注册券 = 4,
        校区券 = 8
    }
    /// <summary>
    /// 付款方式
    /// </summary>
    public enum CashPayTypeEnum
    {
        微信 = 1
    }
    /// <summary>
    /// 交易记录付款方式，Yw_StudentOrderPay表Yop_PayType字段。
    /// 100+CashPayTypeEnum：现金支付
    /// 200+VoucherTypeEnum：优惠券支付
    /// <see cref="AbhsChinese.Domain.Enum.CashPayTypeEnum"/>
    /// <see cref="AbhsChinese.Domain.Enum.VoucherTypeEnum"/>
    /// </summary>
    public enum StudentOrderPaymnentTypeEnum
    {
        微信支付 = 101,

        优惠券_全场券_支付 = 201,
        优惠券_成单券_支付 = 202,
        优惠券_注册券_支付 = 204,
        优惠券_校区券_支付 = 208
    }

    public enum VoucherGotTypeEnum
    {
        注册领取 = 1,
        个人领取 = 2,
        成单领取 = 3,
    }

    public enum PayLogStepEnum
    {
        生成微信预订单 = 1,
        微信支付回调 = 2,
        微信关闭订单 = 3,
        微信交易查询 = 4
    }

    public enum StudentVoucherStatusEnum
    {
        未使用 = 1,
        已使用 = 2,
        //已过期 = 3
    }

    public enum CoinTranTypeEnum
    {
        课堂学习 = 1,
        课后任务 = 2,
        课后练习 = 3
    }
}
