using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository.School
{
    public interface ISchoolClassRepository : IRepositoryBase<Yw_SchoolClass>
    {
        List<DtoSchoolClass> GetSchoolClassList(DtoSchoolClassSearch search);
        List<Yw_SchoolClass> GetSchoolClassByCourseId(int courseId, int schoolId);
        Yw_SchoolClass Get(int id);
        bool Update(Yw_SchoolClass entity);

        int Insert(Yw_SchoolClass obj);

        bool IncrementStudentCount(int classId, int count,int oper);
    }
}
