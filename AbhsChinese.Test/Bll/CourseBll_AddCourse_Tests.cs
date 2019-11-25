using AbhsChinese.Bll;
using AbhsChinese.Domain.Dto.Request;
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
    public class CourseBll_AddCourse_Tests
    {
        [TestMethod]
        public void CourseBll_AddCourse_ShouldSuccess()
        {
            var currentUser = 10000;
            CourseBll bll = new CourseBll();
            DtoCourse c = new DtoCourse
            {
                CourseType = 2,
                CurrentUser = currentUser,
                Description = "d",
                Grade = 1,
                LessonCount = 3,
                Lessons = new List<string> { "1", "3" },
                Name = "sfsf",
                Owner = currentUser,
                ResourceGroupId = 2,
                Employees = "1,3"
            };
            bll.AddCourse(c);
            Assert.IsTrue(true);
        }
    }
}
