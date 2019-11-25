using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;

namespace AbhsChinese.Repository.IRepository
{
    public interface ITextObjectRepository : IRepositoryBase<Yw_TextObject>
    {
        int Add(Yw_TextObject entity);

        bool Update(Yw_TextObject entity);

        Yw_TextObject GeTextObject(int id);

        DtoMediaResourceToCourse GetTextDetailToCourse(int id);

        List<Yw_TextObject> GetTextObjectByIdList(List<int> ids);
    }
}
