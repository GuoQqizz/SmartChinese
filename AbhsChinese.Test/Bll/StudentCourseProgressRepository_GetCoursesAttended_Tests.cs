using System;
using AbhsChinese.Bll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Repository.Repository;
using AbhsChinese.Domain.Dto.Request;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class StudentCourseProgressRepository_GetCoursesAttended_Tests
    {
        [TestMethod]
        public void StudentCourseProgressRepository_GetCoursesAttended_ShouldSuccess()
        {
            var search = new DtoCoursesSearch() { StudentId = 10011 };
            var result = new StudentCourseProgressRepository().GetCoursesAttended(search);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }


    }
}