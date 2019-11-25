using AbhsChinese.Code.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace AbhsChinese.Repository.RepositoryBase
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepositoryBase<T> where T : class,new()
    {
        string TranId
        {
            set; get;
        }
        //int Insert(TEntity entity);
        //int Insert(List<TEntity> entitys);
        //int Update(TEntity entity);
        //int Delete(TEntity entity);
        ////int Delete(Expression<Func<TEntity, bool>> predicate);
        //List<TEntity> All();
        //TEntity Find(object keyValue);
        //TEntity FindEntity(Expression<Func<TEntity, bool>> predicate);
        //IQueryable<TEntity> IQueryable();
        //IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate);
        //List<TEntity> FindList(string strSql);
        //List<TEntity> FindList(string strSql, DbParameter[] dbParameter);
        //List<TEntity> FindList(Pagination pagination);
        //List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination);

        //List<T> Paging<T>(string fields, string where, string orderby, PagingObject paging, object param = null) where T : class;
    }
}
