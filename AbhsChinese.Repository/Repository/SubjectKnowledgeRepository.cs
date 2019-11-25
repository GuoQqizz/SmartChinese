using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace AbhsChinese.Repository.Repository
{
    public class SubjectKnowledgeRepository : RepositoryBase<Yw_SubjectKnowledge>,
        ISubjectKnowledgeRepository
    {
        public SubjectKnowledgeRepository() : base(AppSetting.ConnString)
        {
        }

        public void Delete(IEnumerable<int> ids, bool physicalDeletion = false)
        {
            if (ids == null || !ids.Any())
            {
                return;
            }

            if (!physicalDeletion)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("(");
                foreach (var id in ids)
                {
                    builder.Append(id.ToString());
                    builder.Append(", ");
                }
                string parameters = builder.ToString().TrimEnd(new char[] { ',', ' ' });
                parameters += ")";
                string sql = "DELETE FROM Yw_SubjectKnowledge WHERE Ysw_Id IN " + parameters;
                base.Execute(sql);
            }
        }

        public IEnumerable<Yw_SubjectKnowledge> GetKnowledgesBySubject(int subjectId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Yw_SubjectKnowledge");
            builder.Append(" WHERE Ysw_SubjectId = @subjectId");
            string sql = builder.ToString();
            return Query<Yw_SubjectKnowledge>(sql, new { subjectId = subjectId });
        }

        public IEnumerable<DtoKnowledge> GetKnowledgesWithNamesBySubject(int subjectId)
        {
             StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Yw_SubjectKnowledge");
            builder.Append(" INNER JOIN Yw_Knowledge ON Ysw_KnowledgeId = Ykl_Id");
            builder.Append(" WHERE Ysw_SubjectId = @subjectId");
            string sql = builder.ToString();
            return Query<DtoKnowledge>(sql, new { subjectId = subjectId });
        }

        public Yw_SubjectKnowledge Insert(Yw_SubjectKnowledge entity)
        {
            base.InsertEntity(entity);
            return entity;
        }

        public void Insert(IEnumerable<Yw_SubjectKnowledge> entities)
        {
            foreach (Yw_SubjectKnowledge entity in entities)
            {
                base.InsertEntity(entity);
            }
        }

        public void Update(Yw_SubjectKnowledge entity)
        {
            Check.IfNull(entity, nameof(entity));
            base.UpdateEntity(entity);
        }

        public void Update(IEnumerable<Yw_SubjectKnowledge> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }
    }
}