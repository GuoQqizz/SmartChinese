using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IStudentLoginRepository : IRepositoryBase<Bas_StudentLogin>
    {
        int Add(Bas_StudentLogin entity);
        Bas_StudentLogin Get(int id);
        List<Bas_StudentLogin> GetStudentLogin(PagingObject paging, int studentId);
    }
}
