using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using Business;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace AbhsChinese.Bll
{
    public class ProductBll : BllBase
    {
        private IProductRepository service;

        public IProductRepository Service
        {
            get
            {
                if (service == null)
                {
                    service = new ProductRepository();
                }

                service.TranId = tranId;
                return service;
            }
        }

        private IProductCategoryRepository categoryService;

        public IProductCategoryRepository CategoryService
        {
            get
            {
                if (categoryService == null)
                {
                    categoryService = new ProductCategoryRepository();
                }

                categoryService.TranId = tranId;
                return categoryService;
            }
        }

        private ISumAssessmentDetailRepository sumAssessmentDetailRepository;

        public ISumAssessmentDetailRepository SumAssessmentDetailRepository
        {
            get
            {
                if (sumAssessmentDetailRepository == null)
                {
                    sumAssessmentDetailRepository = new SumAssessmentDetailRepository();
                }

                sumAssessmentDetailRepository.TranId = tranId;
                return sumAssessmentDetailRepository;
            }
        }

        public ProductBll() : base()
        {

        }

        public List<ProductCategory> Categories()
        {
            return CategoryService.GetAll();
        }

        public List<sum_AssessmentDetail> GetAssessments(int id)
        {
            return SumAssessmentDetailRepository.GetAssessments(id);
        } 

        //public List<Product> GetAll()
        //{
        //    DateTime x = DateTime.Now;
        //    Service.GetAll();
        //    DateTime y = DateTime.Now;
        //    LogHelper.WriteLog((y - x).TotalMilliseconds.ToString());
        //    return Service.GetAll();
        //}

        public void AddProduct(Product p)
        {
            Service.Add(p);
        }

        //public void AddProducts(List<Product> entities)
        //{
        //    Service.Add(entities);
        //}

        public void TestMultipleOperation()
        {
            //contextFactory.DBContext.Products.Add(new Product() { Desc = "test 6-3" });
            //contextFactory.DBContext.SaveChanges();
            //contextFactory.DBContext.ProductCategorys.Add(new ProductCategory() { Name = "test" });
            //contextFactory.DBContext.SaveChanges();
        }

        public Product GetProduct(int id)
        {
            return Service.Get(id);
        }

        public bool UpdateProduct(Product p)
        {
            return Service.Update(p);
        }

        public bool DeleteProduct(Product p)
        {
            return Service.Delete(p);
        }

        public void TranTest(Product p)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    BeginTran();
                    AddProduct(p);
                    //p.ForceAudit();
                    //p.Desc = "desc";
                    var px = GetProduct(222);
                    px.p_Desc = "ceshi222";
                    UpdateProduct(px);
                    //UpdateProduct(p);
                    new ProductBll().AddProductCategory(new ProductCategory() {pc_Name = "name.6.15" });
                    TranTest1(p);
                    scope.Complete();
                    //CommitTran();
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    //throw ex;
                }
            }
        }

        public void TranTest1(Product p)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    BeginTran();
                    AddProduct(p);
                    //p.ForceAudit();
                    //p.Desc = "desc";
                    var px = GetProduct(222);
                    px.p_Desc = "ceshi222";
                    UpdateProduct(px);
                    //UpdateProduct(p);
                    new ProductBll().AddProductCategory(new ProductCategory() { pc_Name = "name.6.15" });
                    throw new Exception("test tran rollback");
                    scope.Complete();
                    //CommitTran();
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw;
                }
            }
        }

        public void AddProductCategory(ProductCategory cat)
        {
            CategoryService.Add(cat);
        }

        public IList<DtoProductCategory> GetPagingProduct(PagingObject paging, int id)
        {
            return Service.GetPagingProduct(paging, id);
        }

        public List<ComplexEntity<Product, ProductCategory>> GetProductWithCategory()
        {
            return Service.GetProductWithCategory();
        }

        public List<ComplexEntity<ProductCategory, int>> ProductGroup()
        {
            return Service.ProductGroup();
        }

        public List<Product> GetByIds()
        {
            return Service.GetByIds();
        }
    }
}
