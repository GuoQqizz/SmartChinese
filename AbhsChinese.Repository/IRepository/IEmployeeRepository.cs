using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System.Collections.Generic;
using AbhsChinese.Domain.Dto.Request.Teachers;
using AbhsChinese.Domain.Dto.Response.Teachers;

namespace AbhsChinese.Repository.IRepository
{
    public interface IEmployeeRepository : IRepositoryBase<Bas_Employee>
    {
        int Add(Bas_Employee entity);

        Bas_Employee GetEntity(int bemId);

        List<DtoEmployee> GetPagingEmployee(PagingObject paging, string accountOrNameOrPhone, int broId, int status);
        /// <summary>
        /// 根据id集合批量获取用户名
        /// </summary>
        /// <param name="ids">用户id集合</param>
        /// <returns>返回key:id,value:name的字典</returns>
        Dictionary<int, string> GetEmployeeNameByIds(List<int> ids);
        bool Update(Bas_Employee entity);
        int CheckUniqueAccount(string account);

        DtoEmployee GetByAccount(string account,int status=0);
        IList<Bas_Employee> GetEntities(IEnumerable<int> ids);
        IList<DtoTeacher> GetEmployees(DtoEmployeeSearch search);
    }
}