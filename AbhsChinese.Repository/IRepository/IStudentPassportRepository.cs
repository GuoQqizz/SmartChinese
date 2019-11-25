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
    public interface IStudentPassportRepository : IRepositoryBase<Bas_StudentPassport>
    {
        int Register(Bas_StudentPassport entity);
        Bas_StudentPassport Login(string passportKey,string password);
        int CheckUniqueAccount(string account);
        Bas_StudentPassport Get(int id);
        bool IsBindFaceLogin(int studentId);
        bool IsExistPhone(string phone);
        bool Update(Bas_StudentPassport entity);
        Bas_StudentPassport GetPassportByStuIdAndType(int studentId,int passportType);
        Bas_StudentPassport GetPassportByPassportAndType(string passportKey, int passportType);
        Bas_StudentPassport GetByPassport(string passportKey);
    }
}
