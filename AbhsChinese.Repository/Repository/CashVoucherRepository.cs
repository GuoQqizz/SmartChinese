using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class CashVoucherRepository : RepositoryBase<Yw_CashVoucher>, ICashVoucherRepository
    {
        public CashVoucherRepository() : base(AppSetting.ConnString) { }

        public int Add(Yw_CashVoucher entity)
        {
            return base.InsertEntity(entity);
        }

        public Yw_CashVoucher Get(int id)
        {
            return base.GetEntity(id);
        }

        public List<DtoCashVoucher> GetPagingCashVoucher(PagingObject paging, int id, string name, int status, int voucherType)
        {
            var strWhere = new StringBuilder();
            var parameters = new DynamicParameters();
            strWhere.Append("Yw_CashVoucher left join Yw_Course on Ycv_CourseId=Ycs_Id left join Bas_School on Ycv_SchoolId=Bsl_Id where Ycv_Status!=4");
            if (voucherType > 0)
            {
                strWhere.Append(" and Ycv_VoucherType=@VoucherType");
                parameters.Add("VoucherType", voucherType);
            }
            if (id > 0)
            {
                strWhere.Append(" and Ycv_Id=@Id");
                parameters.Add("Id", id);
            }
            else
            {
                if (!string.IsNullOrEmpty(name))
                {
                    strWhere.Append(" and Ycv_Name like @Name");
                    parameters.Add("Name", "%" + name + "%");
                }
                if (status == 1)
                {
                    strWhere.Append(" and Ycv_PublishCount>Ycv_TakenCount");
                }
                if (status == 2)
                {
                    strWhere.Append(" and Ycv_PublishCount<=Ycv_TakenCount");
                }
            }
            return base.QueryPaging<DtoCashVoucher>("Yw_CashVoucher.*,Ycs_Name,Bsl_SchoolName as SchoolName", strWhere._ToString(), "Ycv_CreateTime DESC", paging, parameters).ToList();
        }


        public List<DtoCashVoucher> GetPagingCashVoucherForSchool(PagingObject paging, int id, string name, int status, int schoolId)
        {
            var strWhere = new StringBuilder();
            var parameters = new DynamicParameters();
            strWhere.Append("Yw_CashVoucher left join Yw_Course on Ycv_CourseId=Ycs_Id where Ycv_Status!=4");

            strWhere.Append(" and Ycv_VoucherType=@VoucherType");
            parameters.Add("VoucherType", (int)VoucherTypeEnum.校区券);
            if (schoolId > 0)
            {
                strWhere.Append(" and Ycv_SchoolId=@Ycv_SchoolId");
                parameters.Add("Ycv_SchoolId", schoolId);
            }
            if (id > 0)
            {
                strWhere.Append(" and Ycv_Id=@Id");
                parameters.Add("Id", id);
            }
            else
            {
                if (!string.IsNullOrEmpty(name))
                {
                    strWhere.Append(" and Ycv_Name like @Name");
                    parameters.Add("Name", "%" + name + "%");
                }
                if (status == 1)
                {
                    strWhere.Append(" and Ycv_PublishCount>Ycv_TakenCount");
                }
                if (status == 2)
                {
                    strWhere.Append(" and Ycv_PublishCount<=Ycv_TakenCount");
                }
            }
            return base.QueryPaging<DtoCashVoucher>("Yw_CashVoucher.*,Ycs_Name", strWhere._ToString(), "Ycv_CreateTime DESC", paging, parameters).ToList();
        }
        public DtoCashVoucher GetVoucherById(int id)
        {
            string sql = "select *from Yw_CashVoucher left join Yw_Course on Ycv_CourseId=Ycs_Id where Ycv_Id=@Id";
            return base.QueryObject<DtoCashVoucher>(sql, new { Id = id }).FirstOrDefault();
        }

        public bool Update(Yw_CashVoucher entity)
        {
            return base.UpdateEntity(entity);
        }

        public DtoCashVoucher GetCashVoucherDetail(int cashVoucherId)
        {
            string sql = @"SELECT  Yw_CashVoucher.* ,
                                    t.* ,
                                    c.Ycs_Name,
                                    s.Bsl_SchoolName as SchoolName
                            FROM    Yw_CashVoucher
                                    LEFT JOIN(SELECT  Ysv_CashVoucherId,
                                                        COUNT(1) AS UsedCount
                                                FROM    Yw_StudentCashVoucher
                                                WHERE   Ysv_Status = 2
                                                GROUP BY Ysv_CashVoucherId
                                              ) t ON Ycv_Id = Ysv_CashVoucherId
                                    LEFT JOIN dbo.Yw_Course c ON Ycv_CourseId = c.Ycs_Id
                                    LEFT JOIN dbo.Bas_School s ON Ycv_SchoolId=s.Bsl_Id
                           WHERE Ycv_Id=@CashVoucherId";
            return base.QueryObject<DtoCashVoucher>(sql, new { CashVoucherId = cashVoucherId }).FirstOrDefault();
        }

        /// <summary>
        /// 学生可领取注册券Id
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public List<Yw_CashVoucher> GetAvailableRegisterVoucherId(int studentId, int schoolId)
        {
            string sql = @"select * from yw_cashvoucher
                              where Ycv_VoucherType=4 
                            	and (Ycv_ExpireType>1 or Ycv_ExpireDate>=getdate())
                            	and (ycv_schoolid = @SchoolId OR ycv_schoolid = 0) 
                            	and ycv_status=1 
                            	and Ycv_Id not in (select ysv_cashvoucherid from yw_studentcashvoucher WHERE  ysv_studentid = @StudentId)
                            	and Ycv_PublishCount>Ycv_TakenCount";
            return Query(sql, new { SchoolId = schoolId, StudentId = studentId }).ToList();
        }
        
        /// <summary>
        /// 学生可领取注册券
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<DtoOwnCashVoucher> GetAvailableRegisterVoucherByIds(List<int> ids)
        {
            string sql = @"select Yw_CashVoucher.*,Bsl_SchoolName,Ycs_Name from Yw_CashVoucher left join Yw_Course on Ycv_CourseId=Ycs_Id left join Bas_School on Ycv_SchoolId=Bsl_Id where Ycv_Id in @Ids";
            return QueryObject<DtoOwnCashVoucher>(sql, new { Ids=ids }).ToList();
        }
        
        /// <summary>
        /// 查询学生可使用的所有现金券（包括可领取的和已拥有的）
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<DtoOwnCashVoucher> GetAvailableCashVoucherByIds(List<int> ids)
        {
            string sql = @"select Yw_CashVoucher.*,Bsl_SchoolName,Ycs_Name from Yw_CashVoucher left join Yw_Course on Ycv_CourseId=Ycs_Id left join Bas_School on Ycv_SchoolId=Bsl_Id where Ycv_Id in @Ids";
            return QueryObject<DtoOwnCashVoucher>(sql, new { Ids=ids }).ToList();
        }

        /// <summary>
        /// 查询已下单的现金券
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<DtoOwnCashVoucher> GetCashVoucherByOrderId(int studentId,int orderId)
        {
            string sql = @"select Yw_CashVoucher.*,Ysv_Id,Ysv_ExpireDate,Ysv_TakenTime,Bsl_SchoolName,Ycs_Name from Yw_CashVoucher join Yw_StudentCashVoucher on Ycv_Id=Ysv_CashVoucherId left join Yw_Course on Ycv_CourseId=Ycs_Id left join Bas_School on Ycv_SchoolId=Bsl_Id where Ysv_StudentId=@StudentId and Ysv_UsedReferId=@OrderId";
            return QueryObject<DtoOwnCashVoucher>(sql, new { StudentId = studentId, OrderId = orderId }).ToList();
        }

        /// <summary>
        /// 修改现金券已领取数量
        /// </summary>
        /// <param name="cashVoucherId"></param>
        /// <param name="schoolId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateCashVoucherTakenCount(int cashVoucherId, int schoolId)
        {
            var sql = @"update yw_cashvoucher set Ycv_TakenCount=Ycv_TakenCount+1
                            where ycv_id = @CashVoucherId and (Ycv_ExpireType > 1 or Ycv_ExpireDate >= getdate())
                            and(ycv_schoolid = @SchoolId OR ycv_schoolid = 0) 
                            and ycv_status = 1 
                            and Ycv_PublishCount> Ycv_TakenCount
                            and Ycv_VoucherType <> 2";
            return Execute(sql, new { CashVoucherId = cashVoucherId, SchoolId = schoolId });
        }

        /// <summary>
        /// 查询学生可使用的所有现金券Id（成单券除外）
        /// </summary>
        /// <param name="simpleCourse"></param>
        /// <param name="userId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public List<DtoSimpleCachVoucher> GetCashVoucherId(DtoSimpleCourse simpleCourse, int userId, int schoolId)
        {
            List<DtoSimpleCachVoucher> result = new List<DtoSimpleCachVoucher>();
            if (simpleCourse!=null)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@CouserInfo", simpleCourse.Row, DbType.String, ParameterDirection.Input);
                parameter.Add("@UserId", userId, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@SchoolId", schoolId, DbType.Int32, ParameterDirection.Input);
                result = QueryProc<DtoSimpleCachVoucher>("P_getallvoucherforusercourse", parameter).ToList();
            }
            return result;
        }
    }
}
