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
    public class SubjectBllTest
    {
        [TestMethod]
        public void GetCourseForSelectCenterTest()
        {
            List<int> list = new List<int>();
            for (int index = 1; index <= 200; index++)
            {
                list.Add(10000 + index);
            }
            SubjectBll bll = new SubjectBll();
            var x = bll.GetTaskSubjectsAutoForKnowledges(10062, list);
        }

        [TestMethod]
        public void GetLessonTaskSubjectTest()
        {
            List<Tuple<int,int>> list = new List<Tuple<int,int>>();
            for (int index = 1; index <= 200; index++)
            {
                list.Add(new Tuple<int, int>(10000 + index, 2));
            }
            SubjectBll bll = new SubjectBll();
            var x = bll.GetLessonTaskSubject(10062, list);
        }

        [TestMethod]
        public void GetSubjectForPracticeTest()
        {
            SubjectBll bll = new SubjectBll();
            var x = bll.GetSubjectForPractice(10003, 10001, 10, 20);
        }

        [TestMethod]
        public void TestGroupBy()
        {
            XClass c1 = new Bll.XClass() { Id = 1, Number = 30, Score = 100 };
            XClass c2 = new Bll.XClass() { Id = 1, Number = 20, Score = 100 };
            XClass c4 = new Bll.XClass() { Id = 2, Number = 60, Score = 101 };
            XClass c3 = new Bll.XClass() { Id = 2, Number = 40, Score = 101 };

            List<XClass> c = new List<XClass>();
            c.Add(c1);
            c.Add(c2);
            c.Add(c4);
            c.Add(c3);

            var xx = c.OrderBy(x => x.Number).GroupBy(x => new { Id = x.Id, Score = x.Score })
                .OrderBy(x => x.Key.Score).ToList();
            foreach (IGrouping<dynamic, XClass> item in xx)
            {
                int a = item.Key.Id;
                int b = item.Key.Score;
            }
        }
    }

    public class XClass
    {
        public int Id
        {
            set;get;
        }

        public int Score
        {
            set;get;
        }

        public int Number
        {
            set;get;
        }
    }

    public class XClassKey
    {
          public int Id
        {
            set;get;
        }

        public int Score
        {
            set;get;
        }
    }
}
