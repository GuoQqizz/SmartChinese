using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Dto.Response;
using Dapper;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using System.Data;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Repository.Repository
{
    public class StudentCashVoucherRepository : RepositoryBase<Yw_StudentCashVoucher>, IStudentCashVoucherRepository
    {
        public StudentCashVoucherRepository() : base(AppSetting.ConnString) { }

        /// <summary>
        /// 查询学生现金券明细
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="usedReferNo">订单编号</param>
        /// <returns></returns>
        public List<DtoStudentCashVoucher> GetPagingStudentCashVoucher(PagingObject paging, int cashVoucherId, int status, string usedReferNo)
        {
            var strWhere = new StringBuilder();
            var parameters = new DynamicParameters();
            strWhere.Append("Yw_StudentCashVoucher join Bas_Student on Ysv_StudentId=Bst_Id where Ysv_CashVoucherId=@CashVoucherId");
            parameters.Add("CashVoucherId", cashVoucherId);
            if (status > 0)
            {
                if (status == 1)
                {
                    strWhere.Append(" and Ysv_Status=1 and getdate()<=Ysv_ExpireDate");
                }
                else if (status == 3)
                {
                    strWhere.Append(" and Ysv_Status=1 and getdate()>Ysv_ExpireDate");
                }
                else
                {
                    strWhere.Append(" and Ysv_Status=@Status");
                    parameters.Add("Status", status);
                }
            }
            if (!string.IsNullOrEmpty(usedReferNo))
            {
                strWhere.Append(" and Ysv_UsedReferNo=@UsedReferNo");
                parameters.Add("UsedReferNo", usedReferNo);
            }
            return base.QueryPaging<DtoStudentCashVoucher>("Yw_StudentCashVoucher.*,Bst_Id,Bst_Phone", strWhere._ToString(), "Ysv_TakenTime desc", paging, parameters).ToList();
        }

        public List<DtoVoucherForUserCourse> GetBestVoucherForUserCourse(List<DtoSimpleCourse> simpleCourses, int userId, int schoolId)
        {
            List<DtoVoucherForUserCourse> result = new List<DtoVoucherForUserCourse>();
            if (simpleCourses.Count() > 0)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@CouserInfo", string.Join(";", simpleCourses.Select(x => x.Row)), DbType.String, ParameterDirection.Input);
                parameter.Add("@UserId", userId, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@SchoolId", schoolId, DbType.Int32, ParameterDirection.Input);
                result = base.QueryProc<DtoVoucherForUserCourse>("P_getvoucherforusercourse", parameter).ToList();
            }
            return result;
        }

        public List<DtoOwnCashVoucher> GetPagingOwnCashVoucher(PagingObject paging, int studentId, int status)
        {
            var fields = "Yw_StudentCashVoucher.*,Ycv_Id,Ycv_Name,Ycv_VoucherType,Ycv_SchoolId,Bsl_SchoolName,Ycv_OrderAmountLimit,Ycv_Amount,Ycv_ExpireType,Ycv_ApplyScopeType,Ycv_ApplyGrade,Ycv_CourseType,Ycv_CourseId,Ycs_Name";
            var sql = @"Yw_StudentCashVoucher 
                inner join Yw_CashVoucher on Ysv_CashVoucherId=Ycv_Id 
                left join Yw_Course on Ycv_CourseId=Ycs_Id
                left join Bas_School on Ycv_SchoolId=Bsl_Id where Ysv_StudentId=@StudentId ";
            if (status == (int)StudentCashVoucherStatusEnum.未使用)
            {
                sql += " and Ysv_Status=@Status";
            }
            else if (status > (int)StudentCashVoucherStatusEnum.未使用)
            {
                sql += " and Ysv_Status>1";
            }
            return QueryPaging<DtoOwnCashVoucher>(fields, sql, "Ysv_TakenTime DESC", paging, new { StudentId = studentId, Status = status }).ToList();
        }

        public int Add(Yw_StudentCashVoucher entity)
        {
            return InsertEntity(entity);
        }

        public Yw_StudentCashVoucher Get(int id)
        {
            return GetEntity(id);
        }

        /// <summary>
        /// 获取现金券编号
        /// </summary>
        /// <param name="voucherId"></param>
        /// <param name="userId"></param>
        /// <param name="voucherType"></param>
        /// <returns></returns>
        public string GetVoucherNo(int voucherId, int userId, int voucherType)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@VoucherId", voucherId, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@UserId", userId, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@VoucherType", voucherType, DbType.Int32, direction: ParameterDirection.Input);
            parameter.Add("@VoucherNo", "", DbType.String, direction: ParameterDirection.Output);
            ExecuteProc("P_CreateVoucherNo", parameter);
            return parameter.Get<string>("@VoucherNo");
        }

        /// <summary>
        /// 修改学生现金券状态（已使用）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateUsedStatus(int id)
        {
            string sql = "update Yw_StudentCashVoucher set Ysv_Status=2,Ysv_UsedTime=getdate(),Ysv_UpdateTime=getdate() where Ysv_Id=@Id";
            return Execute(sql, new { Id = id });
        }

        /// <summary>
        /// 修改现金券状态（检测是否过期）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateExpiredStatusByStudentId(int studentId)
        {
            string sql = "update Yw_StudentCashVoucher set Ysv_Status=3,Ysv_UpdateTime=getdate() where Ysv_StudentId=@StudentId and Ysv_ExpireDate<=getdate()";
            return Execute(sql, new { StudentId = studentId });
        }

        /// <summary>
        /// 判断学生是否已经拥有该现金券
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="cashVoucherId"></param>
        /// <returns></returns>
        public bool StudentIsHaveCashVoucher(int studentId, int cashVoucherId)
        {
            string sql = "select count(1) from Yw_StudentCashVoucher where Ysv_StudentId=@StudentId and Ysv_CashVoucherId=@CashVoucherId";
            return ExecuteScalar<int>(sql, new { StudentId = studentId, CashVoucherId = cashVoucherId }) > 0;
        }

        public List<Yw_StudentCashVoucher> GetStudentCashVoucherByStudentIdAndVoucherId(int studentId, List<int> voucherIds)
        {
            string sql = "select *from Yw_StudentCashVoucher where Ysv_Status=1 and Ysv_StudentId=@StudentId and Ysv_CashVoucherId in @Ids";
            return Query(sql, new { StudentId = studentId, Ids = voucherIds }).ToList();
        }

        /// <summary>
        /// 学生已领取的注册券
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public List<Yw_StudentCashVoucher> GetHaveRegisterVoucherId(int studentId)
        {
            string sql = "select *from Yw_StudentCashVoucher where Ysv_StudentId=@StudentId and Ysv_VoucherType=4";
            return Query(sql, new { StudentId = studentId }).ToList();
        }
    }
}
