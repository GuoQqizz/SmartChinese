using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Repository.Repository;
using AbhsChinese.Domain.Dto.Request.StudyTask;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class StudentTaskRepository_GetTotalRecordByStatus_Tests
    {
        [TestMethod]
        public void StudentTaskRepository_GetTotalRecordByStatus_ShouldReturnCorrect()
        {
            var resp = new StudentTaskRepository();
            var result = resp.GetTotalRecordByStatus(new DtoStudyTaskSearch
            {
                StudentId = 10017,
                TaskType = StudyTaskTypeEnum.系统课后任务                
            });

            Assert.IsNotNull(result);
            Console.WriteLine(result.ToJson());
        }
    }
}
