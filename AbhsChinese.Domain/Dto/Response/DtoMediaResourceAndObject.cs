﻿using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoMediaResourceAndObject
    {
        public int Ymr_Id { get; set; }

        public string Ymr_Name { get; set; }

        public int Ymr_MediaType { get; set; }

        public int Ymr_Grade { get; set; }

        public int Ymr_MediaObjectId { get; set; }

        public string Ymr_Keywords { get; set; }

        public int Ymr_Status { get; set; }
        
        public int Yme_Id { get; set; }

        public int Yme_MediaType { get; set; }

        public string Yme_Url { get; set; }

        public int Yme_TextObjectId { get; set; }
        
        public int Yme_ImageCoverId { get; set; }

        public string Yme_Description { get; set; }

        public int Yme_Status { get; set; }

        public int AudioId { get; set; }
        
        public string AudioUrl { get; set; }

        public int ImgId { get; set; }
        
        public string ImgUrl { get; set; }

        public int Yxo_Id { get; set; }

        public string Yxo_Content { get; set; }

        public string Grade => CustomEnumHelper.Parse(typeof(GradeEnum), Ymr_Grade);

        public string MediaType => CustomEnumHelper.Parse(typeof(MediaResourceTypeEnum), Yme_MediaType);

        public string Url => AudioUrl.ToOssPath();
        public string ImagesUrl => ImgUrl.ToOssPath();
        public string AudioUrlStr { get; set; }

        public string XiaoAiBUrl => Yme_Url.ToOssPath();
    }
}
