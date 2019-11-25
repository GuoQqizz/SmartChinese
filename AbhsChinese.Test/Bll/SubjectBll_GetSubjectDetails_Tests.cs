using AbhsChinese.Bll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AbhsChinese.Test.Bll
{
    /// <summary>
    /// SubjectBll_GetSubjectDetails_Tests 的摘要说明
    /// </summary>
    [TestClass]
    public class SubjectBll_GetSubjectDetails_Tests
    {
        [TestMethod]
        public void SubjectBll_GetSubjectDetails_ShouldReturnAllInfo()
        {
            SubjectBll bll = new SubjectBll();
            var result = bll.GetSubjectDetails(10000);
            Assert.IsNotNull(result);
            Console.WriteLine(result.ToJson());
        }
    }
}