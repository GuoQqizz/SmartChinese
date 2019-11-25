using AbhsChinese.Admin.Controllers;
using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Web.Controllers;
using AbhsChinese.Web.Models.Subjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AbhsChinese.Test.Controller
{
    [TestClass]
    public class StudyTaskController_GetSubjectsToPractice_Tests
    {
        [TestMethod]
        public void StudyTaskController_GetSubjectsToPractice_ShouldReturn()
        {
            StudyTaskController controller = new StudyTaskController();

            var result = controller.GetSubjectsToPractice(10004, 1) as JsonResult;

            Assert.IsNotNull(result);
            var subjects = result.Data as List<SubjectVm>;
            foreach (var item in subjects)
            {
                Console.WriteLine(item.ToJson());
            }
        }
    }
}