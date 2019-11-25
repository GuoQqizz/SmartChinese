using System.Collections.Generic;

namespace AbhsChinese.Admin.Models.Common
{
    public class Select2
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Multiple { get; set; } = string.Empty;
        public string Placeholder { get; set; } = "请选择";
        public IList<string> Values { get; set; }
    }
}