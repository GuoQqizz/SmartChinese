using System.Collections.Generic;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;

namespace AbhsChinese.Repository.IRepository
{
    public interface ICourseProcessRepository : IRepositoryBase<Yw_CourseProcess>
    {
        void Insert(Yw_CourseProcess entity);
        IList<Yw_CourseProcess> GetLastestEntity(int courseId);
    }
}