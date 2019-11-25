using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class CourseRepository_GetByPage_Tests
    {
        [TestMethod]
        public void CourseRepository_GetByPage_ShouldReturnValue()
        {
            var search = new DtoCurriculumSearch();
            var result = new CourseRepository().GetByPage(search);

            Assert.IsNotNull(result);
        }
    }
}