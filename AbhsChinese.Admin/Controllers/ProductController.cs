using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private static List<AbhsChinese.Domain.Entity.Table> _tables = new List<Domain.Entity.Table>();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Table()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetTable(int pageSize, int pageIndex, string search_key)
        {
            TableBll bll = new TableBll();

            PagingObject pagingObject = new PagingObject(pageIndex, pageSize);

            // 获取数据源
            List<AbhsChinese.Domain.Entity.Table> tables = bll.GetPagingTable("Id,Name,Age,Sex,Phone ", $" [Table] where Name like '%{search_key}%' ", "Id", pagingObject);
            _tables = tables;
            return Json(new { data = tables, limit = pageSize, total = pagingObject.TotalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Playiframe()
        {
            return View();
        }

        public ActionResult Export()
        {
            string filePath = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

            ExcelHelper.ExportByWeb(_tables, "sheet1", filePath);

            return Content("");
        }

        [HttpPost]
        public int UpdateCell(int field, int id)
        {
            TableBll bll = new TableBll();
             bll.UpdateCell(field, id);
            return 1;
        }
    }
}