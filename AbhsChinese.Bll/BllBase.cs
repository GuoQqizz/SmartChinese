using AbhsChinese.Code.Cache;
using System;

namespace AbhsChinese.Bll
{
   public abstract class BllBase
    {

        public BllBase()
        {
        }

        public virtual void InitTran(string tranId)
        {
            this.tranId = tranId;
        }

        protected string tranId;
        public void BeginTran()
        {
            if (string.IsNullOrEmpty(tranId))
            {
                tranId = Guid.NewGuid().ToString();
            }
        }

        public void CommitTran()
        {
            CacheHelper.NotifyRefreshCacheForTranCommit(tranId);
            tranId = "";
        }

        public void RollbackTran()
        {
            tranId = "";
        }
    }
}
