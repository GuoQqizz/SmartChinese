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

    public partial class sum_GrammarSummarize
    {
        [Key]
        public int sgs_StudentID { get; set; }
        public int sgs_CategoryID1 { get; set; }
        public int sgs_CategoryID2 { get; set; }
        public int sgs_CategoryID3 { get; set; }
        public Nullable<int> sgs_FillScore { get; set; }
        public string sgs_FillCategoryName { get; set; }
        public Nullable<System.DateTime> sgs_CreateDate { get; set; }
        public Nullable<int> sgs_MaxScore { get; set; }
        public Nullable<int> sgs_LastScore { get; set; }
        public Nullable<System.DateTime> sgs_LastDate { get; set; }
        public Nullable<int> sgs_RightCount { get; set; }
        public Nullable<int> sgs_ErrorCount { get; set; }
        public Nullable<int> sgs_LearningSteps { get; set; }
        public Nullable<System.DateTime> sgs_LearningDate { get; set; }
        public Nullable<int> sgs_ZNFYCount { get; set; }
        public Nullable<int> sgs_ZNTXCount { get; set; }
        public Nullable<int> sgs_XHCSCount { get; set; }
        public Nullable<int> sgs_KYPCCount { get; set; }
        public Nullable<int> sgs_ZNMXCount { get; set; }
    }
}
