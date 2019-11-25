using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Repository.Repository;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Code.Common;
using AbhsChinese.Repository.IRepository;
using System.Collections.Generic;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class CourseLessonRespository_Insert_Tests
    {
        [TestMethod]
        public void CourseLessonRespository_Insert_ShouldSuccess()
        {
            var list = new List<string> { "1", "2" };
            CourseLessonRepository r = new CourseLessonRepository();

            var result = r.Insert(
                10009,
                new List<string> { "1", "2" },
                0,
                10000);
            Assert.IsTrue(result == list.Count);
        }
    }
}
