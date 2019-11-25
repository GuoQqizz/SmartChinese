using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface ISubjectGroupRepository: IRepositoryBase<Yw_SubjectGroup>
    {
        Yw_SubjectGroup GetBySubjectId(int subjectId);
        IList<Yw_SubjectGroup> GetBySubjectIds(IEnumerable<int> ids);
        void Update(Yw_SubjectGroup entity);
        void Insert(Yw_SubjectGroup entity);
    }
}
