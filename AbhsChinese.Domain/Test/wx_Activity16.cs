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

    public partial class wx_Activity16
    {
        [Key]
        public int wa_Id { get; set; }
        public string wa_Name { get; set; }
        public string wa_Content { get; set; }
        public Nullable<System.DateTime> wa_BeginDate { get; set; }
        public Nullable<System.DateTime> wa_EndDate { get; set; }
        public Nullable<int> wa_Status { get; set; }
        public Nullable<int> wa_MaxCount { get; set; }
        public Nullable<int> wa_TotalCount { get; set; }
        public Nullable<int> wa_SystemId { get; set; }
        public Nullable<int> wa_GradeIds { get; set; }
    }
}
