using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Bll;
using System.Text;
using System.Security.Cryptography;

namespace AbhsChinese.Test
{
    [TestClass]
    public class CourseSubjectRelBllTest
    {
        [TestMethod]
        public void AddSubjectToLessonTest()
        {
            Yw_CourseLesson lesson = new Yw_CourseLesson();
            lesson.Ycl_Id = 20000;
            lesson.Ycl_CourseId = 20000;
            lesson.Ycl_Index = 1;

            CourseSubjectRelBll bll = new CourseSubjectRelBll();
            bll.AddSubjectToLesson(lesson, 10000);
        }

        [TestMethod]
        public void RemoveSubjectFromLessonTest()
        {
            CourseSubjectRelBll bll = new CourseSubjectRelBll();
            bll.RemoveSubjectFromLesson(20000, 10000);
        }

        [TestMethod]
        public void AddSubjectToGroupTest()
        {
            CourseSubjectRelBll bll = new CourseSubjectRelBll();
            bll.AddSubjectToGroup(10004, 10000);
        }

        [TestMethod]
        public void RemoveSubjectFromGroup()
        {
            CourseSubjectRelBll bll = new CourseSubjectRelBll();
            bll.RemoveSubjectFromGroup(10004, 10000);
        }

        [TestMethod]
        public void MD5Test()
        {
            string md5text = MD5Hash("123456");
        }

        public string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
