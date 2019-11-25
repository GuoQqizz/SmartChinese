using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class SumStudyFactDailyRepository : RepositoryBase<Sum_StudyFactDaily>, ISumStudyFactDailyRepository
    {
        public SumStudyFactDailyRepository() : base(AppSetting.ConnString)
        {

        }

        public void Add(Sum_StudyFactDaily entity)
        {
            InsertEntity(entity);
        }

        public void Update(Sum_StudyFactDaily entity)
        {
            UpdateEntity(entity);
        }

        public Sum_StudyFactDaily GetByStudentDate(int studentId, int classId, DateTime asOfDate)
        {
            string sql = "select * from Sum_StudyFactDaily where Ssd_Date=@Date and Ssd_StudentId=@StudentId and Ssd_ClassId=@ClassId";
            Sum_StudyFactDaily entity = Query(sql, new { StudentId = studentId, Date = asOfDate, ClassId = classId }).FirstOrDefault();
            if (entity != null)
            {
                entity.EnableAudit();
            }

            return entity;
        }

        public int GetStudyDayCountOfStudent(int studentId)
        {
            string sql = "select count(distinct(Ssd_Date)) from Sum_StudyFactDaily where Ssd_StudentId=@StudentId";
            int count = Query<int>(sql, new { StudentId = studentId }).FirstOrDefault();
            return count;
        }

        public int GetSumByStuDate(int studentId, DateTime asOfDate)
        {
            string sql = "select sum(Ssd_StudySeconds) as Ssd_StudySeconds from Sum_StudyFactDaily where Ssd_Date=@Date and Ssd_StudentId=@StudentId";
            return ExecuteScalar<int>(sql, new { StudentId = studentId, Date = asOfDate });
        }
    }
}
