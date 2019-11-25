using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IProductRepository : IRepositoryBase<Product>
    {

        int Add(Product entity);

        bool Update(Product entity);

        Product Get(int keyValue);

        bool Delete(Product entity);

        List<DtoProductCategory> GetPagingProduct(PagingObject paging, int id);

        List<ComplexEntity<Product, ProductCategory>> GetProductWithCategory();

        List<ComplexEntity<ProductCategory, int>> ProductGroup();

        List<Product> GetByIds();

    }
}
