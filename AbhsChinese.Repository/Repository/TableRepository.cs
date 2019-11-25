using AbhsChinese.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using AbhsChinese.Code.Common;

namespace AbhsChinese.Repository.Repository
{
    public class TableRepository :RepositoryBase<Table>,ITableRepository
    {
        public TableRepository() : base("")
        {

        }

        public Table Get(int id)
        {
            Table entity= base.GetEntity(id);
            entity.EnableAudit();
            return entity;
        }

        public List<Table> GetAll()
        {
            List<Table> list = null; //base.AllEntities();
            
            return list;
        }

        public List<Table> GetPagingTable(string fields, string where, string orderby, PagingObject paging)
        {
            
            List<Table> list = base.QueryPaging<Table>(fields,where,orderby,paging).ToList();
            return list;
        }

        public bool UpdateCell(Table table)
        {
            return base.UpdateEntity(table);
        }
    }
}
