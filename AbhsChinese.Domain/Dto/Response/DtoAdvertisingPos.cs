using AbhsChinese.Code.Extension;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoAdvertisingPos
    {
        /// <summary>
        /// 楼层Id
        /// </summary>
        public int Bap_Id { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string Bap_Code { get; set; }
        /// <summary>
        /// 广告Id
        /// </summary>
        public int Bad_Id { get; set; }
        /// <summary>
        /// 推荐图
        /// </summary>
        public string Bad_ImageUrl { get; set; }

        public string Bad_ImageUrlShow => Bad_ImageUrl.ToOssPath();
        /// <summary>
        /// 推荐课程Id
        /// </summary>
        public int Bad_ReferId { get; set; }
        /// <summary>
        /// 推荐课程名称
        /// </summary>
        public string Bad_ReferName { get; set; }
        /// <summary>
        /// 推荐跳转url
        /// </summary>
        public string Bad_Url { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int Bad_HitCount { get; set; }
        /// <summary>
        /// 购买次数
        /// </summary>
        public int Bad_ValidCount { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Bad_CreateTime { get; set; }

        public string CreateTime => Bad_CreateTime == DateTime.MinValue ? "" : Bad_CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime Bad_EndTime { get; set; }

        public string EndTime => Bad_EndTime == DateTime.MinValue || Bad_EndTime == DateTimeExtensions.DefaultDateTime ? "" : Bad_EndTime.ToString("yyyy-MM-dd HH:mm:ss");
        /// <summary>
        /// 推荐状态
        /// </summary>
        public int Bap_Status { get; set; }

        public StatusEnum Bap_StatusEnum { get; set; }
    }
}
