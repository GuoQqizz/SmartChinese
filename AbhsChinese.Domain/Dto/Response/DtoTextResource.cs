using AbhsChinese.Code.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoTextResource
    {
        public int Ytr_Id { get; set; }
        
        public string Ytr_Name { get; set; }

        public int Ytr_TextType { get; set; }

        public int Ytr_Grade { get; set; }

        public int Ytr_TextObjectId { get; set; }

        public string Yxo_Content { get; set; }

        public int Ytr_Status { get; set; }

        public DateTime Ytr_CreateTime { get; set; }

        public int Ytr_Creator { get; set; }

        public DateTime Ytr_UpdateTime { get; set; }
        
        public int Ytr_Editor { get; set; }

        public string Ytr_Keywords { get; set; }

        public string UploadTime => Ytr_CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
        
        public string Grade=> CustomEnumHelper.Parse(typeof(GradeEnum), Ytr_Grade);

        public string TextType => CustomEnumHelper.Parse(typeof(TextResourceTypeEnum), Ytr_TextType);
    }
}
