using AbhsChinese.Web.Models.StudentInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.Login
{
    public class FaceBindInputModel: SmsInputModel
    {
        public string Image { get; set; }
        public string ImageType { get; set; }
    }
}