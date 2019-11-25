using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Bll;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class ReadRecordBllTest
    {
        [TestMethod]
        public void SaveAndCalculateRankPercentTest()
        {
            ReadRecordBll bll = new ReadRecordBll();
            int x = bll.SaveAndCalculateRankPercent(10073, 10000, 10000, 10000, "10090", 92, "http://www.sina.com.cxn.95");

            int y = bll.SaveAndCalculateRankPercent(10021, 10000, 10000, 10000, "10000", 10, "http://www.sina.com.cnx");

            int z = bll.SaveAndCalculateRankPercent(10032, 10000, 10000, 10000, "10000", 15, "http://www.sina.com.cnx");

            int a = bll.SaveAndCalculateRankPercent(10051, 10000, 10000, 10000, "10001", 40, "http://www.sina.com.cnx");

            int b = bll.SaveAndCalculateRankPercent(10061, 10000, 10000, 10000, "10001", 30, "http://www.sina.com.cnx");
        }
    }
}
