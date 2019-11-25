using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Common;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.Dapper.Contrib.Extensions;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace AbhsChinese.Repository.RepositoryBase
{
    public class RepositoryBase<T> where T : class, new()
    {
        protected string connString;
        protected string tranId;

        public string TranId
        {
            set; get;
        }

        protected RepositoryBase(string connStr)
        {
            connString = connStr;
        }

        protected virtual bool DeleteEntity(T obj)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                bool result = conn.Delete<T>(obj);
                return result;
            }
        }

        protected virtual int Execute(string sql, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                return conn.Execute(sql, param);
            }
        }

        protected virtual T1 ExecuteScalar<T1>(string sql, object param = null)
        {
            T1 obj = default(T1);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                obj = conn.ExecuteScalar<T1>(sql, param);
            }
            return obj;
        }

        protected virtual T1 ExecuteProcScalar<T1>(string sql, object param = null)
        {
            T1 obj = default(T1);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                obj = conn.ExecuteScalar<T1>(sql, param, null, default(int?), CommandType.StoredProcedure);
            }
            return obj;
        }

        protected virtual IEnumerable<T1> QueryProc<T1>(string sql, object param = null)
        {
            IEnumerable<T1> result = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                result = conn.Query<T1>(sql, param, null, true, null, CommandType.StoredProcedure);
            }
            return result;
        }

        protected virtual void ExecuteProc(string sql, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Execute(sql, param, null, default(int?), CommandType.StoredProcedure);
            }
        }

        protected virtual T GetEntity(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                T obj = conn.Get<T>(id);
                return obj;
            }
        }

        protected virtual int InsertEntity(T obj)
        {
            if (obj is IJsonTranslator)
            {
                ((IJsonTranslator)obj).ResetJsonValue();
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                long id = conn.Insert<T>(obj);
                return Convert.ToInt32(id);
            }
        }
        protected virtual int InsertListEntity(List<T> list)
        {
            list.ForEach(obj =>
            {
                if (obj is IJsonTranslator)
                {
                    ((IJsonTranslator)obj).ResetJsonValue();
                }
            });
            using (SqlConnection conn = new SqlConnection(connString))
            {
                long id = conn.Insert<List<T>>(list);
                return Convert.ToInt32(id);
            }
        }

        protected virtual IEnumerable<T1> QueryPaging<T1>(string fields,
            string where,
            string orderby, PagingObject paging, object param = null)
        {
            DataSet dataSet = new DataSet();
            string pagingTemplate = @"WITH PagingSet AS
                                   (
                                      SELECT {1} , ROW_NUMBER() OVER(ORDER BY {0}) AS RowNum from {2}
                                    )
                                    SELECT * FROM PagingSet WHERE RowNum BETWEEN {3} AND {4} ;";

            string totalCountTemplate = @"declare @total int;
                                          set @total = (select COUNT(1) from {0});
                                          select @total; ";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                paging.TotalCount = conn.ExecuteScalar<int>(string.Format(totalCountTemplate, where), param);
            }

            IEnumerable<T1> result = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                result = conn.Query<T1>(string.Format(pagingTemplate, orderby, fields, where, (paging.PageIndex - 1) * paging.PageSize + 1, paging.PageIndex * paging.PageSize), param);
            }

            return result;
        }

        protected virtual bool UpdateEntity(T obj)
        {
            if (obj is IJsonTranslator)
            {
                ((IJsonTranslator)obj).ResetJsonValue();
            }
            if (obj is IChangeTrack && ((IChangeTrack)obj).IsEnableAudit())
            {
                List<string> changeFields = ((IChangeTrack)obj).ChangedFields();
                if (changeFields != null && changeFields.Count > 0)
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        bool result = conn.UpdateByAudit<T>(obj, changeFields);
                        return result;
                    }
                }
                return true;
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    bool result = conn.Update<T>(obj);
                    return result;
                }
            }
        }

        protected virtual IEnumerable<T> Query(string sql, object param = null)
        {
            IEnumerable<T> result = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                result = conn.Query<T>(sql, param);
            }

            return result;
        }
        protected IEnumerable<T1> Query<T1>(string sql, object param = null)
        {
            //throw new NotImplementedException();
            IEnumerable<T1> result = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                result = conn.Query<T1>(sql, param);
            }

            return result;
        }

        protected virtual List<ComplexEntity<T1, T2>> Query<T1, T2>(string sql, object param = null) where T1 : new() where T2 : new()
        {
            List<ComplexEntity<T1, T2>> results = new List<ComplexEntity<T1, T2>>();
            List<dynamic> result = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                result = conn.Query(sql, param).ToList();
            }

            if (result != null && result.Count > 0)
            {
                foreach (dynamic item in result)
                {
                    IDictionary<string, object> fields = GetDynamicFields(item);

                    T1 t1Obj = new T1();
                    T2 t2Obj = new T2();
                    ComplexEntity<T1, T2> entity = new ComplexEntity<T1, T2>() { Obj1 = t1Obj, Obj2 = t2Obj };

                    Dictionary<string, Tuple<Type, PropertyInfo>> dicFields = GetGenericFields<T1, T2>();

                    foreach (KeyValuePair<string, object> field in fields)
                    {
                        if (dicFields.ContainsKey(field.Key))
                        {
                            Type t = dicFields[field.Key].Item1;
                            PropertyInfo property = dicFields[field.Key].Item2;
                            if (t == typeof(T1))
                            {
                                property.SetValue(t1Obj, field.Value);
                            }
                            else if (t == typeof(T2))
                            {
                                property.SetValue(t2Obj, field.Value);
                            }
                        }
                    }
                    results.Add(entity);
                }
            }

            return results;
        }

        protected virtual IEnumerable<T1> QueryObject<T1>(string sql, object param = null) where T1 : class
        {
            IEnumerable<T1> result = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                result = conn.Query<T1>(sql, param);
            }

            return result;
        }

        protected bool BulkCopy(DataTable table)
        {
            using (var bulkCopy = new SqlBulkCopy(connString))
            {
                bulkCopy.BatchSize = table.Rows.Count;
                bulkCopy.DestinationTableName = table.TableName;

                foreach (DataColumn column in table.Columns)
                {
                    bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }

                bulkCopy.WriteToServer(table);
            }
            return true;
        }

        private IDictionary<string, object> GetDynamicFields(object obj)
        {
            List<string> fields = new List<string>();
            return (IDictionary<string, object>)obj;
        }

        private Dictionary<string, Tuple<Type, PropertyInfo>> GetGenericFields<T1, T2>()
        {
            Type t = typeof(ComplexEntity<T1, T2>);

            if (RepositoryCache.ComplexPropertyCache.ContainsKey(t))
            {
                return RepositoryCache.ComplexPropertyCache[t];
            }

            Dictionary<string, Tuple<Type, PropertyInfo>> dic = new Dictionary<string, Tuple<Type, PropertyInfo>>();
            PropertyInfo[] ps1 = typeof(T1).GetProperties();
            PropertyInfo[] ps2 = typeof(T2).GetProperties();

            foreach (PropertyInfo property in ps1)
            {
                dic[property.Name] = new Tuple<Type, PropertyInfo>(typeof(T1), property);
            }

            foreach (PropertyInfo property in ps2)
            {
                dic[property.Name] = new Tuple<Type, PropertyInfo>(typeof(T2), property);
            }

            RepositoryCache.ComplexPropertyCache[t] = dic;
            return dic;
        }
    }
}