using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request.Subject;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System.Collections.Generic;

namespace AbhsChinese.Repository.IRepository
{
    public interface ISubjectIndexRepository : IRepositoryBase<Yw_SubjectIndex>
    {
        void Delete(IEnumerable<int> ids, bool physicalDeletion = false);

        IEnumerable<Yw_SubjectIndex> GetSubjectIndexBySubject(int id);

        Yw_SubjectIndex Insert(Yw_SubjectIndex entity);

        IEnumerable<Yw_SubjectIndex> Insert(IEnumerable<Yw_SubjectIndex> entities);
        IList<int> GetSubjectIdsByKeyword(DtoQuestionSearch search);
        void Update(Yw_SubjectIndex entity);
        IList<int> GetPagingSubjectIds(PagingObject paging,int grade,int subjectType,string nameOrkey);
    }
}