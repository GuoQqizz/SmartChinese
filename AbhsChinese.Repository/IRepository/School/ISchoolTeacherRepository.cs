using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Dto.Response;

namespace AbhsChinese.Repository.IRepository.School
{
    public interface ISchoolTeacherRepository : IRepositoryBase<Yw_SchoolTeacher>
    {
        List<DtoSchoolTeacher> GetSchoolTeacherList(DtoSchoolTeacherSearch search);

        int GetCountByPhone(string phone);

        DtoSchoolTeacher GetSchoolTeacherByPhone(string phone);

        bool UpdatePwd(int id, string pwd, int edit);

        Yw_SchoolTeacher Get(int id);
        int Insert(Yw_SchoolTeacher obj);

        bool Update(Yw_SchoolTeacher entity);

        bool UpdateSchoolId(int id, int schoolId, int edit);
        bool UpdateStatus(int id, int toStatus, int edit);
        List<Yw_SchoolTeacher> GetTeacherByGrade(int grade,int schoolId);
    }
}
