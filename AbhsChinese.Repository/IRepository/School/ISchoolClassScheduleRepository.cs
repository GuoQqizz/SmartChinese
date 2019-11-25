using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository.School
{
    public interface ISchoolClassScheduleRepository : IRepositoryBase<Yw_SchoolClassSchedule>
    {

        List<Yw_SchoolClassSchedule> GetByClassId(int classId);
        List<Yw_SchoolClassSchedule> GetByClassIds(List<int> classIds);

        int InsertList(List<Yw_SchoolClassSchedule> list);

        bool DeleteByClassIds(List<int> classIds);
    }
}
