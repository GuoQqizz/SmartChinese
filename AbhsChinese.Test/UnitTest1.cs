//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using AbhsChinese.Bll;
//using AbhsChinese.Domain.Entity;
//using AbhsChinese.Code.Common;
//using System.Collections.Generic;
//using AbhsChinese.Domain.Dto.Response;
//using System.Collections;
//using AbhsChinese.Domain.Common;
//using AbhsChinese.Code.Cache;
//using Newtonsoft.Json;
//using System.Threading.Tasks;
//using System.Reflection;
//using AbhsChinese.Domain.JsonTranslator;
//using AbhsChinese.Domain.JsonEntity;
//using AbhsChinese.Domain.Entity.Subject;
//using AbhsChinese.Domain.JsonEntity.Subject;

//namespace AbhsChinese.Test
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        //AbhsDbContextFactory db = AbhsDbContextFactory.CreateDbContext();
//        //ProductBll x1 = new ProductBll();
//        [TestMethod]
//        public void TestMethod1()
//        {
//            //string st1r = "sss";
//            //string s=JsonConvert.SerializeObject(new DateTime(1900,1,1));
//            //DateTime s1 = JsonConvert.DeserializeObject<DateTime>(s);

//            CacheHelper.SetCache<string>("test1", CacheKeyEnum.Test_Cache, 1);
//            DateTime xa = DateTime.Now;
//            Product str = CacheHelper.GetCache<Product>(CacheKeyEnum.Test_Cache, 100);
//            DateTime ya = DateTime.Now;
//            var zxx = (ya - xa).TotalMilliseconds;
//            xa = DateTime.Now;
//            CacheHelper.SetCache<Product>(new Product() { p_Id = 100, p_Desc = "ss", p_Name = "100.name" }, CacheKeyEnum.Test_Cache, 100);
//            //CacheHelper.SetCache<string>("test2", CacheKeyEnum.Test_Cache, 2);
//            //string str2 = CacheHelper.GetCache<string>(CacheKeyEnum.Test_Cache, 2);

//            //CacheHelper.SetCache<string>("test3", CacheKeyEnum.Test_Cache, 3);
//            //string str3 = CacheHelper.GetCache<string>(CacheKeyEnum.Test_Cache, 3);

//            //CacheHelper.SetCache<string>("test4", CacheKeyEnum.Test_Cache, 4);
//            //string str4 = CacheHelper.GetCache<string>(CacheKeyEnum.Test_Cache,4);

//            //CacheHelper.SetCache<string>("test5", CacheKeyEnum.Test_Cache, 5);
//            //string str5 = CacheHelper.GetCache<string>(CacheKeyEnum.Test_Cache, 5);

//            //CacheHelper.SetCache<string>("test6", CacheKeyEnum.Test_Cache, 6);
//            //string str6 = CacheHelper.GetCache<string>(CacheKeyEnum.Test_Cache, 6);

//            //CacheHelper.SetCache<string>("test7", CacheKeyEnum.Test_Cache, 7);
//            //string str7 = CacheHelper.GetCache<string>(CacheKeyEnum.Test_Cache, 7);

//            //CacheHelper.SetCache<string>("test8", CacheKeyEnum.Test_Cache, 8);
//            //string str8 = CacheHelper.GetCache<string>(CacheKeyEnum.Test_Cache, 48);

//            ya = DateTime.Now;
//            xa = DateTime.Now;
//            CacheHelper.SetCache<Product>(new Product() { p_Id = 4, p_Desc = "ss", p_Name = "100.name" }, CacheKeyEnum.Test_Cache, 100);
//            ya = DateTime.Now;
//            var z = (ya - xa).TotalMilliseconds;
//            ProductBll x = new ProductBll();
//            //x.Categories();
//            //xa = DateTime.Now;
//            var te = x.ProductGroup();
//            ya = DateTime.Now;
//            z = (ya - xa).Milliseconds;
//            xa = DateTime.Now;
//            x.AddProduct(new Product() { p_Id = 61, p_Desc = "ss", p_Name = "100.name", p_CatId = 10 });
//            ya = DateTime.Now;
//            xa = DateTime.Now;
//            x.AddProduct(new Product() { p_Id = 62, p_Desc = "ss", p_Name = "100.name", p_CatId = 10 });
//            ya = DateTime.Now;
//            //x.AddProduct(new Product() { Id = 101, Desc = "ss", Name = "101.name" });
//            //ya = DateTime.Now;
//            z = (ya - xa).TotalMilliseconds;

//            xa = DateTime.Now;
//            Product pd = x.GetProduct(3334);
//            ya = DateTime.Now;
//            z = (ya - xa).Milliseconds;

//            xa = DateTime.Now;
//            pd = x.GetProduct(50001);
//            ya = DateTime.Now;
//            z = (ya - xa).Milliseconds;

//            xa = DateTime.Now;
//            pd = x.GetProduct(90001);
//            ya = DateTime.Now;
//            z = (ya - xa).Milliseconds;

//            xa = DateTime.Now;
//            pd = x.GetProduct(10001);
//            ya = DateTime.Now;
//            z = (ya - xa).Milliseconds;

//            Product po = new Product();
//            //po.TestName = "22";
//            po.p_Id = 20;
//            po.p_Name = "20.name";

//            x.AddProduct(po);

//            List<ComplexEntity<Product, ProductCategory>> re = x.GetProductWithCategory();

//            Product p = re[0].Obj1;
//            ProductCategory p1 = re[0].Obj2;

//            p.p_Desc = "lisicong";

//            //PagingObject paging = new PagingObject(1, 5);
//            //IList<DtoProductCategory> list = x.GetPagingProduct(paging, 1000);
//            //PagingObject paging1 = new PagingObject(2, 5);
//            //IList<DtoProductCategory> list1 = x.GetPagingProduct(paging1, 1000);
//            //Product p=x.GetProduct(2);
//            //Role r=x.GetRole(10008);
//            //Product p = x.GetProduct(1);
//            //if (p != null)
//            //{
//            //    x.DeleteProduct(p);
//            //}

//            //x.TranTest(new Product() { Id = 1, Name = "1.name.new" });
//            //p = x.GetProduct(1);

//            //Assert.IsTrue(p == null);

//            //x.AddProduct(new Product() { Id = 1, Name = "1.name" });
//            //p = x.GetProduct(1);
//            //Assert.IsTrue(p != null && p.Name == "1.name");

//            //p.Name = "x.namex";
//            //p.Name = "x.name";
//            //p.CatId = null;
//            x.UpdateProduct(p);

//            //var y = x.GetAll();

//            //p.Name = "1.name.update.10";
//            //x.UpdateProduct(p);
//            //y = x.GetAll();
//            //p = x.GetProduct(1);
//            //Assert.IsTrue(p != null && p.Name == "1.name.update");

//        }

//        [TestMethod]
//        public void TestMethod()
//        {
//            for (int i = 10001; i <= 50000; i++)
//            {
//                CacheHelper.SetCache<Product>(new Product() { p_Id = i, p_Desc = i + ".name", p_Name = i + ".name", p_CatId = 2 }, CacheKeyEnum.Test_Cache, i);
//            }
//        }

//        [TestMethod]
//        public void TestTranslatorAdd()
//        {
//            Yw_SubjectFreeContent content = new Yw_SubjectFreeContent();
//            content.Ysc_Content = @"jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//原文介绍: jieba中文分词的.NET版本：jieba.NET
//.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//代码地址github: https://github.com/anderscui/jieba.NET
//            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//原文介绍: jieba中文分词的.NET版本：jieba.NET
//.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//代码地址github: https://github.com/anderscui/jieba.NET
//            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//原文介绍: jieba中文分词的.NET版本：jieba.NET
//.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//代码地址github: https://github.com/anderscui/jieba.NET
//之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//原文介绍: jieba中文分词的.NET版本：jieba.NET";
//            content.Ysc_Answer = @"jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//原文介绍: jieba中文分词的.NET版本：jieba.NET
//.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//代码地址github: https://github.com/anderscui/jieba.NET
//            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//原文介绍: jieba中文分词的.NET版本：jieba.NET
//这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。";
//            content.Ysc_Explain= @"jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//原文介绍: jieba中文分词的.NET版本：jieba.NET
//.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//代码地址github: https://github.com/anderscui/jieba.NET
//            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//原文介绍: jieba中文分词的.NET版本：jieba.NET
//这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。";
//            content.Ysc_CreateTime = DateTime.Now;
//            content.Ysc_UpdateTime = DateTime.Now;
//            content.Ysc_Creator = 10001;
//            content.Ysc_Editor = 10002;
//            content.Ysc_SubjectType = 2;

//            SubjectBll bll = new Bll.SubjectBll();
//            bll.AddSubject(content);

//            Yw_SubjectContent subject = bll.GetSubject(10001);
//            subject.Ysc_UpdateTime = DateTime.Now;
//            ((Yw_SubjectFreeContent)subject).Ysc_Content = "OptionA.A";
//            bll.SaveSubject(subject);
//            //List<Task> list = new List<Task>();
//            //for (int index = 1; index <= 10000; index++)
//            //{
//            //    list.Add(Task.Factory.StartNew(() =>
//            //   {
//            //       //SubjectSelectContentJson entity = TranslatorFactory.Translator<SubjectSelectContentJson>(json);
//            //   }));
//            //}
//            //Task.WaitAll(list.ToArray());
//        }

//        [TestMethod]
//        public void TestTranslatorSelectAdd()
//        {
//            string s2 = "";
//           string s= JsonConvert.SerializeObject(s2);
//            //            Yw_SubjectSelectContent content = new Yw_SubjectSelectContent();
//            //            content.Ysc_Content_Json = new SubjectSelectContentJson();
//            //            content.Ysc_Content_Json.Stem= @"jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//            //代码地址github: https://github.com/anderscui/jieba.NET
//            //            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            //            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//            //代码地址github: https://github.com/anderscui/jieba.NET
//            //            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            //            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。";

//            //            content.Ysc_Content_Json.OptionA = "OptionA,jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古";
//            //            content.Ysc_Content_Json.OptionB = "OptionB,jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古";

//            //            content.Ysc_Explain = @"jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//            //代码地址github: https://github.com/anderscui/jieba.NET
//            //            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            //            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。";
//            //            content.Ysc_CreateTime = DateTime.Now;
//            //            content.Ysc_UpdateTime = DateTime.Now;
//            //            content.Ysc_Creator = 10001;
//            //            content.Ysc_Editor = 10002;
//            //            content.Ysc_SubjectType = 1;

//            SubjectBll bll = new Bll.SubjectBll();
////            bll.AddSubject(content);

//            Yw_SubjectContent subject = bll.GetSubject(10004);
//            subject.Ysc_UpdateTime = DateTime.Now;
//            ((Yw_SubjectSelectContent)subject).Ysc_Content_Obj.OptionA = "OptionA.A.1";
//            bll.SaveSubject(subject);
//            //List<Task> list = new List<Task>();
//            //for (int index = 1; index <= 10000; index++)
//            //{
//            //    list.Add(Task.Factory.StartNew(() =>
//            //   {
//            //       //SubjectSelectContentJson entity = TranslatorFactory.Translator<SubjectSelectContentJson>(json);
//            //   }));
//            //}
//            //Task.WaitAll(list.ToArray());
//        }

//        [TestMethod]
//        public void TestTranslatorGet()
//        {
//            SubjectBll bll = new Bll.SubjectBll();
//            Yw_SubjectContent subject = bll.GetSubject(10000);

//            subject.Ysc_UpdateTime = DateTime.Now;

//            ((Yw_SubjectSelectContent)subject).Ysc_Content_Obj.OptionA = "OptionA.update";
//            bll.SaveSubject(subject);
//        }

//        [TestMethod]
//        public void TestTranslator()
//        {
//            Yw_SubjectSelectContent content = new Yw_SubjectSelectContent();
//            content.Ysc_SubjectType = 1;
//            content.Ysc_Answer_Obj = new SubjectSelectAnswerObj();
//            content.Ysc_Answer_Obj.Answers = new List<string>() { "A", "B" };
//            content.Ysc_Content_Obj = new SubjectSelectContentObj();
//            content.Ysc_Content_Obj.Stem = "大气层的含氧量是:";
//            content.Ysc_Content_Obj.OptionA = "OptionA";
//            content.Ysc_Content_Obj.OptionB = "OptionB";
//            content.Ysc_Content_Obj.OptionC = "OptionC";
//            content.Ysc_CreateTime = DateTime.Now;
//            content.Ysc_Creator = 1000;
//            content.Ysc_Explain = "解析";
//            SubjectBll bll = new Bll.SubjectBll();
//            bll.AddSubject(content);
//            //SubjectSelectContentJson jsonObj = new SubjectSelectContentJson();
//            //jsonObj.OptionA = "A.";
//            //jsonObj.OptionB = "B.";
//            //jsonObj.OptionC = "C.";
//            //jsonObj.OptionD = "D.";
//            //jsonObj.Stem = "测试是工程是";
//            //string json = JsonConvert.SerializeObject(jsonObj);
//            //List<Task> list = new List<Task>();
//            //for (int index = 1; index <= 10000; index++)
//            //{
//            //    list.Add(Task.Factory.StartNew(() =>
//            //   {
//            //       //SubjectSelectContentJson entity = TranslatorFactory.Translator<SubjectSelectContentJson>(json);
//            //   }));
//            //}
//            //Task.WaitAll(list.ToArray());
//        }

//        [TestMethod]
//        public void TestMethod2()
//        {
//            //CacheHelper.SetCache<string>("test1", CacheKeyEnum.Test_Cache, 1);

//            DateTime xa = DateTime.Now;
//            List<Task> list = new List<Task>();
//            for (int i = 1; i <= 50000; i++)
//            {
//                list.Add(Task.Factory.StartNew(RedisTest, (object)i));
//            }
//            Task.WaitAll(list.ToArray());

//            DateTime ya = DateTime.Now;
//            var z = (ya - xa).TotalMilliseconds;

//        }

//        public void RedisTest(object x)
//        {
//            Random r = new Random();
//            for (int index = 1; index <= 10; index++)
//            {
//                var id = r.Next(50000);
//                Product str = CacheHelper.GetCache<Product>(CacheKeyEnum.Test_Cache, id);
//                if (str != null)
//                {
//                    Console.WriteLine("正确" + x +","+ str.p_Id);
//                }
//                else
//                {
//                    Console.WriteLine("错误" + id);
//                }
//            }
//        }

//        [TestMethod]
//        public void TestMethod3()
//        {
//            DateTime xa = DateTime.Now;
//            List<Task> list = new List<Task>();
//            for (int i = 1; i <= 50000; i++)
//            {
//                list.Add(Task.Factory.StartNew(SqlTest, (object)i));
//            }
//            Task.WaitAll(list.ToArray());

//            DateTime ya = DateTime.Now;
//            var z = (ya - xa).TotalMilliseconds;

//        }

//        public void SqlTest(object x)
//        {
//            Random r = new Random();
//            ProductBll x1 = new ProductBll();
//            for (int index = 1; index <= 10; index++)
//            {
//                var id = r.Next(50000);
//                Product str = x1.GetProduct(id);
//                if (str != null)
//                {
//                    Console.WriteLine("正确" + id);
//                }
//                else
//                {
//                    Console.WriteLine("错误" + id);
//                }
//            }
//        }

//        [TestMethod]
//        public void Test4()
//        {
//            ProductBll x1 = new ProductBll();
//            x1.DeleteProduct(new Product() { p_Id = 2 });
//        }

//        [TestMethod]
//        public void Test5()
//        {
//            Action<object> b = (target) => { Console.WriteLine(target.GetType().Name); };
//            Action<string> d = b;
//            d("rr");
//        }

//        [TestMethod]
//        public void LogTest()
//        {
//            List<Task> ts = new List<Task>();
//            //for (int i = 1; i <= 100; i++)
//            //{
//            //    ts.Add(Task.Factory.StartNew(() =>
//            //    {
//            //        LogHelper.WriteLog("sss");
//            //    }));
//            //}
//            Task.WaitAll(ts.ToArray());

//        }

//        [TestMethod]
//        public void Test7()
//        {
//            var x1 = GetGenericFields<Product, ProductCategory>();
//            var x2 = GetGenericFields<ProductCategory, Product>();
//            var x3 = GetGenericFields<Table, Product>();
//        }

//        [TestMethod]
//        public void Test8()
//        {
//            ProductBll bll = new ProductBll();
//            //bll.AddProduct(new Product() { p_Name = "李思聪xx"});
//            Product x = bll.GetProduct(216);
//            x.p_Desc = "修改李思聪0830";
//            x.p_IsTrue = true;
//            x.p_time = DateTime.Now;
//            x.p_money = 100;
//            bll.UpdateProduct(x);
//            bll.GetProductWithCategory();
//        }

//        [TestMethod]
//        public void Test9()
//        {
//            ProductBll bll = new ProductBll();
//            bll.TranTest(new Product() { p_Desc = "Desc", p_IsTrue = true, p_Name = "Name" });
//        }

//        [TestMethod]
//        public void Test101()
//        {
//            string str= @"Yw_SubjectSelectContent content = new Yw_SubjectSelectContent();
//            //            content.Ysc_Content_Json = new SubjectSelectContentJson();
//            //            content.Ysc_Content_Json.Stem= jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//            //代码地址github: https://github.com/anderscui/jieba.NET
//            //            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            //            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//            //代码地址github: https://github.com/anderscui/jieba.NET
//            //            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            //            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。Yw_SubjectSelectContent content = new Yw_SubjectSelectContent();
//            //            content.Ysc_Content_Json = new SubjectSelectContentJson();
//            //            content.Ysc_Content_Json.Stem= jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//            //代码地址github: https://github.com/anderscui/jieba.NET
//            //            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            //            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//            //代码地址github: https://github.com/anderscui/jieba.NET
//            //            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            //            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。Yw_SubjectSelectContent content = new Yw_SubjectSelectContent();
//            //            content.Ysc_Content_Json = new SubjectSelectContentJson();
//            //            content.Ysc_Content_Json.Stem= jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//            //代码地址github: https://github.com/anderscui/jieba.NET
//            //            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            //            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//            //代码地址github: https://github.com/anderscui/jieba.NET
//            //            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            //            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。Yw_SubjectSelectContent content = new Yw_SubjectSelectContent();
//            //            content.Ysc_Content_Json = new SubjectSelectContentJson();
//            //            content.Ysc_Content_Json.Stem= jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//            //代码地址github: https://github.com/anderscui/jieba.NET
//            //            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            //            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。
//            //代码地址github: https://github.com/anderscui/jieba.NET
//            //            我们直接在VS2013的nuget包管理器里面搜索下载也行:
//            //            jieba是python下的一个检索库, 有人将这个库移植到了asp.net 平台下, 完全可以替代lucene.net以及盘古分词的搭配
//            //            之所以写这个, 其实是因为昨天面试时, 被问到网站的关键字检索你怎么做？我就是说了下sql模糊查询以及sql语句优化, 缓存。以前接触过关键字分词, 但是在.net平台下没有成熟的分词检索库, 不像java有lucene, 尽管也移植到了.net, 但是更新慢。我之前学python的时候留意到了python的分词检索, 以及做词云, 就想着有没有python的分词检索库移植到了.net的查了下python的jieba库 果然有移植的！
//            //原文介绍: jieba中文分词的.NET版本：jieba.NET
//            //.NET平台上常见的分词组件是盘古分词，但是已经好久没有更新了。最明显的是内置词典，jieba的词典有50万个词条，而盘古的词典是17万，这样会造成明显不同的分词效果。另外，对于未登录词，jieba“采用了基于汉字成词能力的HMM模型，使用了Viterbi算法”，效果看起来也不错。";
//            str += str;

//        }

//        [TestMethod]
//        public void Test10()
//        {
//            ProductBll bll = new ProductBll();
//            var x = bll.GetProductWithCategory();
//        }

//        private PropertyInfo[] GetDynamicFields(object obj)
//        {
//            List<string> fields = new List<string>();
//            PropertyInfo[] ps = obj.GetType().GetProperties();
//            return ps;
//        }
//        private Dictionary<string, Tuple<Type, PropertyInfo>> GetGenericFields<T1, T2>()
//        {
//            Type t = typeof(ComplexEntity<T1, T2>);
//            if (TestRepositoryCache.ComplexPropertyCache.ContainsKey(t))
//            {
//                return TestRepositoryCache.ComplexPropertyCache[t];
//            }
//            Dictionary<string, Tuple<Type, PropertyInfo>> dic = new Dictionary<string, Tuple<Type, PropertyInfo>>();
//            PropertyInfo[] ps1 = typeof(T1).GetProperties();
//            PropertyInfo[] ps2 = typeof(T2).GetProperties();
//            foreach (PropertyInfo property in ps1)
//            {
//                dic[property.Name] = new Tuple<Type, PropertyInfo>(typeof(T1), property);
//            }

//            foreach (PropertyInfo property in ps2)
//            {
//                dic[property.Name] = new Tuple<Type, PropertyInfo>(typeof(T2), property);
//            }

//            TestRepositoryCache.ComplexPropertyCache[t] = dic;
//            return dic;
//        }
//    }
//}

//public class TestRepositoryCache
//{
//    public static readonly Dictionary<Type, Dictionary<string, Tuple<Type, PropertyInfo>>> ComplexPropertyCache;

//    static TestRepositoryCache()
//    {
//        ComplexPropertyCache = new Dictionary<Type, Dictionary<string, Tuple<Type, PropertyInfo>>>();
//    }
//}