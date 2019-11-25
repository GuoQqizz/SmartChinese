using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Request.Student;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using AbhsChinese.Domain.JsonEntity.Coin;
using AbhsChinese.Domain.JsonTranslator;
using AbhsChinese.Web.Models.CurriculumSetViewModel;
using AbhsChinese.Web.Models.LearningCenter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static AbhsChinese.Domain.JsonEntity.Answer.StuBlankAnswer;

namespace AbhsChinese.Web.Controllers
{
    /// <summary>
    /// 学习中心控制器
    /// </summary>
    public class LearningCenterController : BaseController
    {
        // GET: LearningCenter
        public ActionResult Index()
        {
            SumStudentBll sumStudentBll = new SumStudentBll();
            DtoSumStudent student = sumStudentBll.GetSumByStuDate(GetCurrentUser().StudentId);
            student.Name = GetCurrentUser().Name;
            student.Sex = GetCurrentUser().Sex;
            if (!string.IsNullOrEmpty(WebHelper.GetCookie("CashVoucher")))
            {
                student.ShowTicket = true;
                WebHelper.RemoveCookie("CashVoucher");
            }

            return View(student);
        }

        public ActionResult CoruseInfo(int id)
        {
            StudentStudyBll bll = new StudentStudyBll();
            int studentid = GetCurrentUser().StudentId;
            StudentCourseInfoViewModel info = bll.GetCourseProgress(studentid, id).ConvertTo<StudentCourseInfoViewModel>();
            info.TeacherImg = ConfigurationManager.AppSettings["OssHostUrl"] + info.TeacherImg;
            var list = bll.GetLessonProgressByStudent(studentid, id);
            info.StudiedLesson = list.Where(s => s.IsStudyOver).ToList();
            info.NotStudyLesson = list.Where(s => !s.IsStudyOver).ToList();
            return View(info);
        }

        /// <summary>
        /// 课时学习视图
        /// </summary>
        /// <returns></returns>
        public ActionResult LessonStudy(int courseId, int lessonId)
        {
            StudentStudyBll bll = new StudentStudyBll();
            var p = bll.GetLastProgressByStudent(GetCurrentUser().StudentId, courseId, lessonId);
            if (p != null)
            {
                var lesson = new LessonBll().GetLesson(lessonId);//获取课程数据信息
                Chapter chapter = new Chapter()
                {
                    isApprove = false,
                    lastTimeIsOver = p.Yle_IsFinished || p.Yle_UnitIndex == 1,//如果已经完成或者从第一页开始,我们认为上次学习已经结束了
                    courseId = p.Yle_CourseId,
                    lessonId = p.Yle_LessonId,
                    lessonIndex = lesson.Ycl_Index,
                    lessonProgressId = p.Yle_Id,
                    progressKey = p.Yle_key,
                    startAddGoldsPageNum = p.Yle_CoinFromUIndex,
                    startPageNum = p.Yle_UnitIndex,
                    totalPageNum = lesson.Ycl_UnitCount
                };
                return View(chapter);
            }
            else
            {
                throw new Exception("课时数据错误");
            }
        }
        /// <summary>
        /// 审批界面
        /// </summary>
        /// <param name="ApproveKey"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult LessonApprove(string ApproveKey)
        {
            var jm = Encrypt.DecryptQueryString(ApproveKey).Split('_');//解密数据
            int courseId, lessonId, unitId, r;//定义课程id,课时id,单元id,随机数
            if (jm.Length == 4 && int.TryParse(jm[0], out r) && int.TryParse(jm[1], out unitId) && int.TryParse(jm[2], out lessonId) && int.TryParse(jm[3], out courseId))
            {
                Chapter chapter = new Chapter()
                {
                    isApprove = true,
                    lastTimeIsOver = courseId == 0,
                    startPageNum = unitId,
                    progressKey = Encrypt.MD5Hash(unitId.ToString(), "jzxd")
                };
                return View(chapter);
            }
            else
            {
                throw new Exception("课时数据错误");
            }
        }

        /// <summary>
        /// 获取用户的课程完成与未完成数量
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCourserCount()
        {
            StudentStudyBll bll = new StudentStudyBll();
            var data = bll.GetCourseCountByStudent(GetCurrentUser().StudentId);
            return Json(new JsonResponse<object>
            {
                State = true,
                ErrorCode = 0,
                ErrorMsg = "",
                Data = new { totel = data.Item1, notFinish = data.Item2, finished = data.Item3 }
            });

        }

        /// <summary>
        /// 根据课程完成状态分页查询学生课程信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCourseByPage(DtoStudentCourseSearch search)
        {
            search.StudentId = GetCurrentUser().StudentId;
            StudentStudyBll bll = new StudentStudyBll();
            var list = bll.GetCourseProgressByStudentSearch(search);
            list.ForEach(s =>
            {
                s.TeacherImg = ConfigurationManager.AppSettings["OssHostUrl"] + s.TeacherImg;
            });
            return Json(new JsonResponse<object>
            {
                State = true,
                ErrorCode = 0,
                ErrorMsg = "",
                Data = new { totel = search.Pagination.TotalCount, list = list }
            });
        }

        /// <summary>
        /// 根据课程id获取学生课时信息
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetLessons(int courseId)
        {
            StudentStudyBll bll = new StudentStudyBll();
            var list = bll.GetLessonProgressByStudent(GetCurrentUser().StudentId, courseId);
            return Json(new JsonResponse<List<DtoStudentLessonInfo>>
            {
                State = true,
                ErrorCode = 0,
                ErrorMsg = "",
                Data = list
            });
        }
        /// <summary>
        /// 根据课程id,课时id获取连续几页数据
        /// </summary>
        /// <param name="courseid">课程id</param>
        /// <param name="lessonid">课时id</param>
        /// <param name="page">开始页</param>
        /// <param name="pagesize">请求页数</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetLessonPage(DtoLessonUnitSearch search, bool isApprove = false)
        {
            LessonUnitBll bll = new LessonUnitBll();
            search.StudentID = GetCurrentUser().StudentId;//设置当前学生id
            if (isApprove) { search.Pagination.PageSize = 1; }//如果是审批的情况,只返回一条数据
            var pages = bll.SelectUnitByNext(search);
            List<int> mediaIDs = new List<int>();
            List<int> textIDs = new List<int>();
            var list = pages.Select(p => new Page()
            {
                pageId = p.Id,
                pageNum = p.Index,
                pageName = p.Name,
                coinsKey = Encrypt.EncryptQueryString($"{p.Coins}_{p.Id}_{p.LessonId}_{p.CourseId}"),//金币加密串(加密内容为"单元金币数_单元id_课时id_课程id")//Remark1
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
                var medias = rbll.GetMediaObjectByIdList(mediaIDs.Distinct().ToList(), true);//获取所有的媒体对象字典
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

            return Json(new JsonResponse<List<Page>>
            {
                State = true,
                ErrorCode = 0,
                ErrorMsg = "",
                Data = list
            });
        }

        /// <summary>
        /// 服务端审批调取单页数据接口
        /// </summary>
        /// <param name="unitid">单元id</param>
        /// <param name="isshow">是否是制作中的展示(及是否从radis中取数据)</param>
        /// <param name="jy">校验值</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetUnitPage(int unitid, bool isshow, string jy)
        {
            if (Encrypt.MD5Hash(unitid.ToString(), "jzxd") == jy)
            {
                DtoLessonUnit p = null;
                LessonUnitBll bll = new LessonUnitBll();
                if (!isshow)
                {
                    p = bll.SelectUnit(unitid, 0);
                }
                else
                {
                    p = bll.GetUnitFromRadis(unitid);
                }
                List<int> mediaIDs = new List<int>();
                List<int> textIDs = new List<int>();
                var page = new Page()
                {
                    pageId = p.Id,
                    pageNum = p.Index,
                    pageName = p.Name,
                    coinsKey = Encrypt.EncryptQueryString($"{p.Coins}_{p.Id}_{p.LessonId}_{p.CourseId}"),//金币加密串(加密内容为"单元金币数_单元id_课时id_课程id")//Remark1
                    steps = p.Steps.Select(s => new Step()
                    {
                        stepNum = s.StepNum,
                        actions = s.Actions.Select(a => ActionTranslator.DataToViewData(a, mediaIDs, textIDs)).ToList()
                    }).ToList()
                };
                //如果媒体id或文本id有值的话
                if (mediaIDs.Count > 0 || textIDs.Count > 0)
                {
                    ResourceBll rbll = new ResourceBll();
                    var medias = rbll.GetMediaObjectByIdList(mediaIDs.Distinct().ToList(), true);//获取所有的媒体对象字典
                    var texts = rbll.GetTextObjectByIdList(textIDs.Distinct().ToList());//获取文本对象字典

                    page.steps.ForEach(s =>
                    {
                        s.actions.ForEach(a =>
                        {
                            ActionTranslator.SetViewDataMedia(a, medias, texts);
                        });
                    });
                }
                return Json(new JsonResponse<Page>
                {
                    State = true,
                    ErrorCode = 0,
                    ErrorMsg = "",
                    Data = page
                });
            }
            else
            {
                throw new Exception("GetUnitPage服务端校验失败");
            }
        }

        /// <summary>
        /// 添加新的学习记录
        /// </summary>
        /// <param name="courseId">课程id</param>
        /// <param name="lessonId">课时id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult NewProgress(int courseId, int lessonId)
        {
            StudentStudyBll bll = new StudentStudyBll();
            var p = bll.GetNewProgressByStudent(GetCurrentUser().StudentId, courseId, lessonId);
            if (p != null)
            {
                return Json(new JsonResponse<Chapter>
                {
                    State = true,
                    ErrorCode = 0,
                    ErrorMsg = "",
                    Data = new Chapter()
                    {
                        isApprove = false,
                        lastTimeIsOver = true,
                        courseId = p.Yle_CourseId,
                        lessonId = p.Yle_LessonId,
                        lessonIndex = p.Yle_LessonIndex,
                        lessonProgressId = p.Yle_Id,
                        progressKey = p.Yle_key,
                        startAddGoldsPageNum = p.Yle_CoinFromUIndex,
                        startPageNum = p.Yle_UnitIndex,
                    }

                });
            }
            else
            {
                return Json(new JsonResponse<object>
                {
                    State = false,
                    ErrorCode = -1,
                    ErrorMsg = "课时数据错误"
                });
            }
        }

        /// <summary>
        /// 提交当页数据
        /// </summary>
        /// <param name="dataStr"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SubmintProcressData(string dataStr)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append($"学生id:{GetCurrentUser().StudentId},操作时间:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")};");
            sb.AppendLine($"接收数据:{dataStr}");
            DtoStudnetUnitSubmit info = new DtoStudnetUnitSubmit();
            #region json字符串填充数据
            try
            {
                JObject data = JsonConvert.DeserializeObject(dataStr) as JObject;
                info.StudnetId = GetCurrentUser().StudentId;
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
                        info.Answers.Add(SubjectAnswerObjectBuilder.TranslateAnswer(a.ToString()));
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
                    sb.AppendLine("回复数据:金币加密串解析错误");
                    LogHelper.ErrorLog(sb.ToString(), new Exception("金币加密串解析错误"));
                    return Json(new JsonResponse<object>
                    {
                        State = false,
                        ErrorCode = -100,
                        ErrorMsg = "金币加密串解析错误"
                    });
                }

            }
            catch (Exception ex)
            {
                sb.AppendLine("回复数据:json数据结构错误");
                LogHelper.ErrorLog(sb.ToString(), ex);
                return Json(new JsonResponse<object>
                {
                    State = false,
                    ErrorCode = -200,
                    ErrorMsg = "json数据结构错误"
                });
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
                    r.ErrorMsg = "成功";
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
            if (!r.State)
            {
                sb.AppendLine($"回复数据:{r.ErrorMsg}");
                LogHelper.ErrorLog(sb.ToString(), new Exception(r.ErrorMsg));
            }
            return Json(r);
        }

        /// <summary>
        /// 页面提交数据并且返回排行
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="lessonId"></param>
        /// <param name="unitId"></param>
        /// <param name="actionId"></param>
        /// <param name="score"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveAndCalculateRankPercent(int courseId, int lessonId, int unitId, string actionId, int score, string url)
        {
            ReadRecordBll bll = new ReadRecordBll();
            int s = bll.SaveAndCalculateRankPercent(GetCurrentUser().StudentId, courseId, lessonId, unitId, actionId, score, url);
            return Json(new JsonResponse<int> { State = true, Data = s });
        }



    }
}