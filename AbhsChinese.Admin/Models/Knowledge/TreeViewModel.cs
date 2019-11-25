using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Knowledge
{
    public class TreeViewModel
    {
        public string text { get; set; }

        public string nodeid { get; set; }

        public string color { get; set; }

        public string expanded { get; set; }

        public string backColor { get; set; }

        public bool hasChildrenField { get; set; }
        
        public Object state { get; set; }

        public List<TreeViewModel> nodes { get; set; }
    }
}