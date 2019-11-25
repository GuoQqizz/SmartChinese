using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface ITableRepository:IRepositoryBase<Table>
    {
        Table Get(int id);

        List<Table> GetAll();

        List<Table> GetPagingTable(string fields,string where,string orderby, PagingObject paging);

        bool UpdateCell(Table table);
    }
}
