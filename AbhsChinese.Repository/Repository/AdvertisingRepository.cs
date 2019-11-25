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
    public class AdvertisingRepository : RepositoryBase<Bas_Advertising>, IAdvertisingRepository
    {
        public AdvertisingRepository() : base(AppSetting.ConnString)
        {
        }

        public bool UpdateStatusByBabPosId(int bad_PosId, int fromStatus, int toStatus, DateTime endTime)
        {
            string sql = "UPDATE  dbo.Bas_Advertising SET  Bad_Status=@ToStatus,Bad_EndTime=@Bad_EndTime WHERE  Bad_PosId=@Bad_PosId AND Bad_Status = @FromStatus";
            return Execute(sql, new
            {
                FromStatus = fromStatus,
                ToStatus = toStatus,
                Bad_EndTime = endTime,
                Bad_PosId = bad_PosId
            }) > 0;
        }


        #region update
        public bool IncrementHitCount(int badId, int count)
        {
            string sql = "UPDATE  dbo.Bas_Advertising SET Bad_HitCount=Bad_HitCount+@count WHERE Bad_Id=@Bad_Id";
            return Execute(sql, new
            {
                count = count,
                Bad_Id = badId
            }) > 0;
        }

        public bool IncrementValidCount(int badId, int count)
        {
            string sql = "UPDATE  dbo.Bas_Advertising SET Bad_ValidCount=Bad_ValidCount+@count WHERE Bad_Id=@Bad_Id";
            return Execute(sql, new
            {
                count = count,
                Bad_Id = badId
            }) > 0;
        }
        #endregion

        #region insert
        public int InsertList(List<Bas_Advertising> list)
        {
            return base.InsertListEntity(list);
        }
        #endregion
        #region basecrud
        public Bas_Advertising Get(int id)
        {
            var result = base.GetEntity(id);
            if (result != null)
            {
                result.EnableAudit();
            }
            return result;
        }
        public Bas_Advertising GetByPosId(int bapId)
        {
            var result = Query<Bas_Advertising>("SELECT TOP 1 * FROM dbo.Bas_Advertising WHERE Bad_PosId=@Bad_PosId AND Bad_Status=@Bad_Status ORDER BY Bad_Id DESC", new { Bad_PosId = bapId, Bad_Status = (int)Domain.Enum.StatusEnum.有效 }).FirstOrDefault();
            if (result != null)
            {
                result.EnableAudit();
            }
            return result;
        }
        public int Insert(Bas_Advertising entity)
        {
            return base.InsertEntity(entity);
        }

        public bool Update(Bas_Advertising entity)
        {
            return base.UpdateEntity(entity);
        }


        #endregion
    }
}
