using AbhsChinese.Code.Common;

namespace AbhsChinese.Admin.Models.Common
{
    public class Search : IPagination
    {
        public PagingObject Pagination { get; set; } = new PagingObject(1, 10);
        public bool Removed { get; set; } = false;
    }
}