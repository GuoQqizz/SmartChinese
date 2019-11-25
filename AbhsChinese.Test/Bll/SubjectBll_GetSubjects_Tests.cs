using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Domain.Dto.Request.Subject;
using AbhsChinese.Bll;
using AbhsChinese.Domain.Enum;
using System.Linq;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class SubjectBll_GetSubjects_Tests
    {
        private readonly SubjectBll bll = new SubjectBll();
        [TestMethod]
        public void SubjectBll_GetSubjects_GetByIdShouldReturnValue()
        {
            var search = new DtoQuestionSearch() { Id = 10098 };
            var result = bll.GetSubjects(search);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1);
        }
        [TestMethod]
        public void SubjectBll_GetSubjects_GetByDifficultyAndStatusShouldReturnValue()
        {
            var search = new DtoQuestionSearch()
            {
                Difficulty = DifficultyEnum.较易,
                SubjectStatus = SubjectStatusEnum.不合格
            };
            var result = bll.GetSubjects(search);
            Assert.IsNotNull(result);
            foreach (var item in result)
            {
                if (item.Ysj_Status == (int)SubjectStatusEnum.不合格
                    && item.Ysj_Difficulty == (int)DifficultyEnum.较易)
                {
                    continue;
                }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]

        public void SubjectRepository_GetByPage_GetByKeywordShouldReturnValue()
        {
            int subjectId = 10049;
            var search = new DtoQuestionSearch()
            {
                Keyword = "sfsfsfsddd"
            };
            var result = bll.GetSubjects(search);
            Assert.IsNotNull(result);
            Assert.AreEqual(subjectId, result.FirstOrDefault()?.Ysj_Id);
        }
    }
}
