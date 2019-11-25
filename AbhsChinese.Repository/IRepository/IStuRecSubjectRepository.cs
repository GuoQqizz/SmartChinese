using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IStuRecSubjectRepository : IRepositoryBase<Yw_StudentRecentSubject>
    {
        List<Yw_StudentRecentSubject> GetByStudent(int studentId);

        void Add(Yw_StudentRecentSubject subject);

        void AddBatch(List<DtoSimpleStuRecentSub> objs);

        void DeleteByStudent(int studentId, List<int> subjectIds);

        void UpdateToLastestforStudent(int studentId, List<int> subjectIds);
    }
}
