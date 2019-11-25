using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class KnowledgeIndexRepository : RepositoryBase<Yw_KnowledgeIndex>, IKnowledgeIndexRepository
    {
        public KnowledgeIndexRepository() : base(AppSetting.ConnString)
        {
        }

        public int Add(Yw_KnowledgeIndex entity)
        {
            return base.InsertEntity(entity);
        }

        public int Delete(List<int> ids)
        {
            return Execute($"delete from Yw_ResourceIndex where Yrx_Id in @Ids", new { Ids = ids });
        }

        public List<Yw_KnowledgeIndex> GetKnowledgeIndexs(int knowledgeId)
        {
            return base.Query("select *from Yw_KnowledgeIndex where Yki_KnowledgeId=@KnowledgeId",
                new { KnowledgeId = knowledgeId }).ToList();
        }

        public List<int> GetPagingKnowledgeIndexIds(PagingObject paging, int level, string key)
        {
            var strWhere = new StringBuilder();
            var parameters = new DynamicParameters();
            strWhere.Append("Yw_KnowledgeIndex where 1=1");
            if (!string.IsNullOrEmpty(key))
            {
                strWhere.Append(" and Yki_Keyword=@Keyword");
                parameters.Add("Keyword", key);
            }
            if (level > 0)
            {
                strWhere.Append(" and Yki_Level=@Level");
                parameters.Add("Level", level);
            }
            return QueryPaging<int>("Yki_KnowledgeId", strWhere.ToString(), "Yki_CreateTime Desc", paging, parameters).ToList();
        }

        public List<int> GetPagingKnowledgeIndexIds(
            PagingObject paging,
            int level,
            string key,
            int kernelKownledagePointId)
        {
            Check.IfNull(paging, nameof(paging));
            Check.IfNullOrEmpty(key, nameof(key));
            if (level != (int)KnowledgeEnum.四级知识点)
            {
                throw new ArgumentException();
            }
            if (kernelKownledagePointId < 10000 && kernelKownledagePointId != 0)
            {
                throw new AbhsException(
                    ErrorCodeEnum.IdOutOfRange,
                    AbhsErrorMsg.IdOutOfRange);
            }

            var strWhere = new StringBuilder();
            var parameters = new DynamicParameters();
            strWhere.Append("Yw_KnowledgeIndex where 1=1");
            if (kernelKownledagePointId >= 10000)
            {
                strWhere.Append(" AND Yki_KnowledgeId != @knowledgeId");
                parameters.Add("knowledgeId", kernelKownledagePointId);
            }
            if (!string.IsNullOrEmpty(key))
            {
                strWhere.Append(" and Yki_Keyword=@Keyword");
                parameters.Add("Keyword", key);
            }
            if (level > 0)
            {
                strWhere.Append(" and Yki_Level=@Level");
                parameters.Add("Level", level);
            }
            return QueryPaging<int>("Yki_KnowledgeId", strWhere.ToString(), "Yki_CreateTime Desc", paging, parameters).ToList();
        }
    }
}
