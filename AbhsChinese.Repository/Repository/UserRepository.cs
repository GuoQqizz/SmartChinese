using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System.Collections.Generic;
using System.Linq;

namespace AbhsChinese.Repository.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository() : base("")
        {
        }

        public int Add(User entity)
        {
            int addedRows = base.InsertEntity(entity);
            return addedRows;
        }

        public bool Delete(User entity)
        {
            return base.DeleteEntity(entity);
        }

        public User Get(int keyValue)
        {
            User user = base.GetEntity(keyValue);
            if (user != null)
            {
                user.EnableAudit();
            }
            return user;
        }

        public bool Update(User entity)
        {
            return base.UpdateEntity(entity);

        }

        public List<User> GetPagingUser(string fields, string where, string orderby,
            PagingObject paging)
        {
            List<User> list = base.QueryPaging<User>(fields, where, orderby, paging).ToList();
            return list;
        }
    }
}