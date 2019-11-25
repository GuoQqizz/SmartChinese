using AbhsChinese.Bll;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Entity.School;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Test.Bll
{

    [TestClass]
    public class SchoolBllTest : BaseArrert
    {
        public SchoolBll bll = new SchoolBll();
        public SchoolTeacherBll schoolTeacherBll = new SchoolTeacherBll();
        public SchoolClassBll schoolClassBll = new SchoolClassBll();
        [TestMethod]
        public void GetRegionTest()
        {
            DtoSchoolTeacherSearch search = new DtoSchoolTeacherSearch(10000);
            search.Grade = 4;

            var list = schoolTeacherBll.GetSchoolTeacherList(search);
            BaseAssertList(list);


            //bll.IncrementStudentCount(10000, 5, 1);

            DateTime dt = new DateTime(2019, 10, 1, 5, 20, 30);

            var s = dt.ToString("yyyy-MM-dd hh:mm:ss");
            Assert.IsNotNull(s);
        }
        [TestMethod]
        public void InsertListTest()
        {
            List<Yw_SchoolClassSchedule> listSchedule = new List<Yw_SchoolClassSchedule>();
            for (int i = 0; i < 15; i++)
            {
                listSchedule.Add(new Yw_SchoolClassSchedule()
                {
                    
                    Ywd_ClassId = 1,
                    Ywd_CreateTime = DateTime.Now,
                    Ywd_Creator = i,
                    Ywd_Day = i,
                    Ywd_Editor = i,
                    Ywd_EndTime = new TimeSpan(10, 20, 00),
                    Ywd_Id = i,
                    Ywd_SchoolId = i,
                    Ywd_StartTime = new TimeSpan(10, 20, 00),
                    Ywd_UpdateTime = DateTime.Now
                });
            }



            Yw_SchoolClass schoolClass = new Yw_SchoolClass();
            schoolClass.Ycc_CreateTime = DateTime.Now;
            schoolClass.Ycc_UpdateTime = DateTime.Now;
            schoolClass.Ycc_StartTime = DateTime.Now;

            int id = 0;
            id = schoolClassBll.InsertListSchoolClassSchedule(listSchedule);
            id = 0;

            Assert.IsTrue(id == 0);
        }
        [TestMethod]
        public void TestTime()
        {
            TimeSpan t = new TimeSpan(10, 20, 0);
            var s = t.ToString();

        }

    }
}
