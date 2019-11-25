using AbhsChinese.Bll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class StudentBllTest
    {
        [TestMethod]
        public void RefreshStudentRecentSubjectTest()
        {
            StudentBll bll = new StudentBll();
            List<int> subjectIds = new List<int>();
            subjectIds.Add(10208);
            subjectIds.Add(10004);
            //for (int index = 1; index <= 2; index++)
            //{
            //    subjectIds.Add(10205 + index);

            //}

            bll.RefreshStudentRecentSubject(10000, subjectIds);
        }
    }
}
