using AbhsChinese.Domain.JsonEntity.Answer;
using AbhsChinese.Domain.JsonEntity.Coin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.StudentLessonAnswer
{
    public class Yw_StudentLessonAnswerExt : Yw_StudentLessonAnswer, IJsonTranslator
    {
        /// <summary>
        /// 学生答案对象集合
        /// </summary>
        public List<StudentAnswerCard> Yla_Answer_Obj
        {
            set; get;
        }
        /// <summary>
        /// 学生金币记录集合
        /// </summary>
        public List<StuLessonCoinRecord> Yla_StudentCoin_Obj
        {
            set; get;
        }

        public void ResetJsonValue()
        {
            if (Yla_Answer_Obj == null)
            {
                Yla_StudentAnswer = "";
            }
            else
            {
                Yla_StudentAnswer = JsonConvert.SerializeObject(Yla_Answer_Obj).Trim('[', ']');//记录时去出两边括号
            }
            if (Yla_StudentCoin_Obj == null)
            {
                Yla_StudentCoin = "";
            }
            else
            {
                Yla_StudentCoin = JsonConvert.SerializeObject(Yla_StudentCoin_Obj).Trim('[', ']');//金币记录时去除两边括号
            }
        }
    }
}
