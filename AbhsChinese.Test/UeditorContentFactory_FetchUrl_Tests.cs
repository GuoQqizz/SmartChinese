using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Domain.JsonTranslator;

namespace AbhsChinese.Test
{
    [TestClass]
    public class UeditorContentFactory_FetchUrl_Tests
    {
        [TestMethod]
        public void UeditorContentFactory_FetchUrl_ReturnVirtualPath()
        {
            string name = "<p><img src=\"http://abhstest.oss-cn-beijing.aliyuncs.com/QuestionDatabase/298e6150c1174043b655967dd4430099.jpg\" title=\"800x400.jpg\" alt=\"800x400.jpg\"/></p>";

            string url = UeditorContentFactory.FetchUrl(name, Domain.Enum.UeditorType.Image);
            if (url.Contains(".aliyuncs.com"))
            {
                Assert.Fail();
            }
        }
    }
}
