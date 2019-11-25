using System.Collections.Generic;
using System.Linq;

namespace AbhsChinese.Admin.Models.Common
{
    public static class OptionFactory
    {
        public static IList<Select2Option> CreateOptions(IDictionary<int, string> options)
        {
            return options.Select(o => new Select2Option()
            {
                id = o.Key,
                text = o.Value
            }).ToList();
        }
    }
}