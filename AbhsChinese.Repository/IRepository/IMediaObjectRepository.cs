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
    public interface IMediaObjectRepository : IRepositoryBase<Yw_MediaObject>
    {
        int Add(Yw_MediaObject entity);

        bool Update(Yw_MediaObject entity);

        Yw_MediaObject GetMediaObject(int id);

        DtoMediaResourceAndObject GetMedia(int id);
        
        List<DtoMediaResourceAndObject> GetMediaList(int grade,int mediaType,string nameOrKey);

        List<Yw_MediaObject> GetAudioAndVideo(string nameOrKey);

        DtoMediaResourceToCourse GetMediaDetailToCourse(int id);

        List<DtoMediaObject> GetMediaObjectByIdList(List<int> ids);

        List<DtoMediaObject> GetMediaObjectByIdListExceptText(List<int> ids);

        int UpdatePropogue(int id, string description, string url);

        DtoMediaForKnowledge GetMediaByKnowledgeId(int knowledgeId);

        List<Yw_MediaResource> GetPagingImgList(PagingObject paging, int grade, int id);
    }
}
