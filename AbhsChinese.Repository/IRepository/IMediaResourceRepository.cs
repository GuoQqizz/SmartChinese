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
    public interface IMediaResourceRepository : IRepositoryBase<Yw_MediaResource>
    {
        int Add(Yw_MediaResource entity);

        bool Update(Yw_MediaResource entity);
        
        Yw_MediaResource GetMediaResource(int id);

        DtoAudioResource GetAudioMedia(int id);
        
        List<Yw_MediaResource> GetMediaListByIds(List<int> resourceIds);

        List<Yw_MediaResource> GetPagingMediaList(PagingObject paging, int resourceId, int grade, int mediaType,bool isList,int status);

        List<Yw_MediaResource> GetAudioAndVideo(PagingObject paging, string nameOrKey);

        List<DtoMediaResourceAndObject> GetXiaoAiBianOrPrologue(PagingObject paging, int mediaType);

        List<DtoMediaResourceToCourse> GetMediaToCourse(PagingObject paging, int courseId, int resourceType, string nameOrkey);

        List<DtoMediaResourceToCourse> GetMediaToCourseById(PagingObject paging, int mediaType, int courseId, int resourceId);

        List<DtoMediaResourceToCourse> GetXiaoAiToCourse(PagingObject paging,string name, int mediaType);

        List<DtoMediaResourceToCourse> GetPrologues(string description);

        DtoMediaResourceToCourse GetPrologueById(int prologueId);

        List<DtoResourceGroupItem> GetMediaForGroupItem(PagingObject paging,List<int> ids);

        List<Yw_MediaResource> GetMediaListForGroup(PagingObject paging, int resourceId, int grade, int mediaType);
    }
}
