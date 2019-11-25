using AbhsChinese.Domain.Entity.UnitStep;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.UnitStep;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonTranslator
{
    public class UntiStepTranslator : ITranslator<Yw_LessonUnitStep, Yw_LessonUnitStepActions>
    {
        public Yw_LessonUnitStepActions MakeTranslator(Yw_LessonUnitStep entity)
        {
            Yw_LessonUnitStepActions re = new Yw_LessonUnitStepActions();
            if (entity != null)
            {
                re.Yls_CourseId = entity.Yls_CourseId;
                re.Yls_LessonId = entity.Yls_LessonId;
                re.Yls_UnitId = entity.Yls_UnitId;
                re.Yls_UnitIndex = entity.Yls_UnitIndex;
                re.Yls_Status = entity.Yls_Status;
                re.Yls_Steps = entity.Yls_Steps;
                re.Yls_Coins = entity.Yls_Coins;
                re.Yls_CreateTime = entity.Yls_CreateTime;
                re.Yls_UpdateTime = entity.Yls_UpdateTime;
                //创建一个步骤动作集合
                re.Steps = new List<Step>();
                if (re.Yls_Steps != null)
                {
                    JArray items = JsonConvert.DeserializeObject(re.Yls_Steps) as JArray;
                    foreach (JObject o in items)
                    {
                        Step step = new Step();
                        step.StepNum = Convert.ToInt32(o["StepNum"]);
                        step.Actions = new List<StepAction>();

                        string actions = o["Actions"].ToString();
                        JArray actionObjs = JsonConvert.DeserializeObject(actions) as JArray;
                        foreach (JObject action in actionObjs)
                        {
                            #region 根据不同的类型反序列化并添加不同的对象
                            switch ((LessonActionTypeEnum)Convert.ToInt32(action["Type"]))
                            {
                                //设置标题
                                case LessonActionTypeEnum.SetTitle:
                                    step.Actions.Add(JsonConvert.DeserializeObject<SetTitle>(action.ToString()));
                                    break;
                                //设置背景
                                case LessonActionTypeEnum.SetBackground:
                                    step.Actions.Add(JsonConvert.DeserializeObject<SetBackground>(action.ToString()));
                                    break;
                                //小艾说
                                case LessonActionTypeEnum.XiaoAiSay:
                                    step.Actions.Add(JsonConvert.DeserializeObject<XiaoAiSay>(action.ToString()));
                                    break;
                                //小艾变
                                case LessonActionTypeEnum.XiaoAiChange:
                                    step.Actions.Add(JsonConvert.DeserializeObject<XiaoAiChange>(action.ToString()));
                                    break;
                                //插入图片
                                case LessonActionTypeEnum.InsertImg:
                                    step.Actions.Add(JsonConvert.DeserializeObject<InsertImg>(action.ToString()));
                                    break;
                                //插入文字
                                case LessonActionTypeEnum.InsertText:
                                    step.Actions.Add(JsonConvert.DeserializeObject<InsertText>(action.ToString()));
                                    break;
                                //等待
                                case LessonActionTypeEnum.SetWaitMillisecond:
                                    step.Actions.Add(JsonConvert.DeserializeObject<SetWaitMillisecond>(action.ToString()));
                                    break;
                                //移动
                                case LessonActionTypeEnum.MoveDom:
                                    step.Actions.Add(JsonConvert.DeserializeObject<MoveDom>(action.ToString()));
                                    break;
                                //缩放
                                case LessonActionTypeEnum.ScaleDom:
                                    step.Actions.Add(JsonConvert.DeserializeObject<ScaleDom>(action.ToString()));
                                    break;
                                //闪烁
                                case LessonActionTypeEnum.TwinkleDom:
                                    step.Actions.Add(JsonConvert.DeserializeObject<TwinkleDom>(action.ToString()));
                                    break;
                                //隐藏
                                case LessonActionTypeEnum.HideDom:
                                    step.Actions.Add(JsonConvert.DeserializeObject<HideDom>(action.ToString()));
                                    break;
                                //音频
                                case LessonActionTypeEnum.StudyAudio:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudyAudio>(action.ToString()));
                                    break;
                                //视频
                                case LessonActionTypeEnum.StudyVideo:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudyVideo>(action.ToString()));
                                    break;
                                //图文
                                case LessonActionTypeEnum.StudyArticle:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudyArticle>(action.ToString()));
                                    break;
                                //朗读
                                case LessonActionTypeEnum.StudyRecitation:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudyRecitation>(action.ToString()));
                                    break;
                                //快速阅读-简单
                                case LessonActionTypeEnum.StudyFastReadEasy:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudyFastReadEasy>(action.ToString()));
                                    break;
                                //快速阅读
                                case LessonActionTypeEnum.StudyFastRead:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudyFastRead>(action.ToString()));
                                    break;
                                //圈点批注-标色
                                case LessonActionTypeEnum.StudyAnnotation:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudyAnnotation>(action.ToString()));
                                    break;
                                //圈点批注-断句
                                case LessonActionTypeEnum.StudyAnnotation2:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudyAnnotation2>(action.ToString()));
                                    break;
                                //判断题
                                case LessonActionTypeEnum.StudyJudgment:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudyJudgment>(action.ToString()));
                                    break;
                                //连线题
                                case LessonActionTypeEnum.StudyLinking:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudyLinking>(action.ToString()));
                                    break;
                                //选择题
                                case LessonActionTypeEnum.StudyOption:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudyOption>(action.ToString()));
                                    break;
                                //选择填空
                                case LessonActionTypeEnum.StudyOptionFill:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudyOptionFill>(action.ToString()));
                                    break;
                                //填空题
                                case LessonActionTypeEnum.StudyFill:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudyFill>(action.ToString()));
                                    break;
                                //主观题
                                case LessonActionTypeEnum.StudySubjective:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudySubjective>(action.ToString()));
                                    break;
                                //田字格写字
                                case LessonActionTypeEnum.StudyCalligraphy:
                                    step.Actions.Add(JsonConvert.DeserializeObject<StudyCalligraphy>(action.ToString()));
                                    break;
                            }
                            #endregion
                        }
                        re.Steps.Add(step);
                    }
                }
                return re;
            }
            return null;
        }
    }
}
