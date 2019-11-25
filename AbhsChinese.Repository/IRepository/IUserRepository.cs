using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System.Collections.Generic;

namespace AbhsChinese.Repository.IRepository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        int Add(User entity);

        bool Update(User entity);

        User Get(int keyValue);

        bool Delete(User entity);

        List<User> GetPagingUser(string fields, string where, string orderby,
            PagingObject paging);
    }
}