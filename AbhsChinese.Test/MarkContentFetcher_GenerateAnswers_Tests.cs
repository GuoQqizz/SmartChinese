using AbhsChinese.Admin.Models.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AbhsChinese.Test
{
    [TestClass]
    public class MarkContentFetcher_GenerateAnswers_Tests
    {
        [TestMethod]
        public void MarkContentFetcher_GenerateAnswers()
        {
            List<int[]> list = new List<int[]>();
            list.Add(new int[2] { 0, 1 });
            list.Add(new int[2] { 5, 1 });
            list.Add(new int[2] { 11, 2 });
            list.Add(new int[2] { 14, 2 });
            string content = "{:鹅}路上有只{:狗}，房子，汽{:山羊}车{:鸭子}";
            MarkContentFetcher fetcher = new MarkContentFetcher();
            List<int[]> answers = fetcher.GenareteAnswers(content);
            Assert.IsNotNull(answers);
            Assert.IsTrue(list.Count == answers.Count);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i][0] != answers[i][0]
                    || list[i][1] != answers[i][1])
                {
                    Assert.Fail();
                }
            }
        }
    }
}