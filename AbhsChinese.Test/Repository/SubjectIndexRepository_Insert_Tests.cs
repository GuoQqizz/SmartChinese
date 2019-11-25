using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class SubjectIndexRepository_Insert_Tests
    {
        [TestMethod]
        public void SubjectIndexRepository_Insert_ShouldSuccess()
        {
            SubjectIndexRepository repository = new SubjectIndexRepository();
            DateTime now = DateTime.Now;
            Yw_SubjectIndex addedEntity = repository.Insert(new Yw_SubjectIndex()
            {
                Ysi_Id = 0,
                Ysi_CreateTime = now,
                Ysi_Creator = 10000,
                Ysi_Difficulty = (int)DifficultyEnum.一般,
                Ysi_Keyword = "key-zhcy",
                Ysi_SubjectId = 10005
            });

            Assert.IsNotNull(addedEntity);
            Assert.IsTrue(addedEntity.Ysi_Id > 0);
            Assert.AreEqual(10005, addedEntity.Ysi_SubjectId);
        }
    }
}