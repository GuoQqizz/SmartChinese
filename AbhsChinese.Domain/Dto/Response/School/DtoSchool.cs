using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response.School
{
    public class DtoSchool
    {
        /// <summary>
        /// 学校id
        /// </summary>
        public int Bsl_Id { get; set; }

        /// <summary>
        /// 学校等级id
        /// </summary>
        public int Bsl_Level { get; set; }

        /// <summary>
        /// 等级名称
        /// </summary>
        public string LevelName { get; set; }

        public decimal Bhl_DividePercent { get; set; }

        public string Bsl_SchoolName { get; set; }

        public string Bsl_MasterName { get; set; }

        public int Bsl_SchoolMasterId { get; set; }

        public string Bsl_MasterPhone { get; set; }

        public string LoginPhone { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }

        public string Bsl_Address { get; set; }
        [JsonIgnore]
        public DateTime Bsl_ExpiredDate { get; set; }
        public string ExpiredDate => Bsl_ExpiredDate.ToString("yyyy-MM-dd");
        [JsonIgnore]
        public DateTime Bsl_ContractDate { get; set; }

        public string ContractDate => Bsl_ContractDate.ToString("yyyy-MM-dd");

        public int Bsl_Status { get; set; }

        public string StatusStr => CustomEnumHelper.Parse(typeof(SchoolStatusEnum), Bsl_Status);

        public bool Bsl_IsValid { get; set; }
    }
}
