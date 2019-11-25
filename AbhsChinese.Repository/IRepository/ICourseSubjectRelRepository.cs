using AbhsChinese.Domain.Dto.Response.StudentPractice;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface ICourseSubjectRelRepository : IRepositoryBase<Yw_CourseSubjectRelation>
    {
        Yw_CourseSubjectRelation GetDirectRelation(int lessonId, int subjectId);

        List<Yw_CourseSubjectRelation> GetDirectRelationsOfLesson(int lessonId);

        List<Yw_CourseSubjectRelation> GetDirectRelation(int subjectId);

        void Add(Yw_CourseSubjectRelation relation);

        void CreateInDirectRelation(int relationId, int[] subjectIds);

        void AddSubjectToGroup(int subjectId, int subjectType, int targetSubjectId);

        void DeleteDirectRelation(int lessonId, int subjectId);

        void DeleteInDirectRelation(int directSubjectId, int subjectId);
        //IList<int> GetSubjectIdsOfStudyTask(int lessonId, int lessonIndex);

        void DeleteDirectRelationOfLesson(int lessonId, List<int> subjectIds);

        void AddBatch(DataTable table);

        /// <summary>
        /// 获取课后任务中的某个课程每个课时有多少练习题
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="targetSubjectId"></param>
        IList<DtoSubjectCountToPractice> GetSubjectIdsOfStudyTask(
            IEnumerable<Tuple<int, int>> where);
    }
}
