using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request.StudyTask;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class StudentTaskRepository_GetEntities_Tests
    {
        [TestMethod]
        public void StudentTaskRepository_GetEntities_ShouldReturn()
        {
            var search = new DtoStudyTaskSearch
            {
                Pagination = new PagingObject(1, 10),
                StudentId = 10011,
                TaskType = StudyTaskTypeEnum.系统课后任务,
                TaskStatus = "1"
            };

            var result = new StudentTaskRepository().GetEntities(search);

            Assert.IsNotNull(result);
        }
    }
}
