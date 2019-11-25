using AbhsChinese.Bll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class TranScopeTest
    {
        [TestMethod]
        public void TestRollBack()
        {
            AdvertisingBll bll = new AdvertisingBll();

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    TestCommitSuccess();
                    TestCommitFalse();
                    bll.IncrementValidCount(10002, 10);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private void TestCommitSuccess()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                AdvertisingBll bll = new AdvertisingBll();
                bll.IncrementValidCount(10000, 10);
                scope.Complete();
            }
        }

        private void TestCommitFalse()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    AdvertisingBll bll = new AdvertisingBll();
                    bll.IncrementValidCount(10001, 10);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
