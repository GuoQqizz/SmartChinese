using AbhsChinese.Code.Common;
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
    public interface ISchoolRepository : IRepositoryBase<Bas_School>
    {
        Bas_School GetSchoolByStudent(int studentId);

        List<DtoSchool> GetSchoolList(DtoSchoolSearch search);
        bool Update(Bas_School entity);

        int Insert(Bas_School obj);

        bool UpdateMasterId(int id, int masterId);
        Bas_School Get(int schoolId);

        List<Bas_School> GetSchoolByIdOrName(PagingObject paging,string idOrName);
    }
}
