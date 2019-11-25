using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Dto.Request.StudyTask;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response.StudentPractice;

namespace AbhsChinese.Repository.IRepository
{
    public interface IStudentTaskRepository : IRepositoryBase<Yw_StudentTask>
    {
        void Add(Yw_StudentTask stuTask);

        void Update(Yw_StudentTask stuTask);

        Yw_StudentTask GetByStudentTask(int student, int taskId);
        IList<DtoStudyTaskListItem> GetEntities(DtoStudyTaskSearch search);

        List<DtoStudentReportList> GetStuTaskReport(PagingObject paging, int studentId, int taskType);

        List<Yw_StudentTask> StuTaskReportCount(int studentId);

        DtoStudentReportList GetStuTaskReportById(int taskId);

        int GetPracticeReportCount(int studentId,int courseId,int id);
        IList<DtoStatusTotalRecord> GetTotalRecordByStatus(DtoStudyTaskSearch search);

        bool IsHasTaskReport(int studentId, int id);
    }
}
