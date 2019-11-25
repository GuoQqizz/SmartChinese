using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System.Linq;
using System.Text;

namespace AbhsChinese.Repository.Repository
{
    public class SubjectProcessRepository : RepositoryBase<Yw_SubjectProcess>,
        ISubjectProcessRepository
    {
        public SubjectProcessRepository() : base(AppSetting.ConnString)
        {
        }

        public Yw_SubjectProcess GetCurrentProcess(int subjectId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP 1 * FROM Yw_SubjectProcess");
            builder.Append(" WHERE Ysp_SubjectId=@subjectId");
            builder.Append(" ORDER BY Ysp_CreateTime DESC");
            string sql = builder.ToString();
            return base.Query(sql, new { subjectId = subjectId }).FirstOrDefault();
        }

        public Yw_SubjectProcess Insert(Yw_SubjectProcess entity)
        {
            base.InsertEntity(entity);
            return entity;
        }
    }
}