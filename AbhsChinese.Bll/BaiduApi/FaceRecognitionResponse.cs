using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Bll.BaiduApi
{
    public class FaceRecognitionResponse
    {
        public int error_code { get; set; }

        public string error_msg { get; set; }

        public UInt64 log_id { get; set; }

        public int timestamp { get; set; }

        public int cached { get; set; }

        public result result { get; set; }
    }

    public class result
    {
        public string face_token { get; set; }

        public location location { get; set; }

        public List<user_list> user_list { get; set; }
    }

    /// <summary>
    /// 注册与修改返回信息
    /// </summary>
    public class location
    {
        public double left { get; set; }

        public double top { get; set; }

        public double width { get; set; }

        public double height { get; set; }

        public double rotation { get; set; }
    }

    /// <summary>
    /// 登录返回信息
    /// </summary>
    public class user_list
    {
        public string group_id { get; set; }

        public string user_id { get; set; }

        public string user_info { get; set; }

        public float score { get; set; }
    }
}