using System;
using System.Collections.Generic;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System.Linq;

namespace AbhsChinese.Repository.Repository
{
    public class CourseProcessRepository : RepositoryBase<Yw_CourseProcess>,
        ICourseProcessRepository
    {
        public CourseProcessRepository() : base(AppSetting.ConnString)
        {
        }

        public IList<Yw_CourseProcess> GetLastestEntity(int courseId)
        {
            string sql = "SELECT TOP 2 * FROM Yw_CourseProcess WHERE Ycp_CourseId = @courseId ORDER BY Ycp_CreateTime DESC";
            return Query<Yw_CourseProcess>(sql, new { courseId = courseId }).ToArray();
        }

        public void Insert(Yw_CourseProcess entity)
        {
            InsertEntity(entity);
        }
    }
}