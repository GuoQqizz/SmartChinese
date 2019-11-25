using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AbhsChinese.Test.Extension
{
    [TestClass]
    public class ObjectExtensions_ToJson_Tests
    {
        [TestMethod]
        public void ObjectExtensions_ToJson_ShouldReturnNullValue()
        {
            int? i = null;
            var result = i.ToJson();
            Assert.IsNotNull(result);
        }
    }
}