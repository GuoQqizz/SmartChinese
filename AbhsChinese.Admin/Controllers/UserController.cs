using AbhsChinese.Admin.Models;
using AbhsChinese.Admin.Models.Region;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class UserController : BaseController
    {
        private UserBll userBll;
        private UserBll UserBll
        {
            get
            {
                if (userBll == null)
                {
                    userBll = new UserBll();
                }

                return userBll;
            }
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetUsers(int pageSize, int pageIndex, string search_key)
        {
            PagingObject pagingObject = new PagingObject(pageIndex, pageSize);

            List<User> users = UserBll.GetUsers(search_key, pagingObject);

            return Json(new
            {
                data = users,
                limit = pageSize,
                total = pagingObject.TotalCount
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            User user = UserBll.Get(id);
            string userJson = JsonConvert.SerializeObject(new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age,
                Phone = user.Phone,
                Email = user.Email,
                Hobby = user.Hobby,
                Resume = new UEditor { Content = user.Resume }
            });
            return Content(userJson);
        }

        [HttpPost]
        [ActionName("Add")]
        public ActionResult Add(UserViewModel user)
        {
            bool result = UserBll.Add(new User
            {
                Name = user.Name,
                Age = user.Age,
                Phone = user.Phone,
                Email = user.Email,
                Hobby = user.Hobby,
                Resume = user.Resume.Content,
            });
            return Json(result);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            User userUpdate = UserBll.Get(user.Id);
            userUpdate.Name = user.Name;
            userUpdate.Age = user.Age;
            userUpdate.Phone = user.Phone;
            userUpdate.Email = user.Email;
            userUpdate.Hobby = user.Hobby;
            userUpdate.Resume = user.Resume.Content;
            bool result = UserBll.Update(userUpdate);
            return Json(result);
        }

        [HttpPost]
        public ActionResult UpdateAge(int age, int id)
        {
            return Json("");
        }

        public ActionResult Delete(int id)
        {
            User userDelete = UserBll.Get(id);
            bool result = UserBll.Delete(userDelete);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxAdd()
        {
            bool isAjaxRequest = Request.IsAjaxRequest();
            return Json(isAjaxRequest);
        }

        public ActionResult Forms()
        {
            return View();
        }

        public ActionResult DialogForms()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowUMEditor()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveUMEditor(ShowUEditorModel model)
        {
            return Json("");
        }

        public ActionResult GetOptions(Region region, string search, int parentId = 0)
        {
            List<Address> addresses = new List<Address>();
            switch (region)
            {
                case Region.Country:
                    break;

                case Region.Province:
                    addresses.Add(new Province { Id = 0, Text = "河北" });
                    addresses.Add(new Province { Id = 1, Text = "北京" });
                    addresses.Add(new Province { Id = 2, Text = "河南" });
                    break;

                case Region.City:
                    addresses.Add(new Province { Id = 0, Text = "保定", BelongsTo = 0 });
                    addresses.Add(new Province { Id = 1, Text = "石家庄", BelongsTo = 0 });
                    addresses.Add(new Province { Id = 2, Text = "秦皇岛", BelongsTo = 0 });

                    addresses.Add(new Province { Id = 3, Text = "海淀", BelongsTo = 1 });
                    addresses.Add(new Province { Id = 4, Text = "昌平", BelongsTo = 1 });
                    addresses = addresses.Where(a => a.BelongsTo == parentId).ToList();
                    break;

                case Region.County:
                    addresses.Add(new Province { Id = 0, Text = "易县", BelongsTo = 0 });
                    addresses.Add(new Province { Id = 1, Text = "满城", BelongsTo = 0 });

                    addresses.Add(new Province { Id = 3, Text = "西北旺", BelongsTo = 1 });
                    addresses.Add(new Province { Id = 4, Text = "四季青", BelongsTo = 1 });
                    addresses = addresses.Where(a => a.BelongsTo == parentId).ToList();
                    break;

                default:
                    break;
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                addresses = addresses.Where(a => a.Text.Contains(search)).ToList();
            }

            string result = JsonConvert.SerializeObject(addresses);

            return Content(result);
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}