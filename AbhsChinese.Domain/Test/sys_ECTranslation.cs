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

    public partial class sys_ECTranslation
    {
        [Key]
        public int sec_id { get; set; }
        public int sp_ID { get; set; }
        public string sec_Content { get; set; }
        public string sec_Result { get; set; }
        public int sec_Index { get; set; }
        public Nullable<decimal> sec_Score { get; set; }
        public Nullable<int> sec_Num { get; set; }
        public string sec_SentencePattern { get; set; }
        public string sec_Tense { get; set; }
        public string sec_Phrase { get; set; }
        public string sec_Fill { get; set; }
    }
}
