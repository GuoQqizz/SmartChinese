using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Bll;
using System.Collections.Generic;
using System.Linq;
using AbhsChinese.Web.Models.CurriculumSetViewModel;
using Newtonsoft.Json;
using AbhsChinese.Domain.Dto.Request.Student;
using AbhsChinese.Code.Common;
using Newtonsoft.Json.Linq;
using AbhsChinese.Domain.JsonEntity.Coin;
using AbhsChinese.Domain.JsonEntity.Answer;
using AbhsChinese.Domain.Enum;
using static AbhsChinese.Domain.JsonEntity.Answer.StuBlankAnswer;

namespace AbhsChinese.Test.Controller
{
    [TestClass]
    public class LessonCenterController_GetLessonPage_Test
    {
        [TestMethod]
        public void TestMethod1()
        {
            int page = 1;
            int pagesize = 5;
            int courseid = 10016;
            int lessonid = 10047;

            LessonUnitBll bll = new LessonUnitBll();
            var pager = new Code.Common.PagingObject()
            {
                PageIndex = page,//开始页(包含)
                PageSize = pagesize,//条数
                TotalCount = 0
            };
            var pages = bll.SelectUnitByNext(new Domain.Dto.Request.DtoLessonUnitSearch()
            {
                CourseID = courseid,
                LessonID = lessonid,
                StudentID = 10000,
                Pagination = pager
            });
            List<int> mediaIDs = new List<int>();
            List<int> textIDs = new List<int>();
            var list = pages.Select(p => new Page()
            {
                pageId = p.Id,
                pageNum = p.Index,
                pageName = p.Name,
                steps = p.Steps.Select(s => new Step()
                {
                    stepNum = s.StepNum,
                    actions = s.Actions.Select(a => ActionTranslator.DataToViewData(a, mediaIDs, textIDs)).ToList()
                }).ToList()
            }).ToList();
            //如果媒体id或文本id有值的话
            if (mediaIDs.Count > 0 || textIDs.Count > 0)
            {
                ResourceBll rbll = new ResourceBll();
                var medias = rbll.GetMediaObjectByIdList(mediaIDs.Distinct().ToList());//获取所有的媒体对象字典
                var texts = rbll.GetTextObjectByIdList(textIDs.Distinct().ToList());//获取文本对象字典

                list.ForEach(p =>
                {
                    p.steps.ForEach(s =>
                    {
                        s.actions.ForEach(a =>
                        {
                            ActionTranslator.SetViewDataMedia(a, medias, texts);
                        });
                    });
                });
            }
            var str = JsonConvert.SerializeObject(list);

        }

        [TestMethod]
        public void TestMethod2()
        {
            DtoStudentCourseSearch search = new DtoStudentCourseSearch()
            {
                IsFinished = false,
                Pagination = new Code.Common.PagingObject() {
                    PageIndex = 1,
                    PageSize = 10,
                    TotalCount = 0
                },
                StudentId = 10000
            };
            StudentStudyBll bll = new StudentStudyBll();
            var data = bll.GetCourseProgressByStudentSearch(search);
            string json = JsonConvert.SerializeObject(data);
        }

        [TestMethod]
        public void SubmintProcressData() {
            var dataStr = "{\"coins\":[],\"answers\":[],\"useTime\":5,\"courseId\":10000,\"lessonId\":10000,\"lessonNum\":1,\"unitId\":10000,\"unitNum\":1,\"totalUnitNum\":14,\"progressId\":10001,\"progressKey\":\"71441bd833884cc78e6ae8434e552ff1\",\"coinsKey\":\"E490AD625ABC1D994DA1F5D897591D3A0A745B468D778F7B\"}";

            DtoStudnetUnitSubmit info = new DtoStudnetUnitSubmit();
            #region json字符串填充数据
            try
            {
                JObject data = JsonConvert.DeserializeObject(dataStr) as JObject;
                info.StudnetId = 10000;
                info.CoruseId = Convert.ToInt32(data["courseId"]);//课程id
                info.LessonId = Convert.ToInt32(data["lessonId"]);//课时id
                info.LessonNum = Convert.ToInt32(data["lessonNum"]);//课时序号
                info.UnitID = Convert.ToInt32(data["unitId"]);//单元(讲义页)id
                string coinKey = Encrypt.DecryptQueryString(data["coinsKey"].ToString());//金币加密字符串
                string[] check = coinKey.Split('_');
                //验证金币加密串是否可用
                //这个验证方式要与GetLessonPage方法中加密字符串对应
                if (check.Length == 4 && check[1] == info.UnitID.ToString() && check[2] == info.LessonId.ToString() && check[3] == info.CoruseId.ToString())
                {
                    info.AllCoin = Convert.ToInt32(check[0]);//本页最大金币数
                    info.UnitNum = Convert.ToInt32(data["unitNum"]);//单元(讲义页)序号
                    info.UseTime = Convert.ToInt32(data["useTime"]);//学习时长(秒)
                    info.ProgressID = Convert.ToInt32(data["progressId"]);//学习进度id
                    info.ProgressKey = data["progressKey"].ToString();//学习进度key
                    info.TotalUnitNum = Convert.ToInt32(data["totalUnitNum"]);//最大学习数
                    #region 填充答题结果
                    JArray answers = JsonConvert.DeserializeObject(data["answers"].ToString()) as JArray;
                    foreach (var a in answers)
                    {
                        info.Answers.Add(TranslateAnswer(a.ToString()));
                    }
                    #endregion
                    #region 填充金币记录
                    JArray coins = JsonConvert.DeserializeObject(data["coins"].ToString()) as JArray;
                    foreach (var c in coins)
                    {
                        JObject o = JsonConvert.DeserializeObject(c.ToString()) as JObject;
                        info.Coins.Add(new StuLessonCoinItem
                        {
                            ActionId = o["acid"].ToString(),
                            ActionCoins = Convert.ToInt32(o["acin"]),
                            ActionType = o["type"].ToString(),
                            GetCoins = Convert.ToInt32(o["gcin"])
                        });
                    }
                    #endregion                 
                }
                else
                {
                    throw new Exception("金币加密数据错误");
                }

            }
            catch
            {
                throw new Exception("字符串转对象错误");
            }
            #endregion
            StudentStudyBll bll = new StudentStudyBll();
            int result = bll.SubmitStudyProgress(info);//提交数据结果
            var r = new JsonResponse<object>();
            #region 根据结果填充返回数据
            switch (result)
            {
                case 0://成功
                    r.State = true;
                    break;
                case -1://课时进度秘钥错误
                    r.State = false;
                    r.ErrorCode = result;
                    r.ErrorMsg = "课时进度秘钥错误";
                    break;
                case -2://数据重复提交
                    r.State = false;
                    r.ErrorCode = result;
                    r.ErrorMsg = "数据重复提交";
                    break;
                case -3://金币数目错误
                    r.State = false;
                    r.ErrorCode = result;
                    r.ErrorMsg = "金币数目错误";
                    break;
                default:
                    r.State = false;
                    r.ErrorCode = result;
                    r.ErrorMsg = "未知错误";
                    break;
            }
            #endregion

        }


        /// <summary>
        /// 将字符串转换成学生答题结果对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private StudentAnswerBase TranslateAnswer(string json)
        {
            JObject o = JsonConvert.DeserializeObject(json) as JObject;
            StudentAnswerBase data = null;
            switch ((SubjectTypeEnum)Convert.ToInt32(o["type"]))
            {
                case SubjectTypeEnum.选择题:
                    data = new StuSelectAnswer()
                    {
                        Type = (int)SubjectTypeEnum.选择题,
                        Answers = JsonConvert.DeserializeObject<List<int>>(o["sanw"].ToString()),
                        OptionSequences = JsonConvert.DeserializeObject<List<int>>(o["sops"].ToString())
                    };
                    break;
                case SubjectTypeEnum.判断题:
                    data = new StuTrueFalseAnswer()
                    {
                        Type = (int)SubjectTypeEnum.判断题,
                        Answer = Convert.ToInt32(o["sanw"])
                    };
                    break;
                case SubjectTypeEnum.填空题:
                    data = new StuBlankAnswer()
                    {
                        Type = (int)SubjectTypeEnum.填空题,
                        Answers = JsonConvert.DeserializeObject<List<BlankAnswerItem>>(o["sanw"].ToString())
                    };
                    break;
                case SubjectTypeEnum.选择填空:
                    data = new StuSelectBlankAnswer()
                    {
                        Type = (int)SubjectTypeEnum.选择填空,
                        Answers = JsonConvert.DeserializeObject<List<int[]>>(o["sanw"].ToString()),
                        OptionSequences = JsonConvert.DeserializeObject<List<int>>(o["sops"].ToString())
                    };
                    break;
                case SubjectTypeEnum.连线题:
                    data = new StuMatchAnswer()
                    {
                        Type = (int)SubjectTypeEnum.连线题,
                        Answers = JsonConvert.DeserializeObject<List<int[]>>(o["sanw"].ToString()),
                        LeftOptionSequences = JsonConvert.DeserializeObject<List<int>>(o["slos"].ToString()),
                        RightOptionSequences = JsonConvert.DeserializeObject<List<int>>(o["sros"].ToString())
                    };
                    break;
                case SubjectTypeEnum.主观题:
                    data = new StuFreeAnswer()
                    {
                        Type = (int)SubjectTypeEnum.主观题,
                        Answer = o["sanw"].ToString()
                    };
                    break;
                case SubjectTypeEnum.圈点批注标色:
                    data = new StuMarkColorAnswer()
                    {
                        Type = (int)SubjectTypeEnum.圈点批注标色,
                        Answers = JsonConvert.DeserializeObject<List<int[]>>(o["sanw"].ToString())
                    };
                    break;
                case SubjectTypeEnum.圈点批注断句:
                    data = new StuMarkCutAnswer()
                    {
                        Type = (int)SubjectTypeEnum.圈点批注断句,
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
