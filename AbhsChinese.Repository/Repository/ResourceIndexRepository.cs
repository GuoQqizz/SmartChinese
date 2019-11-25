using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using AbhsChinese.Code.Common;
using Dapper;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Repository.Repository
{
    public class ResourceIndexRepository : RepositoryBase<Yw_ResourceIndex>, IResourceIndexRepository
    {
        public ResourceIndexRepository() : base(AppSetting.ConnString)
        {
        }

        public int Add(Yw_ResourceIndex entity)
        {
            return base.InsertEntity(entity);
        }

        public List<Yw_ResourceIndex> GetResourceIndices(int resourceId, int resourceCategory)
        {
            return base.Query("select *from Yw_ResourceIndex where Yrx_ResourceId=@ResourceId and Yrx_ResourceCategory=@ResourceCategory",
                new { ResourceId = resourceId, ResourceCategory = resourceCategory }).ToList();
        }

        public int Delete(int[] ids)
        {
            return base.Execute($"delete from Yw_ResourceIndex where Yrx_Id in @Ids", new { Ids = ids });
        }

        public List<int> GetResourceIndexIds(PagingObject paging, int grade, int mediaType, int textType, string nameOrKey, ResourceTypeEnum resourceType)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();

            if (resourceType == ResourceTypeEnum.文本资源)
            {
                strSql.Append($"Yw_ResourceIndex where Yrx_ResourceType<{(int)MediaResourceTypeEnum.视频}");
            }
            else if (resourceType == ResourceTypeEnum.多媒体资源)
            {
                strSql.Append($"Yw_ResourceIndex where Yrx_ResourceType>100 and Yrx_ResourceType<{(int)MediaResourceTypeEnum.开场语}");
            }

            if (grade > 0)
            {
                strSql.Append(" and Yrx_Grade=@Grade");
                parameters.Add("Grade", grade);
            }

            if (mediaType >= (int)MediaResourceTypeEnum.视频 && mediaType < (int)MediaResourceTypeEnum.开场语)
            {
                strSql.Append(" and Yrx_ResourceType=@ResourceType");
                parameters.Add("ResourceType", mediaType);
            }
            else if (textType >= (int)TextResourceTypeEnum.现代文 && textType < (int)MediaResourceTypeEnum.视频)
            {
                strSql.Append(" and Yrx_ResourceType=@ResourceType");
                parameters.Add("ResourceType", textType);
            }

            if (!string.IsNullOrEmpty(nameOrKey))
            {
                strSql.Append(" and Yrx_Keyword=@NameOrKey");
                parameters.Add("NameOrKey", nameOrKey);
            }

            return QueryPaging<int>("Yrx_ResourceId", strSql._ToString(), "Yrx_CreateTime DESC", paging, parameters).ToList();
        }

        public int UpdateByIds(List<int> ids, int resourceType, int grade)
        {
            var sql = "update Yw_ResourceIndex set Yrx_ResourceType=@ResourceType,Yrx_Grade=@Grade where Yrx_Id in @Ids";
            return Execute(sql, new { ResourceType = resourceType, Grade = grade, Ids = ids });
        }

        public List<int> GetMediaIndexIds(PagingObject paging, int grade, int mediaType, string nameOrKey)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append($"Yw_ResourceIndex where Yrx_ResourceType>100 and Yrx_ResourceType<104");
            if (grade > 0)
            {
                strSql.Append(" and Yrx_Grade=@Grade");
                parameters.Add("Grade", grade);
            }
            if (mediaType >= (int)MediaResourceTypeEnum.视频 && mediaType < (int)MediaResourceTypeEnum.小艾说)
            {
                strSql.Append(" and Yrx_ResourceType=@ResourceType");
                parameters.Add("ResourceType", mediaType);
            }
            if (!string.IsNullOrEmpty(nameOrKey))
            {
                strSql.Append(" and Yrx_Keyword=@NameOrKey");
                parameters.Add("NameOrKey", nameOrKey);
            }

            return QueryPaging<int>("Yrx_ResourceId", strSql._ToString(), "Yrx_CreateTime DESC", paging, parameters).ToList();
        }
    }
}
