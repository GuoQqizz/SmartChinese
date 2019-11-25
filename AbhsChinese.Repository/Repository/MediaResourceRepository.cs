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
    public class MediaResourceRepository : RepositoryBase<Yw_MediaResource>, IMediaResourceRepository
    {
        public MediaResourceRepository() : base(AppSetting.ConnString)
        {
        }

        public int Add(Yw_MediaResource entity)
        {
            return base.InsertEntity(entity);
        }

        public Yw_MediaResource GetMediaResource(int id)
        {
            return base.GetEntity(id);
        }

        public DtoAudioResource GetAudioMedia(int id)
        {
            return QueryObject<DtoAudioResource>("select Yw_MediaResource.*,a.Yme_Id as AudioId,a.Yme_Url as AudioUrl,b.Yme_Id as ImgId,b.Yme_Url as ImgUrl,Yxo_Id,Yxo_Content from Yw_MediaResource join Yw_MediaObject a on Ymr_MediaObjectId=a.Yme_Id and a.Yme_Status=1 left join Yw_MediaObject b on a.Yme_ImageCoverId=b.Yme_Id and b.Yme_Status=1 left join Yw_TextObject on a.Yme_TextObjectId=Yxo_Id and Yxo_Status=1 where Ymr_Id=@Id", new { Id = id }).FirstOrDefault();
        }

        public bool Update(Yw_MediaResource entity)
        {
            return UpdateEntity(entity);
        }

        public List<Yw_MediaResource> GetMediaListByIds(List<int> resourceIds)
        {
            var strSql = new StringBuilder();
            strSql.Append("select * from Yw_MediaResource where Ymr_Status=1 and Ymr_Id in @ResourceIds");
            return QueryObject<Yw_MediaResource>(strSql._ToString(), new { ResourceIds = resourceIds }).ToList();
        }

        public List<Yw_MediaResource> GetPagingMediaList(PagingObject paging, int resourceId, int grade, int mediaType, bool isList, int status)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append($"Yw_MediaResource where Ymr_MediaType>100 and Ymr_MediaType<{(int)MediaResourceTypeEnum.开场语}");
            if (isList)
            {
                if (mediaType > 100 && mediaType < 105)
                {
                    strSql.Append(" and Ymr_MediaType=@MediaType");
                    parameters.Add("MediaType", mediaType);
                }
            }
            if (status > 0)
            {
                strSql.Append(" and Ymr_Status=@Status");
                parameters.Add("Status", status);
            }
            if (resourceId > 0)
            {
                strSql.Append(" and Ymr_Id=@Id");
                parameters.Add("Id", resourceId);
            }
            else
            {
                if (grade > 0)
                {
                    strSql.Append(" and Ymr_Grade=@Grade");
                    parameters.Add("Grade", grade);
                }
                if (mediaType > 100 && mediaType < 105)
                {
                    strSql.Append(" and Ymr_MediaType=@MediaType");
                    parameters.Add("MediaType", mediaType);
                }
            }
            return QueryPaging<Yw_MediaResource>("*", strSql._ToString(), "Ymr_CreateTime DESC", paging, parameters).ToList();
        }

        public List<Yw_MediaResource> GetAudioAndVideo(PagingObject paging, string nameOrKey)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append("Yw_MediaResource where (Ymr_MediaType=101 or Ymr_MediaType=102)");
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
                   " and Ymr_Id in (select Yrx_ResourceId from Yw_ResourceIndex where Yrx_Keyword=@NameOrKey and (Yrx_ResourceType=101 or Yrx_ResourceType=102))");
                    parameters.Add("NameOrKey", nameOrKey);
                }
            }
            return QueryPaging<Yw_MediaResource>("*", strSql._ToString(), "Ymr_Id", paging, parameters).ToList();
        }

        public List<DtoMediaResourceAndObject> GetXiaoAiBianOrPrologue(PagingObject paging, int mediaType)
        {
            var sql = "Yw_MediaResource inner join Yw_MediaObject on Ymr_MediaObjectId=Yme_Id where Ymr_MediaType=@MediaType and Ymr_Status=1 and Yme_Status=1";
            return QueryPaging<DtoMediaResourceAndObject>("*", sql, "Ymr_CreateTime DESC", paging, new { MediaType = mediaType }).ToList();
        }

        public List<DtoMediaResourceToCourse> GetMediaToCourse(PagingObject paging, int courseId, int resourceType, string nameOrkey)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            var fields = "Ymr_Id as MediaID,Ymr_Name as MediaName,Ymr_MediaType as MediaType,Ymr_Grade as Grade,Ymr_Keywords as KeyWords,Yme_Id as ObjectID,Yme_MediaType as ObjectType,Yme_Url as MediaUrl";
            strSql.Append("Yw_MediaResource inner join Yw_MediaObject on Ymr_MediaObjectId=Yme_Id and Yme_Status=1 where Ymr_Id in (select b.Yrx_ResourceId from((select Ygi_ResourceId from Yw_Course join Yw_ResourceGroupItem on Ygi_GroupId=Ycs_ResourceGroupId  where Ycs_Id=@CourseId");
            parameters.Add("CourseId", courseId);
            if (resourceType > 0)
            {
                strSql.Append(" and Ygi_ResourceType=2");
            }
            strSql.Append(")a inner join (select Yrx_ResourceId from Yw_ResourceIndex where 1=1");
            if (!string.IsNullOrEmpty(nameOrkey))
            {
                strSql.Append(" and Yrx_Keyword=@Keyword");
                parameters.Add("Keyword", nameOrkey);
            }
            strSql.Append(")b on a.Ygi_ResourceId=b.Yrx_ResourceId))");

            return QueryPaging<DtoMediaResourceToCourse>(fields, strSql.ToString(), "Ymr_CreateTime DESC", paging, parameters).ToList();
        }

        public List<DtoMediaResourceToCourse> GetMediaToCourseById(PagingObject paging, int mediaType, int courseId, int resourceId)
        {
            var fields = "Ymr_Id as MediaID,Ymr_Name as MediaName,Ymr_MediaType as MediaType,Ymr_Grade as Grade,Ymr_Keywords as KeyWords,Yme_Id as ObjectID,Yme_MediaType as ObjectType,Yme_Url as MediaUrl";
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append("Yw_MediaResource inner join Yw_MediaObject on Ymr_MediaObjectId=Yme_Id and Yme_Status=1 where Ymr_Id in (select a.Ygi_ResourceId from(select Ygi_ResourceId from Yw_Course join Yw_ResourceGroupItem on Ygi_GroupId=Ycs_ResourceGroupId  where Ycs_Id=@CourseId and Ygi_ResourceType=2");
            parameters.Add("CourseId", courseId);

            if (resourceId > 0)
            {
                strSql.Append(" and Ygi_ResourceId=@ResourceId");
                parameters.Add("ResourceId", resourceId);
            }
            strSql.Append(")a)");
            if (mediaType > 0)
            {
                strSql.Append(" and Ymr_MediaType=@MediaType");
                parameters.Add("MediaType", mediaType);
            }

            return QueryPaging<DtoMediaResourceToCourse>(fields, strSql.ToString(), "Ymr_CreateTime DESC", paging, parameters).ToList();
        }

        public List<DtoMediaResourceToCourse> GetPrologues(string description)
        {
            var sql = "select Yme_Id as MediaID,Yme_Description as MediaName from Yw_MediaResource join Yw_MediaObject on Ymr_MediaObjectId=Yme_Id where Ymr_MediaType=105 and Yme_Description like @Description";
            return base.QueryObject<DtoMediaResourceToCourse>(sql, new { Description = "%" + description + "%" }).ToList();
        }

        public List<DtoMediaResourceToCourse> GetXiaoAiToCourse(PagingObject paging, string name, int mediaType)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append("Yw_MediaResource join Yw_MediaObject on Ymr_MediaObjectId=Yme_Id and Yme_Status=1 where Ymr_Status=1");
            if (mediaType > 0)
            {
                strSql.Append(" and Ymr_MediaType=@MediaType");
                parameters.Add("MediaType", mediaType);
            }
            if (name.IsNumberic() && name.Length >= 5)
            {
                strSql.Append(" and Ymr_Id=@Id");
                parameters.Add("Id", name);
            }
            else
            {
                strSql.Append(" and Ymr_Name like @Name");
                parameters.Add("Name", "%" + name + "%");
            }
            return QueryPaging<DtoMediaResourceToCourse>("Ymr_Id as MediaID,Ymr_Name as MediaName,Ymr_MediaType as MediaType,Ymr_Grade as Grade,Ymr_Keywords as KeyWords,Yme_Id as ObjectID,Yme_MediaType as ObjectType,Yme_Url as MediaUrl", strSql.ToString(), "Ymr_Id", paging, parameters).ToList();
        }

        public DtoMediaResourceToCourse GetPrologueById(int prologueId)
        {
            var sql = "select Yme_Id as MediaID,Yme_Description as MediaName from Yw_MediaResource join Yw_MediaObject on Ymr_MediaObjectId=Yme_Id where Ymr_MediaType=105 and Yme_Id=@Id";
            return base.QueryObject<DtoMediaResourceToCourse>(sql, new { Id = prologueId }).FirstOrDefault();
        }

        public List<DtoResourceGroupItem> GetMediaForGroupItem(PagingObject paging, List<int> ids)
        {
            var fields = "Ymr_Id as Id,Ymr_Name as Name,Ymr_MediaType as MediaType,Ymr_Grade as Grade,Yme_Url as Url,Ymr_Keywords as Keywords,Ymr_Status as Status,Ymr_CreateTime as CreateTime,Ymr_Creator as Creator,Ymr_UpdateTime as UpdateTime,Ymr_Editor as Editor";
            var sql = "Yw_MediaResource join Yw_MediaObject on Ymr_MediaObjectId=Yme_Id where Ymr_Id in @Ids";
            return QueryPaging<DtoResourceGroupItem>(fields, sql, "Ymr_CreateTime DESC", paging, new { Ids = ids }).ToList();
        }

        public List<Yw_MediaResource> GetMediaListForGroup(PagingObject paging, int resourceId, int grade, int mediaType)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append($"Yw_MediaResource where Ymr_MediaType>100 and Ymr_MediaType<{(int)MediaResourceTypeEnum.小艾说} and Ymr_Status=1");
            if (resourceId > 0)
            {
                strSql.Append(" and Ymr_Id=@Id");
                parameters.Add("Id", resourceId);
            }
            else
            {
                if (grade > 0)
                {
                    strSql.Append(" and Ymr_Grade=@Grade");
                    parameters.Add("Grade", grade);
                }
                if (mediaType > 100 && mediaType < 104)
                {
                    strSql.Append(" and Ymr_MediaType=@MediaType");
                    parameters.Add("MediaType", mediaType);
                }
            }
            return QueryPaging<Yw_MediaResource>("*", strSql._ToString(), "Ymr_CreateTime DESC", paging, parameters).ToList();
        }
    }
}
