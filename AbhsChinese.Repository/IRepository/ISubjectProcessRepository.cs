using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;

namespace AbhsChinese.Repository.IRepository
{
    public interface ISubjectProcessRepository : IRepositoryBase<Yw_SubjectProcess>
    {
        Yw_SubjectProcess GetCurrentProcess(int subjectId);

        Yw_SubjectProcess Insert(Yw_SubjectProcess entity);
    }
}