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
    public interface ITextResourceRepository : IRepositoryBase<Yw_TextResource>
    {
        int Add(Yw_TextResource entity);

        bool Update(Yw_TextResource entity);

        DtoTextResource GetDtoTextResource(int id);

        Yw_TextResource GetTextResource(int id);

        List<Yw_TextResource> GetTextListByIds(List<int> resourceIds);

        List<Yw_TextResource> GetPagingTextList(PagingObject paging, int resourceId, int grade, int textType,bool isList, int status);

        List<DtoMediaResourceToCourse> GetTextToCourse(PagingObject paging, int courseId, int resourceType, string nameOrkey);

        List<DtoMediaResourceToCourse> GetTextToCourseById(PagingObject paging, int courseId, int textType,int resourceId);

        List<DtoResourceGroupItem> GetTextForGroupItem(PagingObject paging, List<int> ids);
    }
}
