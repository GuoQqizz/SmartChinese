//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Business
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class wx_UserContactDetail
    {
        [Key]
        public int wux_ID { get; set; }
        public string wux_OpenId { get; set; }
        public Nullable<int> wux_SystemId { get; set; }
        public string wux_Name { get; set; }
        public string wux_Telephone { get; set; }
        public System.DateTime wux_CreateDate { get; set; }
        public Nullable<int> wux_Status { get; set; }
        public string wux_Remark { get; set; }
        public Nullable<System.DateTime> wux_ReturnVisitDate { get; set; }
        public string wux_Operator { get; set; }
        public Nullable<int> wux_SchoolID { get; set; }
        public Nullable<int> wux_LinkSystemID { get; set; }
    }
}
