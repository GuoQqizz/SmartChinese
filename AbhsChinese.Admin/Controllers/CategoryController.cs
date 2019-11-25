using AbhsChinese.Admin.Models.Category;
using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult CategoryList()
        {
            var categories = GetCategory();
            return View(categories);
        }

        public CategoryViewModel[] GetCategory()
        {
            CategoryViewModel[] viewModels = new CategoryViewModel[5];
            CategoryViewModel gradeRow = new CategoryViewModel()
            {
                Id = 10000,
                Description = "年级分类",
                Category = string.Join(",", Enum.GetNames(typeof(GradeEnum)))
            };
            viewModels[0] = gradeRow;
            CategoryViewModel textRow = new CategoryViewModel()
            {
                Id = 10000,
                Description = "文本资源分类",
                Category = string.Join(",", Enum.GetNames(typeof(TextResourceTypeEnum)))
            };
            viewModels[1] = textRow;
            CategoryViewModel mediaRow = new CategoryViewModel()
            {
                Id = 10000,
                Description = "多媒体资源分类",
                Category = string.Join(",", Enum.GetNames(typeof(MediaResourceTypeEnum)))
            };
            viewModels[2] = mediaRow;
            CategoryViewModel knowledgeRow = new CategoryViewModel()
            {
                Id = 10000,
                Description = "知识点分类",
                Category = string.Join(",", Enum.GetNames(typeof(KnowledgeEnum)))
            };
            viewModels[3] = knowledgeRow;
            CategoryViewModel subjectRow = new CategoryViewModel()
            {
                Id = 10000,
                Description = "题目分类",
                Category = string.Join(",", Enum.GetNames(typeof(SubjectTypeEnum)))
            };
            viewModels[4] = subjectRow;
            return viewModels;
        }
    }
}