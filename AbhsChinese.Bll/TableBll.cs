using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using System;
using System.Collections.Generic;

namespace AbhsChinese.Bll
{
    public class TableBll:BllBase
    {
        private ITableRepository service;

        public ITableRepository Service
        {
            get
            {
                if(service==null)
                {
                    service = new TableRepository();
                }

                return service;
            }
        }

        public TableBll() : base()
        {

        }

        public List<Table> GetAll()
        {
            return Service.GetAll();
        }

        public List<Table> GetPagingTable(string fields, string where, string orderby, PagingObject paging)
        {
            List<Table> list =  Service.GetPagingTable(fields,where,orderby,paging);
            return list;

        }

        public bool UpdateCell(int field,int id)
        {
            Table table = Service.Get(id);
            table.Age = field;
            return Service.UpdateCell(table);
        }

    }
}
