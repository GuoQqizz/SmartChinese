using AbhsChinese.Bll;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class SubjectGroupBll_CancelRelation_Tests
    {
        [TestMethod]
        public void SubjectGroupBll_CancelRelation_ShouldSuccess()
        {
            var bll = new SubjectGroupBll();
            bll.CancelRelation(10002, 10003);
            var result = bll.GetBySubjectIds(new int[] { 10002, 10003 });

            foreach (var item in result)
            {
                if (item.Ysg_SubjectId == 10002)
                {
                    Assert.AreEqual("10000,10001", item.Ysg_RelSubjectId);
                }
                else
                {
                    Assert.AreEqual("10000,10001", item.Ysg_RelSubjectId);
                }
            }
        }
    }
}