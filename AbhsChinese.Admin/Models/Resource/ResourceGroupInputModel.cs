using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Resource
{
    public class ResourceGroupInputModel
    {
        public int Yrg_Id { get; set; }
        
        public string Yrg_Name { get; set; }

        public int Yrg_Grade { get; set; }

        public int Yrg_Status { get; set; }
    }
}