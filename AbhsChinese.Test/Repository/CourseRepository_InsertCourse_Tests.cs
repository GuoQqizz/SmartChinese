using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Repository.Repository;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Code.Common;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class CourseRepository_InsertCourse_Tests
    {
        [TestMethod]
        public void CourseRepository_InsertCourse_ShouldSuccess()
        {
            CourseRepository r = new CourseRepository();
            var entity = new Yw_Course
            {
                Ycs_Creator = 10000,
                Ycs_Description = "desc",
                Ycs_CourseType = 0,
                Ycs_CreateTime = Clock.Now,
                Ycs_Id = 0,
                Ycs_CoverImage = "",
                Ycs_Editor = 10000,
                Ycs_Employees = "",
                Ycs_Grade = 3,
                Ycs_LessonCount = 3,
                Ycs_Name = "55",
                Ycs_Owner = 10000,
                Ycs_PublishTime = Clock.MinValue,
                Ycs_ResourceGroupId = 5,
                Ycs_SellCount = 7,
                Ycs_Status = 7,
                Ycs_UpdateTime = Clock.Now

            };
            r.InsertCourse(entity);
            Assert.IsTrue(entity.Ycs_Id > 0);
        }
    }
}
