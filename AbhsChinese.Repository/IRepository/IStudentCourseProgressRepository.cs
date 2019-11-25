using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Request.Student;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Dto.Response.Student;
using StudentPractice = AbhsChinese.Domain.Dto.Response.StudentPractice;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IStudentCourseProgressRepository : IRepositoryBase<Yw_StudentCourseProgress>
    {
        Yw_StudentCourseProgress Get(int id);
        Yw_StudentCourseProgress GetByStudentCourse(int studentId, int courseId);

        /// <summary>
        /// 根据学生id获取学生拥有的课时数量
        /// </summary>
        /// <param name="studentId">学生id</param>
        /// <returns>第一个值:总课程数,第二个值:未完成课程数,第三个值:完成课程数</returns>
        Tuple<int, int, int> GetCourseCountByStudnet(int studentId);

        /// <summary>
        /// 获取学生课程详情
        /// </summary>
        /// <param name="studentId">学生id</param>
        /// <param name="courseId">课程id</param>
        /// <returns></returns>
        DtoStudentCourseInfo GetByStudentCourseId(int studentId, int courseId);
        List<DtoSchoolNoClassStudent> GetNoClassStudent(DtoSchoolNoClassStudentSearch search);

        /// <summary>
        /// 根据条件获取学生拥有的课程数据
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<DtoStudentCourseInfo> GetByStudentCourseSearch(DtoStudentCourseSearch search);

        bool UpdateClassId(int ypsId, int classId);

        /// <summary>
        /// 更新最后学习时间
        /// </summary>
        /// <param name="ypsId"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        bool UpdateStudyTime(int ypsId, DateTime dt);

        /// <summary>
        /// 更新学生课程进度数据
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        bool Update(Yw_StudentCourseProgress progress);
        /// <summary>
        /// 获取上过课的课程
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        IList<StudentPractice.DtoCourse> GetCoursesAttended(DtoCoursesSearch search);

    }
}
