using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoMediaResource
    {
        public int Ymr_Id { get; set; }

        public string Ymr_Name { get; set; }

        public int Ymr_MediaType { get; set; }

        public int Ymr_Grade { get; set; }
        
        public int Ymr_Status { get; set; }

        public DateTime Ymr_CreateTime { get; set; }

        public int Ymr_Creator { get; set; }

        public DateTime Ymr_UpdateTime { get; set; }

        public int Ymr_Editor { get; set; }

        public string Ymr_Keywords { get; set; }

        public string UploadTime => Ymr_CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

        public string Grade => CustomEnumHelper.Parse(typeof(GradeEnum), Ymr_Grade);

        public string MediaType => CustomEnumHelper.Parse(typeof(MediaResourceTypeEnum), Ymr_MediaType);
    }
}
