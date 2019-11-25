using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Knowledge
{
    public class SelectPageModel
    {
        public int totalRow { get; set; }


        public List<Dictionary<string,object>> list { get; set; }
    }
}