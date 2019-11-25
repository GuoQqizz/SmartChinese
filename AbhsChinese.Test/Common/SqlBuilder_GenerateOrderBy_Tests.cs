using AbhsChinese.Code.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AbhsChinese.Test.Common
{
    [TestClass]
    public class SqlBuilder_GenerateOrderBy_Tests
    {
        [TestMethod]
        public void SqlBuilder_GenerateOrderBy_ShouldReturnValue()
        {
            List<int> ids = new List<int> { 10048, 10049 };
            string parameters = string.Empty;
            string result = SqlBuilder.GenerateOrderBy(ids, "Bem_Id", out parameters);
            Assert.IsNotNull(result);
        }
    }
}