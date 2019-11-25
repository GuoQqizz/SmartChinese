using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class AdvertisingBllTest : BaseArrert
    {

        public AdvertisingBll bll = new AdvertisingBll();

        [TestMethod]
        public void TestIndex()
        {
            //var list = bll.GetAdvertisingForIndex();

            //BaseAssertList(list);

            bll.IncrementValidCount(10000, 1);
            bll.IncrementHitCount(10000, 1);

            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var t = (DateTime.Now.Ticks - dt.Ticks) / 10000;

            var s = Encrypt.EncryptQueryString("123");
            var s2 = Encrypt.DecryptQueryString(s);
            var i = s;


        }
        [TestMethod]
        public void TestInsert()
        {
            List<Bas_Advertising> list = new List<Bas_Advertising>();
            for (int i = 0; i < 15; i++)
            {
                list.Add(new Bas_Advertising()
                {

                    Bad_CreateTime = DateTime.Now,
                    Bad_Creator = i,
                    Bad_EndTime = DateTime.Now,
                    Bad_HitCount = 1,
                    Bad_Id = 1,
                    Bad_ImageUrl = "",
                    Bad_PosId = 1,
                    Bad_ReferId = 1,
                    Bad_Status = 1,
                    Bad_Url = "",
                    Bad_ValidCount = 1,
                });
            }
            var s = bll.InsertList(list);
        }
    }
}
