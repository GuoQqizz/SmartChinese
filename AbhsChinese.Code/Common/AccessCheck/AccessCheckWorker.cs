using AbhsChinese.Code.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Code.Common.AccessCheck
{
    public class AccessCheckWorker
    {
        /// <summary>
        /// 用户访问控制策略的键
        /// </summary>
        public string AccessKey
        {
            set; get;
        }

        /// <summary>
        /// 用户标识(用户Id,Ip,OpenId,Token,...)
        /// </summary>
        public string Identity
        {
            set; get;
        }

        /// <summary>
        /// 监控周期（秒）
        /// </summary>
        public int CycleSeconds
        {
            set; get;
        }

        /// <summary>
        /// 次数限制
        /// </summary>
        public int LimitCount
        {
            set; get;
        }

        /// <summary>
        /// 启用冻结的次数限制（达到这个限制次数，就会启用冻结）
        /// </summary>
        public int ForzenLimitCount
        {
            set; get;
        }

        /// <summary>
        /// 冻结时间（秒）
        /// </summary>
        public int FrozenSeconds
        {
            set; get;
        }

        public bool Check()
        {
            AccessCheckEnum check = CheckAdvanced();
            if (check == AccessCheckEnum.Normal || check == AccessCheckEnum.PreLimited)
            {
                return true;
            }

            return false;
        }

        public AccessCheckEnum CheckAdvanced()
        {
            if (FrozenSeconds > 0 && ForzenLimitCount > 0)
            {
                string isLock = RedisCache.Instance.Get<string>(AccessKey + "_Lock_" + Identity);
                if (isLock == "TRUE")
                {
                    return AccessCheckEnum.Locked;
                }
            }

            long x = RedisCache.Instance.Increment(AccessKey + "_" + Identity, 1);
            if (x == 1)
            {
                RedisCache.Instance.Expire(AccessKey + "_" + Identity, CycleSeconds);
            }
            if (x > LimitCount)
            {
                if (FrozenSeconds > 0 && ForzenLimitCount > 0 && x > ForzenLimitCount)
                {
                    RedisCache.Instance.Set<string>(AccessKey + "_Lock_" + Identity, "TRUE", FrozenSeconds);
                    return AccessCheckEnum.Locked;
                }
                else if (FrozenSeconds > 0 && ForzenLimitCount > 0 && x == ForzenLimitCount)
                {
                    return AccessCheckEnum.PreLocked;
                }
                return AccessCheckEnum.Limited;
            }
            else if (x == LimitCount)
            {
                return AccessCheckEnum.PreLimited;
            }

            return AccessCheckEnum.Normal;
        }
    }
}
