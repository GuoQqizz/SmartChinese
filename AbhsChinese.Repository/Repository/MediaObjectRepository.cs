using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using Dapper;

namespace AbhsChinese.Repository.Repository
{
    public class MediaObjectRepository : RepositoryBase<Yw_MediaObject>, IMediaObjectRepository
    {
        public MediaObjectRepository() : base(AppSetting.ConnString)
        {
        }

        public int Add(Yw_MediaObject entity)
        {
            return base.InsertEntity(entity);
        }

        public Yw_MediaObject GetMediaObject(int id)
        {
            return base.GetEntity(id);
        }

        public DtoMediaResourceAndObject GetMedia(int id)
        {
            return base.QueryObject<DtoMediaResourceAndObject>("select Yw_MediaResource.*,a.*,b.Yme_Id as ImgId,b.Yme_Url as ImgUrl,Yxo_Id,Yxo_Content,a.Yme_Description from Yw_MediaResource left join Yw_MediaObject a on Ymr_MediaObjectId=a.Yme_Id and a.Yme_Status=1 left join Yw_MediaObject b on a.Yme_ImageCoverId=b.Yme_Id and b.Yme_Status=1 left join Yw_TextObject on a.Yme_TextObjectId=Yxo_Id and Yxo_Status=1 where Ymr_Id=@Id", new { Id = id }).FirstOrDefault();
        }
        
        public bool Update(Yw_MediaObject entity)
        {
            return base.UpdateEntity(entity);
        }

        public List<DtoMediaResourceAndObject> GetMediaList(int grade, int mediaType, string nameOrKey)
        {
            if (grade <= 0 && mediaType <= 0 && string.IsNullOrEmpty(nameOrKey))
            {
                return new List<DtoMediaResourceAndObject>();
            }
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append("select a.*,Yxo_Id,Yxo_Content,b.Ymr_Id as ImgId,b.Ymr_Name as ImgName,b.Ymr_Url as ImgUrl from Yw_MediaResource a left join Yw_TextObject on a.Ymr_TextObjectId=Yxo_Id left join Yw_MediaResource b on a.Ymr_ImageCoverId=b.Ymr_Id where 1=1");
            if (nameOrKey.IsNumberic())
            {
                strSql.Append(" and a.Ymr_Id=@Id");
                parameters.Add("Id", nameOrKey);
            }
            else
            {
                strSql.Append(" and a.Ymr_Id in (select Yrx_ResourceId from Yw_ResourceIndex where Yrx_ResourceType>100");
                if (grade > 0)
                {
                    strSql.Append(" and Yrx_Grade=@Grade");
                    parameters.Add("Grade", grade);
                }
                if (!string.IsNullOrEmpty(nameOrKey))
                {
                    strSql.Append(" and Yrx_Keyword=@NameOrKey");
                    parameters.Add("NameOrKey", nameOrKey);
                }
                if (mediaType > 100 && mediaType < 104)
                {
                    strSql.Append(" and Yrx_ResourceType=@ResourceType");
                    parameters.Add("ResourceType", mediaType);
                }
                strSql.Append(")");
            }
            return base.QueryObject<DtoMediaResourceAndObject>(strSql._ToString(), parameters).ToList();
        }

        public List<Yw_MediaObject> GetAudioAndVideo(string nameOrKey)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append("select *from Yw_MediaResource where (Ymr_MediaType=1 or Ymr_MediaType=2)");
            if (!string.IsNullOrEmpty(nameOrKey))
            {
                if (nameOrKey.IsNumberic())
                {
                    strSql.Append(" and Ymr_Id=@Id");
                    parameters.Add("Id", nameOrKey);
                }
                else
                {
                    strSql.Append(
                   " and Ymr_Id in (select Yrx_ResourceId from Yw_ResourceIndex where Yrx_Keyword=@NameOrKey and Yrx_ResourceType=2)");
                    parameters.Add("NameOrKey", nameOrKey);
                }
            }
            return base.Query(strSql._ToString(), parameters).ToList();
        }

        public DtoMediaResourceToCourse GetMediaDetailToCourse(int id)
        {
            var sql = "select Ymr_Id as MediaID,Ymr_Name as MediaName,Ymr_MediaType as MediaType,Ymr_Grade as Grade,Ymr_Keywords as KeyWords,a.Yme_Id as ObjectID,a.Yme_MediaType as ObjectType,a.Yme_Url as MediaUrl,b.Yme_Id as ObjectImgID,b.Yme_Url as ImgUrl,Yxo_Id as ObjectTextID,Yxo_Content as ObjectText from Yw_MediaResource join Yw_MediaObject a on Ymr_MediaObjectId=a.Yme_Id left join Yw_MediaObject b on a.Yme_ImageCoverId=b.Yme_Id and b.Yme_Status=1 left join Yw_TextObject on a.Yme_TextObjectId=Yxo_Id and Yxo_Status=1 where a.Yme_Id=@Id";
            return base.QueryObject<DtoMediaResourceToCourse>(sql, new { Id = id }).FirstOrDefault();
        }

        public List<DtoMediaObject> GetMediaObjectByIdList(List<int> ids)
        {
            var sql = "select a.Yme_Id as MediaId,Ymr_Name as MediaName,Ymr_MediaType as MediaType,a.Yme_Url as MediaUrl,a.Yme_Description as Description,b.Yme_Id as ImgId,b.Yme_Url as ImgUrl,Yxo_Id as TextId,Yxo_Content as TextContent from Yw_MediaObject a left join Yw_MediaResource on Yme_Id=Ymr_MediaObjectId left join Yw_MediaObject b on a.Yme_ImageCoverId=b.Yme_Id left join Yw_TextObject on a.Yme_TextObjectId=Yxo_Id where a.Yme_Id in @Id";
            return base.QueryObject<DtoMediaObject>(sql, new { Id = ids }).ToList();
        }

        public List<DtoMediaObject> GetMediaObjectByIdListExceptText(List<int> ids)
        {
            var sql = "select a.Yme_Id as MediaId,Ymr_Name as MediaName,Ymr_MediaType as MediaType,a.Yme_Url as MediaUrl,a.Yme_Description as Description,b.Yme_Id as ImgId,b.Yme_Url as ImgUrl from Yw_MediaObject a left join Yw_MediaResource on Yme_Id=Ymr_MediaObjectId left join Yw_MediaObject b on a.Yme_ImageCoverId=b.Yme_Id where a.Yme_Id in @Id";
            return base.QueryObject<DtoMediaObject>(sql, new { Id = ids }).ToList();
        }

        public int UpdatePropogue(int id,string description,string url)
        {
            string sql = "update Yw_MediaObject set Yme_Url=@Url,Yme_Description=@Description where Yme_Id=@Id";
            return Execute(sql, new { Url=url,Description = description, Id = id });
        }

        public DtoMediaForKnowledge GetMediaByKnowledgeId(int knowledgeId)
        {
            string sql = "select Yme_Id,Yme_MediaType,Yme_Url,Ykl_Id,Ykl_Name,Ykl_ParentId,Ykl_Level from Yw_Knowledge left join Yw_MediaResource on Ykl_ResourceId=Ymr_Id left join Yw_MediaObject on Ymr_MediaObjectId=Yme_Id and Yme_Status=1 where Ykl_Id=@KnowledgeId";
            return QueryObject<DtoMediaForKnowledge>(sql, new { KnowledgeId = knowledgeId }).FirstOrDefault();
        }

        public List<Yw_MediaResource> GetPagingImgList(PagingObject paging, int grade,int id)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append($"Yw_MediaResource where Ymr_MediaType=103 and Ymr_Status=1");
            if(id>0)
            {
                strSql.Append(" and Ymr_Id=@Id");
                parameters.Add("Id", id);
            }
            else
            {
                if (grade > 0)
                {
                    strSql.Append(" and Ymr_Grade=@Grade");
                    parameters.Add("Grade", grade);
                }
            }
            return QueryPaging<Yw_MediaResource>("*", strSql._ToString(), "Ymr_CreateTime DESC", paging, parameters).ToList();
        }
    }
}
