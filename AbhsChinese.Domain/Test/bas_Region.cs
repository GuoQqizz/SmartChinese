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

    public partial class bas_Region
    {
        [Key]
        public int reg_ID { get; set; }
        public string reg_Name { get; set; }
        public Nullable<int> reg_ParentID { get; set; }
        public string reg_Level { get; set; }
        public string reg_FullName { get; set; }
        public string reg_Pinyin { get; set; }
        public string reg_Alias { get; set; }
        public string reg_CPY { get; set; }
        public string reg_GBCode { get; set; }
    }
}
