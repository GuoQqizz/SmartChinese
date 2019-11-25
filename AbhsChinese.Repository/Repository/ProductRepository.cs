using AbhsChinese.Domain.Entity;
using System.Linq;
using AbhsChinese.Repository.RepositoryBase;
using AbhsChinese.Repository.IRepository;
using System.Collections.Generic;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Common;
using AbhsChinese.Code.Cache;
using System.Data;
using System;

namespace AbhsChinese.Repository.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository() : base(AppSetting.ConnString)
        {
        }

        public int Add(Product entity)
        {
            var result = base.InsertEntity(entity);
            CacheHelper.NotifyRefreshCache(CacheDependencyEnum.Product, CacheDependencyActionType.Add, entity.p_Id, tranId);
            return result;
        }

        public bool Update(Product entity)
        {
            var result = base.UpdateEntity(entity);
            //CacheHelper.NotifyRefreshCache(CacheDependencyEnum.Product, CacheDependencyActionType.Update, entity.p_Id, tranId);
            return result;
        }

        public Product Get(int keyValue)
        {
            Product p = base.GetEntity(keyValue);
            //Product p = CacheHelper.GetCacheWithFill(CacheKeyEnum.Product_Cache, base.FindEntity, keyValue);

            if (p != null)
            {
                p.EnableAudit();
            }
         
            return p;
        }

        public bool Delete(Product entity)
        {
            var result= base.DeleteEntity(entity);
            CacheHelper.NotifyRefreshCache(CacheDependencyEnum.Product, CacheDependencyActionType.Delete, entity.p_Id, tranId);
            return result;
        }

        public bool Delete(int id)
        {
            bool result= base.DeleteEntity(new Product() { p_Id = id });
            CacheHelper.NotifyRefreshCache(CacheDependencyEnum.Product, CacheDependencyActionType.Delete, id, tranId);
            return result;
        }

        public List<DtoProductCategory> GetPagingProduct(PagingObject paging, int id)
        {
            return base.QueryPaging<DtoProductCategory>("a.Id,a.Name as ProductName,b.Name as CatName", "Product a inner join ProductCategory b on a.CatId=b.Id and a.Id<=@Id", "a.Id", paging, new { Id = id }).ToList();
        }

        public List<ComplexEntity<Product, ProductCategory>> GetProductWithCategory()
        {
            string sql = "select * from Product p inner join ProductCategory c on p.p_catid = c.pc_id";
            return base.Query<Product, ProductCategory>(sql);
        }

        public List<ComplexEntity<ProductCategory, int>> ProductGroup()
        {
            //var query = from pc in dbcontext.ProductCategorys
            //            join p in dbcontext.Products
            //            on pc.Id equals p.CatId into g
            //            select new { Obj1 = pc, Obj2 = g.Count() };
            //var c = query.ToList();

            return new List<ComplexEntity<ProductCategory, int>>();
        }

        public List<Product> GetByIds()
        {
            string inNames = "'3.name',";
            for (int index = 1; index <= 200; index++)
            {
                inNames += "'发简历开发商',";
            }
            inNames = inNames.Trim(',');
            string sql = $@"select* from product where p_name in ({inNames})";
            return Query(sql).ToList();
        }
    }
}
