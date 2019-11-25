using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Code.Cache;
using System.Collections.Generic;
using System.Linq;
using AbhsChinese.Admin.Models.Common;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AbhsChinese.Bll.AccessCheckPolicy;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Code.Common;
using System.Text;
using Newtonsoft.Json;

namespace AbhsChinese.Test
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void TestMethod1()
        {
            RedisCache.Instance.EnQueue("sicong test", "1");
            RedisCache.Instance.EnQueue("sicong test", "2");
            RedisCache.Instance.EnQueue("sicong test", "3");
            string sql = RedisCache.Instance.DeQueue("sicong test");
        }

        [TestMethod]
        public void TestMethod2()
        {
            string sql = RedisCache.Instance.DeQueue("sicong test");
        }

        [TestMethod]
        public void TestMethod3()
        {
            RedisCache.Instance.Set<int>("Access", 1, 10);
            int value = RedisCache.Instance.Get<int>("Access");
            RedisCache.Instance.Increment("Access", 1);
            value = RedisCache.Instance.Get<int>("Access");
            value = RedisCache.Instance.Get<int>("Access");
        }

        [TestMethod]
        public void TestMethodConcurrent()
        {
            RedisCache.Instance.Remove("Access1");
            List<Task> list = new List<Task>();

            for (int index = 1; index <= 100; index++)
            {
                list.Add(Task.Factory.StartNew(AddValueToRedis));
            }
            Task.WaitAll(list.ToArray());

            int value = RedisCache.Instance.Get<int>("Access1");

            value = RedisCache.Instance.Get<int>("Access1");
        }

        public void AddValueToRedis()
        {
            long x = RedisCache.Instance.Increment("Access1", 1);
            if (x == 1)
            {
                RedisCache.Instance.Expire("Access1", 10);
            }
        }

        [TestMethod]
        public void TestMethod5()
        {
            StringBuilder sb = new StringBuilder();
            for (int index = 1; index <= 50000; index++)
            {
                sb.Append("你");
            }
            string sbtext = sb.ToString();

            RedisCache.Instance.Set<string>("test", sbtext);

            string str = RedisCache.Instance.Get<string>("test");
        }

        [TestMethod]
        public void TestMethod6()
        {
            int x = 100;
            DateTime cacheTime = DateTime.Now;
            CacheHelper.NotifyRefreshCache(CacheDependencyEnum.Test, CacheDependencyActionType.Update, 1, "");
            CacheHelper.SetCache((object)x, CacheKeyEnum.Test_Cache, cacheTime, 1);
            object y = CacheHelper.GetCache<object>(CacheKeyEnum.Test_Cache, 1);
        }

        [TestMethod]
        public void TestMethod4()
        {
            LogonUserAccessCheckPolicy checkPolicy = new LogonUserAccessCheckPolicy(
                AccessCheckKeyEnum.Chinese_SchoolSMSCode_Check,
                "100",
                50,
                1);
            var jj = checkPolicy.Check();
        }

        [TestMethod]
        public void TestBase64()
        {
            // Define a byte array.
            byte[] bytes = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };
            Console.WriteLine("The byte array: ");
            Console.WriteLine("   {0}\n", BitConverter.ToString(bytes));

            // Convert the array to a base 64 string.
            string s = Convert.ToBase64String(bytes);
            Console.WriteLine("The base 64 string:\n   {0}\n", s);

            // Restore the byte array.
            byte[] newBytes = Convert.FromBase64String(s);
            Console.WriteLine("The restored byte array: ");
            Console.WriteLine("   {0}\n", BitConverter.ToString(newBytes));
        }

        [TestMethod]
        public void TestDate()
        {
            DateTime x = DateTool.FirstDayOfWeek(DateTime.Now);
            DateTime y = DateTool.FirstDayOfMonth(DateTime.Now);
            DateTime z = DateTool.FirstDayOfYear(DateTime.Now);

            x = DateTool.FirstDayOfWeek(new DateTime(2019,10,28));
            x = DateTool.FirstDayOfWeek(new DateTime(2019, 11, 3));
            y = DateTool.FirstDayOfMonth(new DateTime(2019, 10, 1));
            y = DateTool.FirstDayOfMonth(new DateTime(2019, 10, 31));
            //z = DateTool.FirstDayOfMonth(new DateTime(2019, 11, 3));
        }

        [TestMethod]
        public void GroupTest()
        {
            Dictionary<int, int> sort = new Dictionary<int, int>();
            sort[1] = 300;
            sort[2] = 200;
            sort[3] = 100;

            TestX x1 = new TestX();
            x1.Id = 1;
            x1.Name = "1";
            TestX x2 = new TestX();
            x2.Id = 2;
            x2.Name = "2";
            TestX x3 = new TestX();
            x3.Id = 3;
            x3.Name = "3";
            TestX x4 = new TestX();
            x4.Id = 3;
            x4.Name = "3";

            List<TestX> list = new List<TestX>();
            list.Add(x1);
            list.Add(x4);
            list.Add(x2);
            list.Add(x3);

            var ff = list.GroupBy(x => x.Id).OrderBy(x => sort[x.Key]);

        }

        [TestMethod]
        public void TestRegex()
        {
            string markContent = "{:xxx}决议{第一号字}{:1}xx{:abc}{:答案1}。";

            Regex regex = new Regex(@"{:(\S+?)}");
            MatchCollection coll = regex.Matches(markContent);
            if (coll.Count > 0)
            {
                foreach (Match item in coll)
                {
                    string expression = item.Value;
                    string text = item.Groups[1].Value;
                    markContent = markContent.Replace(expression, text);
                }
            }
            string str = markContent;
        }

        [TestMethod]
        public void TestRedisUnitStepEditTest()
        {
            CacheHelper.SetCache<TestX>(new TestX(), CacheKeyEnum.UnitStepEdit_Cache, DateTime.Now, 10000);
            TestX obj = CacheHelper.GetCache<TestX>(CacheKeyEnum.UnitStepEdit_Cache, 10000);
        }

        [TestMethod]
        public void XTest()
        {
            TestY y = new Test.UnitTest3.TestY();
            TestX x = new Test.UnitTest3.TestX();
            x.Name = "sdfsfs";
            y.Obj = x;
            var ff = JsonConvert.DeserializeObject<Test.UnitTest3.TestX>(null);
            string json = JsonConvert.SerializeObject(y);
        }

        [TestMethod]
        public void Test190()
        {
            List<x> x1 = new List<Test.UnitTest3.x>();
            x1.Add(new Test.UnitTest3.x() { name = "ss", value = "sss" });
            x1.Add(new Test.UnitTest3.x() { name = "xx", value = "xxx" });
            string s = JsonConvert.SerializeObject(x1);
            var u = x1.ToDictionary(x => x.name, x => x.value);
            string ss = JsonConvert.SerializeObject(u);
        }

        public class x
        {
            public string name
            {
                set;get;
            }

            public string value
            {
                set;get;
            }
        }

        [TestMethod]
        public void Test1590()
        {
            DateTime x = DateTime.Now;
            DateTime y = DateTime.Now.AddSeconds(1);
            long x1 = x.Ticks;
            long y1 = y.Ticks;

            DateTime x5 = new DateTime(x1);
            DateTime y5 = new DateTime(y1);
            TimeSpan ts5 = new TimeSpan(y1) - new TimeSpan(x1);
            double i9 = ts5.TotalSeconds;

            string t = DateTime.Now.Ticks.ToString();
            string sign = Encrypt.MD5Hash(t, "8j0pf");
            string url = "http://xxxx?sign=" + sign + "&t=" + t;

            TimeSpan span = new TimeSpan(Convert.ToInt64(t)) - new TimeSpan(DateTime.Now.Ticks);
            if (span.TotalSeconds > 60)
            {
                //throw;
            }
        }

        [TestMethod]
        public void Testo0()
        {
            string str = @"x
n";
            int len = str.Length;
        }

        public class TestY
        {
            public object Obj
            {
                set;get;
            }
        }

        public class TestX
        {
            public int Id
            {
                set; get;
            }

            [JsonProperty("n")]
            public string Name
            {
                set; get;
            }
        }
    }
}
