using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class SubjectRepository_Insert_Tests
    {
        [TestMethod]
        public void SubjectRepository_Insert_ShouldSuccess()
        {
            SubjectRepository repository = new SubjectRepository();
            DateTime now = DateTime.Now;
            Yw_Subject addedEntity = repository.Insert(new Yw_Subject()
            {
                Ysj_Id = 0,
                Ysj_CreateTime = now,
                Ysj_Creator = 1000,
                Ysj_Difficulty = (int)DifficultyEnum.一般,
                Ysj_Editor = 1000,
                Ysj_Grade = (int)GradeEnum.一年级,
                Ysj_Keywords = "keywords",
                Ysj_Name = "danxuanti",
                Ysj_Status = (int)SubjectStatusEnum.编辑中,
                Ysj_SubjectType = (int)SubjectTypeEnum.选择题,
                Ysj_UpdateTime = now
            });

            Assert.IsNotNull(addedEntity);
            Assert.IsTrue(addedEntity.Ysj_Id > 0);
        }
    }
}