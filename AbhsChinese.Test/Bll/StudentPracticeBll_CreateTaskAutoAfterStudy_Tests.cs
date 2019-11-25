using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Bll;
using AbhsChinese.Domain.Entity;
using System.Collections.Generic;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class StudentPracticeBll_CreateTaskAutoAfterStudy_Tests
    {
        StudentPracticeBll bll = new StudentPracticeBll();
        [TestMethod]
        public void StudentPracticeBll_CreateTaskAutoAfterStudy_ShouldSuccess()
        {
            Yw_StudentCourseProgress progress = new Yw_StudentCourseProgress
            {
                Yps_ClassId = 10000,
                Yps_CourseId = 10000,
                Yps_CreateTime = DateTime.Now,
                Yps_FinishStudyTime = DateTime.Now,
                Yps_Id = 0,
                Yps_IsFinished = false,
                Yps_LastStudyTime = DateTime.Now,
                Yps_LessonFinishedCount = 20,
                Yps_NextLessonIndex = 5,
                Yps_OrderId = 10000,
                Yps_SchoolId = 11000,
                Yps_StartStudyTime = DateTime.Now,
                Yps_Status = 3,
                Yps_StudentId = 10011,
                Yps_UpdateTime = DateTime.Now
            };

            bll.CreateStudyPractice(
                progress, 
                10011, 
                10000,
                4, 
                10000,
                new List<int> { 10000 }, 
                StudyTaskTypeEnum.系统课后任务);
            //bll.CreateTaskAutoAfterStudy(10011, 10008);

        }
    }
}
