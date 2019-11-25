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
    public class ReadRecordRepository : RepositoryBase<Yw_ReadRecord>, IReadRecordRepository
    {
        public ReadRecordRepository() : base(AppSetting.ConnString)
        {
        }

        public void Add(Yw_ReadRecord entity)
        {
            InsertEntity(entity);
        }

        public void Update(Yw_ReadRecord entity)
        {
            UpdateEntity(entity);
        }

        public Yw_ReadRecord Get(int unitId, string actionId, int studentId)
        {
            string sql = "select * from Yw_ReadRecord where yrr_unitId=@UnitId and yrr_actionId=@ActionId and Yrr_StudentId=@StudentId";
            return Query(sql, new { UnitId = unitId, ActionId = actionId, StudentId = studentId }).FirstOrDefault();
        }

        public int CalculateRankPercent(int unitId, string actionId, int score)
        {
            string sql = @"DECLARE @num1 INT 
                        DECLARE @num2 INT
                        DECLARE @num3 INT

                        SELECT @num1 = Count(0)
                        FROM yw_readrecord with(nolock)
                        WHERE yrr_unitid = @UnitId
                                AND yrr_actionid = @ActionId 
                                AND yrr_score <= @Score

                        SELECT @num2 = Count(0)
                        FROM yw_readrecord with(nolock)
                        WHERE yrr_unitid = @UnitId
                                AND yrr_actionid = @ActionId

                        IF(@num2 = 0)
                            BEGIN
                                SET @num3 = 100
                            END
                        ELSE
                            BEGIN
                                SET @num3 = @num1 * 100 / @num2

                                IF(@num3 = 0)
                                BEGIN
                                    SET @num3 = 3
                                END
                            END

                        SELECT @num3 ";

            return Query<int>(sql, new { Score = score, UnitId = unitId, ActionId = actionId }).FirstOrDefault();
        }
    }
}
