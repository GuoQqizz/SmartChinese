using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
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
    public class StudentOrderRepository : RepositoryBase<Yw_StudentOrder>, IStudentOrderRepository
    {
        public StudentOrderRepository() : base(AppSetting.ConnString)
        {

        }

        public void Add(Yw_StudentOrder order)
        {
            InsertEntity(order);
        }

        public Yw_StudentOrder Get(int id)
        {
            return GetEntity(id);
        }

        public Yw_StudentOrder Get(string orderNo)
        {
            string sql = "  select * from Yw_StudentOrder where Yod_OrderNo=@OrderNo";
            return Query(sql, new { OrderNo = orderNo }).FirstOrDefault();
        }
        public Yw_StudentOrder GetFinishOrder(int studentId, int courseId)
        {
            string sql = "  select * from Yw_StudentOrder where Yod_StudentId=@StudentId AND Yod_CourseId=@CourseId AND Yod_Status=3";
            return Query(sql, new { StudentId = studentId, CourseId=courseId }).FirstOrDefault();
        }

        public string GenerateOrderNo(int courseId, int userId, OrderTypeEnum orderType)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@CouserId", courseId, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@UserId", userId, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@OrderType", (int)orderType, DbType.Int32, direction: ParameterDirection.Input);
            parameter.Add("@OrderNo", "", DbType.String, direction: ParameterDirection.Output);
            ExecuteProc("P_CreateOrderNo", parameter);
            return parameter.Get<string>("@OrderNo");
        }

        public List<DtoStudentOrder> GetStudentOrderByStudentId(PagingObject paging,int studentId)
        {
            string sql = "Yw_StudentOrder inner join Yw_Course on Yod_CourseId=Ycs_Id where Yod_StudentId=@StudentId";
            return QueryPaging<DtoStudentOrder>("Yw_StudentOrder.*,Ycs_Name as CourseName", sql, "Yod_OrderTime DESC",paging, new { StudentId = studentId }).ToList();
        }

        public void CancelOrder(int orderId)
        {
            var parems = new DynamicParameters();
            parems.Add("@OrderId", orderId, DbType.Int16, ParameterDirection.Input);
            int result = ExecuteProcScalar<int>("P_cancelorder", parems);
            if (result < 0)
            {
                throw new AbhsException(ErrorCodeEnum.OrderCancelError, AbhsErrorMsg.ConstOrderCancelError, result);
            }
        }

        public List<Yw_StudentOrder> GetOverdueOrder()
        {
            string sql = "select top 500 * from Yw_StudentOrder where yod_status in (1,2) and yod_ordertime<dateadd(minute,-30,getdate())";
            return Query(sql).ToList();
        }
    }
}