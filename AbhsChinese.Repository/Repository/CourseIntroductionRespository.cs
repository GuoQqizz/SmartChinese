using AbhsChinese.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;

namespace AbhsChinese.Repository.Repository
{
    public class CourseIntroductionRespository : RepositoryBase<Yw_CourseIntroduction>, ICourseIntroductionRespository
    {
        public CourseIntroductionRespository() : base(AppSetting.ConnString)
        {
        }

        public Yw_CourseIntroduction GetCourseIntroduction(int courseId)
        {
            string sql = @"SELECT [Yci_CourseId]
          ,[Yci_Introduction]
          ,[Yci_Arrange]
          ,[Yci_CreateTime]
          ,[Yci_Creator]
          ,[Yci_UpdateTime]
          ,[Yci_Editor]
      FROM [dbo].[Yw_CourseIntroduction](NOLOCK)
      WHERE [Yci_CourseId] = @CourseId";
            Yw_CourseIntroduction entity = Query(sql, new { CourseId = courseId }).FirstOrDefault();
            if (entity != null)
            {
                entity.EnableAudit();
            }
            return entity;
        }

        public void Insert(Yw_CourseIntroduction entity)
        {
            InsertEntity(entity);
        }

        public void Update(Yw_CourseIntroduction entity)
        {
            UpdateEntity(entity);
        }
    }
}
