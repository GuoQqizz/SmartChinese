using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoMediaResourceToCourse
    {
        /// <summary>
        /// 资源id
        /// </summary>
        public int MediaID { get; set; }
        /// <summary>
        /// 资源名称
        /// </summary>
        public string MediaName { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public int MediaType { get; set; }
        /// <summary>
        /// 资源年级
        /// </summary>
        public int Grade { get; set; }
        /// <summary>
        /// 资源关键字
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 资源对象id
        /// </summary>
        public int ObjectID { get; set; }
        /// <summary>
        /// 资源对象类型
        /// </summary>
        public int ObjectType { get; set; }
        /// <summary>
        /// 资源对象链接
        /// </summary>
        public string ObjectUrl => MediaUrl.ToOssPath();
        /// <summary>
        /// 资源对象封面图id
        /// </summary>
        public int ObjectImgID { get; set; }
        /// <summary>
        /// 资源对象封面图路径
        /// </summary>
        public string ObjectImgUrl => ImgUrl.ToOssPath();
        /// <summary> 
        /// 资源对象文本描述
        /// </summary>
        public string ObjectDescription { get; set; }
        /// <summary>
        /// 资源对象文本id
        /// </summary>
        public int ObjectTextID { get; set; }
        /// <summary>
        /// 资源对象文本
        /// </summary>
        public string ObjectText { get; set; }
        


        public string MediaUrl { get; set; }
        public string ImgUrl { get; set; }
    }
}
