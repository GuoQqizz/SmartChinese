using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Resource
{
    public class ResourceGroupViewModel
    {
        public int Yrg_Id { get; set; }
        
        public string Yrg_Name { get; set; }

        public int Yrg_Grade { get; set; }

        public int Yrg_MediaCount { get; set; }

        public int Yrg_TextCount { get; set; }

        public int Yrg_SubjectCount { get; set; }

        public int Yrg_Status { get; set; }

        public DateTime Yrg_CreateTime { get; set; }

        public int Yrg_Creator { get; set; }

        public string CreateTime => Yrg_CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

        public string Grade => CustomEnumHelper.Parse(typeof(GradeEnum), (int)Yrg_Grade);

        public int resourceType { get; set; }
    }
}