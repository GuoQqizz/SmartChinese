using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface ICourseIntroductionRespository : IRepositoryBase<Yw_CourseIntroduction>
    {
        Yw_CourseIntroduction GetCourseIntroduction(int courseId);
        void Update(Yw_CourseIntroduction entity);
        void Insert(Yw_CourseIntroduction entity);
    }
}
