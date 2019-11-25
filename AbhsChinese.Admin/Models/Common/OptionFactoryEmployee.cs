using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Common
{
    public class OptionFactoryEmployee
    {
        public static IList<OptionEmployee> CreateOptions(IDictionary<string, string> options)
        {
            return options.Select(o => new OptionEmployee {
                id = o.Key,
                text = o.Value
            }).ToList();
        }
    }
}