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

    public partial class sum_WordDetail
    {
        [Key]
        public int swd_StudentID { get; set; }
        public int swd_CategoryID1 { get; set; }
        public int swd_CategoryID2 { get; set; }
        public int swd_CategoryID3 { get; set; }
        public System.DateTime swd_CreateDate { get; set; }
        public string swd_FillCategoryName { get; set; }
        public Nullable<int> swd_FillScore { get; set; }
        public Nullable<int> swd_Score { get; set; }
        public Nullable<int> swd_RightCount { get; set; }
        public Nullable<int> swd_ErrorCount { get; set; }
        public Nullable<int> swd_AddRightCount { get; set; }
        public Nullable<int> swd_TestTime { get; set; }
    }
}
