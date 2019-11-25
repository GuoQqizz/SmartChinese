using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Linq;

public static class clsCommon
{
    /// <summary>
    /// 获取客户端IP地址
    /// </summary>
    /// <returns>若失败则返回回送地址</returns>
    public static string GetIP()
    {
        try
        {
            string userHostAddress = string.Empty;

            //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();


            //否则直接读取REMOTE_ADDR获取客户端IP地址
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            //前两者均失败，则利用Request.UserHostAddress属性获取IP地址，但此时无法确定该IP是客户端IP还是代理IP
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }

            return "";
        }
        catch
        {
            return "";
        }
    }


    public static string CityName_ByBaidu(string IP)
    {
        try
        {
            string url = "http://opendata.baidu.com/api.php?query=" + IP + "&co=&resource_id=6006&t=1321340433171&ie=utf8&oe=gbk&cb=bd__cbs__ssms4p&format=json&tn=baidu";
            System.Net.WebRequest wreq = WebRequest.Create(url);
            wreq.Method = "get";
            HttpWebResponse wresp = (HttpWebResponse)wreq.GetResponse();
            Stream s = wresp.GetResponseStream();
            StreamReader objReader = new StreamReader(s, Encoding.GetEncoding("gb2312"));
            string strCityName = objReader.ReadToEnd();
            Match m = Regex.Match(strCityName, "(?<=\"location\":\").*?(?=\",)");
            string d = m.Value.ToString();

            objReader.Dispose();
            wresp.Dispose();
            s.Dispose();
            objReader.Dispose();

            return d;

        }
        catch
        {
            return "";
        }
    }

    /// <summary>
    /// 检查IP地址格式
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static bool IsIP(string ip)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
    }

    public static decimal TryParseInt(this decimal value)
    {
        if ((int)value == value)
        {
            return (int)value;
        }
        else
        {
            return value;
        }
    }

    public static string CheckAgent()
    {
        string agent = HttpContext.Current.Request.UserAgent;
        return agent;
    }
}


