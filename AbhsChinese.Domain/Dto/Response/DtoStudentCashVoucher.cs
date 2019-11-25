using AbhsChinese.Code.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoStudentCashVoucher
    {
        public int Ysv_Id { get; set; }

        public int Ysv_StudentId { get; set; }

        public int Ysv_CashVoucherId { get; set; }

        public string Ysv_VoucherNo { get; set; }

        public int Ysv_VoucherType { get; set; }

        public DateTime Ysv_ExpireDate { get; set; }

        public int Ysv_GotType { get; set; }

        public int Ysv_GotReferId { get; set; }

        public int Ysv_UsedType { get; set; }

        public int Ysv_UsedReferId { get; set; }

        public string Ysv_UsedReferNo { get; set; }

        public int Ysv_Status { get; set; }

        public DateTime Ysv_TakenTime { get; set; }

        public DateTime Ysv_UsedTime { get; set; }

        public DateTime Ysv_UpdateTime { get; set; }


        //学生表
        public int Bst_Id { get; set; }
        public string Bst_Phone { get; set; }


        public string ExpireDate => Ysv_ExpireDate.IsDefaultTime() ? "" : Ysv_ExpireDate.ToString("yyyy-MM-dd HH:mm:ss");
        public string TakenTime => Ysv_TakenTime.IsDefaultTime() ? "" : Ysv_TakenTime.ToString("yyyy-MM-dd HH:mm:ss");
        public string UsedTime => Ysv_UsedTime.IsDefaultTime() ? "" : Ysv_UsedTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
