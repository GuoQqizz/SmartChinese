using AbhsChinese.Bll;
using AbhsChinese.Code.Cache;
using AbhsChinese.Domain.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AbhsChinese.Test
{
    [TestClass]
    public class ProductBllTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            ProductBll bll = new ProductBll();
            //bll.AddProduct(new Product() { Name = "3.name", CatId = 1 });
            //bll.TestMultipleOperation();
            //bll.TranTest(new Product() { Name = "7.15.name", CatId = 1 });

            //List<Product> li = new List<Product>();
            //li.Add(new Product() { Name = "3.name", CatId = 1 });
            //li.Add(new Product() { Name = "4.name", CatId = 1 });
            //li.Add(new Product() { Name = "31.name", CatId = 1 });
            //bll.AddProducts(li);

            //List<Product> ps = bll.GetAll();
            //Product p = bll.GetProduct(3);
            //bll.GetProductWithCategory();
            //bll.GetAssessments(13581);
            //List<Product> ps1 = bll.GetAll();
            //Product p1 = bll.GetProduct(1);
            //Product p12 = bll.GetProduct(1);
            //bll.DeleteProduct(p);

            //List<Product> p2 = bll.GetAll();
            ////ProductBll bll1 = new ProductBll();
            ////Product p2 = bll1.GetProduct(50);
            //Product p3 = p2.Where(x => x.Id == 50).FirstOrDefault();
            //p1.Desc = "update.x.y.z.";
            //bll.UpdateProduct(p1);
            //p3 = bll.GetProduct(50);

            //p1.Desc = "update.x.y.z.0";
            //bll.UpdateProduct(p3);

            //ProductBll bllq = new ProductBll();
            //Product p1q = bll.GetProduct(50);
            //List<Product> p2q = bll.GetAll();
            ////ProductBll bll1 = new ProductBll();
            ////Product p2 = bll1.GetProduct(50);
            //Product p3q = p2q.Where(x => x.Id == 50).FirstOrDefault();
            //p1q.Desc = "update.x.y.z.";
            //bll.UpdateProduct(p1q);
            //p3q = bllq.GetProduct(50);

            //p1q.Desc = "update.x.y.z.0";
            //bll.UpdateProduct(p3q);
        }

        [TestMethod]
        public void TestTran()
        {
            ProductBll bll = new ProductBll();
            bll.TranTest(new Product() { p_CatId = 1, p_IsTrue = true, p_money = 100, p_Name = "xxx", p_Desc = "yyy", p_time = new System.DateTime(2010, 1, 1) });
        }

        [TestMethod]
        public void RedisTest()
        {
            string content = @"5月26日，天虽然阴沉沉，但兴奋的我怀着期待的心情来到了小记者活动集合点——萧山剧院，听说今天我们要去宠物公园。
到了集合点，好多小记者就已经在议论今天的行程，大家都特别的兴奋。13点30分，大巴车准时出发。一路上，我们欢声笑语，四十分钟后，我们到了目的地——宠物公园。欣赏了鳄龟，山羊，七彩鸡等小动物，我终于在竹林里见到了我盼望已久的孔雀。
只见披着棕色外衣的孔雀，独自站在一根树枝上，它的脖子抬的高高的，尾巴长长的，上面闪着宝蓝色的光，好像一个拖着长裙的孤独的公主。我很想看到作文http://Www.zUoWEn8.coM/孔雀开屏的一幕，所以我不停的呼喊着：“孔雀宝宝，快点开屏。”可是我叫了半天，孔雀毫无反应，我失望极了。
            带着这份失落，我来到了山羊区。我在草丛里采了一把带着水珠的嫩草，来到羊圈前，我拿出青草，小心翼翼的伸向山羊的嘴边，小羊激动的扑向我，一把咬住青草，吓得我连忙把手缩回。有一只白山羊，它非常的霸道，小记者一拿出青草，它总是第一个扑向青草，挤开别的小伙伴。
快乐的半天之旅马上就要了，我怀着依依不舍的心情踏上了回家之路。今天的小记者活动让我与宠物公园有了一个美好的约会，期待下次活动。
5月26日，天虽然阴沉沉，但兴奋的我怀着期待的心情来到了小记者活动集合点——萧山剧院，听说今天我们要去宠物公园。
到了集合点，好多小记者就已经在议论今天的行程，大家都特别的兴奋。13点30分，大巴车准时出发。一路上，我们欢声笑语，四十分钟后，我们到了目的地——宠物公园。欣赏了鳄龟，山羊，七彩鸡等小动物，我终于在竹林里见到了我盼望已久的孔雀。
只见披着棕色外衣的孔雀，独自站在一根树枝上，它的脖子抬的高高的，尾巴长长的，上面闪着宝蓝色的光，好像一个拖着长裙的孤独的公主。我很想看到作文http://Www.zUoWEn8.coM/孔雀开屏的一幕，所以我不停的呼喊着：“孔雀宝宝，快点开屏。”可是我叫了半天，孔雀毫无反应，我失望极了。
            带着这份失落，我来到了山羊区。我在草丛里采了一把带着水珠的嫩草，来到羊圈前，我拿出青草，小心翼翼的伸向山羊的嘴边，小羊激动的扑向我，一把咬住青草，吓得我连忙把手缩回。有一只白山羊，它非常的霸道，小记者一拿出青草，它总是第一个扑向青草，挤开别的小伙伴。
快乐的半天之旅马上就要了，我怀着依依不舍的心情踏上了回家之路。今天的小记者活动让我与宠物公园有了一个美好的约会，期待下次活动。
5月26日，天虽然阴沉沉，但兴奋的我怀着期待的心情来到了小记者活动集合点——萧山剧院，听说今天我们要去宠物公园。
5月26日，天虽然阴沉沉，但兴奋的我怀着期待的心情来到了小记者活动集合点——萧山剧院，听说今天我们要去宠物公园。
到了集合点，好多小记者就已经在议论今天的行程，大家都特别的兴奋。13点30分，大巴车准时出发。一路上，我们欢声笑语，四十分钟后，我们到了目的地——宠物公园。欣赏了鳄龟，山羊，七彩鸡等小动物，我终于在竹林里见到了我盼望已久的孔雀。
只见披着棕色外衣的孔雀，独自站在一根树枝上，它的脖子抬的高高的，尾巴长长的，上面闪着宝蓝色的光，好像一个拖着长裙的孤独的公主。我很想看到作文http://Www.zUoWEn8.coM/孔雀开屏的一幕，所以我不停的呼喊着：“孔雀宝宝，快点开屏。”可是我叫了半天，孔雀毫无反应，我失望极了。
            带着这份失落，我来到了山羊区。我在草丛里采了一把带着水珠的嫩草，来到羊圈前，我拿出青草，小心翼翼的伸向山羊的嘴边，小羊激动的扑向我，一把咬住青草，吓得我连忙把手缩回。有一只白山羊，它非常的霸道，小记者一拿出青草，它总是第一个扑向青草，挤开别的小伙伴。
快乐的半天之旅马上就要了，我怀着依依不舍的心情踏上了回家之路。今天的小记者活动让我与宠物公园有了一个美好的约会，期待下次活动。
5月26日，天虽然阴沉沉，但兴奋的我怀着期待的心情来到了小记者活动集合点——萧山剧院，听说今天我们要去宠物公园。
到了集合点，好多小记者就已经在议论今天的行程，大家都特别的兴奋。13点30分，大巴车准时出发。一路上，我们欢声笑语，四十分钟后，我们到了目的地——宠物公园。欣赏了鳄龟，山羊，七彩鸡等小动物，我终于在竹林里见到了我盼望已久的孔雀。
只见披着棕色外衣的孔雀，独自站在一根树枝上，它的脖子抬的高高的，尾巴长长的，上面闪着宝蓝色的光，好像一个拖着长裙的孤独的公主。我很想看到作文http://Www.zUoWEn8.coM/孔雀开屏的一幕，所以我不停的呼喊着：“孔雀宝宝，快点开屏。”可是我叫了半天，孔雀毫无反应，我失望极了。
            带着这份失落，我来到了山羊区。我在草丛里采了一把带着水珠的嫩草，来到羊圈前，我拿出青草，小心翼翼的伸向山羊的嘴边，小羊激动的扑向我，一把咬住青草，吓得我连忙把手缩回。";
            content += content;
            CacheHelper.SetCache<string>(content, CacheKeyEnum.Test_Cache,DateTime.Now, 100);
            string result = CacheHelper.GetCache<string>(CacheKeyEnum.Test_Cache, 100);
        }

        [TestMethod]
        public void GetByIds()
        {
            ProductBll bll = new ProductBll();
           var x= bll.GetByIds();
        }
    }
}