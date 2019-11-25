using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace AbhsChinese.Domain.JsonTranslator
{
    public static class UeditorContentFactory
    {
        /// <summary>
        /// 字符 -十六进制
        /// </summary>
        public const string HEX_REG = @"[0-9a-fA-F]";

        /// <summary>
        /// GUID -正则表达式 -单行匹配
        /// </summary>
        public const string GUID_REG = "(" + HEX_REG + "{8}(-" + HEX_REG + "{4}){3}-" + HEX_REG + "{12})";
        private const string blank = "{:}";
        //private const string blankPattern = "<input type=\"button\" style=\"border:none;color:#fff;background-color:#1ab394;vertical-align:middle;cursor:pointer;outline:none;\" data-num=\"" + GUID_REG + "\" onclick=\"parent[.]showDialog[(]this[)]\" value=\"答案\"/>";
        private const string blankPattern = "<input.{0,}?>";
        private static readonly string ossUrl = ConfigurationManager.AppSettings["OssHostUrl"];
        public static string Blank(string name)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(name))
            {
                result = new Regex(blankPattern).Replace(name, blank);
            }
            return result;
        }

        public static string FetchUrl(string content, UeditorType type)
        {
            string result = content;
            if (type == UeditorType.Image && !string.IsNullOrWhiteSpace(content))
            {
                string reg = "https?:\\/\\/[^\"]+\\.(jpg|jpeg|png|gif)";
                Regex r = new Regex(reg);
                result = r.Match(content).Value;

                result = result.Replace(ossUrl, string.Empty);
            }
            return result;
        }

        public static string InitBlank(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return content;
            }

            string result = new Regex(blankPattern).Replace(content, "__");
            return result;
        }

        public static string RestoreBlank(string content)
        {
            string result = content;
            if (!string.IsNullOrWhiteSpace(content))
            {
                Regex reg = new Regex(blank);
                var metches = reg.Matches(content);
                foreach (Match metch in metches)
                {
                    string index = Guid.NewGuid().ToString();
                    string btn = $"<input type=\"button\" style=\"border:none;color:#fff;background-color:#1ab394;vertical-align:middle;cursor:pointer;outline:none;\" data-num=\"{index}\" onclick=\"parent.showDialog(this)\" value=\"答案\"/>";
                    result = reg.Replace(result, btn, 1);
                }
            }
            return result;
        }

        public static string FormatReadonlyStem(string content)
        {
            string result = content;
            if (!string.IsNullOrWhiteSpace(content))
            {
                result = content.Replace("{:}", "__");
            }
            return result;
        }

        public static string RestoreContent(string content, UeditorType type)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return string.Empty;
            }
            string result = content;
            if (type == UeditorType.Image)
            {
                string url = ossUrl + content;
                result = $"<p><img class=\"img-fluid\" src=\"{url}\" /></p>";
            }
            return result;
        }
        public static string RestoreUrl(string content, UeditorType type)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return string.Empty;
            }
            string result = content;
            if (type == UeditorType.Image)
            {
                result = ossUrl + content;
            }
            return result;
        }
    }
}