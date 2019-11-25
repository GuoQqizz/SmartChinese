using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IStuStudyTaskAnsRepository : IRepositoryBase<Yw_StudentStudyTaskAnswer>
    {
        Yw_StudentStudyTaskAnswer GetByStudentTask(int studentId, int taskId);

        void Add(Yw_StudentStudyTaskAnswer entity);

        void Update(Yw_StudentStudyTaskAnswer entity);

        Yw_StudentStudyTaskAnswer Get(int id);
    }
}
