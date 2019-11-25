using AbhsChinese.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class SubjectIndexRepository_Delete_Tests
    {
        [TestMethod]
        public void SubjectIndexRepository_Delete_ShouldSuccess()
        {
            SubjectIndexRepository repository = new SubjectIndexRepository();
            var added = repository.Insert(new Domain.Entity.Yw_SubjectIndex
            {
                Ysi_Keyword = "key",
                Ysi_Difficulty = 2,
                Ysi_SubjectId = 10000,
                Ysi_CreateTime = DateTime.Now,
                Ysi_Creator = 10000
            });
            //var dbEntity = repository.GetEntity(added.Ysi_Id);
            //Assert.IsNotNull(dbEntity);
            //Assert.IsTrue(dbEntity.Ysi_Keyword == "key");
            //repository.Delete(
            //    new List<int> { added.Ysi_Id });

            //dbEntity = repository.GetEntity(added.Ysi_Id);
            //Assert.IsNull(dbEntity);
        }
    }
}