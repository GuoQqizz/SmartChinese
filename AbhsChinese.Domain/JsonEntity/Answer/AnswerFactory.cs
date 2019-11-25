using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.Answer
{
    public static class AnswerFactory
    {
        public static StudentAnswerBase ConvertByType(SubjectTypeEnum type, string answer)
        {
            StudentAnswerBase res = null;
            try
            {
                switch (type)
                {
                    case SubjectTypeEnum.选择题:
                        res = JsonConvert.DeserializeObject<StuSelectAnswer>(answer);
                        break;
                    case SubjectTypeEnum.判断题:
                        res = JsonConvert.DeserializeObject<StuTrueFalseAnswer>(answer);
                        break;
                    case SubjectTypeEnum.填空题:
                        res = JsonConvert.DeserializeObject<StuBlankAnswer>(answer);
                        break;
                    case SubjectTypeEnum.选择填空:
                        res = JsonConvert.DeserializeObject<StuSelectBlankAnswer>(answer);
                        break;
                    case SubjectTypeEnum.连线题:
                        res = JsonConvert.DeserializeObject<StuMatchAnswer>(answer);
                        break;
                    case SubjectTypeEnum.主观题:
                        res = JsonConvert.DeserializeObject<StuFreeAnswer>(answer);
                        break;
                    case SubjectTypeEnum.圈点批注标色:
                        res = JsonConvert.DeserializeObject<StuMarkColorAnswer>(answer);
                        break;
                    case SubjectTypeEnum.圈点批注断句:
                        res = JsonConvert.DeserializeObject<StuMarkCutAnswer>(answer);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                //return null;
            }
            return res;
        }
    }
}
