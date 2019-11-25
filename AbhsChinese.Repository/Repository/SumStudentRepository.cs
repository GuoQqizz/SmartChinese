using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class SumStudentRepository : RepositoryBase<Sum_Student>, ISumStudentRepository
    {
        public SumStudentRepository() : base(AppSetting.ConnString) { }

        public int Add(Sum_Student entity)
        {
            var sql = "insert into Sum_Student (Sst_StudentId,Sst_FirstOrderTime,Sst_LastLoginIp,Sst_LastLoginArea,Sst_LastLoginTime,Sst_OwnCourseCount,Sst_ExperiencePoints,Sst_Coins,Sst_StudyDayCount,Sst_UpdateTime) values (@Sst_StudentId,@Sst_FirstOrderTime,@Sst_LastLoginIp,@Sst_LastLoginArea,@Sst_LastLoginTime,@Sst_OwnCourseCount,@Sst_ExperiencePoints,@Sst_Coins,@Sst_StudyDayCount,@Sst_UpdateTime)";
            return Execute(sql, entity);
        }

        public Sum_Student Get(int id)
        {
            var sumStudent = GetEntity(id);
            if (sumStudent != null)
            {
                sumStudent.EnableAudit();
            }
            return sumStudent;
        }

        public bool Update(Sum_Student entity)
        {
            return UpdateEntity(entity);
        }

        public void AddCoins(int studentId, int coins)
        {
            string sql = "update Sum_Student set Sst_Coins=isnull(Sst_Coins,0)+@Amount,Sst_ExperiencePoints=isnull(Sst_ExperiencePoints,0)+@Amount where Sst_StudentId=@StudentId";
            Execute(sql, new { Amount = coins, StudentId = studentId });
        }
    }
}
