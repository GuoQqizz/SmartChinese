using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class CoursePriceRepository_GetEntity_Tests
    {
        [TestMethod]
        public void CoursePriceRepository_GetEntity_ShouldReturnValue()
        {
            CoursePriceRepository repository = new CoursePriceRepository();
            var result = repository.GetEntity(10018, 10002);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Yce_Id == 10008);
        }
    }
}