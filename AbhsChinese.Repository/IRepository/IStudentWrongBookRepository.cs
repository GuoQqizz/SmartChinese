using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Dto.Request.StudentWrong;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity.StudentWrong;

namespace AbhsChinese.Repository.IRepository
{
    public interface IStudentWrongBookRepository : IRepositoryBase<Yw_StudentWrongBook>
    {
        Yw_StudentWrongBook GetByStudentAndStudy(int studentId, int courseId, int lessonId, int progressId, int taskId);

        int Insert(Yw_StudentWrongBook entity);

        Yw_StudentWrongBook Get(int id);
        bool Update(Yw_StudentWrongBook entity);
        bool RefreshStatus(int bookId);
        List<DtoStudentWrongBookInfo> GetBookList(DtoStudentWrongSearch search);
    }
}
