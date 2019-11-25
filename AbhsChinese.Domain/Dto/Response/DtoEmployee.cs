using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Code.Extension;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoEmployee //: Bas_Employee
    {


        public int Bem_Id { get; set; }

        public string Bem_Account { get; set; }

        [ScriptIgnore]
        [JsonIgnore]
        public string Bem_Password { get; set; }
        public string Bem_Name { get; set; }
        public string Bem_Sex { get; set; }
        public string Bem_Phone { get; set; }
        public string Bem_Email { get; set; }
        public string Bem_LastLoginIp { get; set; }
        public int Bem_Status { get; set; }

        public int Bem_Grades { get; set; }

        public int Bro_Id { get; set; }
        public string Bro_Name { get; set; }
        public DateTime Bem_LastLoginTime { get; set; }
        public string Formate_Bem_LastLoginTime => Bem_LastLoginTime == DateTimeExtensions.DefaultDateTime ? "" : Bem_LastLoginTime.ToString("yyyy-MM-dd hh:mm:ss");


    }
}
