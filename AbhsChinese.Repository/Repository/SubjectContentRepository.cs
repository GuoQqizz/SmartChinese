using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbhsChinese.Repository.Repository
{
    public class SubjectContentRepository : RepositoryBase<Yw_SubjectContent>, ISubjectContentRepository
    {
        public SubjectContentRepository() : base(AppSetting.ConnString)
        {
        }

        public Yw_SubjectContent Get(int id)
        {
            Yw_SubjectContent entity = base.GetEntity(id);
            Yw_SubjectContent realEntity = SubjectContentFactory.Create(entity);
            if (realEntity != null)
            {
                realEntity.EnableAudit();
            }
            return realEntity;
        }

        public List<Yw_SubjectContent> GetByIds(List<int> ids)
        {
            List<Yw_SubjectContent> result = new List<Yw_SubjectContent>();
            if (ids.Count > 0)
            {
                string sql = @"select * from Yw_SubjectContent
                               inner join yw_subject on Ysj_Id = Ysc_SubjectId
                               where Ysc_SubjectId in @Ids";
                var objs = Query(sql, new { Ids = ids.ToArray() });

                foreach (Yw_SubjectContent item in objs)
                {
                    Yw_SubjectContent realEntity = SubjectContentFactory.Create(item);
                    realEntity.EnableAudit();
                    result.Add(realEntity);
                }
            }

            return result;
        }

        public Yw_SubjectContent Insert(Yw_SubjectContent entity)
        {
            entity.Ysc_CreateTime = DateTime.Now;
            entity.Ysc_UpdateTime = DateTime.Now;

            base.InsertEntity(entity);
            return entity;
        }

        public void Update(Yw_SubjectContent entity)
        {
            Check.IfNull(entity, nameof(entity));

            base.UpdateEntity(entity);
        }
    }
}