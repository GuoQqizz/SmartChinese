using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IStuLesProgressRepository : IRepositoryBase<Yw_StudentLessonProgress>
    {
        /// <summary>
        /// 根据Id获取
        /// </summary>
        Yw_StudentLessonProgress Get(int id);

        /// <summary>
        /// 获取学生完成的课时id数据
        /// </summary>
        /// <param name="studnetId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        List<int> GetFinishedByLesson(int studnetId, int courseId);
        /// <summary>
        /// 插入新的学习记录
        /// </summary>
        /// <param name="slp"></param>
        void Insert(Yw_StudentLessonProgress slp);
        /// <summary>
        /// 根据学生id和课时id获取最后一次学习记录
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="lessonid"></param>
        /// <returns></returns>
        Yw_StudentLessonProgress GetLastProgress(int studentId, int lessonid);
        /// <summary>
        /// 根据学习进度id查看学习信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Yw_StudentLessonProgress GetProgressByID(int id);

        /// <summary>
        /// 更新课程学习进度的秘钥
        /// </summary>
        /// <param name="id">学习进度id</param>
        string UpdateKey(int id);

        /// <summary>
        /// 更新课程学习进度数据
        /// </summary>
        /// <param name="progress"></param>
        void Update(Yw_StudentLessonProgress progress);

        List<DtoStudentReportList> GetStudentReport(PagingObject paging, int studentId);

        DtoStudentReportList GetStuCourseReportById(int lessonProcessId);

        int StuCourseReportCount(int studentId);

        List<DtoStudentReportList> GetStuCourseReport(PagingObject paging, int studentId);

        bool IsHasStudy(int id, int studentId);

        Yw_StudentLessonProgress IsHasStudys(int id, int studentId);
    }
}
