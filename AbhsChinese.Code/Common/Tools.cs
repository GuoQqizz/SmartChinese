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
    /// ���÷�������
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// �������ڼ�
        /// </summary>
        /// <returns></returns>
        public static string DayOfWeek
        {
            get
            {
                switch (DateTime.Now.DayOfWeek.ToString("D"))
                {
                    case "0":
                        return "������ ";

                    case "1":
                        return "����һ ";

                    case "2":
                        return "���ڶ� ";

                    case "3":
                        return "������ ";

                    case "4":
                        return "������ ";

                    case "5":
                        return "������ ";

                    case "6":
                        return "������ ";

                    default:
                        return "";
                }
            }
        }

        //base64������ı� תΪ    ͼƬ
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
        /// ��γ��֮��ľ���(ע�⣺��λ��km)
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
        /// �ж�һ���ַ����ǲ��� ���Ƶ绰����
        /// </summary>
        /// <param name="strText"></param>
        public static bool CheckIsTel(string strText)
        {
            string[,] arryReplace = new string[,]
                                         {
                                           { "��", "��", "��", "O"  },
                                           { "Ҽ", "һ", "��", "��" },
                                           { "��", "��", "��", "��" },
                                           { "��", "��", "��", "��" },
                                           { "��", "��", "��", "��" },
                                           { "��", "��", "��", "��" },
                                           { "½", "��", "��", "��" },
                                           { "��", "��", "��", "��" },
                                           { "��", "��", "��", "��" },
                                           { "��", "��", "��", "��" }
                                         };

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    strText = strText.Replace(arryReplace[i, j], i.ToString());
                }
            }

            ///�����м���3�������ַ��ģ���Ϊ������������������
            Regex regex = new Regex(@"\d{1}[^������]{0,3}?\d{1}[^������]{0,3}?\d{1}[^������]{0,3}?\d{1}[^������]{0,3}?\d{1}[^������]{0,3}?\d{1}[^������]{0,3}?\d{1}");
            Match mc = regex.Match(strText);

            return mc.Length > 0;
        }

        /// <summary>
        /// ���˲���ȫ�ַ�
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckString(string str, System.Web.UI.Page page)
        {
            bool result = false;
            string SQL_injdata = @"��|-|and|exec|insert|select|delete|update|count|%|
                                master|truncate|char|declare|<html>|<script>|<a>";
            char[] a = new char[] { '|' };
            string[] strarr = SQL_injdata.Split(a);
            if (str != "")
            {
                str = str.Replace("'", "��").Replace("��", "-");
                foreach (string strvalue in strarr)
                {
                    if (str.ToLower().Contains(strvalue))
                    {
                        page.ClientScript.RegisterStartupScript(page.GetType(), "IsSecurity", "<script>alert('����д�İ�������ȫ�ַ�:[" + strvalue + "]')</script>");
                        result = true;
                        break;
                    }
                }
            }
            else
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "AddInfo", "<script>alert('��д��Ϣ����Ϊ�գ���������д!')</script>");
                result = true;
            }
            return result;
        }

        /// <summary>
        /// ���ߣ�Conis
        /// ���ڣ�2007-02-27
        /// �޸����ڣ�2009-3-31
        /// �޸��ˣ�����ѵ
        /// ������
        ///     ���ֽ�ת��ΪMB,������λС��
        /// </summary>
        /// <param name="byteValue"></param>
        /// <returns></returns>
        public static double ConvertByteToMB(double byteValue)
        {
            double douMB = byteValue / 1024 / 1024;
            return Math.Round(douMB, 2);
        }

        /// <summary>
        /// ��MBת��Ϊ�ֽ�
        /// </summary>
        /// <param name="mbValue"></param>
        /// <returns></returns>
        public static double ConvertMBToByte(int mbValue)
        {
            return mbValue * 1024 * 1024;
        }

        /// <summary>
        /// ��Object��������json��...
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
        /// �����ļ��ĸ�ʽ������
        /// </summary>
        /// <param name="fPath">�ļ�������·��</param>
        public static void CreateReadCopy(string fPath)
        {
            string html;
            Regex regex = new Regex(@"([\s\S]{20,}?[.?!;]{1,3}(?![\s\S]{1,5}?[.?!;])|[\s\S]{1,}[\S]*$)");
            Regex regex1 = new Regex(@"<[^>]+>"); // <>�м�Ĳ���span
            using (StreamReader sr = new StreamReader(fPath))
            {
                html = sr.ReadToEnd().Replace("��", "?").Replace("��", "! ").Replace("��", ". ").Replace("��", ", ").Replace("��", "; ").Replace("��", ". ").Replace("��", "\"").Replace("��", "\"").Replace("��", "\'").Replace("��", "\'");
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
                    if (htmls[i].Replace("\r\n", "").Replace(" ", "").Length == 0) //ֻ�л���
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
                html = sr.ReadToEnd().Replace("��", "?").Replace("��", "! ").Replace("��", ". ").Replace("��", ", ").Replace("��", "; ").Replace("��", ". ").Replace("��", "\"").Replace("��", "\"").Replace("��", "\'").Replace("��", "\'");
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
                    if (htmls[i].Replace("\r\n", "").Replace(" ", "").Length == 0) //ֻ�л���
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
        /// ������.�����������ڵ�ʱ����,���ص���ʱ���������ڲ�ľ���ֵ.
        /// </summary>
        /// <param name="DateTime1">��һ�����ں�ʱ��</param>
        /// <param name="DateTime2">�ڶ������ں�ʱ��</param>
        /// <returns></returns>
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();

            if (ts.Days > 0)
                dateDiff = (ts.Days + 1).ToString() + "��";
            else if (ts.Hours > 0)
                dateDiff = ts.Hours.ToString() + "Сʱ";
            else if (ts.Minutes > 0)
                dateDiff = ts.Minutes.ToString() + "����";
            else if (ts.Seconds > 0)
                dateDiff = ts.Seconds.ToString() + "��";
            return dateDiff;
        }

        /// <summary>
        /// ������.����һ��ʱ���뵱ǰ�������ں�ʱ���ʱ����,���ص���ʱ���������ڲ�ľ���ֵ.
        /// </summary>
        /// <param name="DateTime1">һ�����ں�ʱ��</param>
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
                ShowStr += (HowManySecond / 24 * 3600) + "��";
                HowManySecond %= (24 * 3600);
            }
            if (HowManySecond >= 3600)
            {
                ShowStr += (HowManySecond / 3600) + "Сʱ";
                HowManySecond %= 3600;
            }
            if (HowManySecond >= 60)
            {
                ShowStr += (HowManySecond / 60) + "��";
            }
            if (HowManySecond % 60 > 0)
            {
                ShowStr += (HowManySecond % 60) + "��";
            }
            return ShowStr;
        }

        /// <summary>
        /// ���ߣ�Conis
        /// ���ڣ�2007-02-27
        /// ������
        ///     ����Try�ķ������һ�������Ƿ�Ϊ�����ͣ�������������ͣ��򷵻�Ĭ��ֵ
        /// </summary>
        /// <param name="obj">Ҫ���Ķ���</param>
        /// <param name="dateDefault">���������ͣ��򷵻�Ĭ��ֵ</param>
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
        /// ���һ�������Ƿ�Ϊ�����ͣ������򷵻�Ĭ��ֵ;��,�򷵻�ԭֵ
        /// </summary>
        /// <param name="obj">Ҫ���Ķ���</param>
        /// <param name="deciDefault">���ص�Ĭ��ֵ</param>
        /// <returns>decimal�͵Ľ��</returns>
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
        /// ���ߣ�Conis
        /// ���ڣ�2007-02-27
        /// ������
        ///     �������������һ�������Ƿ�Ϊ�������֣�����������֣��򷵻�Ĭ��ֵ����������֣���ת��������
        /// </summary>
        /// <param name="str">Ҫ���Ķ���</param>
        /// <param name="intDefault">�����ַ������ص�Ĭ��ֵ</param>
        /// <returns>���صĽ��</returns>
        public static int DefIsNumeric(object obj, int intDefault)
        {
            return IsNumeric(obj) ? Convert.ToInt32(obj) : intDefault;
        }

        /// <summary>
        /// ɾ���ļ�����
        /// </summary>
        /// <param name="fPath">�ļ�������·��</param>
        public static void DeleteReadCopy(string fPath)
        {
            File.Delete(GetReadCopyName(fPath));
        }

        /// <summary>
        /// ��web�ؼ���Ϊ������״̬
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
        /// ����ͼƬ...
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
            int c = 0; //ʵ�ʶ�ȡ���ֽ���
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
        /// ��DataGrid�ķ�ʽ�����ݵ�����Excel
        /// </summary>
        /// <param name="fileName">�����ļ���</param>
        /// <param name="dt">Ҫ����������DataTable</param>
        public static void ExportToExcel(Page page, string fileName, DataTable dt)
        {
            GridView gv = new GridView();
            gv.DataSource = dt;
            gv.DataBind();
            //���ݹ�������ֹת��
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

            //�����������ؼ�����
            page.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=GB2312>");

            gv.Visible = true;
            gv.HeaderStyle.Reset();
            gv.AlternatingRowStyle.Reset();

            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(tw);
            gv.RenderControl(hw);

            //5.���DataGrid����
            page.Response.Write(tw.ToString());
            page.Response.End();
        }

        /// <summary>
        /// ��һ��С����ʽ��Ϊ���0.5�����ַ���
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        public static double FormatDouble(double intput)
        {
            double intNum = Math.Round(intput, 0);
            double decNum = Math.Round(intput, 2);
            double decDecimal = decNum - intNum;
            double result = 0.5;                       //���յ�С��
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
        /// ������ת��ΪС��������ָ��λ���ֵ�С��
        /// </summary>
        /// <param name="numer">��Ҫת����ԭ����</param>
        /// <param name="digit">С��������λ��</param>
        /// <returns></returns>
        public static object FormatNumeric(object numer, int digit)
        {
            string tmpStr = numer.ToString();

            // С�������ڵ�λ��
            int tmp = tmpStr.IndexOf('.');
            // ��Ҫ�����λ��
            string tmpDigit = string.Empty;
            StringBuilder builder = new StringBuilder();

            // ����������Ϊ�������ָ����0
            if (tmp < 0)
            {
                for (int i = 1; i <= digit; i++)
                {
                    builder.Append("0");
                }
                // ��Ҫ�����λ��
                tmpDigit = builder.ToString();
                return tmpStr + "." + tmpDigit;
            }
            //���ԭʼ���ݵľ��ȱ�Ҫ��ĸ����ȡ
            else if ((tmpStr.Length - tmp - 1) > digit)
            {
                return tmpStr.Substring(0, tmp + digit + 1);
            }
            //���ԭʼ���ݵľ��ȱ�Ҫ��ĵ���0
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
        /// ���ߣ�Conis
        /// ���ڣ�2007-02-27
        /// ������
        ///     ����һ���ַ������飬����ָ�����ȵ�����ַ���
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
        /// ���ߣ�Conis
        /// ���ڣ�2007-02-27
        /// ������
        ///     ����һ���ַ������飬����ָ�����ȵ�����ַ���
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
        /// ��ȡ�ַ������ȣ�һ�������������ֽ�
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
        /// �����ض���Ϣ�ĶԻ���js
        /// </summary>
        /// <param name="Msn">��ʾ��Ϣ</param>
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
        /// ��ȡö�����ƣ�û�з��ؿ��ַ���...
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
        /// ��ȡö�����ƣ�û�з��ؿ��ַ���...
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
        /// ��ȡö�����ƣ�û�з��ؿ��ַ���...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// Common.Tools.GetEnumNameCombin<Common.enmн�����>(_drQInvite["qi_JobType"]._ToInt32(), "+")
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
        /// ��Ҫ���ܣ��ض�ָ�����ȵ��ַ���
        /// �������ͣ�Object
        /// �����������ڴ�ת�ַ����ζ���
        /// ����ֵ��String����
        /// </summary>
        /// <param name="mObjStr">�ַ����ζ���</param>
        /// <param name="mMaxLen">�涨�ضϳ���</param>
        /// <returns>�ѽض��ַ���</returns>
        static public string GetFormatString(object mObjStr, int mMaxLen)
        {
            //�ų�Null
            if (mObjStr == null)
            {
                return string.Empty;
            }
            //�ų�DBNull
            if (mObjStr.Equals(DBNull.Value))
            {
                return string.Empty;
            }

            //�����ȴ��ڹ涨���ȣ����ȡ
            if (mObjStr.ToString().Length > mMaxLen)
            {
                return mObjStr.ToString().Substring(0, mMaxLen) + "...";
            }
            //���򷵻�ԭʼ��
            return mObjStr.ToString();
        }

        /// <summary>
        /// ��ȡ��ʱ��Ϊ���Ƶ��ַ���
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyString()
        {
            string TempFileName = DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;

            return TempFileName;
        }

        /// <summary>
        /// ��ȡ�ļ�����
        /// </summary>
        /// <param name="fPath">�ļ�������·��</param>
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
                html = "ָ���ļ������ڣ��޷��Ķ�����";
            }
            return html;
        }

        /// <summary>
        /// ��ȡhtml�ļ�����
        /// </summary>
        /// <param name="fPath">�ļ�������·��</param>
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
                html = "ָ���ļ������ڣ��޷��Ķ�����";
            }
            return html;
        }

        /// <summary>
        /// �õ�http��Ŀ¼
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            // �Ƿ�ΪSSL��֤վ��
            string secure = HttpContext.Current.Request.ServerVariables["HTTPS"];
            string httpProtocol = (secure == "on" ? "https://" : "http://");

            // ����������
            string serverName = HttpContext.Current.Request.ServerVariables["Server_Name"];

            // Ӧ�÷�������
            string applicationName = HttpContext.Current.Request.ApplicationPath;

            string siteURL = httpProtocol + serverName + applicationName;
            return siteURL;
        }

        /// <summary>
        /// ������������ȡʱ���...
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string GetTimeBySeconds(int seconds)
        {
            DateTime t = DateTime.Now;
            return DateDiff(t.AddSeconds(seconds), t);
        }

        /// <summary>
        /// �õ����ܵ�һ��(������һΪ��һ��)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //����һΪ��һ��
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //��Ϊ��������һΪ��һ�죬����Ҫ�ж�weeknow����0ʱ��Ҫ��ǰ��6�졣
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //���ܵ�һ��
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }

        /// <summary>
        /// �õ��������һ��(��������Ϊ���һ��)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //������Ϊ���һ��
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //�������һ��
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }

        /// <summary>
        /// �� �� ����HTMLDecode
        /// ��Ҫ���ܣ�ȥ���ַ����е�HTMLԪ��
        /// ������ڣ�2006-03-21
        /// </summary>
        /// <param name="sText">����ʽ�����ı�</param>
        /// <returns>��ʽ������ı�</returns>
        public static string HTMLDecode(string sText)
        {
            string s = sText.Replace("&nbsp;", " ").Replace("<br />", "\r\n").Replace("<br/>", "\r\n");
            return s;
        }

        /// <summary>
        /// �� �� ����HTMLDecode
        /// ��Ҫ���ܣ�ȥ���ַ����е�HTMLԪ��
        /// ������ڣ�2006-03-21
        /// </summary>
        /// <param name="sText">����ʽ�����ı�</param>
        /// <returns>��ʽ������ı�</returns>
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
        /// �� �� ����HTMLEncode
        /// ��Ҫ���ܣ���HTML�ַ������и�ʽ������
        /// ������ڣ�2006-03-02
        /// </summary>
        /// <param name="sText">����ʽ�����ı�</param>
        /// <returns>��ʽ������ı�</returns>
        public static string HTMLEncode(string sText)
        {
            return sText.Replace("  ", "&nbsp;&nbsp;").Replace("\r\n", "<br />").Replace("\n\n", "<br />").Replace("\n", "<br />").Replace("\"", "&quot;");
        }

        /// <summary>
        /// �� �� ����HTMLEncode
        /// ��Ҫ���ܣ���HTML�ַ������и�ʽ������
        /// ������ڣ�2006-03-02
        /// </summary>
        /// <param name="sText">����ʽ�����ı�</param>
        /// <returns>��ʽ������ı�</returns>
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
        /// ��ʼ���ı���
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
        /// ���ߣ�Conis
        /// ���ڣ�2007-02-27
        /// ������
        ///     ���������ַ����Ƿ�ΪInt����
        /// </summary>
        /// <param name="str">Ҫ�����ַ���</param>
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
        /// ��ʾ���ͻ��˵���Ϣ�Ի���...
        /// </summary>
        /// <param name="strMSG">��Ϣ</param>
        public static void Message(Page page, string strMSG)
        {
            strMSG = HTMLEncode(strMSG);
            strMSG = strMSG.Replace("'", " | ");
            page.ClientScript.RegisterStartupScript(page.GetType(), "MsgBox", string.Format("<script>alert('{0}');</script>", strMSG));
        }

        /// <summary>
        /// ��ʾ���ͻ��˵���Ϣ�Ի���...
        /// </summary>
        /// <param name="strMSG">��Ϣ</param>
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
                page.ClientScript.RegisterStartupScript(page.GetType(), "MsgBox", string.Format("<script>jAlert('{0}','��ʾ'{1});</script>", strMSG, url.Length > 0 ? string.Format(",function(){{window.location='{0}';}}", url) : ""));
            }
        }

        /// <summary>
        /// ��ʾ���ͻ��˵���Ϣ�Ի���...
        /// </summary>
        /// <param name="strMSG">��Ϣ</param>
        public static void MessageByLoadOver(Page page, string strMSG)
        {
            strMSG = HTMLEncode(strMSG);
            strMSG = strMSG.Replace("'", " | ");
            page.ClientScript.RegisterStartupScript(page.GetType(), "MsgBox", string.Format("<script>$(function(){{alert('{0}');}});</script>", strMSG));
        }

        /// <summary>
        /// ��ʾ���ͻ��˵���Ϣ�Ի���...
        /// </summary>
        /// <param name="strMSG">��Ϣ</param>
        public static void MessageByLoadOver(Page page, string strMSG, string url)
        {
            strMSG = HTMLEncode(strMSG);
            strMSG = strMSG.Replace("'", " | ");
            page.ClientScript.RegisterStartupScript(page.GetType(), "MsgBox", string.Format("<script>$(function(){{alert('{0}');window.location='{1}';}});</script>", strMSG, url));
        }

        /// <summary>
        /// ���ߣ�Conis
        /// ���ڣ�2007-02-27
        /// ������
        ///     ���һ�������Ƿ��ǿ�ֵ
        /// </summary>
        /// <param name="input">����ֵ</param>
        /// <param name="defValue">Ĭ��ֵ��</param>
        /// <returns></returns>
        public static object ObjectIsNull(object input, object defValue)
        {
            return (input == null) ? defValue : input;
        }

        /// <summary>
        /// �رմ���
        /// </summary>
        /// <param name="page"></param>
        public static void PageClose(Page page)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "pageClose", "<script>window.close();</script>");
        }

        /// <summary>
        /// �������һ���Զ��Ÿ������ַ���
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
        /// ��ȡXML�ڵ�ֵ...
        /// </summary>
        /// <param name="strNode">XML�ַ���</param>
        /// <param name="strNode">�ڵ�����</param>
        /// <returns>�ڵ�ֵ</returns>
        public static string ReadXMLNodeValue(string strXML, string strNode)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(strXML);
            string result = xd.GetElementsByTagName(strNode).Item(0).InnerText.Trim();
            return result;
        }

        /// <summary>
        /// ȥ��html�е�����htmlԪ�ر�ǩ
        /// </summary>
        /// <param name="strhtml"></param>
        /// <returns></returns>
        public static string RemoveHtmlMark(string strhtml)
        {
            Regex regex = new Regex(@"<[^>]+>|</[^>]+>");

            return regex.Replace(strhtml, "");
        }

        /// <summary>
        /// ȥ���ַ����е�HTMLԪ��
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string RemoveSpaceHtmlTag(string Input)
        {
            string input = Input;
            //ȥhtml��ǩ
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
            //ȥ���˿ո��м����ո�
            input = Regex.Replace(input.Trim(), "\\s+", " ");
            return input;
        }

        /// <summary>
        /// ʵ�����ݵ��������뷨
        /// </summary>
        /// <param name="value">Ҫ���д��������</param>
        /// <param name="decimals">������С��λ��</param>
        /// <returns>���������Ľ��</returns>
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
        /// ѡ��Grid�е���...
        /// </summary>
        /// <param name="intIndex">������</param>
        public static void SelectRowScript(Page page, int intIndex)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "SelectRow", string.Format("<script type='text/javascript'>$('.table tr:nth-child({0})').addClass('hh1');</script>", intIndex + 2));
        }

        static public string SplitArrayList(Array list, string separator)
        {
            // �ж�Ϊ��
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
        /// string.split���������䣬���ӶԴ��ָ�����֧��
        /// </summary>
        /// <param name="str">Ҫ��ֵ��ַ���</param>
        /// <param name="separator">�ָ���</param>
        /// <returns></returns>
        static public string[] SplitString(string str, string separator)
        {
            // �ж�Ϊ��
            if (str == null || str.Trim().Length == 0)
            {
                return new string[0];
            }
            string tmp = str;

            Hashtable ht = new Hashtable();

            int i = 0;
            int pos = tmp.IndexOf(separator);

            // ���д��ָ���ѭ������
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
        /// ���ߣ�Conis
        /// ���ڣ�2007-02-27
        /// ������
        ///     ���������ָ��ַ��ָ���һ����ά��������飬����ڶ�ά��ά���ǲ��̶���
        /// </summary>
        /// <param name="firstSplit">��һ���ָ��ַ�</param>
        /// <param name="secondSplit">�ڶ��ָ��ַ�</param>
        /// <param name="input">������ַ���</param>
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
        /// �ж�ָ�������ļ��Ƿ����
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
        /// ���Ӷ������ʽ��Ϊ���ֵ�֧��
        /// </summary>
        /// <param name="list">Ҫ��ʽ��������</param>
        /// <param name="separator">�ָ��</param>
        /// <returns></returns>
        /// <summary>
        /// ���ߣ�Conis
        /// ���ڣ�2007-02-27
        /// ������
        ///  ���һ���ַ���ֵ�Ƿ������һ��ֵ�б��У������������Ƿ���Դ�Сд
        /// </summary>
        /// <param name="value">Ҫ�����ַ���</param>
        /// <param name="values">һ���ַ����б�</param>
        /// <param name="ignoreCase">�Ƿ���Դ�Сд</param>
        /// <returns>����Boolֵ</returns>
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
        /// �����ļ������ɸ����ļ���
        /// </summary>
        /// <param name="fPath">�ļ�����·��</param>
        /// <returns></returns>
        private static string GetReadCopyName(string fPath)
        {
            string ext = Path.GetExtension(fPath);
            fPath = string.Format(@"{0}\{1}_1{2}", Path.GetDirectoryName(fPath), Path.GetFileNameWithoutExtension(fPath), ext);
            return fPath;
        }

        #region ��ʽ���ı�����ֹSQLע�룩

        /// <summary>
        /// ��ʽ���ı�����ֹSQLע�룩
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
            html = regex1.Replace(html, ""); //����<script></script>���
            html = regex2.Replace(html, ""); //����href=javascript: (<A>) ����
            html = regex3.Replace(html, " _disibledevent="); //���������ؼ���on...�¼�
            html = regex4.Replace(html, ""); //����iframe
            html = regex5.Replace(html, ""); //����frameset
            html = regex10.Replace(html, "s_elect");
            html = regex11.Replace(html, "u_pudate");
            html = regex12.Replace(html, "d_elete");
            html = html.Replace("'", "��");
            html = html.Replace("&nbsp;", " ");
            return html;
        }

        #endregion ��ʽ���ı�����ֹSQLע�룩

        #region ��ʽ���ı���������ݣ�

        /// <summary>
        /// ��ʽ���ı���������ݣ�
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

        #endregion ��ʽ���ı���������ݣ�

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