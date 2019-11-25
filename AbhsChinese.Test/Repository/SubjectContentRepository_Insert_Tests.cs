using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class SubjectContentRepository_Insert_Tests
    {
        [TestMethod]
        public void SubjectContentRepository_Insert_ShouldSuccess()
        {
            SubjectContentRepository repository = new SubjectContentRepository();
            DateTime now = DateTime.Now;
            Yw_SubjectContent addedEntity = repository.Insert(new Yw_SubjectContent()
            {
                Ysc_SubjectId = 90000,
                Ysc_CreateTime = now,
                Ysc_Creator = 1000,
                Ysc_Editor = 1000,
                Ysc_SubjectType = (int)SubjectTypeEnum.选择题,
                Ysc_UpdateTime = now,
                Ysc_Answer = "answer-zhcy",
                Ysc_Content = "content-zhcy",
                Ysc_Explain = "explain-zhcy"
            });

            Assert.IsNotNull(addedEntity);
            Assert.AreEqual(90000, addedEntity.Ysc_SubjectId);
        }
    }
}