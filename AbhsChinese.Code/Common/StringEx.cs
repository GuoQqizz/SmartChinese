using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Xml;
using System.Text.RegularExpressions;
using AbhsChinese.Code.Common;
using System.Configuration;

public static class StringEx
{
    /// <summary>
    /// Convert a XML string to JSON string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string _XmlToJson(this string xml)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (Regex.Match(xml, @"^<\?xml[^>]+version=""[^""]+?""[^?]*?\?>").Success)
            {
                xmlDoc.Load(xml);
            }
            else
            {
                xml = @"<?xml version=""1.0""?>" + xml;
            }
            xmlDoc.LoadXml(xml);
            if (xmlDoc.DocumentElement.Name == "JsonWrapper")
            {
                string json = JsonConvert.SerializeXmlNode(xmlDoc.DocumentElement, Newtonsoft.Json.Formatting.Indented, true);
                return Regex.Replace(json, @"^{\s*""RootObject"":\s*(?<json>[\s\S]*?)}$", "${json}");
            }
            return JsonConvert.SerializeXmlNode(xmlDoc.DocumentElement, Newtonsoft.Json.Formatting.Indented);
        }
        catch
        { }

        return "";
    }

    public static string _JsonToXml(this string json)
    {
        try
        {
            XmlDocument xml = JsonConvert.DeserializeXmlNode(json);
            return xml.DocumentElement.OuterXml;
        }
        catch (JsonSerializationException)
        {
            try
            {
                XmlDocument xml = JsonConvert.DeserializeXmlNode(json, "JsonWrapper");
                return xml.DocumentElement.OuterXml;
            }
            catch (JsonSerializationException)
            {
                try
                {
                    json = string.Format(@"{{""RootObject"": {0}}}", json);
                    XmlDocument xml = JsonConvert.DeserializeXmlNode(json, "JsonWrapper");
                    return xml.DocumentElement.OuterXml;
                }
                catch
                {
                }
            }
            catch
            {
            }
        }
        catch
        {
        }

        return "";
    }

    /// <summary>
    /// 格式化字符串...
    /// </summary>
    /// <param name="text"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string _FormatWith(this string text, params object[] args)
    {
        return string.Format(text, args);
    }

    /// <summary>
    /// 比较字符串，忽略大小写...
    /// </summary>
    public static bool _EqualsByOrdinalIgnoreCase(this string textA, string textB)
    {
        return string.Equals(textA, textB, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 判读字符串是否为空...
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool _IsNullOrEmpty(this string text)
    {
        return string.IsNullOrEmpty(text);
    }

    //获取字符串的字节长度...
    public static int _ByteLength(this string text)
    {
        return Tools.GetByteLength(text);
    }

    public static string _RegexReplace(this string text, string pattern, string replacement)
    {
        return Regex.Replace(text, pattern, replacement);
    }

    /// <summary>
    /// 判断字符串是否可以转化为数字
    /// </summary>
    /// <param name="str">要检查的字符串</param>
    /// <returns>true:可以转换为数字；false：不是数字</returns>
    public static bool IsNumberic(this string str)
    {
        double vsNum;
        bool isNum;
        isNum = double.TryParse(str, System.Globalization.NumberStyles.Float,
            System.Globalization.NumberFormatInfo.InvariantInfo, out vsNum);
        return isNum;
    }

    public static Dictionary<string, int> ToOrderDic(this List<string> strs)
    {
        Dictionary<string, int> dic = new Dictionary<string, int>();

        for (int index = 0; index < strs.Count(); index++)
        {
            dic.Add(strs[index], index + 1);
        }

        return dic;
    }

    public static string ToOssPath(this string url)
    {
        if (url.HasValue() && !url.StartsWith("http"))
        {
            url = ConfigurationManager.AppSettings["OssHostUrl"] + url;
        }
        return url;
    }
}
