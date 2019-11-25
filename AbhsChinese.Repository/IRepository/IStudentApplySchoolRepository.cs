using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IStudentApplySchoolRepository : IRepositoryBase<Yw_StudentApplySchool>
    {
        List<DtoStudentApplySchool> GetToDoApplyList(DtoApplyStudentSearch request);
        DtoStudentApplySchool GetById(int yayId);
        bool UpdateApplyStatus(int yayId, int toStatus, int fromStatus, int oper);
        int Insert(Yw_StudentApplySchool entity);
        DtoStudentApplySchool GetApplyByStudentId(int studentId);
        bool UpdateApplyStatusByStudentId(int studentId, int toStatus,int fromStatus, int oper);
        
    }
}
