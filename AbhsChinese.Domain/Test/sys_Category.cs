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

    public partial class sys_Category
    {
        [Key]
        public int sc_ID { get; set; }
        public Nullable<int> sc_PID { get; set; }
        public string sc_Name { get; set; }
        public Nullable<int> sc_Index { get; set; }
        public Nullable<int> sc_Level { get; set; }
        public string sc_Note { get; set; }
        public Nullable<int> sc_WordCount { get; set; }
        public string sc_WordFile { get; set; }
        public Nullable<int> sc_GrammarCount { get; set; }
        public string sc_GrammarFile { get; set; }
        public Nullable<int> sc_ReciteTextCount { get; set; }
        public Nullable<int> sc_ReciteTextGrammarCount { get; set; }
        public Nullable<int> sc_Grade { get; set; }
        public Nullable<int> sc_Flag { get; set; }
        public Nullable<int> sc_OldID { get; set; }
        public string sc_OldLevel { get; set; }
        public string sc_StructureVideo { get; set; }
        public Nullable<int> sc_StructureTestID { get; set; }
        public string sc_YinBiaoVideo { get; set; }
        public Nullable<int> sc_YinBiaoTestID { get; set; }
        public Nullable<int> sc_AssessmentTestID { get; set; }
        public Nullable<int> sc_SpeakingCount { get; set; }
        public Nullable<int> sc_SpeakingGrammarCount { get; set; }
        public Nullable<bool> sc_IsTestCheck { get; set; }
        public Nullable<int> sc_TestWordCount { get; set; }
        public string sc_RightsSchoolIDs { get; set; }
        public Nullable<bool> sc_IsRights { get; set; }
    }
}
