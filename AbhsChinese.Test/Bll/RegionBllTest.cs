using AbhsChinese.Bll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class RegionBllTest : BaseArrert
    {
        public RegionBll bll = new RegionBll();
        [TestMethod]
        public void GetRegionTest()
        {
            List<int> ids = new List<int>() { 13, 14 };
            int id = 0;
            var list = bll.GetRegionByIds(ids, RegionTypeEnum.省);
            BaseAssertList(list);

            ids = new List<int>() { 1301, 1302 };
            list = bll.GetRegionByIds(ids, RegionTypeEnum.市);
            BaseAssertList(list);


            ids = new List<int>() { 140825, 140826 };
            list = bll.GetRegionByIds(ids, RegionTypeEnum.区);
            BaseAssertList(list);

            var regs = bll.GetRegionList(id);
            BaseAssertList(regs);

            id = 13;
            regs = bll.GetRegionList(id);
            BaseAssertList(regs);

            id = 1301;
            regs = bll.GetRegionList(id);
            BaseAssertList(regs);
        }
    }
}
