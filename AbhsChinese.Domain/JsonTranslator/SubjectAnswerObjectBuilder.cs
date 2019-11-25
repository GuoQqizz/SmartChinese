using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using static AbhsChinese.Domain.JsonEntity.Answer.StuBlankAnswer;

namespace AbhsChinese.Domain.JsonTranslator
{
    public static class SubjectAnswerObjectBuilder
    {
        /// <summary>
        /// 将字符串转换成学生答题结果对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static StudentAnswerBase TranslateAnswer(string json)
        {
            JObject o = JsonConvert.DeserializeObject(json) as JObject;
            StudentAnswerBase data = null;
            switch ((SubjectTypeEnum)Convert.ToInt32(o["type"]))
            {
                case SubjectTypeEnum.选择题:
                    data = new StuSelectAnswer()
                    {
                        Answers = JsonConvert.DeserializeObject<List<int>>(o["sanw"].ToString()),
                        OptionSequences = JsonConvert.DeserializeObject<List<int>>(o["sops"].ToString())
                    };
                    break;

                case SubjectTypeEnum.判断题:
                    data = new StuTrueFalseAnswer()
                    {
                        Answer = Convert.ToInt32(o["sanw"])
                    };
                    break;

                case SubjectTypeEnum.填空题:
                    data = new StuBlankAnswer()
                    {
                        Answers = JsonConvert.DeserializeObject<List<BlankAnswerItem>>(o["sanw"].ToString())
                    };
                    break;

                case SubjectTypeEnum.选择填空:
                    data = new StuSelectBlankAnswer()
                    {
                        Answers = JsonConvert.DeserializeObject<List<int[]>>(o["sanw"].ToString()),
                        OptionSequences = JsonConvert.DeserializeObject<List<int>>(o["sops"].ToString())
                    };
                    break;

                case SubjectTypeEnum.连线题:
                    data = new StuMatchAnswer()
                    {
                        Answers = JsonConvert.DeserializeObject<List<int[]>>(o["sanw"].ToString()),
                        LeftOptionSequences = JsonConvert.DeserializeObject<List<int>>(o["slos"].ToString()),
                        RightOptionSequences = JsonConvert.DeserializeObject<List<int>>(o["sros"].ToString())
                    };
                    break;

                case SubjectTypeEnum.主观题:
                    data = new StuFreeAnswer()
                    {
                        Answer = o["sanw"].ToString()
                    };
                    break;

                case SubjectTypeEnum.圈点批注标色:
                    data = new StuMarkColorAnswer()
                    {
                        Answers = JsonConvert.DeserializeObject<List<int[]>>(o["sanw"].ToString())
                    };
                    break;

                case SubjectTypeEnum.圈点批注断句:
                    data = new StuMarkCutAnswer()
                    {
                        Answers = JsonConvert.DeserializeObject<List<int>>(o["sanw"].ToString())
                    };
                    break;
            }
            if (data != null)
            {
                data.SubjectId = Convert.ToInt32(o["sbId"]);
                data.ResultStars = Convert.ToInt32(o["rtar"]);
                data.SubjectCoins = Convert.ToInt32(o["scon"]);
                data.ResultCoins = Convert.ToInt32(o["rcon"]);
                data.KnowledgeId = Convert.ToInt32(o["kdge"]);
            }
            return data;
        }
    }
}