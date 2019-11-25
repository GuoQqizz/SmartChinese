using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface ICashVoucherRepository:IRepositoryBase<Yw_CashVoucher>
    {
        List<DtoCashVoucher> GetPagingCashVoucher(PagingObject paging, int id, string name, int status, int voucherType);
        List<DtoCashVoucher> GetPagingCashVoucherForSchool(PagingObject paging, int id, string name, int status, int schoolId);

        int Add(Yw_CashVoucher entity);

        DtoCashVoucher GetVoucherById(int id);

        bool Update(Yw_CashVoucher entity);

        Yw_CashVoucher Get(int id);

        DtoCashVoucher GetCashVoucherDetail(int cahsVoucherId);

        List<Yw_CashVoucher> GetAvailableRegisterVoucherId(int studentId, int schoolId);
        
        List<DtoOwnCashVoucher> GetCashVoucherByOrderId(int studentId, int orderId);

        List<DtoOwnCashVoucher> GetAvailableRegisterVoucherByIds(List<int> ids);
        
        List<DtoOwnCashVoucher> GetAvailableCashVoucherByIds(List<int> ids);

        int UpdateCashVoucherTakenCount(int cashVoucherId, int schoolId);
        
        List<DtoSimpleCachVoucher> GetCashVoucherId(DtoSimpleCourse simpleCourse, int userId, int schoolId);
    }
}
