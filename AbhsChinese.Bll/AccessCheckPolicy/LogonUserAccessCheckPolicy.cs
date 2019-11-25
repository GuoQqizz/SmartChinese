using AbhsChinese.Code.Common.AccessCheck;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Bll.AccessCheckPolicy
{
    /// <summary>
    /// 已登录用户的访问频率限制策略，基于用户Id
    /// </summary>
    public class LogonUserAccessCheckPolicy
    {
        private AccessCheckWorker checkWorker
        {
            set; get;
        }

        public LogonUserAccessCheckPolicy(AccessCheckKeyEnum accessKey, string userId, int cycleSeconds, int limitCount, int frozenLimitCount = 0, int frozenSeconds = 0)
        {
            checkWorker = new AccessCheckWorker();
            checkWorker.AccessKey = accessKey.ToString();
            checkWorker.Identity = userId;
            checkWorker.CycleSeconds = cycleSeconds;
            checkWorker.LimitCount = limitCount;
            checkWorker.FrozenSeconds = frozenSeconds;
            checkWorker.ForzenLimitCount = frozenLimitCount;
        }

        public bool Check()
        {
            return checkWorker.Check();
        }

        public AccessCheckEnum CheckAdvanced()
        {
            return checkWorker.CheckAdvanced();
        }
    }
}
