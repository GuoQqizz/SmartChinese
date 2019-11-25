using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Domain.JsonTranslator;

namespace AbhsChinese.Test
{
    [TestClass]
    public class UeditorContentFactory_RestoreBlank_Tests
    {
        [TestMethod]
        public void UeditorContentFactory_RestoreBlank_ShouldReturnCorrect()
        {
            string content = "<p>sfs{:}sfsfs{:}sfsfs{:}sfsfs{:}{:}</p>";
            string result = UeditorContentFactory.RestoreBlank(content);
            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void UeditorContentFactory_RestoreBlank_ShouldReturnCorrect2()
        {
            string content = "<p>{:}{:}{:}{:}{:}</p>";
            string result = UeditorContentFactory.RestoreBlank(content);
            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }
    }
}
