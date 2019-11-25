using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AbhsChinese.Code.Common
{
    public static class Encrypt
    {
        private static string _strKey = "abcdefgh"; //abcdefgh

        private static string _md5Key = "!@#qaz)(*";
        //URL传输参数加密Key
        //MD5加密时增加的串

        #region 私有方法

        /// <summary>
        /// DEC解密...
        /// </summary>
        /// <param name="strDecrypt">需要解密的原文</param>
        /// <param name="strKey">KEY</param>
        /// <returns></returns>
        private static string DecryptString(string strDecrypt, string strKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[strDecrypt.Length / 2];
            for (int x = 0; x < strDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(strDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(strKey); //建立加密对象的密钥和偏移量，此值重要，不能修改
            des.IV = ASCIIEncoding.ASCII.GetBytes(strKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// DEC加密...
        /// </summary>
        /// <param name="strEncrypt">需要加密的原文</param>
        /// <param name="strKey">KEY</param>
        /// <returns></returns>
        private static string EncryptString(string strEncrypt, string strKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider(); //把字符串放到byte数组中

            byte[] inputByteArray = Encoding.Default.GetBytes(strEncrypt);

            des.Key = ASCIIEncoding.ASCII.GetBytes(strKey); //建立加密对象的密钥和偏移量

            des.IV = ASCIIEncoding.ASCII.GetBytes(strKey); //原文使用ASCIIEncoding.ASCII方法的GetBytes方法
            MemoryStream ms = new MemoryStream(); //使得输入密码必须输入英文文本
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        #endregion 私有方法

        #region 公用接口

        /// <summary>
        /// 解密URL传输的字符串
        /// </summary>
        /// <param name="strDecrypt">需要解密的原文</param>
        /// <returns></returns>
        public static string DecryptQueryString(string strDecrypt)
        {
            return DecryptString(strDecrypt, _strKey);
        }

        /// <summary>
        /// 解密URL传输的字符串
        /// </summary>
        /// <param name="strDecrypt">需要解密的原文</param>
        /// <param name="key">加密串</param>
        /// <returns></returns>
        public static string DecryptQueryString(string strDecrypt, string key)
        {
            return DecryptString(strDecrypt, key);
        }

        /// <summary>
        /// 加密可用于URL传输的字符串
        /// </summary>
        /// <param name="strEncrypt">需要加密的原文</param>
        /// <returns></returns>
        public static string EncryptQueryString(string strEncrypt)
        {
            return EncryptString(strEncrypt, _strKey);
        }
        /// <summary>
        /// 加密可用于URL传输的字符串
        /// </summary>
        /// <param name="strEncrypt">需要加密的原文</param>
        /// <param name="key">加密串</param>
        /// <returns></returns>
        public static string EncryptQueryString(string strEncrypt, string key)
        {
            return EncryptString(strEncrypt, key);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="sDataIn">要加密的字符串</param>
        /// <param name="move">偏移量</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string sDataIn, string move)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = System.Text.Encoding.UTF8.GetBytes(move + sDataIn);
            bytHash = md5.ComputeHash(bytValue); md5.Clear();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytHash.Length; i++)
            {
                builder.Append(bytHash[i].ToString("x").PadLeft(2, '0'));
            }
            string sTemp = builder.ToString();
            return sTemp;
        }

        /// <summary>
        /// 数据加密操作
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            if (str._IsNullOrEmpty()) return "";
            string Sstr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            return Sstr;
        }

        #endregion 公用接口

        public static string Decryption(string str)
        {
            string[] arrPWD = new string[10];

            arrPWD[0] = "MIGADBO6QL8PE19HY05RJWVU2ZX34KN7TSFC";
            arrPWD[1] = "2R9Z0POFSL34NEQJBUMADGK81T7HCXIWV65Y";
            arrPWD[2] = "Q452FARODENJPSVZ6YBX78CK0LW3U1TIGH9M";
            arrPWD[3] = "R29PVINWDBYGQU8ACM463K0LZEO5JSHXT17F";
            arrPWD[4] = "1IOML4JBZ2SYWP8V7FUAC90TNQ536GXHKDER";
            arrPWD[5] = "DPTF9NE74SCV6O20QIYL3RKZGA5J1XW8HMBU";
            arrPWD[6] = "XTQJOZCS10EY48LFDRBN72WIAHUVP5MG396K";
            arrPWD[7] = "7PCVSOWT31D0M2FBH4JXLG89I6RNQAUKZ5EY";
            arrPWD[8] = "H9YI3WZUTFBNRMPAQ04J1EX8G2VK7O6SLDC5";
            arrPWD[9] = "KJDBCH47FMQ5ZUT8OAX3INEP0VWS2L1RG96Y";

            str = str.Trim();

            if (str.Length == 0)
                return "!@#$%^&*()";

            int bi, si, j;

            string ls = str.Substring(str.Length - 1, 1);
            if (!Regex.IsMatch(ls, @"\d"))
                return "!@#$%^&*()";

            bi = int.Parse(str.Substring(str.Length - 1, 1));
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < str.Length - 1; i++)
            {
                j = (i + 1) % 10;
                si = arrPWD[j].IndexOf(str.Substring(i, 1), StringComparison.InvariantCultureIgnoreCase);
                si = si - (i + 1) % 36;

                if (si < 0)
                    si = si + 36;

                builder.Append(arrPWD[bi].Substring(si, 1));
            }
            string PWD = builder.ToString();
            return PWD;
        }

        public static string Encryption(string str)
        {
            string[] arrPWD = new string[10];

            arrPWD[0] = "MIGADBO6QL8PE19HY05RJWVU2ZX34KN7TSFC";
            arrPWD[1] = "2R9Z0POFSL34NEQJBUMADGK81T7HCXIWV65Y";
            arrPWD[2] = "Q452FARODENJPSVZ6YBX78CK0LW3U1TIGH9M";
            arrPWD[3] = "R29PVINWDBYGQU8ACM463K0LZEO5JSHXT17F";
            arrPWD[4] = "1IOML4JBZ2SYWP8V7FUAC90TNQ536GXHKDER";
            arrPWD[5] = "DPTF9NE74SCV6O20QIYL3RKZGA5J1XW8HMBU";
            arrPWD[6] = "XTQJOZCS10EY48LFDRBN72WIAHUVP5MG396K";
            arrPWD[7] = "7PCVSOWT31D0M2FBH4JXLG89I6RNQAUKZ5EY";
            arrPWD[8] = "H9YI3WZUTFBNRMPAQ04J1EX8G2VK7O6SLDC5";
            arrPWD[9] = "KJDBCH47FMQ5ZUT8OAX3INEP0VWS2L1RG96Y";

            str = str.Trim();

            if (str.Length == 0)
                return "加密字符串不符合规范,必须是数字和字母的组合!";

            int bi, si, j;
            string PWD = "";

            //Random rd = new Random()
            //bi = rd.Next(10)
            bi = 1;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                si = arrPWD[bi].IndexOf(str.Substring(i, 1), StringComparison.InvariantCultureIgnoreCase);
                if (si == -1)
                    return "加密字符串不符合规范,必须是数字和字母的组合!";

                j = (i + 1) % 10;
                si = si + (i + 1) % 36;

                if (si >= 36)
                    si = si - 36;

                builder.Append(arrPWD[j].Substring(si, 1));
            }
            PWD = builder.ToString();
            return PWD + bi.ToString();
        }

        /// <summary>
        /// 通过主机ID获取登记码...
        /// </summary>
        /// <param name="strCompuerNum"></param>
        /// <returns></returns>
        public static string GetRegNum(string strCompuerNum)
        {
            return MD5("weilaifu" + strCompuerNum + "weilaifu");
        }

        public static string GetMD5Pwd(string pwd)
        {
            return MD5($"{pwd}{_md5Key}");
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string MD5Hash(string input, string salt)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input + salt));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}