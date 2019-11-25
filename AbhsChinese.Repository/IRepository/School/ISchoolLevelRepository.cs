using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository.School
{
    public interface ISchoolLevelRepository : IRepositoryBase<Bas_SchoolLevel>
    {
        List<Bas_SchoolLevel> GetList(PagingObject pagination);

        int Insert(Bas_SchoolLevel entity);

        bool Update(Bas_SchoolLevel entity);

        Bas_SchoolLevel Get(int id);
    }
}
