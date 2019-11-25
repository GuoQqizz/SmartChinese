using AbhsChinese.Code.Common;
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
    public class SubjectGroupRepository : RepositoryBase<Yw_SubjectGroup>, ISubjectGroupRepository
    {
        public SubjectGroupRepository() : base(AppSetting.ConnString)
        {
        }

        public Yw_SubjectGroup GetBySubjectId(int subjectId)
        {
            string sql = "select * from Yw_SubjectGroup where Ysg_SubjectId=@SubjectId";
            return Query(sql, new { SubjectId = subjectId }).FirstOrDefault();
        }

        public IList<Yw_SubjectGroup> GetBySubjectIds(IEnumerable<int> ids)
        {
            string sql = "select * from Yw_SubjectGroup where Ysg_SubjectId in @ids";
            return Query(sql, new { ids = ids }).ToList();
        }

        public void Insert(Yw_SubjectGroup entity)
        {
            Check.IfNull(entity, nameof(entity));

            InsertEntity(entity);
        }

        public void Update(Yw_SubjectGroup entity)
        {
            Check.IfNull(entity, nameof(entity));

            base.UpdateEntity(entity);
        }
    }
}
