using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Dto.Request;

namespace AbhsChinese.Repository.IRepository
{
    public interface IKnowledgeRepository : IRepositoryBase<Yw_Knowledge>
    {
        int Add(Yw_Knowledge entity);

        List<Yw_Knowledge> GetPagingKnowledge(PagingObject paging, int id,int level);

        List<Yw_Knowledge> GetKnowledgeByIds(List<int> ids);

        bool Update(Yw_Knowledge entity);

        DtoKnowledge GetKnowledge(int id);

        Yw_Knowledge Get(int id);

        List<Yw_Knowledge> GetKnowledgeTreeByParent(int parentId);

        List<Yw_Knowledge> GetKnowledgeTreeByChild(int id);
        
        List<Yw_Knowledge> GetKnowledgeTreeByIds(List<int> ids);

        int UpdatePath(string oldPath, string newPath);

        bool IsLeaf(int id);

        List<Yw_Knowledge> GetTopKnowledgeByPath(List<string> paths);

        List<Yw_Knowledge> GetTopKnowledge(List<int> ids);
        IList<Yw_Knowledge> GetKnowledgeByIds(DtoKnowledgeSearch search);
    }
}
