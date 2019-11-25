using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Domain.JsonTranslator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbhsChinese.Test
{
    [TestClass]
    public class UeditorContentFactory_Blank_Tests
    {
        [TestMethod]
        public void UeditorContentFactory_Blank_ReturnVirtualPath()
        {
            string name = "<p>sfsfsfssf<input type=\"button\" data-num=\"376\" onclick=\"parent.showDialog(this)\" value=\"答案\"/>sfsfssfsfsfsfsfsfsfs<input type=\"button\" data-num=\"736\" onclick=\"parent.showDialog(this)\" value=\"答案\"/>sfdd</p>";
            string result = UeditorContentFactory.Blank(name);
            if (result.Contains("<input type="))
            {
                Assert.Fail();
            }
        }
    }
}