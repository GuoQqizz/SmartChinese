using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Request.Subject;
using AbhsChinese.Domain.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class CourseBll_Pricing_Tests
    {
        private readonly CourseBll bll = new CourseBll();

        [TestMethod]
        public void CourseBll_Pricing_ShouldSuccess()
        {
            var price = new DtoCoursePricing
            {
                Arrange = "arrange" + Clock.Now.ToString(),
                CourseId = 20000,
                Introduction = "Introduction" + Clock.Now.ToString(),
                NextStatus = CourseStatusEnum.待上架,
                Pricings = new List<DtoPricing>(),
                Sort = 2
            };
            bll.Pricing(price, 10000);
        }

        [TestMethod]
        public void CourseBll_Pricing_ShouldSuccess2()
        {
            var price = new DtoCoursePricing
            {
                Arrange = "arrange" + Clock.Now.ToString(),
                CourseId = 20000,
                Introduction = "Introduction" + Clock.Now.ToString(),
                NextStatus = CourseStatusEnum.待上架,
                Pricings = new List<DtoPricing>(),
                Sort = 2
            };
            bll.Pricing(price, 10000);
        }
    }
}