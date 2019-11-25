using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Domain.JsonTranslator;

namespace AbhsChinese.Test
{
    [TestClass]
    public class UeditorContentFactory_RestoreContent_Tests
    {
        [TestMethod]
        public void UeditorContentFactory_RestoreContent_ReturnFullPath()
        {
            string name = "<p><img src=\"http://abhstest.oss-cn-beijing.aliyuncs.com/QuestionDatabase/298e6150c1174043b655967dd4430099.jpg\" /></p>";
            string content = "/QuestionDatabase/298e6150c1174043b655967dd4430099.jpg";
            string url = UeditorContentFactory.RestoreContent(content, Domain.Enum.UeditorType.Image);
            Assert.AreEqual(name, url);
        }
    }
}
