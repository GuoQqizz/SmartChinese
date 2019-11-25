using AbhsChinese.Admin.Models;
using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Request.Teachers;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class Select2Controller : BaseController
    {

        RegionBll regionBll = new RegionBll();
        [HttpGet]
        public ActionResult GetCourseTypes()
        {
            var options = CustomEnumHelper.GetElements(typeof(CourseCategoryEnum));
            var result = OptionFactory.CreateOptions(options);
            return Select2(result);
        }

        [HttpGet]
        public ActionResult GetCurriculumCategaries()
        {
            var categories = CustomEnumHelper.GetElements(typeof(CourseCategoryEnum));
            var result = OptionFactory.CreateOptions(categories);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDifficulties()
        {
            var difficuties = CustomEnumHelper.GetElements(typeof(DifficultyEnum));
            var result = OptionFactory.CreateOptions(difficuties);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployeeStatus()
        {
            var textType = CustomEnumHelper.GetElements(typeof(StatusEnum));
            if (textType.ContainsKey((int)StatusEnum.删除))//
            {
                textType.Remove((int)StatusEnum.删除);
            }
            var result = OptionFactory.CreateOptions(textType);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGrades()
        {
            var difficuties = CustomEnumHelper.GetElements(typeof(GradeEnum));
            var result = OptionFactory.CreateOptions(difficuties);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetKeywords()
        {
            Dictionary<int, string> dics = new Dictionary<int, string>();
            dics.Add(10000, "10000key");
            dics.Add(10001, "10001key");
            dics.Add(10002, "10002key");
            var result = OptionFactory.CreateOptions(dics);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetKnowledges()
        {
            Dictionary<int, string> dics = new Dictionary<int, string>();
            dics.Add(10000, "10000Kouwledge");
            dics.Add(10001, "10001Kouwledge");
            dics.Add(10002, "10002Kouwledge");
            var result = OptionFactory.CreateOptions(dics);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取主知识点的下拉列表项
        /// </summary>
        /// <param name="text"></param>
        /// <param name="searchValue"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult GetKernelKnowledgePoint(
            string text,
            int searchValue = 0,//select page预选值的id
            int pageNumber = 1,
            int pageSize = 10)
       {
            DtoKnowledgeSearch search = new DtoKnowledgeSearch
            {
                Level = KnowledgeEnum.四级知识点,
                Pagination = new PagingObject(pageNumber, pageSize)
            };

            if (searchValue > 0)//为预选值加载数据
            {
                search.Ids = new List<int> { searchValue };
            }
            else//根据用户数据进行查询
            {
                int id = -1;
                if (!int.TryParse(text, out id))//转换id失败，则为根据关键字查询
                {
                    search.Keyword = text;
                }
                else//转换id成功，则是根据id查询
                {
                    search.Ids = new List<int> { id };
                }

            }

            return GetKnowledages(search);
        }
        public ActionResult GetSecondaryKnowledgePoints(
            string text,
            string searchValue,
            int kernelKonwledgePointId = 0,
            int pageNumber = 1,
            int pageSize = 10)
        {
            DtoKnowledgeSearch search = new DtoKnowledgeSearch
            {
                Level = KnowledgeEnum.四级知识点,
                Pagination = new PagingObject(pageNumber, pageSize),
                ParentId = kernelKonwledgePointId
            };

            if (searchValue.HasValue())
            {
                search.Ids = searchValue.Split(',')
                                        .Select(s => int.Parse(s))
                                        .ToList();
            }
            else
            {
                int id = -1;
                if (!int.TryParse(text, out id))//转换id失败，则为根据关键字查询
                {
                    search.Keyword = text;
                }
                else//转换id成功，则是根据id查询
                {
                    search.Ids = new List<int> { id };
                }
            }

            return GetKnowledages(search);
        }

        private JsonResult GetKnowledages(DtoKnowledgeSearch search)
        {
            KnowledgeBll bll = new KnowledgeBll();
            IList<Yw_Knowledge> kernelKnowledgePoints = bll.GetKnowledgeForSubject(search);
            var result = kernelKnowledgePoints.Select(k => new SelectPageOption
            {
                Key = k.Ykl_Id,
                Text = k.Ykl_Name
            });

            return Json(new
            {
                totalRow = search.Pagination.TotalCount,
                list = result
            }, JsonRequestBehavior.AllowGet);
       }

        public ActionResult GetMediaType()
        {
            var mediaType = CustomEnumHelper.GetElements(typeof(MediaResourceTypeEnum));
            var result = OptionFactory.CreateOptions(mediaType);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMediaTypeExceptXiaoAi()
        {
            var mediaType = CustomEnumHelper.GetElements(typeof(MediaResourceTypeEnum));
            mediaType.Remove((int)MediaResourceTypeEnum.小艾说);
            mediaType.Remove((int)MediaResourceTypeEnum.开场语);
            mediaType.Remove((int)MediaResourceTypeEnum.小艾变);
            var result = OptionFactory.CreateOptions(mediaType);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubjectType()
        {
            var subjectType = CustomEnumHelper.GetElements(typeof(SubjectTypeEnum));
            var result = OptionFactory.CreateOptions(subjectType);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetOwners()
        {
            EmployeeBll bll = new EmployeeBll();
            var employees = bll.GetPagingEmployee(
                new PagingObject(1, int.MaxValue),
                string.Empty,
                0,
                (int)StatusEnum.有效);

            Dictionary<int, string> dic = new Dictionary<int, string>();
            employees.ForEach(e => dic.Add(e.Bem_Id, e.Bem_Name));
            var result = OptionFactory.CreateOptions(dic);
            return Select2(result);
        }

        [HttpGet]
        public ActionResult GetResourceGroups()
        {
            ResourceBll bll = new ResourceBll();
            var resourceGroups = bll.GetPagingResourceGroup(
                new PagingObject(1, int.MaxValue),
                0,
                string.Empty,
                0,
                1);

            Dictionary<int, string> dic = new Dictionary<int, string>();
            resourceGroups.ForEach(e => dic.Add(e.Yrg_Id, e.Yrg_Name));
            var result = OptionFactory.CreateOptions(dic);
            return Select2(result);
        }

        public ActionResult GetRoleCode()
        {
            EmployeeBll employeeBll = new EmployeeBll();
            Dictionary<string, string> dicRole = new Dictionary<string, string>();
            List<Bas_Role> rolelist = employeeBll.GetRoleList();
            var list = rolelist.Select(s => { return new { id = s.Bro_Id, text = s.Bro_Name }; });
            return Select2(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTeachers(DtoEmployeeSearch search)
        {
            search.Status = StatusEnum.有效;
            search.RoleCode = "teacher";

            EmployeeBll bll = new EmployeeBll();
            var employees = bll.GetEmployees(search);

            Dictionary<int, string> dic = new Dictionary<int, string>();
            employees.ToList().ForEach(e => dic.Add(e.Id, e.Name));
            var result = OptionFactory.CreateOptions(dic);

            return Select2(result);
        }

        public ActionResult GetTextType()
        {
            var textType = CustomEnumHelper.GetElements(typeof(TextResourceTypeEnum));
            var result = OptionFactory.CreateOptions(textType);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVoucherStatus()
        {
            var voucherStatus = CustomEnumHelper.GetElements(typeof(VoucherStatusEnum));
            var result = OptionFactory.CreateOptions(voucherStatus);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStuentCashVoucherStatus()
        {
            var voucherStatus = CustomEnumHelper.GetElements(typeof(StudentCashVoucherStatusEnum));
            var result = OptionFactory.CreateOptions(voucherStatus);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetRegion(int parentId = 0)
        {
            var list = regionBll.GetRegionList(parentId).ToDictionary(k => { return k.Reg_ID; }, v => { return v.Reg_Name; });
            var result = OptionFactory.CreateOptions(list);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult Select2(
            object data,
            JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { results = data }, null, null, behavior);
        }
    }
}