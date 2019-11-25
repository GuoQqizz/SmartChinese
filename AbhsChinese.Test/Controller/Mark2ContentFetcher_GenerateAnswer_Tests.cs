using AbhsChinese.Admin.Models.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AbhsChinese.Test.Controller
{
    [TestClass]
    public class Mark2ContentFetcher_GenareteAnswers_Tests
    {
        [TestMethod]
        public void Mark2ContentFetcher_GenareteAnswers_ShouldReturn()
        {
            //                  2-1 5-2
            string content = "12/3/4/56";

            var result = new List<int>() { 1, 2, 3 };
            var answers = new Mark2ContentFetcher().GenareteAnswers(content);
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] != answers[i])
                {
                    Assert.Fail();
                }
            }
        }
        [TestMethod]
        public void Mark2ContentFetcher_GenareteAnswers_ShouldReturn2()
        {
            string content = "1/3/456";

            var result = new List<int>() { 0, 1 };
            var answers = new Mark2ContentFetcher().GenareteAnswers(content);
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] != answers[i])
                {
                    Assert.Fail();
                }
            }
        }
        [TestMethod]
        public void Mark2ContentFetcher_GenareteAnswers_ShouldReturn3()
        {
            string content = "1345/6";

            var result = new List<int>() { 3 };
            var answers = new Mark2ContentFetcher().GenareteAnswers(content);
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] != answers[i])
                {
                    Assert.Fail();
                }
            }
        }
        [TestMethod]
        public void Mark2ContentFetcher_GenareteAnswers_ShouldReturn4()
        {
            string content = "13sfs/fs/45/6";

            var result = new List<int>() { 4, 6, 8 };
            var answers = new Mark2ContentFetcher().GenareteAnswers(content);
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] != answers[i])
                {
                    Assert.Fail();
                }
            }
        }
        [TestMethod]
        public void Mark2ContentFetcher_GenareteAnswers_ShouldReturn5()
        {
            //                  1           7               15          21               29          35              43          49
            string content = "国破/山河在，城春/草木深。\r\n感时/花溅泪，恨别/鸟惊心。\r\n烽火/连三月，家书/抵万金。\r\n白头/搔更短，浑欲/不胜簪。";
            var result = new List<int>() { 4, 6, 8 };
            var answers = new Mark2ContentFetcher().GenareteAnswers(content);
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] != answers[i])
                {
                    Assert.Fail();
                }
            }
        }
    }
}