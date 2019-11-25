using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class StudentPassportRepository : RepositoryBase<Bas_StudentPassport>, IStudentPassportRepository
    {
        public StudentPassportRepository() : base(AppSetting.ConnString)
        {
        }

        public int Register(Bas_StudentPassport entity)
        {
            return InsertEntity(entity);
        }

        public Bas_StudentPassport Login(string passportKey, string password)
        {
            var sql = "select *from Bas_StudentPassport where Bsp_PassportKey=@PassportKey and Bsp_Password=@Password and Bsp_Status=1";
            return Query(sql, new { PassportKey = passportKey, Password = password }).FirstOrDefault();
        }

        public int CheckUniqueAccount(string account)
        {
            var sql = "SELECT  COUNT(1) FROM dbo.Bas_StudentPassport WHERE Bsp_PassportKey=@Bsp_PassportKey";
            return Query<int>(sql, new { Bsp_PassportKey = account }).FirstOrDefault();
        }

        public Bas_StudentPassport Get(int id)
        {
            var entity = GetEntity(id);
            if (entity != null)
            {
                entity.EnableAudit();
            }
            return entity;
        }

        public bool IsBindFaceLogin(int studentId)
        {
            var sql = "select count(1) from Bas_StudentPassport where Bsp_PassportType=2 and  Bsp_StudentId=@StudentId and Bsp_Status=1";
            return ExecuteScalar<int>(sql, new { StudentId = studentId }) > 0;
        }

        public bool IsExistPhone(string phone)
        {
            var sql = "select count(1) from Bas_StudentPassport where Bsp_PassportKey=@PassortKey and Bsp_Status=1";
            return ExecuteScalar<int>(sql, new { PassortKey = phone }) > 0;
        }

        public bool Update(Bas_StudentPassport entity)
        {
            return UpdateEntity(entity);
        }

        public Bas_StudentPassport GetPassportByStuIdAndType(int studentId, int passportType)
        {
            var sql = "select *from Bas_StudentPassport where Bsp_StudentId=@StudentId and Bsp_PassportType=@PassportType";
            return Query(sql, new { StudentId = studentId, PassportType = passportType}).FirstOrDefault();
        }

        public Bas_StudentPassport GetPassportByPassportAndType(string passportKey, int passportType)
        {
            var sql = "select *from Bas_StudentPassport where Bsp_PassportKey=@PassportKey and Bsp_PassportType=@PassportType and Bsp_Status=1";
            return Query(sql, new { PassportKey = passportKey, PassportType = passportType }).FirstOrDefault();
        }

        public Bas_StudentPassport GetByPassport(string passportKey)
        {
            var sql = "select *from Bas_StudentPassport where Bsp_PassportKey=@PassportKey";
            return Query(sql, new { PassportKey = passportKey }).FirstOrDefault();
        }
    }
}
