using AbhsChinese.Domain.Dto.Request.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class SubjectRepository_GetByPage_Tests
    {
        [TestMethod]
        public void SubjectRepository_GetByPage_GetByDifficultyShouldReturnValue()
        {
            SubjectRepository repository = new SubjectRepository();
            var search = new DtoQuestionSearch() { Difficulty = DifficultyEnum.较易 };
            var result = repository.GetByPage(search);
            Assert.IsNotNull(result);
            foreach (var item in result)
            {
                if (item.Ysj_Difficulty != (int)DifficultyEnum.较易)
                {
                    Assert.Fail();
                }
            }
        }
        [TestMethod]
        public void SubjectRepository_GetByPage_GetByStatusShouldReturnValue()
        {
            SubjectRepository repository = new SubjectRepository();
            var search = new DtoQuestionSearch() { SubjectStatus = SubjectStatusEnum.不合格 };
            var result = repository.GetByPage(search);
            Assert.IsNotNull(result);
            foreach (var item in result)
            {
                if (item.Ysj_Status != (int)SubjectStatusEnum.不合格)
                {
                    Assert.Fail();
                }
            }
        }

        
    }
}