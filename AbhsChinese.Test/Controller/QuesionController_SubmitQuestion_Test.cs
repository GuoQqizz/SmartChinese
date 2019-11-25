using AbhsChinese.Admin.Controllers;
using AbhsChinese.Admin.Models.Question;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AbhsChinese.Test.Controller
{
    [TestClass]
    public class QuesionController_SubmitQuestion_Test
    {
        [TestMethod]
        public void QuesionController_SubmitQuestion_SubmitAfterSaved()
        {
            QuestionController controller = new QuestionController();
            var question = new MultipleChoice
            {
                Id = 10000,
                //Answers = new List<string> { "a-zhcy-u", "b-zhcy-u" },

                //Display = 1,
                Explain = "ex-zhcy-u-u",
                Name = "xuanzti-zhcy",
            };
            ViewResult result = null;// controller.SubmitQuestion(question) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void QuesionController_SubmitQuestion_SubmitDirectory()
        {
            QuestionController controller = new QuestionController();
            var question = new MultipleChoice
            {
                Id = 0,
                //Answer = new List<string> { "a-zhcy", "b-zhcy" },

                Explain = "ex-zhcy",
                Name = "xuanzti-zhcy",
            };
            ViewResult result = null;//controller.SubmitQuestion(question) as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}