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

    public partial class sys_ItemGroup
    {
        [Key]
        public int sig_ID { get; set; }
        public int sp_ID { get; set; }
        public string sig_Content { get; set; }
        public string sig_Note { get; set; }
        public string sig_SoundPath { get; set; }
        public Nullable<int> sig_Index { get; set; }
        public Nullable<int> sig_ItemCount { get; set; }
        public Nullable<decimal> sig_Score { get; set; }
        public Nullable<int> sig_Num { get; set; }
    }
}
