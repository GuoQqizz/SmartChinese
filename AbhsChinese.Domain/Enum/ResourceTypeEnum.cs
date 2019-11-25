using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Enum
{
    public enum ResourceTypeEnum
    {
        文本资源 = 1,
        多媒体资源 = 2,
        题目 = 3
    }

    public enum TextResourceTypeEnum
    {
        现代文 = 1, 
        文言文 = 2,
        诗词 = 3,
        作文 = 4,
        语基 = 5
    }

    public enum MediaResourceTypeEnum
    {
        视频 = 101,
        音频 = 102,
        图片 = 103,
        小艾说 = 104,
        开场语 = 105,
        小艾变 = 106
    }

    public enum MediaObjectTypeEnum
    {
        图片 = 1,
        音频 = 2,
        视频 = 3
    }

    public enum ResourceGroupStatusEnum
    {
        已启用 = 1,
        未启用 = 2
    }
}
