using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using AbhsChinese.Domain.Dto.Request.Subject;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using Dapper;

namespace AbhsChinese.Repository.Repository
{
    public class SubjectIndexRepository : RepositoryBase<Yw_SubjectIndex>,
        ISubjectIndexRepository
    {
        public SubjectIndexRepository() : base(AppSetting.ConnString)
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
                string sql = "DELETE FROM Yw_SubjectIndex WHERE Ysi_Id IN " + parameters;
                base.Execute(sql);
            }
        }

        public IList<int> GetSubjectIdsByKeyword(DtoQuestionSearch search)
        {
            if (search == null || string.IsNullOrWhiteSpace(search.Keyword))
            {
                return new List<int>();
            }

            string field = "Ysi_SubjectId";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Yw_SubjectIndex");
            stringBuilder.Append(" WHERE Ysi_Keyword = @keyword");
            int difficulty = -1;
            if (search.Difficulty.HasValue)
            {
                difficulty = (int)search.Difficulty.Value;
                stringBuilder.Append(" AND Ysi_Difficulty = @difficulty");
            }
            int status = -1;
            if (search.SubjectStatus.HasValue)
            {
                status = (int)search.SubjectStatus.Value;
                stringBuilder.Append(" AND Ysi_Status = @status");
            }
            else//没有值，搜索除了合格的之外的数据
            {
                status = (int)SubjectStatusEnum.合格;
                stringBuilder.Append(" AND Ysi_Status != @status");
            }

            int subjectType = -1;
            if (search.SubjectType.HasValue)
            {
                subjectType = (int)search.SubjectType.Value;
                stringBuilder.Append(" AND Ysi_SubjectType = @subjectType");
            }

            string where = stringBuilder.ToString();

            string orderby = "Ysi_CreateTime DESC";
            return base.QueryPaging<int>(
                field,
                where,
                orderby,
                search.Pagination,
                new
                {
                    keyword = search.Keyword,
                    difficulty = difficulty,
                    status = status,
                    subjectType = subjectType
                }).ToList();
        }

        public IEnumerable<Yw_SubjectIndex> GetSubjectIndexBySubject(int id)
        {
            if (id <= 0)
            {
                return Enumerable.Empty<Yw_SubjectIndex>();
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Yw_SubjectIndex");
            builder.Append(" WHERE Ysi_SubjectId = @Ysi_SubjectId");
            string sql = builder.ToString();
            return base.Query(sql, new { Ysi_SubjectId = id });
        }

        public Yw_SubjectIndex Insert(Yw_SubjectIndex entity)
        {
            base.InsertEntity(entity);
            return entity;
        }

        public IEnumerable<Yw_SubjectIndex> Insert(IEnumerable<Yw_SubjectIndex> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
            return entities;
        }

        public void Update(Yw_SubjectIndex entity)
        {
            Check.IfNull(entity, nameof(entity));

            base.UpdateEntity(entity);
        }

        public IList<int> GetPagingSubjectIds(PagingObject paging, int grade, int subjectType, string nameOrkey)
        {
            var strSql = new StringBuilder();
            var parameters = new DynamicParameters();
            strSql.Append("Yw_SubjectIndex where Ysi_Status=3");
            if (grade > 0)
            {
                strSql.Append(" and Ysi_Grade=@Grade");
                parameters.Add("Grade", grade);
            }
            if (subjectType >0)
            {
                strSql.Append(" and Ysi_SubjectType=@SubjectType");
                parameters.Add("SubjectType", subjectType);
            }
            if (!string.IsNullOrEmpty(nameOrkey))
            {
                strSql.Append(" and Ysi_Keyword=@NameOrKey");
                parameters.Add("NameOrKey", nameOrkey);
            }
            return QueryPaging<int>("Ysi_SubjectId", strSql._ToString(), "Ysi_CreateTime DESC", paging, parameters).ToList();
        }
    }
}