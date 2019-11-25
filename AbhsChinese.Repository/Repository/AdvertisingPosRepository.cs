using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using Dapper;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Repository.Repository
{
    public class AdvertisingPosRepository : RepositoryBase<Bas_AdvertisingPos>, IAdvertisingPosRepository
    {
        public AdvertisingPosRepository() : base(AppSetting.ConnString)
        {
        }



        #region select
        public List<DtoAdvertisingPos> GetPagingAdvertisingHistory(PagingObject paging, int advPosId)
        {
            var strWhere = new StringBuilder();
            var fields = "ap.*,a.*,c.Ycs_Name AS Bad_ReferName ";
            var orderBy = "a.Bad_Id DESC";
            var parameters = new DynamicParameters();
            strWhere.Append($@"dbo.Bas_AdvertisingPos ap 
                                JOIN dbo.Bas_Advertising a ON ap.Bap_Id=a.Bad_PosId
                                LEFT JOIN dbo.Yw_Course c ON a.Bad_ReferId=c.Ycs_Id
                                WHERE 1=1");

            if (advPosId > 0)
            {
                strWhere.Append(" AND ap.Bap_Id=@Bap_Id");
                parameters.Add("Bap_Id", advPosId);
            }
            return base.QueryPaging<DtoAdvertisingPos>(fields, strWhere._ToString(), orderBy, paging, parameters).ToList();
        }

        public List<DtoAdvertisingPos> GetPagingAdvertisingPos(PagingObject paging, int advPosType, int advStatus)
        {

            var strWhere = new StringBuilder();
            var fields = "ap.*,a.*,c.Ycs_Name AS Bad_ReferName ";
            var orderBy = "ap.Bap_Id";
            var parameters = new DynamicParameters();
            var advertJoinWhere = advStatus > 0 ? " AND a.Bad_Status=@Bad_Status " : "";
            parameters.Add("Bad_Status", advStatus);
            strWhere.Append($@"dbo.Bas_AdvertisingPos ap 
                                LEFT JOIN dbo.Bas_Advertising a ON ap.Bap_Id=a.Bad_PosId  {advertJoinWhere}
                                LEFT JOIN dbo.Yw_Course c ON a.Bad_ReferId=c.Ycs_Id
                                WHERE 1=1");
            if (advPosType > 0)
            {
                strWhere.Append(" AND ap.Bap_Type=@Bap_Type");
                parameters.Add("Bap_Type", advPosType);
            }
            return base.QueryPaging<DtoAdvertisingPos>(fields, strWhere._ToString(), orderBy, paging, parameters).ToList();
        }


        public List<DtoAdvertisingIndex> GetPagingAdvertisingForIndex(string codePre)
        {

            string sql = $@"SELECT  *
                            FROM    dbo.Bas_AdvertisingPos ap
                                    JOIN dbo.Bas_Advertising a ON ap.Bap_Id = a.Bad_PosId
                                                                  AND a.Bad_Status = {(int)StatusEnum.有效}
                                                                  AND ap.Bap_Code like @Bap_Code
                            WHERE   ap.Bap_Status = {(int)StatusEnum.有效} ";

            return base.Query<DtoAdvertisingIndex>(sql, new { Bap_Code = $"{codePre}%" }).ToList();
        }


        #endregion


        #region save
        public bool EditAdertisingPosStatus(int bapId, int bapStatus, int editor)
        {
            string sql = "UPDATE dbo.Bas_AdvertisingPos SET Bap_Status=@Bap_Status ,Bap_Editor=@Bap_Editor,Bap_UpdateTime=GETDATE() WHERE Bap_Id=@Bap_Id";
            return Execute(sql, new
            {
                Bap_Status = bapStatus,
                Bap_Editor = editor,
                Bap_Id = bapId
            }) > 0;
        }
        #endregion


        #region basecrud
        public Bas_AdvertisingPos Get(int id)
        {
            var result = base.GetEntity(id);
            if (result != null)
            {
                result.EnableAudit();
            }
            return result;
        }
        #endregion
    }
}
