using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.Login
{
    public class FaceRegisterRequest
    {
        public string ImgPath { get; set; }

        public string ImgType { get; set; }

        public string GroupId { get; set; }

        public string UserId { get; set; }

        public string UserInfo { get; set; }
    }
}