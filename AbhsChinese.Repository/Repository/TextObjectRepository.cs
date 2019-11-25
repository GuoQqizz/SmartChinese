using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using Dapper;

namespace AbhsChinese.Repository.Repository
{
    public class TextObjectRepository : RepositoryBase<Yw_TextObject>, ITextObjectRepository
    {
        public TextObjectRepository() : base(AppSetting.ConnString)
        {
        }

        public int Add(Yw_TextObject entity)
        {
            return base.InsertEntity(entity);
        }

        public Yw_TextObject GeTextObject(int id)
        {
            var entity = base.GetEntity(id);
            if(entity!=null)
            {
                entity.EnableAudit();
            }
            return entity;
        }

        public DtoMediaResourceToCourse GetTextDetailToCourse(int id)
        {
            var sql = "select Yxo_Id as ObjectTextID,Yxo_Content as ObjectText from Yw_TextObject where Yxo_Id=@Id";
            return base.QueryObject<DtoMediaResourceToCourse>(sql, new { Id = id }).FirstOrDefault();
        }

        public bool Update(Yw_TextObject entity)
        {
            return base.UpdateEntity(entity);
        }

        public List<Yw_TextObject> GetTextObjectByIdList(List<int> ids)
        {
            var sql = "select *from Yw_TextObject where Yxo_Id in @Ids";
            return base.Query(sql, new { Ids = ids }).ToList();
        }
    }
}
