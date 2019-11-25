using AbhsChinese.Domain.Common;
using AbhsChinese.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models
{
    public class ProductModel
    {
        public int Id
        {
            set; get;
        }

        public string FullName
        {
            set; get;
        }

        public string Desc
        {
            set; get;
        }

        public static List<ProductModel> Convert(List<ComplexEntity<Product, ProductCategory>> objs)
        {
            List<ProductModel> models = new List<ProductModel>();
            foreach (ComplexEntity<Product, ProductCategory> item in objs)
            {
                models.Add(Convert(item));
            }
            return models;
        }

        private static ProductModel Convert(ComplexEntity<Product, ProductCategory> obj)
        {
            return new ProductModel() { Id = obj.Obj1.p_Id, FullName = obj.Obj2.pc_Name + " " + obj.Obj1.p_Name, Desc = obj.Obj1.p_Desc };
        }
    }
}