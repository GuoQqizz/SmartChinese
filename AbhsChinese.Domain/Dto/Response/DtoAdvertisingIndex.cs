using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoAdvertisingIndex
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
        /// 推荐图(相对路径)
        /// </summary>
        public string Bad_ImageUrl { get; set; }
        /// <summary>
        ///  推荐图(绝对路径)
        /// </summary>
        public string Bad_ImageUrlShow => Bad_ImageUrl.ToOssPath();
        /// <summary>
        /// 推荐课程Id
        /// </summary>
        public int Bad_ReferId { get; set; }
        /// <summary>
        /// 推荐课程名称
        /// </summary>
        //public string Bad_ReferName { get; set; }

        /// <summary>
        /// 推荐跳转url
        /// </summary>
        public string Bad_Url { get; set; }
    }
}
