using AbhsChinese.Domain.Dto.Request.StudyTask;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity.StudentWrong;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IStudentWrongSubjectRepository : IRepositoryBase<Yw_StudentWrongSubject>
    {
        Yw_StudentWrongSubject Get(int id);

        DtoStudentWrongSubjectInfo GetDto(int id);

        /// <summary>
        /// 批量插入错题
        /// list学生Id,学校，班级，来源必须唯一
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool InsertList(List<Yw_StudentWrongSubject> list);
        
        List<Yw_StudentWrongSubject> GetByBookId(int bookId);
        bool IncrementRemoveCount(int wrongSubjectId, int count);
        bool ClearSubject(int wrongSubjectId, StudyWrongStatusEnum toStatus);
        List<int> GetIdsByBookId(int studentId, int bookId);
    }
}
