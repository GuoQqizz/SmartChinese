using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System.Collections.Generic;

namespace AbhsChinese.Repository.IRepository
{
    public interface ISubjectKnowledgeRepository : IRepositoryBase<Yw_SubjectKnowledge>
    {
        void Delete(IEnumerable<int> ids, bool physicalDeletion = false);

        IEnumerable<Yw_SubjectKnowledge> GetKnowledgesBySubject(int subjectId);
        IEnumerable<DtoKnowledge> GetKnowledgesWithNamesBySubject(int subjectId);

        Yw_SubjectKnowledge Insert(Yw_SubjectKnowledge entity);

        void Insert(IEnumerable<Yw_SubjectKnowledge> entities);

        void Update(Yw_SubjectKnowledge entity);

        void Update(IEnumerable<Yw_SubjectKnowledge> entities);
    }
}