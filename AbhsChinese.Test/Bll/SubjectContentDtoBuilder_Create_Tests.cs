using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Domain.JsonTranslator;
using AbhsChinese.Domain.Dto.Response.Subject;
using AbhsChinese.Domain.Entity.Subject;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class SubjectContentDtoBuilder_Create_Tests
    {
        [TestMethod]
        public void SubjectContentDtoBuilder_Create_ShouldReturnCorrect()
        {
            Yw_SubjectContent content = null;
            Yw_Subject subject = null;
            DtoSubjectContent dto = SubjectContentDtoBuilder.Create(content, subject);

            Assert.IsNotNull(dto);
            Console.WriteLine(dto.ToJson());
        }
    }
}
