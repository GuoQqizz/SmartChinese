using AbhsChinese.Code.Common;

namespace AbhsChinese.Domain.Dto.Request
{
    public class DtoSearch
    {
        public PagingObject Pagination { get; set; } = new PagingObject(1, 10);
        public bool Removed { get; set; } = false;
    }
}