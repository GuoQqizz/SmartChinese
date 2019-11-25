using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll
{
    public class RegionBll : BllBase
    {
        public RegionBll() : base()
        {

        }

        #region repository
        private IRegionRepository regionRepository;
        public IRegionRepository RegionRepository
        {
            get
            {
                if (regionRepository == null)
                {
                    regionRepository = new RegionRepository();
                }
                return regionRepository;
            }
        }
        #endregion

        #region select
        /// <summary>
        /// 级联获取地区
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<Bas_Region> GetRegionList(int parentId = 0)
        {
            return RegionRepository.GetRegionList(parentId);
        }
        /// <summary>
        /// 根据省市区获取地区所有信息(省市区)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<DtoRegion> GetRegionByIds(List<int> ids, RegionTypeEnum type)
        {
            return RegionRepository.GetRegionByIds(ids,type);
        }
         
       
        #endregion
    }
}
