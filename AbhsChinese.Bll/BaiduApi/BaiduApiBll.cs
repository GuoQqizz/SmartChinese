using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.AbhsResource;
using Baidu.Aip.Speech;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll.BaiduApi
{
    public class BaiduApiBll
    {
        #region 人脸登录
        /// <summary>
        /// 人脸注册
        /// </summary>
        /// <param name="imgpath"></param>
        /// <param name="imgType"></param>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static FaceRecognitionResponse FaceRegister(string imgPath, string imgType, string userId, string userInfo)
        {
            try
            {
                var token = GetAccessToken(BaiduApiAppSetting.FaceRecognitionAppId, BaiduApiAppSetting.FaceRecognitionSecret);
                string url = BaiduApiAppSetting.FaceRecognitionAddURL + "?access_token=" + token;
                #region 注释
                //image(必填)            图片信息(总数据大小应小于10M)，图片上传方式根据image_type来判断。注：组内每个uid下的人脸图片数目上限为20张

                //image_type(必填)       图片类型
                //                       BASE64: 图片的base64值，base64编码后的图片数据，编码后的图片大小不超过2M；
                //                       URL: 图片的 URL地址(可能由于网络等原因导致下载图片时间过长)；
                //                       FACE_TOKEN：人脸图片的唯一标识，调用人脸检测接口时，会为每个人脸图片赋予一个唯一的FACE_TOKEN，同一张图片多次检测得到的FACE_TOKEN是同一个。

                //group_id(必填)          用户组id，标识一组用户（由数字、字母、下划线组成），长度限制48B。
                //                        产品建议：根据您的业务需求，可以将需要注册的用户，按照业务划分，分配到不同的group下，例如按照会员手机尾号作为groupid，用于刷脸支付、会员计费消费
                //                        这样可以尽可能控制每个group下的用户数与人脸数，提升检索的准确率

                //user_id(必填)           用户id（由数字、字母、下划线组成），长度限制128B

                //user_info               用户资料，长度限制256B 默认空

                //quality_control         图片质量控制
                //                        NONE: 不进行控制
                //                        LOW:较低的质量要求
                //                        NORMAL: 一般的质量要求
                //                        HIGH: 较高的质量要求
                //                        默认 NONE
                //                        若图片质量不满足要求，则返回结果中会提示质量检测失败

                //liveness_control        活体检测控制
                //                        NONE: 不进行控制
                //                        LOW:较低的活体要求(高通过率 低攻击拒绝率)
                //                        NORMAL: 一般的活体要求(平衡的攻击拒绝率, 通过率)
                //                        HIGH: 较高的活体要求(高攻击拒绝率 低通过率)
                //                        默认NONE
                //                        若活体检测结果不满足要求，则返回结果中会提示活体检测失败

                //action_type             操作方式
                //                        APPEND: 当user_id在库中已经存在时，对此user_id重复注册时，新注册的图片默认会追加到该user_id下
                //                        REPLACE : 当对此user_id重复注册时,则会用新图替换库中该user_id下所有图片
                //                        默认使用APPEND
                #endregion
                var option = new Dictionary<string, object>()
                {
                    {"image",imgPath },
                    {"image_type",imgType },
                    {"group_id",BaiduApiAppSetting.Group },
                    {"user_id",userId },
                    {"user_info",userInfo },
                    {"quality_control",BaiduApiAppSetting.Quality },
                    {"liveness_control",BaiduApiAppSetting.Liveness },
                    {"action_type","REPLACE" }
                };
                var responseModel = GetFaceRecognitionResponse(option, url);
                return responseModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 人脸修改
        /// </summary>
        /// <param name="imgpath"></param>
        /// <param name="imgType"></param>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static FaceRecognitionResponse FaceUpdate(string imgPath, string imgType, string userId, string userInfo)
        {
            try
            {
                var token = GetAccessToken(BaiduApiAppSetting.FaceRecognitionAppId, BaiduApiAppSetting.FaceRecognitionSecret);
                string url = BaiduApiAppSetting.FaceRecognitionUpdateURL + "?access_token=" + token;
                #region 注释
                //image(必填)            图片信息(总数据大小应小于10M)，图片上传方式根据image_type来判断。注：组内每个uid下的人脸图片数目上限为20张

                //image_type(必填)       图片类型
                //                       BASE64: 图片的base64值，base64编码后的图片数据，编码后的图片大小不超过2M；
                //                       URL: 图片的 URL地址(可能由于网络等原因导致下载图片时间过长)；
                //                       FACE_TOKEN：人脸图片的唯一标识，调用人脸检测接口时，会为每个人脸图片赋予一个唯一的FACE_TOKEN，同一张图片多次检测得到的FACE_TOKEN是同一个。

                //group_id(必填)          用户组id，标识一组用户（由数字、字母、下划线组成），长度限制48B。
                //                        产品建议：根据您的业务需求，可以将需要注册的用户，按照业务划分，分配到不同的group下，例如按照会员手机尾号作为groupid，用于刷脸支付、会员计费消费
                //                        这样可以尽可能控制每个group下的用户数与人脸数，提升检索的准确率

                //user_id(必填)           用户id（由数字、字母、下划线组成），长度限制128B

                //user_info               用户资料，长度限制256B 默认空

                //quality_control         图片质量控制
                //                        NONE: 不进行控制
                //                        LOW:较低的质量要求
                //                        NORMAL: 一般的质量要求
                //                        HIGH: 较高的质量要求
                //                        默认 NONE
                //                        若图片质量不满足要求，则返回结果中会提示质量检测失败

                //liveness_control        活体检测控制
                //                        NONE: 不进行控制
                //                        LOW:较低的活体要求(高通过率 低攻击拒绝率)
                //                        NORMAL: 一般的活体要求(平衡的攻击拒绝率, 通过率)
                //                        HIGH: 较高的活体要求(高攻击拒绝率 低通过率)
                //                        默认NONE
                //                        若活体检测结果不满足要求，则返回结果中会提示活体检测失败

                //action_type             操作方式
                //                        APPEND: 当user_id在库中已经存在时，对此user_id重复注册时，新注册的图片默认会追加到该user_id下
                //                        REPLACE : 当对此user_id重复注册时,则会用新图替换库中该user_id下所有图片
                //                        默认使用APPEND
                #endregion
                var option = new Dictionary<string, object>()
                {
                    {"image",imgPath },
                    {"image_type",imgType },
                    {"group_id",BaiduApiAppSetting.Group },
                    {"user_id",userId },
                    {"user_info",userInfo },
                    {"quality_control",BaiduApiAppSetting.Quality },
                    {"liveness_control",BaiduApiAppSetting.Liveness },
                    {"action_type","REPLACE" }
                };
                var responseModel = GetFaceRecognitionResponse(option, url);
                return responseModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 人脸登录
        /// </summary>
        /// <param name="imgpath"></param>
        /// <param name="imgType"></param>
        /// <param name="groupIds">用户组，如果多个组中查找，根据逗号隔开（目前只有一个）</param>
        /// <returns></returns>
        public static FaceRecognitionResponse FaceLogin(string imgPath, string imgType)
        {
            try
            {
                var token = GetAccessToken(BaiduApiAppSetting.FaceRecognitionAppId, BaiduApiAppSetting.FaceRecognitionSecret);
                // 人脸认证
                string url = BaiduApiAppSetting.FaceRecognitionLoginURL + "?access_token=" + token;
                var option = new Dictionary<string, object>()
                {
                    {"image",imgPath },
                    {"image_type",imgType },
                    {"group_id_list",BaiduApiAppSetting.Group },
                    {"quality_control",BaiduApiAppSetting.Quality },
                    {"liveness_control",BaiduApiAppSetting.Liveness },
                    {"max_user_num",1 }
                };
                var loginResponse = GetFaceRecognitionResponse(option, url);
                return loginResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 人脸删除
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <param name="faceToken">用户组，如果多个组中查找，根据逗号隔开</param>
        /// <returns></returns>
        public static FaceRecognitionResponse FaceDelete(string userId, string faceToken)
        {
            try
            {
                var token = GetAccessToken(BaiduApiAppSetting.FaceRecognitionAppId, BaiduApiAppSetting.FaceRecognitionSecret);
                // 人脸认证
                string url = BaiduApiAppSetting.FaceRecognitionDeleteURL + "?access_token=" + token;
                var option = new Dictionary<string, object>()
                {
                    {"user_id",userId },
                    {"group_id",BaiduApiAppSetting.Group },
                    {"face_token",faceToken }
                };
                var loginResponse = GetFaceRecognitionResponse(option, url);
                return loginResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="imgpath"></param>
        /// <param name="imgType"></param>
        /// <returns></returns>
        public static FaceCheckResponse FaceCheck(string image,string imageType)
        {
            try
            {
                var token = GetAccessToken(BaiduApiAppSetting.FaceRecognitionAppId, BaiduApiAppSetting.FaceRecognitionSecret);
                // 人脸认证
                string url = BaiduApiAppSetting.FaceRecognitionCheckURL + "?access_token=" + token;
                var option = new Dictionary<string, object>()
                {
                    {"image",image },
                    {"image_type",imageType },
                    {"liveness_control",BaiduApiAppSetting.Liveness },
                    {"face_field","quality" }
                };
                Encoding encoding = Encoding.Default;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "post";
                request.ContentType = "application/json";
                request.KeepAlive = true;
                String str = JsonConvert.SerializeObject(option);
                byte[] buffer = encoding.GetBytes(str);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string result = reader.ReadToEnd();
                FaceCheckResponse responseModel = JsonConvert.DeserializeObject<FaceCheckResponse>(result);
                return responseModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static FaceRecognitionResponse GetFaceRecognitionResponse(Dictionary<string, object> option, string url)
        {
            try
            {
                Encoding encoding = Encoding.Default;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "post";
                request.ContentType = "application/json";
                request.KeepAlive = true;
                #region 注释
                //image(必填)            图片信息(总数据大小应小于10M)，图片上传方式根据image_type来判断。注：组内每个uid下的人脸图片数目上限为20张

                //image_type(必填)       图片类型
                //                       BASE64: 图片的base64值，base64编码后的图片数据，编码后的图片大小不超过2M；
                //                       URL: 图片的 URL地址(可能由于网络等原因导致下载图片时间过长)；
                //                       FACE_TOKEN：人脸图片的唯一标识，调用人脸检测接口时，会为每个人脸图片赋予一个唯一的FACE_TOKEN，同一张图片多次检测得到的FACE_TOKEN是同一个。

                //group_id(必填)          用户组id，标识一组用户（由数字、字母、下划线组成），长度限制48B。
                //                        产品建议：根据您的业务需求，可以将需要注册的用户，按照业务划分，分配到不同的group下，例如按照会员手机尾号作为groupid，用于刷脸支付、会员计费消费
                //                        这样可以尽可能控制每个group下的用户数与人脸数，提升检索的准确率

                //user_id(必填)           用户id（由数字、字母、下划线组成），长度限制128B

                //user_info               用户资料，长度限制256B 默认空

                //quality_control         图片质量控制
                //                        NONE: 不进行控制
                //                        LOW:较低的质量要求
                //                        NORMAL: 一般的质量要求
                //                        HIGH: 较高的质量要求
                //                        默认 NONE
                //                        若图片质量不满足要求，则返回结果中会提示质量检测失败

                //liveness_control        活体检测控制
                //                        NONE: 不进行控制
                //                        LOW:较低的活体要求(高通过率 低攻击拒绝率)
                //                        NORMAL: 一般的活体要求(平衡的攻击拒绝率, 通过率)
                //                        HIGH: 较高的活体要求(高攻击拒绝率 低通过率)
                //                        默认NONE
                //                        若活体检测结果不满足要求，则返回结果中会提示活体检测失败

                //action_type             操作方式
                //                        APPEND: 当user_id在库中已经存在时，对此user_id重复注册时，新注册的图片默认会追加到该user_id下
                //                        REPLACE : 当对此user_id重复注册时,则会用新图替换库中该user_id下所有图片
                //                        默认使用APPEND
                #endregion
                String str = JsonConvert.SerializeObject(option);
                byte[] buffer = encoding.GetBytes(str);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string result = reader.ReadToEnd();
                FaceRecognitionResponse responseModel = JsonConvert.DeserializeObject<FaceRecognitionResponse>(result);
                return responseModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 语音合成
        public static byte[] GetAudio(string text)
        {
            var tts = new Baidu.Aip.Speech.Tts(BaiduApiAppSetting.TextToAudioAppId, BaiduApiAppSetting.TextToAudioSecret);
            string cuid = BaiduApiAppSetting.TextToAudioCUID;
            var option = new Dictionary<string, object>()
            {
                {"cuid",cuid },
                {"ctp",1 },//客户端类型选择，web端填写固定值1
                {"lan", BaiduApiAppSetting.Language},//固定值zh。语言选择,目前只有中英文混合模式，填写固定值zh
                {"spd",BaiduApiAppSetting.Speed },//语速，取值0-15，默认为5中语速
                {"pit",BaiduApiAppSetting.Pitch },//音调，取值0-15，默认为5中语调
                {"vol",BaiduApiAppSetting.Volume },//音量，取值0-15，默认为5中音量
                {"per",BaiduApiAppSetting.Pronunciation },//（基础音库）度小宇=1，度小美=0，度逍遥=3，度丫丫=4   （精品音库）度博文=106，度小童=110，度小萌=111，度米朵=103，度小娇=5
                {"aue",3 } //mp3格式
            };
            var temp = tts.Synthesis(text, option);
            if (!temp.Success)
            {
                throw new AbhsException(ErrorCodeEnum.BaiduSpeechError, AbhsErrorMsg.ConstBaiduSpeechError);
            }
            return temp.Data;
        }

        #endregion
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="client_id"></param>
        /// <param name="client_secret"></param>
        /// <returns></returns>
        private static string GetAccessToken(string client_id, string client_secret)
        {
            try
            {
                string authHost = BaiduApiAppSetting.BaiduAccessToken;
                HttpClient client = new HttpClient();
                List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
                paraList.Add(new KeyValuePair<string, string>("grant_type", BaiduApiAppSetting.BaiduGrantType));
                paraList.Add(new KeyValuePair<string, string>("client_id", client_id));
                paraList.Add(new KeyValuePair<string, string>("client_secret", client_secret));
                HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                JObject obj = (JObject)JsonConvert.DeserializeObject(result);
                var token = obj["access_token"].ToString();
                return token;
            }
            catch(Exception ex)
            {
                //throw new AbhsException(ErrorCodeEnum.BaiduTokenError, AbhsErrorMsg.ConstBaiduTokenError);
                throw ex;
            }
        }
    }
}
