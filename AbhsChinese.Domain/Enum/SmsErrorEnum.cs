using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Enum
{
    public enum SmsErrorEnum
    {
        [Description("出错了，请稍后再试")]
        SystemError = 10000,
        [Description("无效的电话号码")]
        PhoneInvalid = 10001,
        [Description("发送短消息过于频繁")]
        SendSmsTooOften = 10002,
        [Description("操作过于频繁")]
        RegTrailAccountTooOfter = 10003,
        [Description("验证码不正确")]
        PhoneCodeFault = 10004,
        [Description("手机号已注册")]
        PhoneRegistered = 10005,
        [Description("手机号未注册")]
        PhoneNoRegister = 10006,

        [Description("图文验证码错误")]
        AuthCodeError = 10007,
        [Description("输入不能为空")]
        InputNotNull =10008
    }
}
