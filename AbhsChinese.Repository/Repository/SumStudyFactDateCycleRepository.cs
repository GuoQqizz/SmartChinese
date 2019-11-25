using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbhsChinese.Repository.Repository
{
    public class SumStudyFactDateCycleRepository : RepositoryBase<Sum_StudyFactDateCycle>, ISumStudyFactDateCycleRepository
    {
        public SumStudyFactDateCycleRepository() : base(AppSetting.ConnString)
        {
        }

        public void Add(Sum_StudyFactDateCycle entity)
        {
            InsertEntity(entity);
        }

        public void Update(Sum_StudyFactDateCycle entity)
        {
            UpdateEntity(entity);
        }

        public Sum_StudyFactDateCycle GetByStudentDate(int studentId, int classId, DateTime asOfDate, DateCycleTypeEnum cycle)
        {
            string sql = "select * from Sum_StudyFactDateCycle where Sdc_Date=@Date and Sdc_StudentId=@StudentId and Sdc_ClassId=@ClassId and Sdc_CycleType=@CycleType";
            Sum_StudyFactDateCycle entity = Query(sql, new { StudentId = studentId, Date = asOfDate, ClassId = classId, CycleType = cycle }).FirstOrDefault();
            if (entity != null)
            {
                entity.EnableAudit();
            }

            return entity;
        }

        public List<Sum_StudyFactDateCycle> GetByStudent(int studentId, int classId)
        {
            string sql = "select * from Sum_StudyFactDateCycle where Sdc_StudentId=@StudentId and Sdc_ClassId=@ClassId";

            List<Sum_StudyFactDateCycle> entities = Query(sql, new { StudentId = studentId, ClassId = classId }).ToList();
            if (entities != null)
            {
                entities.ForEach(x => x.EnableAudit());
            }

            return entities;
        }
    }
}