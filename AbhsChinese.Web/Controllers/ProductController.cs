using AbhsChinese.Bll;
using AbhsChinese.Domain.Common;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AbhsChinese.Web.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult Index()
        {
            ProductBll bll = new ProductBll();

            List<ComplexEntity<Product, ProductCategory>> objs = bll.GetProductWithCategory();
            List<ProductModel> model = ProductModel.Convert(objs);
            return View(model);
        }

        public ActionResult Index1()
        {
            ProductBll bll = new ProductBll();
            //List<Product> objs = bll.GetAll();
            
            return null;
        }
    }
}