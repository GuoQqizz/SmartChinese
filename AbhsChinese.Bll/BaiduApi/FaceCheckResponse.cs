using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll.BaiduApi
{
    public class FaceCheckResponse
    {
        public int error_code { get; set; }

        public string error_msg { get; set; }
        
        public Result result { get; set; }
    }

    public class Result
    {
        /// <summary>
        /// 检测到的图片中的人脸数量
        /// </summary>
        public int face_num { get; set; }

        public List<face_list> face_list { get; set; }
    }

    public class face_list
    {
        /// <summary>
        /// 人脸图片的唯一标识
        /// </summary>
        public string face_token { get; set; }

        /// <summary>
        /// 人脸置信度，范围【0~1】，代表这是一张人脸的概率，0最小、1最大。
        /// </summary>
        public double face_probability { get; set; }

        /// <summary>
        /// 人脸旋转角度参数
        /// </summary>
        public Angle angle { get; set; }

        /// <summary>
        /// 人脸质量信息
        /// </summary>
        public Quality quality { get; set; }

        public LiveNess liveness { get; set; }
    }

    public class LiveNess
    {
        /// <summary>
        /// 活体范围
        /// </summary>
        public double livemapscore { get; set; }
    }

    public class Quality
    {
        /// <summary>
        /// 人脸模糊程度，范围[0~1]，0表示清晰，1表示模糊
        /// </summary>
        public double blur { get; set; }

        /// <summary>
        /// 人脸完整度，0或1, 0为人脸溢出图像边界，1为人脸都在图像边界内
        /// </summary>
        public Int64 completeness { get; set; }

        /// <summary>
        /// 取值范围在[0~255], 表示脸部区域的光照程度 越大表示光照越好
        /// </summary>
        public double illumination { get; set; }

        /// <summary>
        ///  人脸各部分遮挡的概率，范围[0~1]，0表示完整，1表示不完整
        /// </summary>
        public Occlusion occlusion { get; set; }
    }

    /// <summary>
    /// 人脸各部分遮挡的概率，范围[0~1]，0表示完整，1表示不完整
    /// left_eye : 0.6, #左眼被遮挡的阈值
    /// right_eye : 0.6, #右眼被遮挡的阈值
    /// nose : 0.7, #鼻子被遮挡的阈值
    /// mouth : 0.7, #嘴巴被遮挡的阈值
    /// left_cheek : 0.8, #左脸颊被遮挡的阈值
    /// right_cheek : 0.8, #右脸颊被遮挡的阈值
    /// chin_contour : 0.6, #下巴被遮挡阈值
    /// </summary>
    public class Occlusion
    {
        /// <summary>
        /// 左眼遮挡比例，[0-1] ，1表示完全遮挡
        /// </summary>
        public double left_eye { get; set; }

        /// <summary>
        /// 右眼遮挡比例，[0-1] ， 1表示完全遮挡
        /// </summary>
        public double right_eye { get; set; }

        /// <summary>
        /// 鼻子遮挡比例，[0-1] ， 1表示完全遮挡
        /// </summary>
        public double nose { get; set; }

        /// <summary>
        /// 嘴巴遮挡比例，[0-1] ， 1表示完全遮挡
        /// </summary>
        public double mouth { get; set; }

        /// <summary>
        /// 左脸颊遮挡比例，[0-1] ， 1表示完全遮挡
        /// </summary>
        public double left_cheek { get; set; }

        /// <summary>
        /// 右脸颊遮挡比例，[0-1] ， 1表示完全遮挡
        /// </summary>
        public double rigth_cheek { get; set; }

        /// <summary>
        /// 下巴遮挡比例，，[0-1] ， 1表示完全遮挡
        /// </summary>
        public double chin { get; set; }
    }

    /// <summary>
    /// 人脸旋转角度参数
    /// </summary>
    public class Angle
    {
        /// <summary>
        /// 三维旋转之左右旋转角[-90(左), 90(右)]
        /// </summary>
        public double yaw { get; set; }

        /// <summary>
        /// 三维旋转之俯仰角度[-90(上), 90(下)]
        /// </summary>
        public double pitch { get; set; }

        /// <summary>
        /// 平面内旋转角[-180(逆时针), 180(顺时针)]
        /// </summary>
        public double roll { get; set; }
    }
}
