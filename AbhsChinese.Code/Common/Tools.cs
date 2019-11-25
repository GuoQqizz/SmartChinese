using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace AbhsChinese.Code.Common
{
    /// <summary>
    /// 公用方法集合
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// 返回日期几
        /// </summary>
        /// <returns></returns>
        public static string DayOfWeek
        {
            get
            {
                switch (DateTime.Now.DayOfWeek.ToString("D"))
                {
                    case "0":
                        return "星期日 ";

                    case "1":
                        return "星期一 ";

                    case "2":
                        return "星期二 ";

                    case "3":
                        return "星期三 ";

                    case "4":
                        return "星期四 ";

                    case "5":
                        return "星期五 ";

                    case "6":
                        return "星期六 ";

                    default:
                        return "";
                }
            }
        }

        //base64编码的文本 转为    图片
        public static void Base64StringToImage(string txtFileName)
        {
            FileStream ifs = new FileStream(txtFileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(ifs);

            String inputStr = sr.ReadToEnd();
            byte[] arr = Convert.FromBase64String(inputStr);
            MemoryStream ms = new MemoryStream(arr);
            Bitmap bmp = new Bitmap(ms);

            bmp.Save(txtFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            ms.Close();
            sr.Close();
            ifs.Close();
            if (File.Exists(txtFileName))
            {
                File.Delete(txtFileName);
            }
        }

        /// <summary>
        /// 经纬度之间的距离(注意：单位是km)
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static double CalcDistance(double fromX, double fromY, double toX, double toY)
        {
            var R = 6371;

            var deltaLatitude = toRadians(toX - fromX);
            var deltaLongitude = toRadians(toY - fromY);
            fromX = toRadians(fromX);
            toX = toRadians(toX);

            var a = Math.Sin(deltaLatitude / 2) *
                    Math.Sin(deltaLatitude / 2) +
                    Math.Cos(fromX) *
                    Math.Cos(toX) *
                    Math.Sin(deltaLongitude / 2) *
                    Math.Sin(deltaLongitude / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d;
        }

        /// <summary>
        /// 判断一个字符串是不是 疑似电话号码
        /// </summary>
        /// <param name="strText"></param>
        public static bool CheckIsTel(string strText)
        {
            string[,] arryReplace = new string[,]
                                         {
                                           { "零", "〇", "０", "O"  },
                                           { "壹", "一", "１", "①" },
                                           { "贰", "二", "２", "②" },
                                           { "叁", "三", "３", "③" },
                                           { "肆", "四", "４", "④" },
                                           { "伍", "五", "５", "⑤" },
                                           { "陆", "六", "６", "⑥" },
                                           { "柒", "七", "７", "⑦" },
                                           { "捌", "八", "８", "⑧" },
                                           { "玖", "九", "９", "⑨" }
                                         };

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    strText = strText.Replace(arryReplace[i, j], i.ToString());
                }
            }

            ///数字中间有3个其他字符的，认为这两个数字是连续的
            Regex regex = new Regex(@"\d{1}[^年月日]{0,3}?\d{1}[^年月日]{0,3}?\d{1}[^年月日]{0,3}?\d{1}[^年月日]{0,3}?\d{1}[^年月日]{0,3}?\d{1}[^年月日]{0,3}?\d{1}");
            Match mc = regex.Match(strText);

            return mc.Length > 0;
        }

        /// <summary>
        /// 过滤不安全字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckString(string str, System.Web.UI.Page page)
        {
            bool result = false;
            string SQL_injdata = @"’|-|and|exec|insert|select|delete|update|count|%|
                                master|truncate|char|declare|<html>|<script>|<a>";
            char[] a = new char[] { '|' };
            string[] strarr = SQL_injdata.Split(a);
            if (str != "")
            {
                str = str.Replace("'", "’").Replace("－", "-");
                foreach (string strvalue in strarr)
                {
                    if (str.ToLower().Contains(strvalue))
                    {
                        page.ClientScript.RegisterStartupScript(page.GetType(), "IsSecurity", "<script>alert('您填写的包括不安全字符:[" + strvalue + "]')</script>");
                        result = true;
                        break;
                    }
                }
            }
            else
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "AddInfo", "<script>alert('填写信息不能为空，请重新填写!')</script>");
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 作者：Conis
        /// 日期：2007-02-27
        /// 修改日期：2009-3-31
        /// 修改人：黄培训
        /// 描述：
        ///     将字节转换为MB,保留两位小数
        /// </summary>
        /// <param name="byteValue"></param>
        /// <returns></returns>
        public static double ConvertByteToMB(double byteValue)
        {
            double douMB = byteValue / 1024 / 1024;
            return Math.Round(douMB, 2);
        }

        /// <summary>
        /// 将MB转换为字节
        /// </summary>
        /// <param name="mbValue"></param>
        /// <returns></returns>
        public static double ConvertMBToByte(int mbValue)
        {
            return mbValue * 1024 * 1024;
        }

        /// <summary>
        /// 将Object对象生成json串...
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CreateJsonString(object obj)
        {
            IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss" };
            string Json = JsonConvert.SerializeObject(obj, dtConverter);
            Json = Json.Replace("\\\"", "\'");
            Json = Regex.Replace(Json, "(?<=:\")(.*?)(?=[(\",)|(\"\\})])", m => { return System.Web.HttpUtility.UrlEncode(m.ToString(), Encoding.UTF8); });
            Json = Json.Replace("+", " ");
            return Json;
        }

        /// <summary>
        /// 生成文件的格式化副本
        /// </summary>
        /// <param name="fPath">文件的完整路径</param>
        public static void CreateReadCopy(string fPath)
        {
            string html;
            Regex regex = new Regex(@"([\s\S]{20,}?[.?!;]{1,3}(?![\s\S]{1,5}?[.?!;])|[\s\S]{1,}[\S]*$)");
            Regex regex1 = new Regex(@"<[^>]+>"); // <>中间的不加span
            using (StreamReader sr = new StreamReader(fPath))
            {
                html = sr.ReadToEnd().Replace("？", "?").Replace("！", "! ").Replace("。", ". ").Replace("，", ", ").Replace("；", "; ").Replace("．", ". ").Replace("“", "\"").Replace("”", "\"").Replace("‘", "\'").Replace("’", "\'");
                html = html.Replace("<strong>", "").Replace("</strong>", "").Replace("<em>", "").Replace("</em>", "").Replace("<u>", "").Replace("</u>", "").Replace("<strike>", "").Replace("</strike>", "");
                html = System.Web.HttpUtility.HtmlDecode(html);
                html = html.Replace("<br />", " ");

                string[] htmlArry = regex1.Split(html);
                MatchCollection mc = regex1.Matches(html);

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < htmlArry.Length; i++)
                {
                    stringBuilder.Append(regex.Replace(htmlArry[i], "<span>${1}</span>"));
                    if (i < htmlArry.Length - 1)
                    {
                        stringBuilder.Append(mc[i].Value);
                    }
                }
                html = stringBuilder.ToString();
            }

            if (html.StartsWith("<span>"))
                html = html.Substring(6);
            if (html.EndsWith("</span>"))
                html = html.Substring(0, html.Length - 7);

            regex = new Regex("</span><span>");
            string[] htmls = regex.Split(html);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < htmls.Length; i++)
            {
                if (htmls[i].Length > 0)
                {
                    if (htmls[i].Replace("\r\n", "").Replace(" ", "").Length == 0) //只有换行
                    {
                        sb.Append(htmls[i]);
                    }
                    else
                    {
                        sb.AppendFormat("<span id='{0}'>{1}", i, System.Web.HttpUtility.HtmlEncode(htmls[i]));
                        if (!sb.ToString().EndsWith("</span>")) sb.Append("</span>");
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(GetReadCopyName(fPath)))
            {
                sw.Write(Tools.HTMLEncode(sb.ToString()));
            }
        }

        public static void CreateReadHtmlCopy(string fPath)
        {
            string html = "";
            using (StreamReader sr = new StreamReader(fPath))
            {
                html = sr.ReadToEnd().Replace("？", "?").Replace("！", "! ").Replace("。", ". ").Replace("，", ", ").Replace("；", "; ").Replace("．", ". ").Replace("“", "\"").Replace("”", "\"").Replace("‘", "\'").Replace("’", "\'");
                html = html.Replace("<strong>", "").Replace("</strong>", "").Replace("<em>", "").Replace("</em>", "").Replace("<u>", "").Replace("</u>", "").Replace("<strike>", "").Replace("</strike>", "");
                html = html.Replace("<br />", " ");
            }

            int x = html.IndexOf("<STYLE>");
            int y = html.IndexOf("</STYLE>", x + 1);
            if (y > x && x >= 0)
            {
                html = html.Substring(y + 8);
            }

            html = Regex.Replace(html, @"alt=[\s\S]*?\s(?=width)", "");

            Regex regex = new Regex("<span", RegexOptions.IgnoreCase);
            string[] htmls = regex.Split(html);

            StringBuilder sb = new StringBuilder();
            if (htmls.Length > 0)
            {
                sb.Append(htmls[0]);
            }
            for (int i = 1; i < htmls.Length; i++)
            {
                if (htmls[i].Length > 0)
                {
                    if (htmls[i].Replace("\r\n", "").Replace(" ", "").Length == 0) //只有换行
                    {
                        sb.Append(htmls[i]);
                    }
                    else
                    {
                        sb.AppendFormat("<span id='{0}'{1}", i, htmls[i]);
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(GetReadCopyName(fPath)))
            {
                sw.Write(sb.ToString());
            }
        }

        /// <summary>
        /// 已重载.计算两个日期的时间间隔,返回的是时间间隔的日期差的绝对值.
        /// </summary>
        /// <param name="DateTime1">第一个日期和时间</param>
        /// <param name="DateTime2">第二个日期和时间</param>
        /// <returns></returns>
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();

            if (ts.Days > 0)
                dateDiff = (ts.Days + 1).ToString() + "天";
            else if (ts.Hours > 0)
                dateDiff = ts.Hours.ToString() + "小时";
            else if (ts.Minutes > 0)
                dateDiff = ts.Minutes.ToString() + "分钟";
            else if (ts.Seconds > 0)
                dateDiff = ts.Seconds.ToString() + "秒";
            return dateDiff;
        }

        /// <summary>
        /// 已重载.计算一个时间与当前本地日期和时间的时间间隔,返回的是时间间隔的日期差的绝对值.
        /// </summary>
        /// <param name="DateTime1">一个日期和时间</param>
        /// <returns></returns>
        public static string DateDiff(DateTime DateTime1)
        {
            return DateDiff(DateTime1, DateTime.Now);
        }

        public static string STD(int HowManySecond)
        {
            string ShowStr = "";
            if (HowManySecond >= (24 * 3600))
            {
                ShowStr += (HowManySecond / 24 * 3600) + "天";
                HowManySecond %= (24 * 3600);
            }
            if (HowManySecond >= 3600)
            {
                ShowStr += (HowManySecond / 3600) + "小时";
                HowManySecond %= 3600;
            }
            if (HowManySecond >= 60)
            {
                ShowStr += (HowManySecond / 60) + "分";
            }
            if (HowManySecond % 60 > 0)
            {
                ShowStr += (HowManySecond % 60) + "秒";
            }
            return ShowStr;
        }

        /// <summary>
        /// 作者：Conis
        /// 日期：2007-02-27
        /// 描述：
        ///     采用Try的方法检测一个对象是否为日期型，如果不是日期型，则返回默认值
        /// </summary>
        /// <param name="obj">要检测的对象</param>
        /// <param name="dateDefault">不是日期型，则返回默认值</param>
        /// <returns></returns>
        public static DateTime DefIsDate(object obj, DateTime dateDefault)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return dateDefault;
            }
        }

        /// <summary>
        /// 检查一个对象是否为浮点型，不是则返回默认值;是,则返回原值
        /// </summary>
        /// <param name="obj">要检查的对象</param>
        /// <param name="deciDefault">返回的默认值</param>
        /// <returns>decimal型的结果</returns>
        public static decimal DefIsDecimal(object obj, decimal deciDefault)
        {
            if (obj == null || obj.ToString() == "")
            {
                return deciDefault;
            }
            string str = obj.ToString();
            bool check = (IsNumeric(obj) || Regex.IsMatch(str, @"\d+\.\d+"));

            return check ? Convert.ToDecimal(obj) : deciDefault;
        }

        /// <summary>
        /// 作者：Conis
        /// 日期：2007-02-27
        /// 描述：
        ///     采用正则来检查一个对象是否为整形数字，如果不是数字，则返回默认值；如果是数字，是转换并返回
        /// </summary>
        /// <param name="str">要检查的对象</param>
        /// <param name="intDefault">不是字符串返回的默认值</param>
        /// <returns>返回的结果</returns>
        public static int DefIsNumeric(object obj, int intDefault)
        {
            return IsNumeric(obj) ? Convert.ToInt32(obj) : intDefault;
        }

        /// <summary>
        /// 删除文件副本
        /// </summary>
        /// <param name="fPath">文件的完整路径</param>
        public static void DeleteReadCopy(string fPath)
        {
            File.Delete(GetReadCopyName(fPath));
        }

        /// <summary>
        /// 将web控件置为不可用状态
        /// </summary>
        /// <param name="control"></param>
        public static void DisabledControls(System.Web.UI.Control control)
        {   //CheckBoxList DropDownList
            if (control != null && control.HasControls())
            {
                foreach (System.Web.UI.Control ctl in control.Controls)
                {
                    switch (ctl.GetType().ToString())
                    {
                        case "System.Web.UI.WebControls.TextBox":
                        case "System.Web.UI.WebControls.DropDownList":
                        case "System.Web.UI.WebControls.CheckBoxList":
                        case "System.Web.UI.WebControls.Button":
                        case "System.Web.UI.WebControls.RadioButtonList":
                            ((WebControl)ctl).Enabled = false;
                            break;
                    }
                    DisabledControls(ctl);
                }
            }
        }

        /// <summary>
        /// 下载图片...
        /// </summary>
        /// <param name="url"></param>
        /// <param name="SavePath"></param>
        public static void DownloadImages(string url, string SavePath)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream reader = response.GetResponseStream();
            FileStream writer = new FileStream(SavePath, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] buff = new byte[512];
            int c = 0; //实际读取的字节数
            while ((c = reader.Read(buff, 0, buff.Length)) > 0)
            {
                writer.Write(buff, 0, c);
            }
            writer.Close();
            writer.Dispose();
            reader.Close();
            reader.Dispose();
            response.Close();
        }

        /// <summary>
        /// 以DataGrid的方式将数据导出到Excel
        /// </summary>
        /// <param name="fileName">导出文件名</param>
        /// <param name="dt">要导出的数据DataTable</param>
        public static void ExportToExcel(Page page, string fileName, DataTable dt)
        {
            GridView gv = new GridView();
            gv.DataSource = dt;
            gv.DataBind();
            //数据过长，防止转换
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                for (int j = 0; j < gv.Rows[i].Cells.Count; j++)
                {
                    gv.Rows[i].Cells[j].Attributes.Add("style", "vnd.ms-excel.numberformat: @");
                }
            }

            page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);

            page.Response.Charset = "GB2312";
            page.Response.ContentType = "application/ms-excel";

            //解决出现乱码关键问题
            page.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=GB2312>");

            gv.Visible = true;
            gv.HeaderStyle.Reset();
            gv.AlternatingRowStyle.Reset();

            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(tw);
            gv.RenderControl(hw);

            //5.输出DataGrid内容
            page.Response.Write(tw.ToString());
            page.Response.End();
        }

        /// <summary>
        /// 将一个小数格式化为间隔0.5的数字返回
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        public static double FormatDouble(double intput)
        {
            double intNum = Math.Round(intput, 0);
            double decNum = Math.Round(intput, 2);
            double decDecimal = decNum - intNum;
            double result = 0.5;                       //最终的小数
            if (decDecimal <= 0.5)
            {
                result = 0;
            }
            result += intNum;
            if (result > 5)
            {
                result = 5;
            }
            return result;
        }

        /// <summary>
        /// 将数字转换为小数点后面带指定位数字的小数
        /// </summary>
        /// <param name="numer">需要转换的原数字</param>
        /// <param name="digit">小数点后面的位数</param>
        /// <returns></returns>
        public static object FormatNumeric(object numer, int digit)
        {
            string tmpStr = numer.ToString();

            // 小数点所在的位置
            int tmp = tmpStr.IndexOf('.');
            // 需要补齐的位数
            string tmpDigit = string.Empty;
            StringBuilder builder = new StringBuilder();

            // 如果传入参数为整数则加指定个0
            if (tmp < 0)
            {
                for (int i = 1; i <= digit; i++)
                {
                    builder.Append("0");
                }
                // 需要补齐的位数
                tmpDigit = builder.ToString();
                return tmpStr + "." + tmpDigit;
            }
            //如果原始数据的精度比要求的高则截取
            else if ((tmpStr.Length - tmp - 1) > digit)
            {
                return tmpStr.Substring(0, tmp + digit + 1);
            }
            //如果原始数据的精度比要求的底则补0
            else
            {
                int tmpZ = digit - (tmpStr.Length - tmp - 1);

                for (int i = 1; i <= tmpZ; i++)
                {
                    builder.Append("0");
                }
                return tmpStr + tmpDigit;
            }
        }

        /// <summary>
        /// 作者：Conis
        /// 日期：2007-02-27
        /// 描述：
        ///     根据一组字符型数组，生成指定长度的随机字符串
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GenRandom(int len, string[] basicString)
        {
            int iCount = basicString.Length;
            StringBuilder builder = new StringBuilder();
            for (int iLoop = 0; iLoop < len; iLoop++)
            {
                Random rand = new Random(iLoop + (int)DateTime.Now.Ticks);
                builder.Append(basicString[rand.Next(iCount)]);
            }
            string result = builder.ToString();
            return result;
        }

        /// <summary>
        /// 作者：Conis
        /// 日期：2007-02-27
        /// 描述：
        ///     根据一组字符型数组，生成指定长度的随机字符串
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GenRandom(int len, string[] basicString, int randId)
        {
            int iCount = basicString.Length;
            StringBuilder builder = new StringBuilder();
            for (int iLoop = 0; iLoop < len; iLoop++)
            {
                Random rand = new Random(iLoop + (int)DateTime.Now.Ticks + randId);
                builder.Append(basicString[rand.Next(iCount)]);
            }

            string result = builder.ToString();

            string a = Encrypt.MD5(result + (randId + len)).ToUpper();
            a = a.Replace("0", "");
            a = a.Replace("O", "");

            return a.Substring(0, len);
        }

        /// <summary>
        /// 获取字符串长度，一个汉字算两个字节
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetByteLength(string str)
        {
            if (str.Length == 0) return 0;
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0; byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
            }
            return tempLen;
        }

        /// <summary>
        /// 返回特定信息的对话框js
        /// </summary>
        /// <param name="Msn">提示信息</param>
        public static string GetConfirm(string Msn)
        {
            return "javascript:return confirm('" + Msn + "')";
        }

        public static T GetEnum<T>(string name)
        {
            Array vals = Enum.GetValues(typeof(T));
            foreach (object val in vals)
            {
                if (val.ToString() == name)
                    return (T)val;
            }

            return (T)vals.GetValue(0);
        }

        /// <summary>
        /// 获取枚举名称，没有返回空字符串...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string GetEnumName<T>(int? Value)
        {
            if (Value == null)
                return "";

            Array vals = Enum.GetValues(typeof(T));
            foreach (object val in vals)
            {
                if ((int)val == Value)
                {
                    if (val.ToString().StartsWith("_"))
                        return val.ToString().Substring(1);
                    else
                        return val.ToString();
                }
            }

            return "";
        }

        /// <summary>
        /// 获取枚举名称，没有返回空字符串...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string GetEnumName<T>(int Value)
        {
            Array vals = Enum.GetValues(typeof(T));
            foreach (object val in vals)
            {
                if ((int)val == Value)
                    return val.ToString();
            }

            return "";
        }

        /// <summary>
        /// 获取枚举名称，没有返回空字符串...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// Common.Tools.GetEnumNameCombin<Common.enm薪资组成>(_drQInvite["qi_JobType"]._ToInt32(), "+")
        public static string GetEnumNameCombin<T>(int Value, string strConn)
        {
            StringBuilder builder = new StringBuilder();
            Array vals = Enum.GetValues(typeof(T));
            foreach (object val in vals)
            {
                if (((int)val & Value) == (int)val)
                {
                    builder.Append(strConn);
                    builder.Append(val.ToString());
                }
            }
            string str = builder.ToString();
            return str.Length > 0 ? str.Substring(strConn.Length) : "";
        }

        /// <summary>
        /// 主要功能：截断指定长度的字符串
        /// 参数类型：Object
        /// 输入参数：入口待转字符串形对象
        /// 返回值：String类型
        /// </summary>
        /// <param name="mObjStr">字符串形对象</param>
        /// <param name="mMaxLen">规定截断长度</param>
        /// <returns>已截断字符串</returns>
        static public string GetFormatString(object mObjStr, int mMaxLen)
        {
            //排除Null
            if (mObjStr == null)
            {
                return string.Empty;
            }
            //排除DBNull
            if (mObjStr.Equals(DBNull.Value))
            {
                return string.Empty;
            }

            //若长度大于规定长度，则截取
            if (mObjStr.ToString().Length > mMaxLen)
            {
                return mObjStr.ToString().Substring(0, mMaxLen) + "...";
            }
            //否则返回原始串
            return mObjStr.ToString();
        }

        /// <summary>
        /// 获取以时间为名称的字符串
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyString()
        {
            string TempFileName = DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;

            return TempFileName;
        }

        /// <summary>
        /// 获取文件副本
        /// </summary>
        /// <param name="fPath">文件的完整路径</param>
        /// <returns></returns>
        public static string GetReadCopy(string fPath)
        {
            string html = string.Empty;
            if (File.Exists(fPath))
            {
                if (!File.Exists(GetReadCopyName(fPath))) CreateReadCopy(fPath);
                using (StreamReader sr = new StreamReader(GetReadCopyName(fPath)))
                {
                    html = sr.ReadToEnd();
                }
            }
            else
            {
                html = "指定文件不存在，无法阅读文章";
            }
            return html;
        }

        /// <summary>
        /// 获取html文件内容
        /// </summary>
        /// <param name="fPath">文件的完整路径</param>
        /// <returns></returns>
        public static string GetReadHtml(string fPath)
        {
            string html = string.Empty;
            if (File.Exists(fPath))
            {
                using (StreamReader sr = new StreamReader(fPath))
                {
                    html = sr.ReadToEnd();
                }
            }
            else
            {
                html = "指定文件不存在，无法阅读文章";
            }
            return html;
        }

        /// <summary>
        /// 得到http根目录
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            // 是否为SSL认证站点
            string secure = HttpContext.Current.Request.ServerVariables["HTTPS"];
            string httpProtocol = (secure == "on" ? "https://" : "http://");

            // 服务器名称
            string serverName = HttpContext.Current.Request.ServerVariables["Server_Name"];

            // 应用服务名称
            string applicationName = HttpContext.Current.Request.ApplicationPath;

            string siteURL = httpProtocol + serverName + applicationName;
            return siteURL;
        }

        /// <summary>
        /// 输入秒数，获取时间差...
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string GetTimeBySeconds(int seconds)
        {
            DateTime t = DateTime.Now;
            return DateDiff(t.AddSeconds(seconds), t);
        }

        /// <summary>
        /// 得到本周第一天(以星期一为第一天)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }

        /// <summary>
        /// 得到本周最后一天(以星期天为最后一天)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }

        /// <summary>
        /// 方 法 名：HTMLDecode
        /// 主要功能：去除字符串中的HTML元素
        /// 完成日期：2006-03-21
        /// </summary>
        /// <param name="sText">待格式化的文本</param>
        /// <returns>格式化后的文本</returns>
        public static string HTMLDecode(string sText)
        {
            string s = sText.Replace("&nbsp;", " ").Replace("<br />", "\r\n").Replace("<br/>", "\r\n");
            return s;
        }

        /// <summary>
        /// 方 法 名：HTMLDecode
        /// 主要功能：去除字符串中的HTML元素
        /// 完成日期：2006-03-21
        /// </summary>
        /// <param name="sText">待格式化的文本</param>
        /// <returns>格式化后的文本</returns>
        public static string HTMLDecodeNew(string sText)
        {
            string stroutput = sText;

            stroutput = stroutput.Replace("&quot;", "\"");
            stroutput = stroutput.Replace("&lt;", "<");
            stroutput = stroutput.Replace("&gt;", ">");
            stroutput = stroutput.Replace("&#146;", "\'");
            stroutput = stroutput.Replace("&nbsp;", " ");
            stroutput = stroutput.Replace("<br>", "\r");
            stroutput = stroutput.Replace("&nbsp;&nbsp;&nbsp;&nbsp;", "\t");

            return stroutput.Replace("&nbsp;", " ").Replace("<br />", "\r\n");
        }

        /// <summary>
        /// 方 法 名：HTMLEncode
        /// 主要功能：对HTML字符串进行格式化处理
        /// 完成日期：2006-03-02
        /// </summary>
        /// <param name="sText">待格式化的文本</param>
        /// <returns>格式化后的文本</returns>
        public static string HTMLEncode(string sText)
        {
            return sText.Replace("  ", "&nbsp;&nbsp;").Replace("\r\n", "<br />").Replace("\n\n", "<br />").Replace("\n", "<br />").Replace("\"", "&quot;");
        }

        /// <summary>
        /// 方 法 名：HTMLEncode
        /// 主要功能：对HTML字符串进行格式化处理
        /// 完成日期：2006-03-02
        /// </summary>
        /// <param name="sText">待格式化的文本</param>
        /// <returns>格式化后的文本</returns>
        public static string HTMLEncodeNew(string sText)
        {
            string s = sText.Replace("&", "&amp;");

            s = s.Replace("\"", "&quot;");
            s = s.Replace("<", "&lt;");
            s = s.Replace(">", "&gt;");
            s = s.Replace("\'", "&#146;");
            s = s.Replace("\r", "<br>");
            s = s.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");

            return s.Replace("  ", "&nbsp;&nbsp;").Replace("\r\n", "<br />").Replace("\n\r", "<br />").Replace("\n", "<br />");
        }

        /// <summary>
        /// 初始化文本框
        /// </summary>
        /// <param name="control"></param>
        public static void InitTextBox(System.Web.UI.Control control)
        {
            if (control != null && control.HasControls())
            {
                foreach (System.Web.UI.Control ctl in control.Controls)
                {
                    if (ctl.GetType().ToString() == "System.Web.UI.WebControls.TextBox") ((System.Web.UI.WebControls.TextBox)ctl).Text = string.Empty;
                    if (ctl.HasControls())
                        InitTextBox(ctl);
                }
            }
        }

        public static string Int2Char(string str)
        {
            Regex reg = new Regex(@"&#(\d+);");
            StringBuilder sb = new StringBuilder();
            foreach (Match m in reg.Matches(str))
            {
                sb.Append((char)(Convert.ToInt32(m.Groups[1].Value)));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 作者：Conis
        /// 日期：2007-02-27
        /// 描述：
        ///     用正则检查字符串是否为Int数字
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns></returns>
        public static bool IsNumeric(object obj)
        {
            if (obj == null || obj.ToString() == "") return false;
            try
            {
                Convert.ToInt32(obj.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 显示到客户端的消息对话框...
        /// </summary>
        /// <param name="strMSG">消息</param>
        public static void Message(Page page, string strMSG)
        {
            strMSG = HTMLEncode(strMSG);
            strMSG = strMSG.Replace("'", " | ");
            page.ClientScript.RegisterStartupScript(page.GetType(), "MsgBox", string.Format("<script>alert('{0}');</script>", strMSG));
        }

        /// <summary>
        /// 显示到客户端的消息对话框...
        /// </summary>
        /// <param name="strMSG">消息</param>
        public static void Message(Page page, string strMSG, string url)
        {
            strMSG = HTMLEncode(strMSG);
            strMSG = strMSG.Replace("'", " | ");
            page.ClientScript.RegisterStartupScript(page.GetType(), "MsgBox", string.Format("<script>alert('{0}');window.location='{1}';</script>", strMSG, url));
        }

        public static void Message(Page page, string strMSG, string url, bool isJAlert)
        {
            strMSG = HTMLEncode(strMSG);
            strMSG = strMSG.Replace("'", " | ");
            if (!isJAlert)
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "MsgBox", string.Format("<script>alert('{0}');{1}</script>", strMSG, url.Length > 0 ? string.Format("window.location='{0}';", url) : ""));
            }
            else
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "MsgBox", string.Format("<script>jAlert('{0}','提示'{1});</script>", strMSG, url.Length > 0 ? string.Format(",function(){{window.location='{0}';}}", url) : ""));
            }
        }

        /// <summary>
        /// 显示到客户端的消息对话框...
        /// </summary>
        /// <param name="strMSG">消息</param>
        public static void MessageByLoadOver(Page page, string strMSG)
        {
            strMSG = HTMLEncode(strMSG);
            strMSG = strMSG.Replace("'", " | ");
            page.ClientScript.RegisterStartupScript(page.GetType(), "MsgBox", string.Format("<script>$(function(){{alert('{0}');}});</script>", strMSG));
        }

        /// <summary>
        /// 显示到客户端的消息对话框...
        /// </summary>
        /// <param name="strMSG">消息</param>
        public static void MessageByLoadOver(Page page, string strMSG, string url)
        {
            strMSG = HTMLEncode(strMSG);
            strMSG = strMSG.Replace("'", " | ");
            page.ClientScript.RegisterStartupScript(page.GetType(), "MsgBox", string.Format("<script>$(function(){{alert('{0}');window.location='{1}';}});</script>", strMSG, url));
        }

        /// <summary>
        /// 作者：Conis
        /// 日期：2007-02-27
        /// 描述：
        ///     检测一个对象是否是空值
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="defValue">默认值勤</param>
        /// <returns></returns>
        public static object ObjectIsNull(object input, object defValue)
        {
            return (input == null) ? defValue : input;
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="page"></param>
        public static void PageClose(Page page)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "pageClose", "<script>window.close();</script>");
        }

        /// <summary>
        /// 随机打乱一个以逗号隔开的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RandSortString(string str)
        {
            if (str == null || str.Length < 2)
                return str;

            string[] sArray = str.Split(',');
            if (sArray.Length < 2)
                return str;

            StringBuilder sbRet = new StringBuilder();
            Random rnd = new Random((int)DateTime.Now.Ticks);

            int i = sArray.Length;
            while (i > 0)
            {
                int n = rnd.Next(i);
                if (sbRet.Length > 0)
                    sbRet.Append(",");
                sbRet.Append(sArray[n]);
                sArray[n] = sArray[i - 1];
                i--;
            }

            return sbRet.ToString();
        }

        /// <summary>
        /// 获取XML节点值...
        /// </summary>
        /// <param name="strNode">XML字符串</param>
        /// <param name="strNode">节点名称</param>
        /// <returns>节点值</returns>
        public static string ReadXMLNodeValue(string strXML, string strNode)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(strXML);
            string result = xd.GetElementsByTagName(strNode).Item(0).InnerText.Trim();
            return result;
        }

        /// <summary>
        /// 去掉html中的所有html元素标签
        /// </summary>
        /// <param name="strhtml"></param>
        /// <returns></returns>
        public static string RemoveHtmlMark(string strhtml)
        {
            Regex regex = new Regex(@"<[^>]+>|</[^>]+>");

            return regex.Replace(strhtml, "");
        }

        /// <summary>
        /// 去除字符串中的HTML元素
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string RemoveSpaceHtmlTag(string Input)
        {
            string input = Input;
            //去html标签
            input = Regex.Replace(input, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"-->", "", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"<!--.*", "", RegexOptions.IgnoreCase);

            input = Regex.Replace(input, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            input = input.Replace("<", "");
            input = input.Replace(">", "");
            input = input.Replace("\r\n", "");
            //去两端空格，中间多余空格
            input = Regex.Replace(input.Trim(), "\\s+", " ");
            return input;
        }

        /// <summary>
        /// 实现数据的四舍五入法
        /// </summary>
        /// <param name="value">要进行处理的数据</param>
        /// <param name="decimals">保留的小数位数</param>
        /// <returns>四舍五入后的结果</returns>
        public static double Round(double value, int decimals)
        {
            if (value < 0)
            {
                return Math.Round(value + 5 / Math.Pow(10, decimals + 1), decimals, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
            }
        }

        /// <summary>
        /// 选中Grid中的行...
        /// </summary>
        /// <param name="intIndex">行索引</param>
        public static void SelectRowScript(Page page, int intIndex)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "SelectRow", string.Format("<script type='text/javascript'>$('.table tr:nth-child({0})').addClass('hh1');</script>", intIndex + 2));
        }

        static public string SplitArrayList(Array list, string separator)
        {
            // 判断为空
            if (list == null || list.Length == 0)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            foreach (object i in list)
            {
                builder.Append(separator);
                builder.Append(i.ToString());
            }

            string strTmp = builder.ToString();
            return strTmp.Substring(separator.Length);
        }

        /// <summary>
        /// string.split方法的扩充，增加对串分隔符的支持
        /// </summary>
        /// <param name="str">要拆分的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        static public string[] SplitString(string str, string separator)
        {
            // 判断为空
            if (str == null || str.Trim().Length == 0)
            {
                return new string[0];
            }
            string tmp = str;

            Hashtable ht = new Hashtable();

            int i = 0;
            int pos = tmp.IndexOf(separator);

            // 进行串分隔的循环处理
            while (pos != -1)
            {
                ht.Add(i, tmp.Substring(0, pos));
                tmp = tmp.Substring(pos + separator.Length);
                pos = tmp.IndexOf(separator);
                i++;
            }

            ht.Add(i, tmp);

            string[] array = new string[ht.Count];

            for (int j = 0; j < ht.Count; j++)
            {
                array[j] = ht[j].ToString();
            }
            return array;
        }

        /// <summary>
        /// 作者：Conis
        /// 日期：2007-02-27
        /// 描述：
        ///     根据两个分隔字符分隔出一个二维锯齿型数组，数组第二维的维数是不固定的
        /// </summary>
        /// <param name="firstSplit">第一个分隔字符</param>
        /// <param name="secondSplit">第二分隔字符</param>
        /// <param name="input">传入的字符串</param>
        /// <returns></returns>
        public static string[][] StringToDouArray(char firstSplit, char secondSplit, string input)
        {
            string[] first = input.Split(firstSplit);
            string[][] result = new string[first.Length][];
            for (int iLoop = 0; iLoop < first.Length; iLoop++)
            {
                string[] second = first[iLoop].Split(secondSplit);
                result[iLoop] = second;
            }
            return result;
        }

        /// <summary>
        /// 判断指定网络文件是否存在
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool UrlExistsUsingHttpWebRequest(string url)
        {
            try
            {
                System.Net.HttpWebRequest myRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                myRequest.Method = "HEAD";
                myRequest.Timeout = 100;
                System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)myRequest.GetResponse();
                return (res.StatusCode == System.Net.HttpStatusCode.OK);
            }
            catch (System.Net.WebException we)
            {
                System.Diagnostics.Trace.Write(we.Message);
                return false;
            }
        }

        public static string UtoGB(string str)
        {
            Regex reg = new Regex(@"(?i)\\u[a-f0-9]{4}");
            Match mat = reg.Match(str);
            while (mat.Success)
            {
                char c = Convert.ToChar(Convert.ToInt32(mat.Value.Substring(2), 16));
                str = str.Replace(mat.Value, c.ToString());
                mat = reg.Match(str);
            }
            return str;
        }

        /// <summary>
        /// 增加对数组格式化为串分的支持
        /// </summary>
        /// <param name="list">要格式化的数组</param>
        /// <param name="separator">分格符</param>
        /// <returns></returns>
        /// <summary>
        /// 作者：Conis
        /// 日期：2007-02-27
        /// 描述：
        ///  检查一个字符的值是否存在于一个值列表中，并可以设置是否忽略大小写
        /// </summary>
        /// <param name="value">要检查的字符串</param>
        /// <param name="values">一组字符串列表</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>返回Bool值</returns>
        public static bool ValueExists(string value, string[] values, bool ignoreCase)
        {
            bool exists = false;
            for (int index = 0; index < values.Length; index++)
            {
                if (ignoreCase)
                    exists = (value.ToLower() == values[index].ToLower());
                else
                    exists = (value == values[index]);

                if (exists) return true;
            }
            return false;
        }

        /// <summary>
        /// 根据文件名生成副本文件名
        /// </summary>
        /// <param name="fPath">文件完整路径</param>
        /// <returns></returns>
        private static string GetReadCopyName(string fPath)
        {
            string ext = Path.GetExtension(fPath);
            fPath = string.Format(@"{0}\{1}_1{2}", Path.GetDirectoryName(fPath), Path.GetFileNameWithoutExtension(fPath), ext);
            return fPath;
        }

        #region 格式化文本（防止SQL注入）

        /// <summary>
        /// 格式化文本（防止SQL注入）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TextFilter(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex10 = new System.Text.RegularExpressions.Regex(@"select", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex11 = new System.Text.RegularExpressions.Regex(@"update", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex12 = new System.Text.RegularExpressions.Regex(@"delete", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex10.Replace(html, "s_elect");
            html = regex11.Replace(html, "u_pudate");
            html = regex12.Replace(html, "d_elete");
            html = html.Replace("'", "’");
            html = html.Replace("&nbsp;", " ");
            return html;
        }

        #endregion 格式化文本（防止SQL注入）

        #region 格式化文本（输出内容）

        /// <summary>
        /// 格式化文本（输出内容）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MyFormatDestr(string str)
        {
            str = str.Replace("  ", " &nbsp;");
            str = str.Replace("\"", "&quot;");
            str = str.Replace("\'", "&#39;");
            str = str.Replace("\r\n", "<br/>");
            str = str.Replace("\n", "<br/>");
            str = str.Replace("\r", "<br/>");

            return str;
        }

        #endregion 格式化文本（输出内容）

        private static double toRadians(double degree)
        {
            return degree * Math.PI / 180;
        }

        public static Dictionary<T, int> ToOrderDic<T>(this List<T> strs)
        {
            Dictionary<T, int> dic = new Dictionary<T, int>();

            for (int index = 0; index < strs.Count; index++)
            {
                dic.Add(strs[index], index + 1);
            }

            return dic;
        }
    }
}