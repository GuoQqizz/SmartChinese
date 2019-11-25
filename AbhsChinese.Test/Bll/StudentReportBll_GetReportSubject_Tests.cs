using AbhsChinese.Bll;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class StudentReportBll_GetReportSubject_Tests
    {
        [TestMethod]
        public void StudentReportController_GetReportSubject_ShouldReturnValue()
        {
            int studentId = 0;
            int taskId = 0;
            StudentReportBll bll = new StudentReportBll();

            var result = bll.GetReportSubject(studentId, taskId, 1);

            Assert.IsNotNull(result);
        }
    }
}