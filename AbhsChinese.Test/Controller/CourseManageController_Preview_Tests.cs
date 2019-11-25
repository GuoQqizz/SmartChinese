using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Admin.Controllers;

namespace AbhsChinese.Test.Controller
{
    [TestClass]
    public class CourseManageController_Preview_Tests
    {
        [TestMethod]
        public void CourseManageController_Preview_ShouldReturnCorrect()
        {
            var result = new CourseManageController().Preview(0);
        }
    }
}
