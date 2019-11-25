using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Repository.Repository;
using AbhsChinese.Domain.Dto.Request.Teachers;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class EmployeeRepository_GetEmployees_Tests
    {
        [TestMethod]
        public void EmployeeRepository_GetEmployees_ShouldOnlyReturnTeachers()
        {
            var result = new EmployeeRepository().GetEmployees(
                new DtoEmployeeSearch { Grade = 1, RoleCode = "teacher", Status = StatusEnum.有效 });

            Assert.IsNotNull(result);
            Console.WriteLine(result.ToJson());
        }
    }
}
