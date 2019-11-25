using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThoughtWorks.QRCode.Codec;
using System.Text;
using System.IO;

namespace WenDu.ZhiBo.Infrastructure
{


    /// <summary> 二维码生成类
    /// </summary>
    public class QRCodeUtil
    {
        /// <summary> 生成二维码图片
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="imagePath">图片存储路径</param>
        /// <param name="errorMsg">(如果发生了错误，则返回)错误信息</param>
        /// <returns></returns>
        public static bool MakeImage(string content, string imagePath, out string errorMsg)
        {
            bool result = false;
            errorMsg = string.Empty;

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            string encoding = "Byte";
            if (encoding == "Byte")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            else if (encoding == "Numeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            }

            qrCodeEncoder.QRCodeScale = 20;
            qrCodeEncoder.QRCodeVersion = 0;

            string errorCorrect = "M";
            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            try
            {
                System.Drawing.Image image =
                    qrCodeEncoder.Encode(content, Encoding.Default);

                image.Save(imagePath, System.Drawing.Imaging.ImageFormat.Gif);

                result = true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
                result = false;
            }

            return result;
        }

        public static byte[] MakeImage(string content)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            string encoding = "Byte";
            if (encoding == "Byte")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            else if (encoding == "Numeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            }

            qrCodeEncoder.QRCodeScale = 20;
            qrCodeEncoder.QRCodeVersion = 0;

            string errorCorrect = "M";
            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            System.Drawing.Image image =
                qrCodeEncoder.Encode(content, Encoding.Default);

            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

            return ms.ToArray();
        }
    }
}