using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IStudentRepository : IRepositoryBase<Bas_Student>
    {
        bool UpdateSchool(int studentId, int schoolId);
        Bas_Student Get(int id);
        int Add(Bas_Student entity);
        List<DtoSchoolStudent> GetSchoolStudentList(DtoSchoolStudentSearch search);
        DtoSumStudentTip GetSumStudentTip(int studentId);
        DtoStudentInfo GetStudentInfoById(int id);
        string GetStudentNo();
        bool Update(Bas_Student entity);
        DtoStudentInfo GetStudentInfoByAccount(string account);

        Bas_Student GetBySchoolId();
    }
}
