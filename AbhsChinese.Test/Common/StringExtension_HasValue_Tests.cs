using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbhsChinese.Test.Common
{
    [TestClass]
    public class StringExtension_HasValue_Tests
    {
        [TestMethod]
        public void StringExtension_HasValue_ShouldSuccess()
        {
            string str = null;
            bool res = str.HasValue();
            Assert.IsFalse(res);
        }
    }
}
