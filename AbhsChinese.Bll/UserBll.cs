using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using System.Collections.Generic;

namespace AbhsChinese.Bll
{
    public class UserBll : BllBase
    {
        private IUserRepository service;

        public IUserRepository Service
        {
            get
            {
                if (service == null)
                {
                    service = new UserRepository();
                }

                service.TranId = tranId;
                return service;
            }
        }

        public UserBll()
        {
        }

        public bool Add(User user)
        {
            return Service.Add(user) > 0;
        }

        public bool Update(User user)
        {
            return Service.Update(user);
        }

        public User Get(int keyValue)
        {
            return Service.Get(keyValue);
        }

        public bool Delete(User user)
        {
            return Service.Delete(user);
        }

        public List<User> GetUsers(string search_key, PagingObject paging)
        {
            return Service.GetPagingUser("Id,Name,Age,Phone,Hobby,Email,Resume ",
                $" [User] where Name like '%{search_key}%' ",
                "Id desc",
                paging);
        }
    }
}