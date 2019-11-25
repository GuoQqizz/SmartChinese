using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Repository.IRepository.School;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Repository.Repository.School
{
    public class SchoolLevelRepository : RepositoryBase<Bas_SchoolLevel>, ISchoolLevelRepository
    {
        public SchoolLevelRepository() : base(AppSetting.ConnString)
        {
        }



        public List<Bas_SchoolLevel> GetList(PagingObject pagination)
        {
            return base.QueryPaging<Bas_SchoolLevel>("*", $"Bas_SchoolLevel WHERE Bhl_Status = {(int)StatusEnum.有效}", "Bhl_Id", pagination).ToList();
        }

        #region basecrud
        public Bas_SchoolLevel Get(int id)
        {
            var result = base.GetEntity(id);
            if (result != null)
            {
                result.EnableAudit();
            }
            return result;
        }
        public int Insert(Bas_SchoolLevel entity)
        {
            return base.InsertEntity(entity);
        }

        public bool Update(Bas_SchoolLevel entity)
        {
            return base.UpdateEntity(entity);
        }
        #endregion
    }
}
