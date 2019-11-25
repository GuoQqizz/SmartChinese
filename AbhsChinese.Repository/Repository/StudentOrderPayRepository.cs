using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using AbhsChinese.Domain.AbhsResource;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Dto.Response;

namespace AbhsChinese.Repository.Repository
{
    public class StudentOrderPayRepository : RepositoryBase<Yw_StudentOrderPay>, IStudentOrderPayRepository
    {
        public StudentOrderPayRepository() : base(AppSetting.ConnString)
        {
        }

        public void OrderPay(int orderId, int studentId, int[] voucherIds, CashPayTypeEnum payType, int originalBatchId, out decimal amount, out string orderNo)
        {
            string voucherStr = "";
            if (voucherIds != null && voucherIds.Length > 0)
            {
                voucherStr = String.Join(",", voucherIds);
            }

            var parems = new DynamicParameters();
            parems.Add("@OrderId", orderId);
            parems.Add("@StudentId", studentId);
            parems.Add("@VoucherIds", voucherStr);
            parems.Add("@CashPayType", (int)payType);
            parems.Add("@OriginalBatchId", originalBatchId);
            parems.Add("@Amount", 0, DbType.Decimal, ParameterDirection.Output);
            parems.Add("@OrderNo", "", DbType.String, ParameterDirection.Output);

            int result = ExecuteProcScalar<int>("P_orderpay", parems);

            decimal? amountValue = parems.Get<decimal?>("@Amount");
            amount = amountValue._ToDecimal();
            orderNo = parems.Get<string>("@OrderNo");

            if (result < 0)
            {
                throw new AbhsException(ErrorCodeEnum.ProcExecError, AbhsErrorMsg.ConstProcExecError, Convert.ToInt32(result));
            }
        }

        public int PreOrderPay(int orderId, int studentId, int[] voucherIds, CashPayTypeEnum payType)
        {
            string voucherStr = "";
            if (voucherIds != null && voucherIds.Length > 0)
            {
                voucherStr = String.Join(",", voucherIds);
            }

            var parems = new DynamicParameters();
            parems.Add("@OrderId", orderId);
            parems.Add("@StudentId", studentId);
            parems.Add("@VoucherIds", voucherStr);
            parems.Add("@CashPayType", (int)payType);
            parems.Add("@BatchId", 0, DbType.Int32, ParameterDirection.Output);

            int result = ExecuteProcScalar<int>("P_preorderpay", parems);

            if (result < 0)
            {
                throw new AbhsException(ErrorCodeEnum.ProcExecError, AbhsErrorMsg.ConstProcExecError, Convert.ToInt32(result));
            }

            int batchId = parems.Get<int>("@BatchId");

            return batchId;
        }
        /// <summary>
        /// 支付成功回调
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="transactionId">交易流水号（微信支付订单号）</param>
        /// <param name="payType">支付方式</param>
        /// <param name="amount">实付款（元）</param>
        /// <returns>
        /// 返回处理结果
        /// 1 正常流程
        /// -1 重复通知支付结果，订单已经不是付款中的状态
        /// -2 应付款与实付款不一致
        /// </returns>
        public int OrderPayCallback(string orderNo, string transactionId, CashPayTypeEnum payType, decimal amount)
        {
            var parems = new DynamicParameters();
            parems.Add("@OrderNo", orderNo, DbType.String, ParameterDirection.Input);
            parems.Add("@TransactionId", transactionId, DbType.String, ParameterDirection.Input);
            parems.Add("@CashPayType", (int)payType, DbType.String, ParameterDirection.Input);
            parems.Add("@Amount", amount, DbType.Decimal, ParameterDirection.Input);
            int result = ExecuteScalar<int>("exec P_orderpaycallback @OrderNo,@TransactionId,@CashPayType,@Amount", parems);

            //1 正常流程
            //-1 微信重复通知支付结果，订单已经不是付款中的状态
            //-2 应付款与实付款不一致
            if (result < -1)
            {
                throw new AbhsException(ErrorCodeEnum.WechatPayNotifyError, AbhsErrorMsg.ConstWechatPayNotifyError, result);
            }

            return result;
        }

        public void OrderPayOver(string orderNo)
        {
            var parems = new DynamicParameters();
            parems.Add("@OrderNo", orderNo, DbType.String, ParameterDirection.Input);
            int result = ExecuteScalar<int>("exec P_orderpayover @OrderNo", parems);

            if (result < 0)
            {
                throw new AbhsException(ErrorCodeEnum.OrderPayOverError, AbhsErrorMsg.ConstOrderPayOverError, result);
            }
        }
        /// <summary>
        /// 根据订单Id查询交易记录
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <param name="batchId">批次Id</param>
        /// <returns>交易记录</returns>
        public List<DtoStudentOrderPay> GetStudentOrderPay(int orderId, int batchId)
        {
            string sql = @"SELECT  [Yop_StudentId] AS StudentId ,
            [Yop_CourseId] AS CourseId ,
            [Yop_OrderId] AS OrderId ,
            [Yop_BatchId] AS BatchId ,
            [Yop_PayType] AS PayType ,
            [Yop_PayOuterId] AS PayOuterId ,
            [Yop_Amount] AS Amount ,
            [Yop_PayTime] AS PayTime ,
            [Yop_Status] AS [Status]
    FROM    [dbo].[Yw_StudentOrderPay]
    WHERE   [Yop_OrderId] = @OrderId AND [Yop_BatchId] = @BatchId;";
            return Query<DtoStudentOrderPay>(sql, new { OrderId = orderId, BatchId = batchId }).ToList();
        }
    }
}
