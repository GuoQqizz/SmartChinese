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

    public partial class sum_SpeakingDetail
    {
        [Key]
        public int ssd_StudentID { get; set; }
        public int ssd_CategoryID1 { get; set; }
        public int ssd_CategoryID2 { get; set; }
        public int ssd_CategoryID3 { get; set; }
        public System.DateTime ssd_CreateDate { get; set; }
        public string ssd_FillCategoryName { get; set; }
        public Nullable<int> ssd_FillScore { get; set; }
        public Nullable<int> ssd_Score { get; set; }
        public string ssd_SoundUrl { get; set; }
        public Nullable<int> ssd_Fluency { get; set; }
        public Nullable<int> ssd_Integrity { get; set; }
        public Nullable<int> ssd_Pron { get; set; }
    }
}
