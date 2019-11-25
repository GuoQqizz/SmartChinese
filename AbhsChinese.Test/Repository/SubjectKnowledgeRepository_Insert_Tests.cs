using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class SubjectKnowledgeRepository_Insert_Tests
    {
        [TestMethod]
        public void SubjectKnowledgeRepository_Insert_ShouldSuccess()
        {
            SubjectKnowledgeRepository repository = new SubjectKnowledgeRepository();
            DateTime now = DateTime.Now;

            List<Yw_SubjectKnowledge> l = new List<Yw_SubjectKnowledge>();
            l.Add(new Yw_SubjectKnowledge()
            {
                Ysw_Id = 0,
                Ysw_CreateTime = now,
                Ysw_Creator = 10000,
                Ysw_Editor = 10000,
                Ysw_UpdateTime = now,
                Ysw_SubjectId = 10005,
                Ysw_IsMain = true
            });

            for (int i = 0; i < 2; i++)
            {
                l.Add(new Yw_SubjectKnowledge()
                {
                    Ysw_Id = 0,
                    Ysw_CreateTime = now,
                    Ysw_Creator = 10000,
                    Ysw_Editor = 10000,
                    Ysw_UpdateTime = now,
                    Ysw_SubjectId = 10005,
                    Ysw_IsMain = false
                });
            }

            repository.Insert(l);

            foreach (var addedEntity in l)
            {
                Assert.IsNotNull(addedEntity);
                Assert.IsTrue(addedEntity.Ysw_Id > 0);
                Assert.AreEqual(10005, addedEntity.Ysw_SubjectId);
            }
        }
    }
}