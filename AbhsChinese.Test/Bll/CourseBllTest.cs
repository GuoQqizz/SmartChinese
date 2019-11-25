using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class CourseBllTest
    {
        [TestMethod]
        public void GetCourseByIdOrName()
        {
            var idOrName = "100";
            var dic = new CourseBll().GetCourseByIdOrName(idOrName);
            Assert.IsNotNull(dic);
            Assert.IsTrue(dic.Count > 0);


            idOrName = "测试1";
            dic = new CourseBll().GetCourseByIdOrName(idOrName);
            Assert.IsNotNull(dic);
            Assert.IsTrue(dic.Count == 0);

            string fileName = "1/2/3.jpg";
            string regStr = @"(\.png)|(\.jpg)";

            Regex reg = new Regex(regStr);

            Assert.IsTrue(reg.IsMatch(fileName));
            Assert.IsTrue(reg.IsMatch("1.jpg"));


        }
        [TestMethod]
        public void GetCourseInfo()
        {
            var info = new CourseBll().GetCourseInfo(10000);

        }

        [TestMethod]
        public void GetCourseForSelectCenterTest()
        {
            CourseBll bll = new CourseBll();
            DtoCourseSelectCondition condition = new DtoCourseSelectCondition();
            condition.StudentId = 10003;
            condition.Grade = 0;
            condition.CourseType = 0;
            condition.OrderBy = DtoCourseSelectCondition.DtoCourseSelectOrderEnum.Lastest;
            PagingObject paging = new PagingObject() { PageIndex = 1, PageSize = 20 };
            bool includePrice = false;
            List<DtoSelectCenterResult> result = bll.GetCourseForSelectCenter(condition, paging, out includePrice);

        }
    }
}
