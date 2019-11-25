using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using Dapper;

namespace AbhsChinese.Repository.Repository
{
    public class ResourceGroupRepository : RepositoryBase<Yw_ResourceGroup>, IResourceGroupRepository
    {
        public ResourceGroupRepository() : base(AppSetting.ConnString)
        {
        }

        public int AddResourceGroup(Yw_ResourceGroup entity)
        {
            return base.InsertEntity(entity);
        }

        public Yw_ResourceGroup GetResourceGroup(int id)
        {
            return base.GetEntity(id);
        }

        public bool Delete(Yw_ResourceGroup entity)
        {
            throw new NotImplementedException();
        }

        public List<Yw_ResourceGroup> GetPagingResourceGroup(PagingObject paging, int id, string name, int grade, int status)
        {
            var strWhere = new StringBuilder();
            var parameters = new DynamicParameters();
            strWhere.Append(@"Yw_ResourceGroup where 1=1");
            if (status > 0)
            {
                strWhere.Append(" and Yrg_Status=@Status");
                parameters.Add("Status", status);
            }
            if (id > 0)
            {
                strWhere.Append(" and Yrg_Id=@Id");
                parameters.Add("Id", id);
            }
            else
            {
                if (grade > 0)
                {
                    strWhere.Append(
                        " and Yrg_Grade=@Grade");
                    parameters.Add("Grade", grade);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    strWhere.Append(" and Yrg_Name like @name");
                    parameters.Add("name", "%" + name + "%");
                }
            }
            return base.QueryPaging<Yw_ResourceGroup>("*", strWhere._ToString(), "Yrg_Id", paging, parameters).ToList();
        }

        public bool Update(Yw_ResourceGroup entity)
        {
            return base.UpdateEntity(entity);
        }
    }
}
