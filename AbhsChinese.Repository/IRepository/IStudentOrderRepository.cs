using AbhsChinese.Code.Common;
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
    public interface IStudentOrderRepository : IRepositoryBase<Yw_StudentOrder>
    {
        void Add(Yw_StudentOrder order);

        Yw_StudentOrder Get(int id);

        Yw_StudentOrder Get(string orderNo);

        string GenerateOrderNo(int courseId, int userId, OrderTypeEnum orderType);

        List<DtoStudentOrder> GetStudentOrderByStudentId(PagingObject paging, int studentId);

        void CancelOrder(int orderId);

        List<Yw_StudentOrder> GetOverdueOrder();
        Yw_StudentOrder GetFinishOrder(int studentId, int courseId);
    }
}
