using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll.BaiduApi
{
    public class BaiduApiAppSetting
    {
        public static string BaiduAccessToken = ConfigurationManager.AppSettings["BaiduAccessToken"];

        public static string BaiduGrantType = "client_credentials";
        //人脸识别
        public static string FaceRecognitionAppId = ConfigurationManager.AppSettings["FaceRecognitionAppId"];
        public static string FaceRecognitionSecret = ConfigurationManager.AppSettings["FaceRecognitionSecret"];
        public static string FaceRecognitionAddURL = ConfigurationManager.AppSettings["FaceRecognitionAddURL"];
        public static string FaceRecognitionUpdateURL = ConfigurationManager.AppSettings["FaceRecognitionUpdateURL"];
        public static string FaceRecognitionDeleteURL = ConfigurationManager.AppSettings["FaceRecognitionDeleteURL"];
        public static string FaceRecognitionLoginURL = ConfigurationManager.AppSettings["FaceRecognitionLoginURL"];
        public static string FaceRecognitionCheckURL = ConfigurationManager.AppSettings["FaceRecognitionCheckURL"];
        public static string Liveness = ConfigurationManager.AppSettings["Liveness"];
        public static string Quality = ConfigurationManager.AppSettings["Quality"];
        public static string Group = ConfigurationManager.AppSettings["FaceRecognitionGroupId"];


        //语音合成
        public static string TextToAudioAppId = ConfigurationManager.AppSettings["TextToAudioAppId"];
        public static string TextToAudioSecret = ConfigurationManager.AppSettings["TextToAudioSecret"];
        public static string TextToAudioCUID = ConfigurationManager.AppSettings["TextToAudioCUID"];
        public static string TextToAudioURL = ConfigurationManager.AppSettings["TextToAudioURL"];

        public static string Language = ConfigurationManager.AppSettings["Language"];
        public static string Speed = ConfigurationManager.AppSettings["Speed"];
        public static string Pitch = ConfigurationManager.AppSettings["Pitch"];
        public static string Volume = ConfigurationManager.AppSettings["Volume"];
        public static string Pronunciation = ConfigurationManager.AppSettings["Pronunciation"];
    }
}
