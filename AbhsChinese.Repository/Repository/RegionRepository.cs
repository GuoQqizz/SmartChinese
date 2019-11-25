using AbhsChinese.Domain.Dto.Response;
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
    public class RegionRepository : RepositoryBase<Bas_Region>, IRegionRepository
    {
        public RegionRepository() : base(AppSetting.ConnString)
        {

        }

        #region select

        public List<Bas_Region> GetRegionList(int parentId = 0)
        {
            string sql = "SELECT * FROM dbo.Bas_Region WHERE  Reg_ParentID=@Reg_ParentID";
            return base.Query(sql, new { Reg_ParentID = parentId }).ToList();
        }
        public List<DtoRegion> GetRegionByIds(List<int> ids, RegionTypeEnum type)
        {
            string sql = @"  SELECT    cp.* ,
                                        d.Reg_ID AS DistrictId ,
                                        d.Reg_Name AS DistrictName
                              FROM(SELECT    c.Reg_ID AS CityId,
                                                    c.Reg_Name AS CityName,
                                                    p.Reg_ID AS ProvId,
                                                    p.Reg_Name AS ProvName
                                          FROM      Bas_Region c
                                                    INNER JOIN Bas_Region p ON c.Reg_ParentID = p.Reg_ID
                                        ) cp
                                       JOIN Bas_Region d ON cp.cityid = d.Reg_ParentID
                              WHERE 1=1 ";
            switch (type)
            {
                case RegionTypeEnum.省:
                    sql += " AND cp.ProvId IN @Ids";
                    break;
                case RegionTypeEnum.市:
                    sql += " AND cp.CityId IN @Ids";
                    break;
                case RegionTypeEnum.区:
                    sql += " AND d.Reg_ID IN @Ids";
                    break;
                default:
                    return null;
            }
            return base.Query<DtoRegion>(sql, new { Ids = ids }).ToList();
        }

        #endregion
    }
}
