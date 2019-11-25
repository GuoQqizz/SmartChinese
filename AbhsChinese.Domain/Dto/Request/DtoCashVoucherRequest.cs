using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.Dto.Request
{
    public class DtoCashVoucherRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int VoucherType { get; set; }

        public int PublishCount { get; set; }

        public decimal Amount { get; set; }

        public int LimitByPerson { get; set; }

        public int OrderAmountLimit { get; set; }

        public int ExpireType { get; set; }
        public DateTime ExpireDate { get; set; }
        public int ExpireDay { get; set; }

        public int SchoolType { get; set; }
        public int SchoolId { get; set; }

        public int ApplyScopeType { get; set; }
        public int Grade { get; set; }
        public int CourseType { get; set; }
        public int CourseId { get; set; }

        public string Remark { get; set; }

        //成单赠券
        public int RelatedCourseId { get; set; }

        /// <summary>
        /// 可叠加赠券（暂无）
        /// </summary>
        public int UseWithVoucherType { get; set; }


        public DateTime CreateTime { get; set; }
        public int Creator { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Editor { get; set; }
        public DtoCashVoucherRequest()
        {

        }
        public DtoCashVoucherRequest(DtoCashVoucher dbModel)
        {
            if (dbModel != null)
            {
                this.Name = dbModel.Ycv_Name;
                this.VoucherType = dbModel.Ycv_VoucherType;
                this.SchoolId = dbModel.Ycv_SchoolId;
                this.PublishCount = dbModel.Ycv_PublishCount;
                this.Amount = dbModel.Ycv_Amount;
                this.LimitByPerson = dbModel.Ycv_LimitByPerson;
                this.OrderAmountLimit = dbModel.Ycv_OrderAmountLimit._ToInt32();
                this.ExpireType = dbModel.Ycv_ExpireType;
                this.ExpireDate = dbModel.Ycv_ExpireDate;
                this.ExpireDay = dbModel.Ycv_ExpireDayCount;
                this.ApplyScopeType = dbModel.Ycv_ApplyScopeType;
                this.Grade = dbModel.Ycv_ApplyGrade;
                this.CourseType = dbModel.Ycv_CourseType;
                this.CourseId = dbModel.Ycv_CourseId;
                this.RelatedCourseId = dbModel.Ycv_RelatedCourseId;
                this.UseWithVoucherType = dbModel.Ycv_UseWithVoucherType;
                this.Remark = dbModel.Ycv_Remark;
                this.CreateTime = dbModel.Ycv_CreateTime;
                this.Creator = dbModel.Ycv_Creator;
                this.UpdateTime = dbModel.Ycv_UpdateTime;
                this.Editor = dbModel.Ycv_Editor;
            }

        }
    }
}