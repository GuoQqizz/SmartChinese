using AbhsChinese.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class CoursePriceRepository_GetEntities_Tests
    {
        [TestMethod]
        public void CoursePriceRepository_GetEntites_ShouldReturnValue()
        {
            var paras = new List<int> { 10018, 10019 };
            var result = new CoursePriceRepository().GetEntities(paras);

            Assert.IsNotNull(result);
        }
    }
}
