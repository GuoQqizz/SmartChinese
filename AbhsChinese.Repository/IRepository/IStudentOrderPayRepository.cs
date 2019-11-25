using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IStudentOrderPayRepository : IRepositoryBase<Yw_StudentOrderPay>
    {
        void OrderPay(int orderId, int studentId, int[] voucherIds, CashPayTypeEnum payType, int originalBatchId, out decimal amount, out string orderNo);

        int PreOrderPay(int orderId, int studentId, int[] voucherIds, CashPayTypeEnum payType);

        int OrderPayCallback(string orderNo, string transactionId, CashPayTypeEnum payType, decimal amount);

        void OrderPayOver(string orderNo);

        List<DtoStudentOrderPay> GetStudentOrderPay(int orderId, int batchId);
    }
}
