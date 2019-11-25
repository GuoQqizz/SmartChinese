using AbhsChinese.Admin.Controllers;
using AbhsChinese.Admin.Models.Question;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AbhsChinese.Test.Controller
{
    [TestClass]
    public class QuesionController_SaveQuestion_Test
    {
        private readonly MultipleChoice question = new MultipleChoice
        {
            Id = 0,
            //OptionA = new List<string> { "a-zhcy", "b-zhcy" },
            //Display = 1,
            Explain = "ex-zhcy",
            Name = "xuanzti-zhcy"
        };

        [TestMethod]
        public void QuesionController_SaveQuestion_FirstSave()
        {
            QuestionController controller = new QuestionController();

            ViewResult result = null;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void QuesionController_SaveQuestion_SaveAgain()
        {
            QuestionController controller = new QuestionController();
            question.Name = "update";
            question.Id = 10006;
            //question.Answer = new List<string> { "update", "update2" };

            ViewResult result = null;//controller.SaveQuestion(question) as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}