using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.UnitStep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 视图模型数据模型转换类
    /// </summary>
    public static class ActionTranslator
    {
        /// <summary>
        /// 数据模型转成视图模型
        /// </summary>
        /// <param name="action">数据模型</param>
        /// <param name="mediaIDs">媒体id(只用于填充),可为null</param>
        /// <param name="textIDs">文本id(只用于填充),可为null</param>
        /// <returns></returns>
        public static ActionBase DataToViewData(StepAction action, List<int> mediaIDs, List<int> textIDs)
        {
            //防止为空
            if (mediaIDs == null) { mediaIDs = new List<int>(); }
            if (textIDs == null) { textIDs = new List<int>(); }

            ActionBase a = null;
            switch (action.Type)
            {
                #region SetTitle
                case LessonActionTypeEnum.SetTitle:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.SetTitle)action;
                        a = new Models.CurriculumSetViewModel.SetTitle()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            text = ta.Text,
                            size = ta.Size,
                            color = ta.Color,
                            align = ta.Align,
                            intype = ta.InType,
                            x = ta.X,
                            y = ta.Y
                        };
                    }
                    break;
                #endregion
                #region SetBackground
                case LessonActionTypeEnum.SetBackground:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.SetBackground)action;
                        a = new Models.CurriculumSetViewModel.SetBackground()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            bg = ta.Bg,
                            bgType = ta.BgType
                        };
                        if (ta.BgType == "image")//如果是图片类型则添加媒体id
                        {
                            mediaIDs.Add(int.Parse(ta.Bg));
                        }
                    }
                    break;
                #endregion
                #region XiaoAiSay
                case LessonActionTypeEnum.XiaoAiSay:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.XiaoAiSay)action;
                        a = new Models.CurriculumSetViewModel.XiaoAiSay()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            src = ta.MediaId
                        };
                        mediaIDs.Add(int.Parse(ta.MediaId));

                    }
                    break;
                #endregion
                #region XiaoAiChange
                case LessonActionTypeEnum.XiaoAiChange:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.XiaoAiChange)action;
                        a = new Models.CurriculumSetViewModel.XiaoAiChange()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            align = ta.Align,
                            height = ta.Height,
                            imgId = ta.ImgId,
                            src = "",
                            valign = ta.VAlign,
                            width = ta.Width,
                            x = ta.X,
                            y = ta.Y
                        };
                        mediaIDs.Add(int.Parse(ta.ImgId));
                    }
                    break;
                #endregion
                #region InsertImg
                case LessonActionTypeEnum.InsertImg:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.InsertImg)action;
                        a = new Models.CurriculumSetViewModel.InsertImg()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            align = ta.Align,
                            height = ta.Height,
                            imgId = ta.ImgId,
                            src = "",//ta.Src,
                            valign = ta.VAlign,
                            width = ta.Width,
                            x = ta.X,
                            y = ta.Y,
                            intype = ta.InType
                        };
                        mediaIDs.Add(int.Parse(ta.ImgId));
                    }
                    break;
                #endregion
                #region InsertText
                case LessonActionTypeEnum.InsertText:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.InsertText)action;
                        a = new Models.CurriculumSetViewModel.InsertText()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            align = ta.Align,
                            color = ta.Color,
                            intype = ta.InType,
                            text = ta.Text,
                            size = ta.Size,
                            x = ta.X,
                            y = ta.Y
                        };
                    }
                    break;
                #endregion
                #region SetWaitMillisecond
                case LessonActionTypeEnum.SetWaitMillisecond:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.SetWaitMillisecond)action;
                        a = new Models.CurriculumSetViewModel.SetWaitMillisecond()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            stop = ta.Stop
                        };
                    }
                    break;
                #endregion
                #region MoveDom
                case LessonActionTypeEnum.MoveDom:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.MoveDom)action;
                        a = new Models.CurriculumSetViewModel.MoveDom()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            objectId = ta.ObjectId,
                            duration = ta.Duration,
                            x = ta.X,
                            y = ta.Y
                        };
                    }
                    break;
                #endregion
                #region ScaleDom
                case LessonActionTypeEnum.ScaleDom:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.ScaleDom)action;
                        a = new Models.CurriculumSetViewModel.ScaleDom()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            duration = ta.Duration,
                            num = ta.Num,
                            objectId = ta.ObjectId,
                            ratio = ta.Ratio,
                        };
                    }
                    break;
                #endregion
                #region TwinkleDom
                case LessonActionTypeEnum.TwinkleDom:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.TwinkleDom)action;
                        a = new Models.CurriculumSetViewModel.TwinkleDom()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            objectId = ta.ObjectId,
                            duration = ta.Duration,
                            num = ta.Num
                        };
                    }
                    break;
                #endregion
                #region HideDom
                case LessonActionTypeEnum.HideDom:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.HideDom)action;
                        a = new Models.CurriculumSetViewModel.HideDom()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            objectId = ta.ObjectId,
                            outtype = ta.OutType
                        };
                    }
                    break;
                #endregion
                #region StudyAudio
                case LessonActionTypeEnum.StudyAudio:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyAudio)action;
                        a = new Models.CurriculumSetViewModel.StudyAudio()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            audioSrc = ta.MediaId,
                            kcbSrc = ta.KcbId,
                            wordPosition = ta.WordPosition
                        };
                        int id = 0;
                        if (int.TryParse(ta.MediaId, out id))
                        {
                            mediaIDs.Add(id);
                            id = 0;
                        }
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                        }
                    }
                    break;
                #endregion
                #region StudyVideo
                case LessonActionTypeEnum.StudyVideo:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyVideo)action;
                        a = new Models.CurriculumSetViewModel.StudyVideo()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            videoSrc = ta.MediaId,
                            kcbSrc = ta.KcbId
                        };
                        int id = 0;
                        if (int.TryParse(ta.MediaId, out id))
                        {
                            mediaIDs.Add(id);
                            id = 0;
                        }
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                        }
                    }
                    break;
                #endregion
                #region StudyArticle
                case LessonActionTypeEnum.StudyArticle:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyArticle)action;
                        a = new Models.CurriculumSetViewModel.StudyArticle()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            articleImgSrc = ta.ImgId,
                            kcbSrc = ta.KcbId,
                            articleTextSrc = ta.TextId,
                            studyTime = ta.UseTime,
                            wordPosition = ta.WordPosition
                        };

                        int id = 0;
                        if (int.TryParse(ta.ImgId, out id))
                        {
                            mediaIDs.Add(id);
                            id = 0;
                        }
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                            id = 0;
                        }
                        if (int.TryParse(ta.TextId, out id))
                        {
                            textIDs.Add(id);
                        }
                    }
                    break;
                #endregion
                #region StudyRecitation
                case LessonActionTypeEnum.StudyRecitation:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyRecitation)action;
                        a = new Models.CurriculumSetViewModel.StudyRecitation()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            kcbSrc = ta.KcbId,
                            recitationSrc = ta.TextId,
                            studyTime = ta.UseTime

                        };
                        int id = 0;
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                            id = 0;
                        }
                        if (int.TryParse(ta.TextId, out id))
                        {
                            textIDs.Add(id);
                        }
                    }
                    break;
                #endregion
                #region StudyFastReadEasy
                case LessonActionTypeEnum.StudyFastReadEasy:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyFastReadEasy)action;
                        a = new Models.CurriculumSetViewModel.StudyFastReadEasy()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            kcbSrc = ta.KcbId,
                            showNum = ta.ShowNum,
                            speed = ta.Speed,
                            fastReadText = ta.TextId,
                        };

                        int id = 0;
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                            id = 0;
                        }
                        if (int.TryParse(ta.TextId, out id))
                        {
                            textIDs.Add(id);
                        }
                    }
                    break;
                #endregion
                #region StudyFastRead
                case LessonActionTypeEnum.StudyFastRead:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyFastRead)action;
                        a = new Models.CurriculumSetViewModel.StudyFastRead()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            showModel = ta.ShowModel,
                            kcbSrc = ta.KcbId,
                            showNum = ta.ShowNum,
                            speed = ta.Speed,
                            fastReadText = ta.TextId,
                        };

                        int id = 0;
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                            id = 0;
                        }
                        if (int.TryParse(ta.TextId, out id))
                        {
                            textIDs.Add(id);
                        }
                    }
                    break;
                #endregion
                #region StudyAnnotation
                case LessonActionTypeEnum.StudyAnnotation:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyAnnotation)action;
                        a = new Models.CurriculumSetViewModel.StudyAnnotation()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            kcbSrc = ta.KcbId,
                            questionId = int.Parse(ta.QuestionId),
                            studyTime = ta.UseTime
                        };
                        int id = 0;
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                        }
                    }
                    break;
                case LessonActionTypeEnum.StudyAnnotation2:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyAnnotation2)action;
                        a = new Models.CurriculumSetViewModel.StudyAnnotation2()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            kcbSrc = ta.KcbId,
                            questionId = int.Parse(ta.QuestionId),
                            studyTime = ta.UseTime
                        };
                        int id = 0;
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                        }
                    }
                    break;
                #endregion
                #region StudyJudgment
                case LessonActionTypeEnum.StudyJudgment:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyJudgment)action;
                        a = new Models.CurriculumSetViewModel.StudyJudgment()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            kcbSrc = ta.KcbId,
                            questionId = int.Parse(ta.QuestionId),
                            studyTime = ta.UseTime
                        };
                        int id = 0;
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                        }
                    }
                    break;
                #endregion
                #region StudyLinking
                case LessonActionTypeEnum.StudyLinking:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyLinking)action;
                        a = new Models.CurriculumSetViewModel.StudyLinking()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            kcbSrc = ta.KcbId,
                            questionId = int.Parse(ta.QuestionId),
                            studyTime = ta.UseTime
                        };
                        int id = 0;
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                        }
                    }
                    break;
                #endregion
                #region StudyOption
                case LessonActionTypeEnum.StudyOption:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyOption)action;
                        a = new Models.CurriculumSetViewModel.StudyOption()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            kcbSrc = ta.KcbId,
                            questionId = int.Parse(ta.QuestionId),
                            studyTime = ta.UseTime
                        };
                        int id = 0;
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                        }
                    }
                    break;
                #endregion
                #region StudyOptionFill
                case LessonActionTypeEnum.StudyOptionFill:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyOptionFill)action;
                        a = new Models.CurriculumSetViewModel.StudyOptionFill()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            kcbSrc = ta.KcbId,
                            questionId = int.Parse(ta.QuestionId),
                            studyTime = ta.UseTime
                        };
                        int id = 0;
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                        }
                    }
                    break;
                #endregion
                #region StudyFill
                case LessonActionTypeEnum.StudyFill:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyFill)action;
                        a = new Models.CurriculumSetViewModel.StudyFill()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            kcbSrc = ta.KcbId,
                            questionId = int.Parse(ta.QuestionId),
                            studyTime = ta.UseTime
                        };
                        int id = 0;
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                        }
                    }
                    break;
                #endregion
                #region StudySubjective
                case LessonActionTypeEnum.StudySubjective:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudySubjective)action;
                        a = new Models.CurriculumSetViewModel.StudySubjective()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            kcbSrc = ta.KcbId,
                            questionId = int.Parse(ta.QuestionId),
                            studyTime = ta.UseTime
                        };
                        int id = 0;
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                        }
                    }
                    break;
                    #endregion
            }
            return a;
        }

        /// <summary>
        /// 填充对象媒体信息
        /// </summary>
        /// <param name="action">动作</param>
        /// <param name="media">资源字典</param>
        /// <param name="text">文本字典</param>
        /// <remarks>
        /// 1.需要考虑给文本赋值时使用id还是文本
        /// </remarks>
        public static void SetViewDataMedia(ActionBase action, Dictionary<int, DtoMediaObject> media, Dictionary<int, string> text)
        {
            switch (action.type)
            {
                #region setTitle
                case "setTitle":
                    {
                        var ta = (Models.CurriculumSetViewModel.SetTitle)action;

                    }
                    break;
                #endregion
                #region setBackground
                case "setBackground":
                    {
                        var ta = (Models.CurriculumSetViewModel.SetBackground)action;
                        if (ta.bgType == "image")
                        {
                            ta.bg = media[int.Parse(ta.bg)].MediaUrl;
                        }
                    }
                    break;
                #endregion
                #region xiaoAiSay
                case "xiaoAiSay":
                    {
                        var ta = (Models.CurriculumSetViewModel.XiaoAiSay)action;
                        ta.src = media[int.Parse(ta.src)].MediaUrl;
                    }
                    break;
                #endregion
                #region xiaoAiChange
                case "xiaoAiChange":
                    {
                        var ta = (Models.CurriculumSetViewModel.XiaoAiChange)action;
                        ta.src = media[int.Parse(ta.imgId)].MediaUrl;
                        ta.imgId = media[int.Parse(ta.imgId)].MediaName;
                    }
                    break;
                #endregion
                #region insertImg
                case "insertImg":
                    {
                        var ta = (Models.CurriculumSetViewModel.InsertImg)action;
                        ta.src = media[int.Parse(ta.imgId)].MediaUrl;
                        ta.imgId = media[int.Parse(ta.imgId)].MediaName;
                    }
                    break;
                #endregion
                #region insertText
                case "insertText":
                    {
                        var ta = (Models.CurriculumSetViewModel.InsertText)action;
                    }
                    break;
                #endregion
                #region setWaitMillisecond
                case "setWaitMillisecond":
                    {
                        var ta = (Models.CurriculumSetViewModel.SetWaitMillisecond)action;
                    }
                    break;
                #endregion
                #region moveDom
                case "moveDom":
                    {
                        var ta = (Models.CurriculumSetViewModel.MoveDom)action;
                    }
                    break;
                #endregion
                #region scaleDom
                case "scaleDom":
                    {
                        var ta = (Models.CurriculumSetViewModel.ScaleDom)action;
                    }
                    break;
                #endregion
                #region twinkleDom
                case "twinkleDom":
                    {
                        var ta = (Models.CurriculumSetViewModel.TwinkleDom)action;
                    }
                    break;
                #endregion
                #region hideDom
                case "hideDom":
                    {
                        var ta = (Models.CurriculumSetViewModel.HideDom)action;
                    }
                    break;
                #endregion
                #region studyAudio
                case "studyAudio":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyAudio)action;
                        var obj = media[int.Parse(ta.audioSrc)];
                        ta.audioSrc = obj.MediaUrl;
                        ta.audioImgSrc = obj.ImgUrl;
                        ta.audioTextSrc = obj.TextContent;//remark1
                        int id = 0;
                        if (int.TryParse(ta.kcbSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbSrc = kcb.MediaUrl;
                            ta.kcbTextSrc = kcb.Description;
                        }
                    }
                    break;
                #endregion
                #region studyVideo
                case "studyVideo":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyVideo)action;
                        var obj = media[int.Parse(ta.videoSrc)];
                        ta.videoSrc = obj.MediaUrl;
                        int id = 0;
                        if (int.TryParse(ta.kcbSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbSrc = kcb.MediaUrl;
                            ta.kcbTextSrc = kcb.Description;
                        }
                    }
                    break;
                #endregion
                #region studyArticle
                case "studyArticle":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyArticle)action;
                        int id = 0;

                        if (int.TryParse(ta.kcbSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbSrc = kcb.MediaUrl;
                            ta.kcbTextSrc = kcb.Description;
                            id = 0;
                        }
                        if (int.TryParse(ta.articleImgSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.articleImgSrc = kcb.MediaUrl;
                            id = 0;
                        }
                        if (int.TryParse(ta.articleTextSrc, out id) && text.ContainsKey(id))
                        {
                            var str = text[id];
                            ta.articleTextSrc = str;//remark1
                        }
                    }
                    break;
                #endregion
                #region studyRecitation
                case "studyRecitation":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyRecitation)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbSrc = kcb.MediaUrl;
                            ta.kcbTextSrc = kcb.Description;
                            id = 0;
                        }
                        if (int.TryParse(ta.recitationSrc, out id) && text.ContainsKey(id))
                        {
                            var str = text[id];
                            ta.recitationSrc = str;//remark1
                        }
                    }
                    break;
                #endregion
                #region studyFastReadEasy
                case "studyFastReadEasy":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyFastReadEasy)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbSrc = kcb.MediaUrl;
                            ta.kcbTextSrc = kcb.Description;
                            id = 0;
                        }
                        if (int.TryParse(ta.fastReadText, out id) && text.ContainsKey(id))
                        {
                            var str = text[id];
                            ta.fastReadText = str;//remark1
                        }
                    }
                    break;
                #endregion
                #region studyFastRead
                case "studyFastRead":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyFastRead)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbSrc = kcb.MediaUrl;
                            ta.kcbTextSrc = kcb.Description;
                            id = 0;
                        }
                        if (int.TryParse(ta.fastReadText, out id) && text.ContainsKey(id))
                        {
                            var kcb = text[id];
                            ta.fastReadText = kcb;//remark1
                        }
                    }
                    break;
                #endregion
                #region studyAnnotation
                case "studyAnnotation":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyAnnotation)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbSrc = kcb.MediaUrl;
                            ta.kcbTextSrc = kcb.Description;
                            id = 0;
                        }
                    }
                    break;
                case "studyAnnotation2":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyAnnotation2)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbSrc = kcb.MediaUrl;
                            ta.kcbTextSrc = kcb.Description;
                            id = 0;
                        }
                    }
                    break;
                #endregion
                #region studyJudgment
                case "studyJudgment":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyJudgment)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbSrc = kcb.MediaUrl;
                            ta.kcbTextSrc = kcb.Description;
                            id = 0;
                        }
                    }
                    break;
                #endregion
                #region studyLinking
                case "studyLinking":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyLinking)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbSrc = kcb.MediaUrl;
                            ta.kcbTextSrc = kcb.Description;
                            id = 0;
                        }
                    }
                    break;
                #endregion
                #region studyOption
                case "studyOption":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyOption)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbSrc = kcb.MediaUrl;
                            ta.kcbTextSrc = kcb.Description;
                            id = 0;
                        }
                    }
                    break;
                #endregion
                #region studyOptionFill
                case "studyOptionFill":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyOptionFill)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbSrc = kcb.MediaUrl;
                            ta.kcbTextSrc = kcb.Description;
                            id = 0;
                        }
                    }
                    break;
                #endregion
                #region studyFill
                case "studyFill":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyFill)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbSrc = kcb.MediaUrl;
                            ta.kcbTextSrc = kcb.Description;
                            id = 0;
                        }
                    }
                    break;
                #endregion
                #region studySubjective
                case "studySubjective":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudySubjective)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbSrc, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbSrc = kcb.MediaUrl;
                            ta.kcbTextSrc = kcb.Description;
                            id = 0;
                        }
                    }
                    break;
                #endregion
            }
        }
    }
}