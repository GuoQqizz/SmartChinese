using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface ICoursePriceRepository : IRepositoryBase<Yw_CoursePrice>
    {
        Yw_CoursePrice GetPrice(int courseId, int schoolLevelId);
        IList<DtoCoursePriceListItem> GetEntities(IEnumerable<int> ids);
        Yw_CoursePrice GetEntity(int courseId, int schoolLevel);
        void Update(Yw_CoursePrice entity);
        void Insert(Yw_CoursePrice entity);
    }
}
