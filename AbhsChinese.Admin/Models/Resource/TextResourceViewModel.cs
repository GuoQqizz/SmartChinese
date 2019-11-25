using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Resource
{
    public class TextResourceViewModel
    {
        public int Ytr_Id { get; set; }
        
        public string Ytr_Name { get; set; }

        public int Ytr_TextType { get; set; }

        public int Ytr_Grade { get; set; }

        public int Ytr_TextObjectId { get; set; }

        public string Ytr_Keywords { get; set; }

        public int Ytr_Status { get; set; }

        public DateTime Ytr_CreateTime { get; set; }

        public int Ytr_Creator { get; set; }

        public DateTime Ytr_UpdateTime { get; set; }

        public int Ytr_Editor { get; set; }

        public string UploadTime => Ytr_CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

        public string Grade => CustomEnumHelper.Parse(typeof(GradeEnum), Ytr_Grade);

        public string TextType => CustomEnumHelper.Parse(typeof(TextResourceTypeEnum), Ytr_TextType);
    }
}