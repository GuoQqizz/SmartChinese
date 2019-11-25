using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiebaNet.Segmenter;

namespace AbhsChinese.Code.Common
{
    #region 分词类型
    public enum JiebaTypeEnum
    {
        /// <summary>
        /// 精确模式---最基础和自然的模式，试图将句子最精确地切开，适合文本分析
        /// </summary>
        Default,
        /// <summary>
        /// 全模式---可以成词的词语都扫描出来, 速度更快，但是不能解决歧义
        /// </summary>
        CutAll,
        /// <summary>
        /// 搜索引擎模式---在精确模式的基础上对长词再次切分，提高召回率，适合用于搜索引擎分词
        /// </summary>
        CutForSearch,
        /// <summary>
        /// 精确模式-不带HMM
        /// </summary>
        Other
    }
    #endregion

    #region 结巴分词
    /// <summary>
    /// 结巴分词
    /// </summary>
    public static class WordSplitHelper
    {
        /// <summary>
        /// 获取分词之后的字符串集合
        /// </summary>
        /// <param name="objStr"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetSplitWords(string objStr, JiebaTypeEnum type = JiebaTypeEnum.Default)
        {
            var jieba = new JiebaSegmenter();
            switch (type)
            {
                case JiebaTypeEnum.Default:
                    return jieba.Cut(objStr);                 //精确模式
                case JiebaTypeEnum.CutAll:
                    return jieba.Cut(objStr, cutAll: true);   //全模式
                case JiebaTypeEnum.CutForSearch:
                    return jieba.CutForSearch(objStr);        //搜索引擎模式
                default:
                    return jieba.Cut(objStr, false, false);   //精确模式-不带HMM
            }
        }

        /// <summary>
        /// 获取分词之后的字符串
        /// </summary>
        /// <param name="objStr"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSplitWordStr(this string objStr, JiebaTypeEnum type = JiebaTypeEnum.Default)
        {
            var words = GetSplitWords(objStr, type);
            //没结果则返回空字符串
            if (words == null || words.Count() < 1)
            {
                return string.Empty;
            }
            for (int i = 0; i < words.Count(); i++)
            {
                if (words.ToArray()[i].Length < 2)
                {
                    words = DeleteArr(i, words.ToArray());
                }
            }

            words = words.Distinct();//去重
            return string.Join(",", words);
        }

        /// <summary>
        /// 获取分词之后去重的集合
        /// </summary>
        /// <param name="objStr"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetSplitWordList(this string objStr, JiebaTypeEnum type = JiebaTypeEnum.Default)
        {
            var words = GetSplitWords(objStr, type);
            //没结果则返回空字符串
            if (words == null || words.Count() < 1)
            {
                return null;
            }

            for (int i = 0; i < words.Count(); i++)
            {
                if (words.ToArray()[i].Length<2)
                {
                   words = DeleteArr(i, words.ToArray());
                }
            }
            words = words.Distinct();//去重
            return words;
        }

        //通过下标删除数组方法
        private static string[] DeleteArr(int num, string[] arr)
        {
            List<string> list = new List<string>();
            foreach (string i in arr)
            {
                list.Add(i);//把数组的每一个元素保存到一个集合中
            }
            list.RemoveAt(num);//根据集合删除指定下标的元素
            arr = new string[list.Count];//重新new一个数组
            for (int j = 0; j < list.Count; j++)
            {
                arr[j] = list[j];//把删除后的集合每一个保存到该数组
            }
            return arr;//返回该数组
        }
    }
    #endregion
}
