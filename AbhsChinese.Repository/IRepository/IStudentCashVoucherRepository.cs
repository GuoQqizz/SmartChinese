using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Dto.Response.Student;
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
    public interface IStudentCashVoucherRepository : IRepositoryBase<Yw_StudentCashVoucher>
    {
        List<DtoStudentCashVoucher> GetPagingStudentCashVoucher(PagingObject paging, int cashVoucherId, int status, string usedReferNo);

        List<DtoVoucherForUserCourse> GetBestVoucherForUserCourse(List<DtoSimpleCourse> simpleCourses, int userId, int schoolId);

        List<DtoOwnCashVoucher> GetPagingOwnCashVoucher(PagingObject paging, int studentId,int status);

        int Add(Yw_StudentCashVoucher entity);

        Yw_StudentCashVoucher Get(int id);
        
        string GetVoucherNo(int voucherId, int userId, int voucherType);
        
        int UpdateUsedStatus(int id);

        int UpdateExpiredStatusByStudentId(int studentId);

        bool StudentIsHaveCashVoucher(int studentId, int cashVoucherId);

        List<Yw_StudentCashVoucher> GetStudentCashVoucherByStudentIdAndVoucherId(int studentId, List<int> voucherIds);

        List<Yw_StudentCashVoucher> GetHaveRegisterVoucherId(int studentId);
    }
}
