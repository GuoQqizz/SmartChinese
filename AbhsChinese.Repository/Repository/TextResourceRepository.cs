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
    public class TextResourceRepository : RepositoryBase<Yw_TextResource>, ITextResourceRepository
    {
        public TextResourceRepository() : base(AppSetting.ConnString)
        {
        }

        public int Add(Yw_TextResource entity)
        {
            return base.InsertEntity(entity);
        }

        public DtoTextResource GetDtoTextResource(int id)
        {
            var strSql = "select Yw_TextResource.*,Yw_TextObject.Yxo_Content from Yw_TextResource join Yw_TextObject on Ytr_TextObjectId=Yxo_Id where Ytr_Id=@Id";
            return QueryObject<DtoTextResource>(strSql, new { Id = id }).FirstOrDefault();
        }

        public List<Yw_TextResource> GetPagingTextList(PagingObject paging, int resourceId, int grade, int textType,bool isList,int status)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append("Yw_TextResource where 1=1");
            if (isList)
            {
                if (textType > 0)
                {
                    strSql.Append(" and Ytr_TextType=@TextType");
                    parameters.Add("TextType", textType);
                }
            }
            if (status > 0)
            {
                strSql.Append(" and Ytr_Status=@Status");
                parameters.Add("Status", status);
            }
            if (resourceId > 0)
            {
                strSql.Append(" and Ytr_Id=@Id");
                parameters.Add("Id", resourceId);
            }
            else
            {
                if (grade > 0)
                {
                    strSql.Append(" and Ytr_Grade=@Grade");
                    parameters.Add("Grade", grade);
                }
                if (textType > 0)
                {
                    strSql.Append(" and Ytr_TextType=@TextType");
                    parameters.Add("TextType", textType);
                }
            }
            return QueryPaging<Yw_TextResource>("*", strSql._ToString(), "Ytr_CreateTime DESC", paging, parameters).ToList();
        }

        public List<Yw_TextResource> GetTextListByIds(List<int> resourceIds)
        {
            var strSql = "select *from Yw_TextResource where Ytr_Status=1 and Ytr_Id in @Ids";
            return Query(strSql, new { Ids = resourceIds }).ToList();
        }

        public bool Update(Yw_TextResource entity)
        {
            return base.UpdateEntity(entity);
        }

        public Yw_TextResource GetTextResource(int id)
        {
            var entity = base.GetEntity(id);
            if (entity != null)
            {
                entity.EnableAudit();
            }
            return entity;
        }

        public List<DtoMediaResourceToCourse> GetTextToCourse(PagingObject paging, int courseId, int resourceType, string nameOrkey)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            var fields = "Ytr_Id as MediaID,Ytr_Name as MediaName,Ytr_TextObjectId as ObjectTextID";
            strSql.Append("Yw_TextResource where Ytr_Id in (select b.Yrx_ResourceId from((select Ygi_ResourceId from Yw_Course join Yw_ResourceGroupItem on Ygi_GroupId=Ycs_ResourceGroupId  where Ycs_Id=@CourseId");
            parameters.Add("CourseId", courseId);
            if (resourceType == 0)
            {
                strSql.Append(" and Ygi_ResourceType=1");
            }
            strSql.Append(")a inner join (select Yrx_ResourceId from Yw_ResourceIndex where 1=1");
            if (!string.IsNullOrEmpty(nameOrkey))
            {
                strSql.Append(" and Yrx_Keyword=@Keyword");
                parameters.Add("Keyword", nameOrkey);
            }
            strSql.Append(")b on a.Ygi_ResourceId=b.Yrx_ResourceId))");
            return QueryPaging<DtoMediaResourceToCourse>(fields, strSql.ToString(), "Ytr_CreateTime DESC", paging, parameters).ToList();
        }

        public List<DtoMediaResourceToCourse> GetTextToCourseById(PagingObject paging, int textType, int courseId, int resourceId)
        {
            var fields = "Ytr_Id as MediaID,Ytr_Name as MediaName,Ytr_TextObjectId as ObjectTextID";
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append("Yw_TextResource where Ytr_Id in (select a.Ygi_ResourceId from(select Ygi_ResourceId from Yw_Course join Yw_ResourceGroupItem on Ygi_GroupId=Ycs_ResourceGroupId  where Ycs_Id=@CourseId");
            parameters.Add("CourseId", courseId);
            if (textType == 0)
            {
                strSql.Append(" and Ygi_ResourceType=1");
            }
            if (resourceId > 0)
            {
                strSql.Append(" and Ygi_ResourceId=@ResourceId");
                parameters.Add("ResourceId", resourceId);
            }
            strSql.Append(")a)");
            return QueryPaging<DtoMediaResourceToCourse>(fields, strSql.ToString(), "Ytr_CreateTime DESC", paging, parameters).ToList();
        }

        public List<DtoResourceGroupItem> GetTextForGroupItem(PagingObject paging, List<int> ids)
        {
            var fields = "Ytr_Id as Id,Ytr_Name as Name,Ytr_TextType as TextType,Ytr_Grade as Grade,Yxo_Content as Content,Ytr_Keywords as Keywords,Ytr_Status as Status,Ytr_CreateTime as CreateTime,Ytr_Creator as Creator,Ytr_UpdateTime as UpdateTime,Ytr_Editor as Editor";
            var sql = "Yw_TextResource join Yw_TextObject on Ytr_TextObjectId=Yxo_Id where Ytr_Id in @Ids";
            return QueryPaging<DtoResourceGroupItem>(fields, sql, "Ytr_CreateTime DESC", paging, new { Ids = ids }).ToList();
        }
    }
}
