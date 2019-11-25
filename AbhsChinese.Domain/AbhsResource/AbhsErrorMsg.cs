using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Domain.AbhsResource
{
    public class AbhsErrorMsg
    {
        public const string CanNotAddRelation = "该题目已经进行了关联";
        public const string CanNotReopen = "只有已关闭的课程才能重新打开";
        public const string ConstNoAuthorize = "没有此模块权限";
        public const string ConstNoAuthorizeViewOrder = "没有此订单权限";
        public const string ConstNotHasLoginInfo = "登录超时";
        public const string ConstParameterInvalid = "参数不正确";
        public const string ConstProcExecError = "执行存储过程失败{0}";
        public const string ConstNotImplementLogic = "逻辑没有实现";
        public const string ConstCourseCanNotBuy = "课程不能购买";
        public const string ConstCourseNotExists = "课程不存在";
        public const string ConstWechatPrePayError = "微信支付统一下单失败";
        public const string ConstWechatPayCloseOrderError = "微信支付关闭订单失败";
        public const string ConstWechatPayQueryOrderError = "微信支付查询订单失败";
        public const string ConstWechatPayNotifyError = "微信支付结果通知失败{0}";
        public const string ConstOrderPayOverError = "订单直接支付完成执行失败{0}"; 
        public const string ConstOrderCancelError = "订单取消失败"; 
        public const string ConstRestNotAvailable = "休息时间已被占用";
        public const string ConstScheduleNotAvailable = "排课时间已被占用";
        public const string ConstTeacherBindingNotFound = "学员没有绑定教师";
        public const string ConstUpgrade = "学生已经完成升级";
        public const string NotImplementingVirtualMethod = "未实现虚方法";
        public const string ConstFileSizeError = "文件尺寸超限";
        public const string ConstFaceRegisterError = "操作失败，请稍后再试";
        public const string ConstBaiduSpeechError = "请检查输入文本，稍后再试";
        public const string ConstBaiduTokenError = "Token过期，请检查获取更新";
        public const string ConstPasswordsDiffer = "旧密码输入错误";
        public const string ConstFaceLoginError = "登录超时，请稍后再试或尝试其他登录方式";
        public const string ConstStudentNotBindSchool = "学生没有绑定校区";
        public const string ConstAlreadyHaveCashVoucher = "已经领取过该现金券";
        public const string ConstNotCashVoucherCount = "现金券数量不足或已过期";
        public const string ConstAlreadyExistResourceItem = "已存在该关联项";
        public const string IdOutOfRange = "Id取值范围出错，Id应该不小于10000";
        public const string PayTypeError = "暂不支持此支付方式";
        public const string ConstSmsCodeFail = "验证码验证失败";
        public const string RelationOutOfRange = "题目关联应该成对出现";
        public const string EnumOutOfRange = "枚举值范围错误";
        public const string NotFoundEntity = "未在数据库中找到数据";
        public const string ConstStudentCourseProgressNotFound = "未找到学生课程进度{0}";
        public const string ConstNotStudyReport = "学生没有课程的报告";
        public const string ConstStudentTaskStatusInvalid = "学生任务状态不正确"; 
        public const string CanNotAddRelationToSelf = "不能自己和自己进行关联";

        /// <summary>
        /// 根据错误枚举获取错误提示
        /// </summary>
        /// <param name="e">错误枚举值</param>
        /// <param name="para">替换参数</param>
        /// <returns></returns>
        public static string GetErrorMsgByEnum(ErrorCodeEnum e, params string[] para)
        {
            switch (e)
            {
                case ErrorCodeEnum.NotHasLoginInfo: return ConstNotHasLoginInfo;
                case ErrorCodeEnum.NoAuthorize: return ConstNoAuthorize;
                case ErrorCodeEnum.ProcExecError: return string.Format(ConstProcExecError, para);
                default: return "未知错误";
            }
        }
    }
}