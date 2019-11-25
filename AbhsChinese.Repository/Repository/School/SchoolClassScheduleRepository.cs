using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Repository.IRepository.School;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository.School
{
    public class SchoolClassScheduleRepository : RepositoryBase<Yw_SchoolClassSchedule>, ISchoolClassScheduleRepository
    {
        public SchoolClassScheduleRepository() : base(AppSetting.ConnString)
        {
        }
        public List<Yw_SchoolClassSchedule> GetByClassId(int classId)
        {
            string sql = "SELECT * FROM dbo.Yw_SchoolClassSchedule WHERE Ywd_ClassId =  @Ywd_ClassId";

            return Query(sql, new { Ywd_ClassId = classId }).ToList();
        }
        public List<Yw_SchoolClassSchedule> GetByClassIds(List<int> classIds)
        {
            string sql = "SELECT * FROM dbo.Yw_SchoolClassSchedule WHERE Ywd_ClassId IN  @Ywd_ClassIds";

            return Query(sql, new { Ywd_ClassIds = classIds }).ToList();
        }

        public bool DeleteByClassIds(List<int> classIds)
        {
            string sql = "DELETE FROM  dbo.Yw_SchoolClassSchedule WHERE Ywd_ClassId IN @Ywd_ClassId";
            return Execute(sql, new
            {
                Ywd_ClassId = classIds
            }) > 0;
        }

        public int InsertList(List<Yw_SchoolClassSchedule> list)
        {
            return base.InsertListEntity(list);
        }
    }
}
