using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class KnowledgeRepository : RepositoryBase<Yw_Knowledge>, IKnowledgeRepository
    {
        public KnowledgeRepository() : base(AppSetting.ConnString)
        {
        }

        public int Add(Yw_Knowledge entity)
        {
            return InsertEntity(entity);
        }

        public Yw_Knowledge Get(int id)
        {
            var entity = GetEntity(id);
            if (entity != null)
            {
                entity.EnableAudit();
            }
            return entity;
        }

        public DtoKnowledge GetKnowledge(int id)
        {
            string strSql = "select a.*,b.Ykl_Name as ParentName,Ymr_Name from Yw_Knowledge a left join Yw_Knowledge b on a.Ykl_ParentId=b.Ykl_Id left join Yw_MediaResource on a.Ykl_ResourceId=Ymr_Id where a.Ykl_Id=@Id";
            return base.QueryObject<DtoKnowledge>(strSql, new { Id = id }).FirstOrDefault();
        }

        public List<Yw_Knowledge> GetKnowledgeTreeByChild(int id)
        {
            string strSql = @"WITH TREE AS(
                                SELECT * FROM Yw_Knowledge  WHERE Ykl_Id = @Id
                                UNION ALL
                                SELECT Yw_Knowledge.* FROM Yw_Knowledge, TREE   WHERE TREE.Ykl_ParentId = Yw_Knowledge.Ykl_Id) 
                            SELECT * FROM TREE order by Ykl_Id";
            return base.Query(strSql, new { Id = id }).ToList();
        }

        //public List<Yw_Knowledge> GetKnowledgeTreeByParent(int parentId)
        //{
        //    string strSql = @"WITH TREE AS( 
        //                        SELECT * FROM Yw_Knowledge 
        //                        WHERE Ykl_ParentId = @ParentId
        //                        UNION ALL 
        //                        SELECT Yw_Knowledge.* FROM Yw_Knowledge, TREE 
        //                        WHERE Yw_Knowledge.Ykl_ParentId = TREE.Ykl_Id) 
        //                    SELECT Ykl_Id,Ykl_Name,Ykl_ParentId,Ykl_Level FROM TREE order by Ykl_Id";
        //    return base.Query(strSql, new { ParentId = parentId }).ToList();
        //}

        public List<Yw_Knowledge> GetKnowledgeTreeByParent(int parentId)
        {
            string strSql = @"select *from Yw_Knowledge where Ykl_ParentId=@ParentId";
            return base.Query(strSql, new { ParentId = parentId }).ToList();
        }

        public List<Yw_Knowledge> GetKnowledgeTreeByIds(List<int> ids)
        {
            string sql = "select * from Yw_Knowledge where Ykl_ParentId in (select Ykl_ParentId from Yw_Knowledge where ykl_id in @Ids)";
            return Query(sql, new { Ids = ids }).ToList();
        }

        public List<Yw_Knowledge> GetPagingKnowledge(PagingObject paging, int id, int level)
        {
            var strWhere = new StringBuilder();
            var parameters = new DynamicParameters();
            strWhere.Append("Yw_Knowledge where 1=1");
            if (id > 0)
            {
                strWhere.Append(" and Ykl_Id=@Id");
                parameters.Add("Id", id);
            }
            if (level > 0)
            {
                strWhere.Append(" and Ykl_Level=@Level");
                parameters.Add("Level", level);
            }
            return QueryPaging<Yw_Knowledge>("*", strWhere._ToString(), "Ykl_CreateTime DESC", paging, parameters).ToList();
        }

        public List<Yw_Knowledge> GetKnowledgeByIds(List<int> ids)
        {
            var sql = "select * from Yw_Knowledge where Ykl_Id in @Ids";
            return Query(sql, new { Ids = ids }).ToList();
        }

        public IList<Yw_Knowledge> GetKnowledgeByIds(DtoKnowledgeSearch search)
        {
            if (search.Ids == null || search.Ids.Count == 0)
            {
                return new List<Yw_Knowledge>();
            }

            StringBuilder whereBuilder = new StringBuilder();
            whereBuilder.Append("Yw_Knowledge where 1 = 1 and Ykl_Id in @Ids");
            if (search.Level.HasValue)
            {
                whereBuilder.Append(" and Ykl_Level = @Level");
            }

            return QueryPaging<Yw_Knowledge>(
                "*",
                whereBuilder.ToString(),
                "Ykl_CreateTime DESC",
                search.Pagination,
                new { Level = (int)search.Level, Ids = search.Ids }).ToList();
        }

        public bool Update(Yw_Knowledge entity)
        {
            return base.UpdateEntity(entity);
        }

        public int UpdatePath(string oldPath, string newPath)
        {
            var sql = "update Yw_Knowledge set Ykl_Path=replace(Ykl_Path,@Path,@NewPath) where Ykl_Id in (select Top 1000 Ykl_Id from Yw_Knowledge where Ykl_Path like @OldPath)";
            return Execute(sql, new { Path = oldPath, NewPath = newPath, OldPath = oldPath + "%" });
        }

        public bool IsLeaf(int id)
        {
            var sql = "select count(1) from Yw_Knowledge where Ykl_ParentId=@Id";
            return ExecuteScalar<int>(sql, new { Id = id }) > 0;
        }

        public List<Yw_Knowledge> GetTopKnowledge(List<int> ids)
        {
            var sql = "select *from Yw_Knowledge where Ykl_Path in (select LEFT(Ykl_Path,CHARINDEX('/',Ykl_Path,5)-1) from Yw_Knowledge where Ykl_Id in @Ids)";
            return Query(sql, new { Ids = ids }).ToList();
        }

        public List<Yw_Knowledge> GetTopKnowledgeByPath(List<string> paths)
        {
            var sql = "select * from Yw_Knowledge where Ykl_Path in @Paths";
            return Query(sql, new { Paths = paths }).ToList();
        }
    }
}
