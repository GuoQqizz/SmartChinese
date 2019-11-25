using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.StudentLessonAnswer;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using AbhsChinese.Domain.JsonEntity.Coin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonTranslator
{
    public class StuLessonAnsTranslator : ITranslator<Yw_StudentLessonAnswer, Yw_StudentLessonAnswerExt>
    {
        public Yw_StudentLessonAnswerExt MakeTranslator(Yw_StudentLessonAnswer entity)
        {
            Yw_StudentLessonAnswerExt realEntity = entity.ConvertTo<Yw_StudentLessonAnswerExt>();

            if (!string.IsNullOrEmpty(entity.Yla_StudentAnswer))
            {
                realEntity.Yla_Answer_Obj = new List<StudentAnswerCard>();
                JArray arr = JsonConvert.DeserializeObject($"[{entity.Yla_StudentAnswer.Trim(',')}]") as JArray;//数据库存储时没有数组的括号,并且两边可能会有","需要去除
                foreach (var o in arr)
                {
                    StudentAnswerCard card = new StudentAnswerCard();
                    card.UseTime = Convert.ToInt32(o["UTim"]);
                    card.SubmitTime = o["STim"].ToString();
                    card.TotalStars = Convert.ToInt32(o["EStar"]);
                    card.TotalCoins = Convert.ToInt32(o["TCoin"]);
                    card.Part = Convert.ToInt32(o["Part"]);
                    JArray answers = JsonConvert.DeserializeObject(o["Anws"].ToString()) as JArray;
                    foreach (var a in answers)
                    {
                        #region 根据类型添加不同的对象
                        switch ((SubjectTypeEnum)Convert.ToInt32(a["Type"]))
                        {
                            case SubjectTypeEnum.选择题:
                                {
                                    card.AnswerCollection.Add(JsonConvert.DeserializeObject<StuSelectAnswer>(a.ToString()));
                                }
                                break;
                            case SubjectTypeEnum.判断题:
                                {
                                    card.AnswerCollection.Add(JsonConvert.DeserializeObject<StuTrueFalseAnswer>(a.ToString()));
                                }
                                break;
                            case SubjectTypeEnum.填空题:
                                {
                                    card.AnswerCollection.Add(JsonConvert.DeserializeObject<StuBlankAnswer>(a.ToString()));
                                }
                                break;
                            case SubjectTypeEnum.选择填空:
                                {
                                    card.AnswerCollection.Add(JsonConvert.DeserializeObject<StuSelectBlankAnswer>(a.ToString()));
                                }
                                break;
                            case SubjectTypeEnum.连线题:
                                {
                                    card.AnswerCollection.Add(JsonConvert.DeserializeObject<StuMatchAnswer>(a.ToString()));
                                }
                                break;
                            case SubjectTypeEnum.主观题:
                                {
                                    card.AnswerCollection.Add(JsonConvert.DeserializeObject<StuFreeAnswer>(a.ToString()));
                                }
                                break;
                            case SubjectTypeEnum.圈点批注标色:
                                {
                                    card.AnswerCollection.Add(JsonConvert.DeserializeObject<StuMarkColorAnswer>(a.ToString()));
                                }
                                break;
                            case SubjectTypeEnum.圈点批注断句:
                                {
                                    card.AnswerCollection.Add(JsonConvert.DeserializeObject<StuMarkCutAnswer>(a.ToString()));
                                }
                                break;
                        }
                        #endregion
                    }
                    realEntity.Yla_Answer_Obj.Add(card);
                }

            }
            if (!string.IsNullOrEmpty(entity.Yla_StudentCoin))
            {
                realEntity.Yla_StudentCoin_Obj = new List<StuLessonCoinRecord>();
                JArray arr = JsonConvert.DeserializeObject($"[{entity.Yla_StudentCoin.Trim(',')}]") as JArray;
                //数据库存储时没有数组的括号,并且两边可能会有","需要去除
                foreach (var o in arr)
                {
                    StuLessonCoinRecord coin = new StuLessonCoinRecord();
                    coin.UseTime = Convert.ToInt32(o["UTim"]);
                    coin.SubmitTime = o["STim"].ToString();
                    coin.TotalCoins = Convert.ToInt32(o["TCon"]);
                    coin.Part = Convert.ToInt32(o["Part"]);
                    coin.CoinCollection = new List<StuLessonCoinItem>();
                    JArray items = JsonConvert.DeserializeObject(o["Cons"].ToString()) as JArray;
                    foreach (var i in items)
                    {
                        coin.CoinCollection.Add(JsonConvert.DeserializeObject<StuLessonCoinItem>(i.ToString()));
                    }
                    realEntity.Yla_StudentCoin_Obj.Add(coin);
                }
            }
            return realEntity;
        }
    }
}
