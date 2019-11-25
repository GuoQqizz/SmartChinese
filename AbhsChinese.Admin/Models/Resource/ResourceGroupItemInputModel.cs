using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Resource
{
    public class ResourceGroupItemInputModel
    {
        public int Ygi_Id { get; set; }
        
        public int Ygi_GroupId { get; set; }

        public int Ygi_ResourceType { get; set; }

        public int Ygi_ResourceId { get; set; }
    }
}