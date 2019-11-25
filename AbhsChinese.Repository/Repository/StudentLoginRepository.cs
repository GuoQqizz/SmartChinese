using AbhsChinese.Code.Common;
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
    public class StudentLoginRepository : RepositoryBase<Bas_StudentLogin>, IStudentLoginRepository
    {
        public StudentLoginRepository() : base(AppSetting.ConnString)
        {
        }

        public int Add(Bas_StudentLogin entity)
        {
            return InsertEntity(entity);
        }

        public Bas_StudentLogin Get(int id)
        {
            return GetEntity(id);
        }

        public List<Bas_StudentLogin> GetStudentLogin(PagingObject paging,int studentId)
        {
            var sql = "Bas_StudentLogin where Bsg_StudentId=@StudentId";
            return QueryPaging<Bas_StudentLogin>("*",sql,"Bsg_LoginTime DESC", paging, new { StudentId = studentId }).ToList();
        }
    }
}
