using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Dto.Response.StudentPractice;
using AbhsChinese.Domain.Dto.Request.StudyTask;

namespace AbhsChinese.Repository.IRepository
{
    public interface IStudyTaskRepository : IRepositoryBase<Yw_StudyTask>
    {
        void Add(Yw_StudyTask task);

        Yw_StudyTask Get(int id);

        void Update(Yw_StudyTask task);
        
        /// <summary>
        /// 获取某个课程下每个课时练习过多少题
        /// </summary>
        /// <param name="courseIds"></param>
        /// <returns></returns>
        IList<DtoSubjectCountToPractice> GetTotalSubjectsPracticed(IList<int> courseIds,int studentId);

        DtoStudyTaskListItem GetCourseLessonByStudyTask(int studentTaskId);
    }
}
