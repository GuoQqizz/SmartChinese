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

    public partial class sum_StudySummarize
    {
        [Key]
        public int s_ID { get; set; }
        public Nullable<int> ms_WStudyCount { get; set; }
        public Nullable<int> ms_WLearnCount { get; set; }
        public Nullable<int> ms_WNewCount { get; set; }
        public Nullable<int> ms_GStudyCount { get; set; }
        public Nullable<int> ms_GLearnCount { get; set; }
        public Nullable<int> ms_GNewCount { get; set; }
        public Nullable<int> ms_ReciteTextCount { get; set; }
        public Nullable<int> ms_PKCount { get; set; }
        public Nullable<int> ms_PKWinCount { get; set; }
        public Nullable<int> ms_PKFailCount { get; set; }
        public Nullable<int> ms_TestCount { get; set; }
        public Nullable<int> ms_ReadCount { get; set; }
        public Nullable<int> ms_ReadWordCount { get; set; }
        public Nullable<int> ms_StructureCount { get; set; }
        public Nullable<int> ms_StructureLearnCount { get; set; }
        public Nullable<int> ms_YinBiaoCount { get; set; }
        public Nullable<int> ms_YinBiaoLearnCount { get; set; }
        public Nullable<int> ms_SpeakingCount { get; set; }
    }
}
