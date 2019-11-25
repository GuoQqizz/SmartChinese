using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface ISumStudyFactDateCycleRepository
    {
        void Add(Sum_StudyFactDateCycle entity);

        void Update(Sum_StudyFactDateCycle entity);

        Sum_StudyFactDateCycle GetByStudentDate(int studentId, int classId, DateTime asOfDate, DateCycleTypeEnum cycle);

        List<Sum_StudyFactDateCycle> GetByStudent(int studentId, int classId);
    }
}
