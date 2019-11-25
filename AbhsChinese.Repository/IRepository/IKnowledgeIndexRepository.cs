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
    public interface IKnowledgeIndexRepository : IRepositoryBase<Yw_KnowledgeIndex>
    {
        int Add(Yw_KnowledgeIndex entity);

        int Delete(List<int> ids);

        List<Yw_KnowledgeIndex> GetKnowledgeIndexs(int knowledgeId);

        List<int> GetPagingKnowledgeIndexIds(PagingObject paging, int level, string key);
        List<int> GetPagingKnowledgeIndexIds(
            PagingObject paging, 
            int level, 
            string key,
            int kernelKownledagePointId);
    }
}
