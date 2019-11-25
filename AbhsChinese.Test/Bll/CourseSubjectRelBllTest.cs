using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Bll;
using AbhsChinese.Domain.Entity;
using System.Collections.Generic;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class CourseSubjectRelBllTest
    {
        [TestMethod]
        public void AddSubjectToLessonTestMethod()
        {
            CourseSubjectRelBll bll = new CourseSubjectRelBll();
            LessonBll lessonBll = new LessonBll();
            Yw_CourseLesson lesson = lessonBll.GetLesson(10023);

            bll.AddSubjectToLesson(lesson, 10005);
        }

        [TestMethod]
        public void RemoveSubjectFromLessonTest()
        {
            CourseSubjectRelBll bll = new CourseSubjectRelBll();
            bll.RemoveSubjectFromLesson(10000, 10000);
        }

        [TestMethod]
        public void AddSubjectToGroupTest()
        {
            CourseSubjectRelBll bll = new CourseSubjectRelBll();
            bll.AddSubjectToGroup(10005, 10000);
        }

        [TestMethod]
        public void RemoveSubjectFromGroupTest()
        {
            CourseSubjectRelBll bll = new CourseSubjectRelBll();
            bll.RemoveSubjectFromGroup(10005, 10000);
        }

        [TestMethod]
        public void AddSubjectToLessonTest()
        {
            LessonBll lessonBll = new LessonBll();
            Yw_CourseLesson lesson = lessonBll.GetLesson(10022);
            CourseSubjectRelBll bll = new CourseSubjectRelBll();
            bll.AddSubjectToLesson(lesson, new List<int> { 10042, 10043 });
      }
    }
}
