namespace AbhsChinese.Domain.Enum
{
    /// <summary>
    /// 系统错误类型
    /// </summary>
    public enum ErrorCodeEnum
    {
        /// <summary>
        /// 成功
        /// </summary>
        Ok = 0,
        NotImplementLogic = 10,
        /// <summary>
        /// 没有登陆信息
        /// </summary>
        NotHasLoginInfo = 100,
        /// <summary>
        /// 没有权限
        /// </summary>
        NoAuthorize = 101,
        /// <summary>
        /// 旧密码不匹配 
        /// </summary>
        PasswordsDiffer = 200,

        SmsCodeFail = 300,
        CourseCanNotBuy = 1001,
        CourseNotExists = 1010,
        /// <summary>
        /// 文件尺寸超出
        /// </summary>
        FileSizeOverFlow = 1100,

        /// <summary>
        /// 资源已经关联
        /// </summary>
        AlreadyExistResourceItem = 1100,

        StudentNotBindSchool = 1500,

        /// <summary>
        /// 未实现虚方法
        /// </summary>
        NotImplementingVirtualMethod = 5000,

        /// <summary>
        /// id取值范围不对
        /// </summary>
        IdOutOfRange = 60000,
        /// <summary>
        /// id取值范围不对
        /// </summary>
        RelationOutOfRange = 60001,
        /// <summary>
        /// Enum取值范围不对
        /// </summary>
        EnumOutOfRange = 60002,
        /// <summary>
        /// 未在数据库中找到数据
        /// </summary>
        NotFoundEntity = 60003,

        /// <summary>
        /// 人脸登录失败
        /// </summary>
        FaceLoginError = 400,
        /// <summary>
        /// 人脸注册失败
        /// </summary>
        FaceRegisterError = 401,

        BaiduSpeechError = 500,
        /// <summary>
        /// 已经拥有该现金券
        /// </summary>
        AlreadyHaveCashVoucher = 600,
        /// <summary>
        /// 现金券数量不足或已过期
        /// </summary>
        NotCashVoucherCount = 601,

        ParameterInvalid = 10001,

        ProcExecError = 10010,

        StudentCourseProgressNotFound = 12000,
        StudentTaskStatusInvalid = 13000,

        WechatPrePayError = 20001,
        WechatPayNotifyError = 20002,
        WechatPayCloseOrderError = 20003,
        WechatPayQueryOrderError = 20004,
        OrderPayOverError = 20008,

        OrderCancelError = 20010,
        /// <summary>
        /// 支付方式错误
        /// </summary>
        PayTypeError = 21000,

        BaiduTokenError = 30001,

        /// <summary>
        /// 不能重新打开
        /// </summary>
        CanNotReopen = 4000,
        /// <summary>
        /// 不能重复关联
        /// </summary>
        CanNotAddRelation = 4001,
        /// <summary>
        /// 不能自己和自己进行关联
        /// </summary>
        CanNotAddRelationToSelf = 4002,
        /// <summary>
        /// 没有学习报告
        /// </summary>
        NotStudyReport = 30000
    }
}