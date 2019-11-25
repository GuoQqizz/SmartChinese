using AbhsChinese.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbhsChinese.Test.Repository
{
    [TestClass()]
    public class CourseLessonRepository_GetLessonWithProcessInfo_Tests
    {
        [TestMethod()]
        public void CourseLessonRepository_GetLessonsWithProcessInfo_ShouldSuccess()
        {
            CourseLessonRepository repository = new CourseLessonRepository();
            var result = repository.GetLessonsWithProcessInfo(10014);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1);
        }


        [TestMethod()]
        public void CourseLessonRepository_GetCourseCountByStudnet_ShouldSuccess()
        {
            StudentCourseProgressRepository repository = new StudentCourseProgressRepository();
            var result = repository.GetCourseCountByStudnet(10004);
          
        }
    }
}