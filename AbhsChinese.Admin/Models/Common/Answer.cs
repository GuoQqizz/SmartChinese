using System.Collections.Generic;

namespace AbhsChinese.Admin.Models.Common
{
    public class Answer
    {
        public IList<string> Contents { get; set; } = new List<string> { "A", "B", "C", "D" };
    }
}