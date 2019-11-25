using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.AbhsResource;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AbhsChinese.Admin.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// 默认规则
        /// </summary>
        public static Dictionary<FileTypeEnum, FileRule> DefaultRules = new Dictionary<FileTypeEnum, FileRule>();

        /// <summary>
        /// 自定义规则
        /// </summary>
        public static Dictionary<FileTypeEnum, FileRule> CustomRules = new Dictionary<FileTypeEnum, FileRule>();
        static FileHelper()
        {
            //DefaultRules.Add();
            DefaultRules.Add(FileTypeEnum.Images, new FileRule()
            {
                FileExtensions = @"(\.png)|(\.jpg)|(\.gif)|(\.bmp)",
                Size = 2 * 1024 * 1024,
                Type = FileTypeEnum.Images
            });
            DefaultRules.Add(FileTypeEnum.Video, new FileRule()
            {
                FileExtensions = @"(\.mp4)",
                Size = 50 * 1024 * 1024,
                Type = FileTypeEnum.Video
            });
            DefaultRules.Add(FileTypeEnum.Audio, new FileRule()
            {
                FileExtensions = @"(\.mp3)",
                Size = 30 * 1024 * 1024,
                Type = FileTypeEnum.Audio
            });
            DefaultRules.Add(FileTypeEnum.Zip, new FileRule()
            {
                FileExtensions = @"(\.zip)|(\.rar)",
                Size = 100 * 1024 * 1024,
                Type = FileTypeEnum.Zip
            });
        }
        /// <summary>
        /// 设置自定义规则
        /// </summary>
        /// <param name="rule"></param>
        public static void SetRule(FileRule rule)
        {
            CustomRules[rule.Type] = rule;
        }
        /// <summary>
        /// 清除自定义规则
        /// </summary>
        /// <param name="rule"></param>
        public static void ClearRule(FileRule rule)
        {
            if (CustomRules.ContainsKey(rule.Type))
            {
                CustomRules.Remove(rule.Type);
            }
        }

        public static FileTypeEnum CheckFile(HttpPostedFileBase file)
        {

            FileTypeEnum result = FileTypeEnum.UnKnow;
            foreach (var rule in DefaultRules.Values)
            {
                var tmp = rule;
                if (CustomRules.ContainsKey(rule.Type))
                {
                    tmp = CustomRules[rule.Type];
                }
                if (CheckFile(file, tmp) != FileTypeEnum.UnKnow)
                {
                    result = rule.Type;
                    break;
                }
            }
            return result;
        }

        public static FileTypeEnum CheckFile(HttpPostedFileBase file, FileRule rule)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            var size = file.ContentLength;
            FileTypeEnum result = FileTypeEnum.UnKnow;
            if (Regex.IsMatch(extension, rule.FileExtensions))
            {
                if (rule.Size > 0)
                {
                    if (rule.Size < size)
                    {
                        throw new AbhsException(ErrorCodeEnum.FileSizeOverFlow, AbhsErrorMsg.ConstFileSizeError);
                    }
                }
                result = rule.Type;
            }
            return result;
        }
    }
    public class FileRule
    {
        /// <summary>
        /// 限制尺寸 （以字节为单位）
        /// 0 不限制
        /// </summary>
        public int Size { get; set; }

        public string FileExtensions { get; set; }

        public FileTypeEnum Type { get; set; }
    }

    public enum FileTypeEnum
    {
        //1视频 2 音频 3 图片 4 zip
        Video = 1,
        Audio = 2,
        Images = 3,
        Zip = 4,
        UnKnow = -1,
    }
}