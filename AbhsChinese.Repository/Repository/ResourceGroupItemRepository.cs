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
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.Dto.Response;

namespace AbhsChinese.Repository.Repository
{
    public class ResourceGroupItemRepository : RepositoryBase<Yw_ResourceGroupItem>, IResourceGroupItemRepository
    {
        public ResourceGroupItemRepository() : base(AppSetting.ConnString)
        {
        }
        
        public int AddResourceGroupItem(Yw_ResourceGroupItem entity)
        {
            return base.InsertEntity(entity);
        }

        public int Delete(int groupId, int resourceType, int resourceId)
        {
            return base.Execute("delete from Yw_ResourceGroupItem where Ygi_GroupId=@GroupId and Ygi_ResourceType=@ResourceType and Ygi_ResourceId=@ResourceId", new { GroupId = groupId, ResourceType = resourceType, ResourceId = resourceId });
        }

        public Yw_ResourceGroupItem GetResourceGroupItem(int groupId, int resourceType, int resourceId)
        {
            return base.Query("select * from Yw_ResourceGroupItem where Ygi_GroupId=@GroupId and Ygi_ResourceType=@ResourceType and Ygi_ResourceId=@ResourceId", new { GroupId = groupId, ResourceType = resourceType, ResourceId = resourceId }).FirstOrDefault();
        }

        public int GetResourceCount(int groupId, int resourceType)
        {
            return base.ExecuteScalar<int>("select count(1) from Yw_ResourceGroupItem where Ygi_GroupId=@GroupId and Ygi_ResourceType=@ResourceType", new { GroupId = groupId, ResourceType = resourceType });
        }

        public List<Yw_ResourceGroupItem> GetGroupIdAndResourceType(int courseId)
        {
            return base.Query("select Yw_ResourceGroupItem.* from Yw_Course join Yw_ResourceGroup on Ycs_ResourceGroupId=Yrg_Id join Yw_ResourceGroupItem on Ygi_GroupId=Yrg_Id where Ycs_Id=@Id", new { Id = courseId }).ToList();
        }

        public List<Yw_ResourceGroupItem> GetResourceIdByTypeAndGroupId(int groupId, int resourceType)
        {
            var sql = "select Ygi_ResourceId from Yw_ResourceGroupItem where Ygi_GroupId=@GroupId and Ygi_ResourceType=@ResourceType";
            return Query(sql, new { GroupId = groupId, ResourceType = resourceType }).ToList();
        }
    }
}
