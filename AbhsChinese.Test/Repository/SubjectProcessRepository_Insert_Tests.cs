using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class SubjectProcessRepository_Insert_Tests
    {
        [TestMethod]
        public void SubjectRepository_Insert_ShouldSuccess()
        {
            SubjectProcessRepository repository = new SubjectProcessRepository();
            DateTime now = DateTime.Now;
            Yw_SubjectProcess addedEntity = repository.Insert(new Yw_SubjectProcess()
            {
                Ysp_Id = 0,
                Ysp_CreateTime = now,
                Ysp_Status = (int)SubjectStatusEnum.编辑中,
                Ysp_Mark = "mark-zhcy",
                Ysp_Action = (int)SubjectActionEnum.提交,
                Ysp_Operator = 10000,
                Ysp_Remark = "Remark-zhcy",
                Ysp_SubjectId = 10005
            });

            Assert.IsNotNull(addedEntity);
            Assert.IsTrue(addedEntity.Ysp_Id > 0);
            Assert.AreEqual(10005, addedEntity.Ysp_SubjectId);
        }
    }
}