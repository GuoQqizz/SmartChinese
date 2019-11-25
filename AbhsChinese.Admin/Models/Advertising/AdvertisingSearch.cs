using AbhsChinese.Admin.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Advertising
{
    public class AdvertisingSearch : Search
    {
        public int AdvStatus { get; set; }
        /// <summary>
        /// 推荐类型 目前只有一个课程1
        /// <see cref="AbhsChinese.Domain.Enum.AdvertisingTypeEnum"/>
        /// </summary>
        public int AdvPosType { get; set; }

        public int AdvPosId { get; set; }

        public AdvertisingSearch()
        {
            this.AdvPosType = 1;
            this.AdvStatus = 1;
            this.AdvPosId = 0;
        }
    }
}