using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;

namespace AbhsChinese.Repository.Repository
{
    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository() : base(AppSetting.ConnString)
        {

        }

        public void Add(ProductCategory entity)
        {
            base.InsertEntity(entity);
        }

        public List<ProductCategory> GetAll()
        {
            return null;
        }
    }
}
