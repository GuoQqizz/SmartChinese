using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface ISubjectContentRepository : IRepositoryBase<Yw_SubjectContent>
    {
        Yw_SubjectContent Insert(Yw_SubjectContent entity);

        Yw_SubjectContent Get(int id);

        List<Yw_SubjectContent> GetByIds(List<int> ids);

        void Update(Yw_SubjectContent entity);
  }
}
