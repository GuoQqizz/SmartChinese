using AbhsChinese.Code.Common;
using AbhsChinese.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AbhsChinese.Test.Common
{
    [TestClass]
    public class EmployeeRepository_GetEntities_Tests
    {
        [TestMethod]
        public void EmployeeRepository_GetEntities_ShouldReturnValue()
        {
            List<int> ids = new List<int> { 10048, 10049 };

            var result = new EmployeeRepository().GetEntities(ids);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2);
        }
    }
}