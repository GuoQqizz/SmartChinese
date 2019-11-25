using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.StudyTaskAnswer;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbhsChinese.Domain.JsonEntity.Answer.StuBlankAnswer;

namespace AbhsChinese.Domain.JsonTranslator
{
    public class StuStudyTaskAnsTranslator : ITranslator<Yw_StudentStudyTaskAnswer, Yw_StudentStudyTaskAnswerExt>
    {
        public Yw_StudentStudyTaskAnswerExt MakeTranslator(Yw_StudentStudyTaskAnswer entity)
        {
            Yw_StudentStudyTaskAnswerExt realEntity = entity.ConvertTo<Yw_StudentStudyTaskAnswerExt>();

            if (!string.IsNullOrEmpty(entity.Yta_StudentAnswer))
            {
                realEntity.Yta_Answer_Obj = new StudentAnswerCard();

                JObject jo = JsonConvert.DeserializeObject(entity.Yta_StudentAnswer) as JObject;

                realEntity.Yta_Answer_Obj.UseTime = Convert.ToInt32(jo["UTim"]);
                realEntity.Yta_Answer_Obj.SubmitTime = jo["STim"].ToString();
                realEntity.Yta_Answer_Obj.TotalStars = (int)jo["EStar"];
                realEntity.Yta_Answer_Obj.TotalCoins = (int)jo["TCoin"];
                realEntity.Yta_Answer_Obj.Part = (int)jo["Part"];

                string innerJson = jo["Anws"].ToString();
                JArray items = JsonConvert.DeserializeObject(innerJson) as JArray;

                foreach (JObject o in items)
                {
                    if ((int)o["Type"] == (int)SubjectTypeEnum.选择题)
                    {
                        StuSelectAnswer an = JsonConvert.DeserializeObject<StuSelectAnswer>(o.ToString());
                        realEntity.Yta_Answer_Obj.AnswerCollection.Add(an);
                    }
                    else if ((int)o["Type"] == (int)SubjectTypeEnum.填空题)
                    {
                        StuBlankAnswer an = JsonConvert.DeserializeObject<StuBlankAnswer>(o.ToString());
                        realEntity.Yta_Answer_Obj.AnswerCollection.Add(an);
                    }
                    else if ((int)o["Type"] == (int)SubjectTypeEnum.判断题)
                    {
                        StuTrueFalseAnswer an = JsonConvert.DeserializeObject<StuTrueFalseAnswer>(o.ToString());
                        realEntity.Yta_Answer_Obj.AnswerCollection.Add(an);
                    }
                    else if ((int)o["Type"] == (int)SubjectTypeEnum.选择填空)
                    {
                        StuSelectBlankAnswer an = JsonConvert.DeserializeObject<StuSelectBlankAnswer>(o.ToString());
                        realEntity.Yta_Answer_Obj.AnswerCollection.Add(an);
                    }
                    else if ((int)o["Type"] == (int)SubjectTypeEnum.连线题)
                    {
                        StuMatchAnswer an = JsonConvert.DeserializeObject<StuMatchAnswer>(o.ToString());
                        realEntity.Yta_Answer_Obj.AnswerCollection.Add(an);
                    }
                    else if ((int)o["Type"] == (int)SubjectTypeEnum.主观题)
                    {
                        StuFreeAnswer an = JsonConvert.DeserializeObject<StuFreeAnswer>(o.ToString());
                        realEntity.Yta_Answer_Obj.AnswerCollection.Add(an);
                    }
                    else if ((int)o["Type"] == (int)SubjectTypeEnum.圈点批注标色)
                    {
                        StuMarkColorAnswer an = JsonConvert.DeserializeObject<StuMarkColorAnswer>(o.ToString());
                        realEntity.Yta_Answer_Obj.AnswerCollection.Add(an);
                    }
                    else if ((int)o["Type"] == (int)SubjectTypeEnum.圈点批注断句)
                    {
                        StuMarkCutAnswer an = JsonConvert.DeserializeObject<StuMarkCutAnswer>(o.ToString());
                        realEntity.Yta_Answer_Obj.AnswerCollection.Add(an);
                    }
                }
            }
            return realEntity;
        }
    }
}
