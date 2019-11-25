using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.UnitStep;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
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
                            mediaid = ta.MediaId
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
                            ox = ta.OX,
                            oy = ta.OY,
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
                            mediaid = ta.MediaId,
                            kcbid = ta.KcbId,
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
                            mediaid = ta.MediaId,
                            kcbid = ta.KcbId
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
                            imgid = ta.ImgId,
                            kcbid = ta.KcbId,
                            textid = ta.TextId,
                            usetime = ta.UseTime,
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
                            kcbid = ta.KcbId,
                            textid = ta.TextId,
                            usetime = ta.UseTime

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
                            kcbid = ta.KcbId,
                            showNum = ta.ShowNum,
                            speed = ta.Speed,
                            textid = ta.TextId,
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
                            kcbid = ta.KcbId,
                            showNum = ta.ShowNum,
                            speed = ta.Speed,
                            textid = ta.TextId,
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
                            kcbid = ta.KcbId,
                            questionid = ta.QuestionId,
                            usetime = ta.UseTime
                        };
                        int id = 0;
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                        }
                    }
                    break;
                #endregion
                #region StudyAnnotation2
                case LessonActionTypeEnum.StudyAnnotation2:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyAnnotation2)action;
                        a = new Models.CurriculumSetViewModel.StudyAnnotation2()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            kcbid = ta.KcbId,
                            questionid = ta.QuestionId,
                            usetime = ta.UseTime
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
                            kcbid = ta.KcbId,
                            questionid = ta.QuestionId,
                            usetime = ta.UseTime
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
                            kcbid = ta.KcbId,
                            questionid = ta.QuestionId,
                            usetime = ta.UseTime
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
                            kcbid = ta.KcbId,
                            questionid = ta.QuestionId,
                            usetime = ta.UseTime
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
                            kcbid = ta.KcbId,
                            questionid = ta.QuestionId,
                            usetime = ta.UseTime
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
                            kcbid = ta.KcbId,
                            questionid = ta.QuestionId,
                            usetime = ta.UseTime
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
                            kcbid = ta.KcbId,
                            questionid = ta.QuestionId,
                            usetime = ta.UseTime
                        };
                        int id = 0;
                        if (int.TryParse(ta.KcbId, out id))
                        {
                            mediaIDs.Add(id);
                        }
                    }
                    break;
                #endregion
                #region StudyCalligraphy
                case LessonActionTypeEnum.StudyCalligraphy:
                    {
                        var ta = (AbhsChinese.Domain.JsonEntity.UnitStep.StudyCalligraphy)action;
                        a = new Models.CurriculumSetViewModel.StudyCalligraphy()
                        {
                            actionId = ta.Actionid,
                            actionNum = ta.ActionNum,
                            goldCoins = ta.Golds,
                            kcbid = ta.KcbId,
                            questionid = ta.QuestionId,
                            usetime = ta.UseTime
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
        /// 视图模型转换成数据模型
        /// </summary>
        /// <param name="action">视图模型</param>
        /// <param name="coins">金币数</param>
        /// <returns>数据模型</returns>
        public static StepAction ViewDataToData(ActionBase action,ref int coins)
        {
            StepAction a = null;
            switch (action.type)
            {
                #region setTitle
                case "setTitle":
                    {
                        var ta = (Models.CurriculumSetViewModel.SetTitle)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.SetTitle()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Text = ta.text,
                            Size = ta.size,
                            Color = ta.color,
                            Align = ta.align,
                            InType = ta.intype,
                            X = ta.x,
                            Y = ta.y
                        };
                    }
                    break;
                #endregion
                #region setBackground
                case "setBackground":
                    {
                        var ta = (Models.CurriculumSetViewModel.SetBackground)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.SetBackground()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Bg = ta.bg,
                            BgType = ta.bgType
                        };
                    }
                    break;
                #endregion
                #region xiaoAiSay
                case "xiaoAiSay":
                    {
                        var ta = (Models.CurriculumSetViewModel.XiaoAiSay)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.XiaoAiSay()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            MediaId = ta.mediaid
                        };
                    }
                    break;
                #endregion
                #region xiaoAiChange
                case "xiaoAiChange":
                    {
                        var ta = (Models.CurriculumSetViewModel.XiaoAiChange)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.XiaoAiChange()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Align = ta.align,
                            Height = ta.height,
                            ImgId = ta.imgId,
                            VAlign = ta.valign,
                            Width = ta.width,
                            X = ta.x,
                            Y = ta.y
                        };
                    }
                    break;
                #endregion
                #region insertImg
                case "insertImg":
                    {
                        var ta = (Models.CurriculumSetViewModel.InsertImg)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.InsertImg()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Align = ta.align,
                            Height = ta.height,
                            ImgId = ta.imgId,
                            VAlign = ta.valign,
                            Width = ta.width,
                            X = ta.x,
                            Y = ta.y,
                            InType = ta.intype
                        };
                    }
                    break;
                #endregion
                #region insertText
                case "insertText":
                    {
                        var ta = (Models.CurriculumSetViewModel.InsertText)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.InsertText()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Align = ta.align,
                            Color = ta.color,
                            InType = ta.intype,
                            Text = ta.text,
                            Size = ta.size,
                            X = ta.x,
                            Y = ta.y
                        };
                    }
                    break;
                #endregion
                #region setWaitMillisecond
                case "setWaitMillisecond":
                    {
                        var ta = (Models.CurriculumSetViewModel.SetWaitMillisecond)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.SetWaitMillisecond()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Stop = ta.stop
                        };
                    }
                    break;
                #endregion
                #region moveDom
                case "moveDom":
                    {
                        var ta = (Models.CurriculumSetViewModel.MoveDom)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.MoveDom()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            ObjectId = ta.objectId,
                            Duration = ta.duration,
                            OX = ta.ox,
                            OY = ta.oy,
                            X = ta.x,
                            Y = ta.y
                        };
                    }
                    break;
                #endregion
                #region scaleDom
                case "scaleDom":
                    {
                        var ta = (Models.CurriculumSetViewModel.ScaleDom)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.ScaleDom()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Duration = ta.duration,
                            Num = ta.num,
                            ObjectId = ta.objectId,
                            Ratio = ta.ratio,
                        };
                    }
                    break;
                #endregion
                #region twinkleDom
                case "twinkleDom":
                    {
                        var ta = (Models.CurriculumSetViewModel.TwinkleDom)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.TwinkleDom()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            ObjectId = ta.objectId,
                            Duration = ta.duration,
                            Num = ta.num
                        };
                    }
                    break;
                #endregion
                #region hideDom
                case "hideDom":
                    {
                        var ta = (Models.CurriculumSetViewModel.HideDom)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.HideDom()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            ObjectId = ta.objectId,
                            OutType = ta.outtype,
                        };
                    }
                    break;
                #endregion
                #region studyAudio
                case "studyAudio":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyAudio)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudyAudio()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            MediaId = ta.mediaid,
                            KcbId = ta.kcbid,
                            Golds = ta.goldCoins,
                            WordPosition = ta.wordPosition
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                #endregion
                #region studyVideo
                case "studyVideo":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyVideo)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudyVideo()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            MediaId = ta.mediaid,
                            KcbId = ta.kcbid,
                            Golds = ta.goldCoins
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                #endregion
                #region studyArticle
                case "studyArticle":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyArticle)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudyArticle()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Golds = ta.goldCoins,
                            ImgId = ta.imgid,
                            TextId = ta.textid,
                            KcbId = ta.kcbid,
                            UseTime = ta.usetime,
                            WordPosition = ta.wordPosition
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                #endregion
                #region studyRecitation
                case "studyRecitation":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyRecitation)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudyRecitation()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Golds = ta.goldCoins,
                            TextId = ta.textid,
                            KcbId = ta.kcbid,
                            UseTime = ta.usetime
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                #endregion
                #region studyFastReadEasy
                case "studyFastReadEasy":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyFastReadEasy)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudyFastReadEasy()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Golds = ta.goldCoins,
                            KcbId = ta.kcbid,
                            TextId = ta.textid,
                            ShowNum = ta.showNum,
                            Speed = ta.speed,
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                #endregion
                #region studyFastRead
                case "studyFastRead":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyFastRead)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudyFastRead()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Golds = ta.goldCoins,
                            KcbId = ta.kcbid,
                            TextId = ta.textid,
                            ShowNum = ta.showNum,
                            Speed = ta.speed,
                            ShowModel = ta.showModel
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                #endregion
                #region studyAnnotation
                case "studyAnnotation":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyAnnotation)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudyAnnotation()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Golds = ta.goldCoins,
                            KcbId = ta.kcbid,
                            UseTime = ta.usetime,
                            QuestionId = ta.questionid
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                #endregion
                #region studyAnnotation2
                case "studyAnnotation2":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyAnnotation2)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudyAnnotation2()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Golds = ta.goldCoins,
                            KcbId = ta.kcbid,
                            UseTime = ta.usetime,
                            QuestionId = ta.questionid
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                #endregion
                #region studyJudgment
                case "studyJudgment":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyJudgment)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudyJudgment()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Golds = ta.goldCoins,
                            KcbId = ta.kcbid,
                            UseTime = ta.usetime,
                            QuestionId = ta.questionid
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                #endregion
                #region studyLinking
                case "studyLinking":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyLinking)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudyLinking()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Golds = ta.goldCoins,
                            KcbId = ta.kcbid,
                            UseTime = ta.usetime,
                            QuestionId = ta.questionid
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                #endregion
                #region studyOption
                case "studyOption":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyOption)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudyOption()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Golds = ta.goldCoins,
                            KcbId = ta.kcbid,
                            UseTime = ta.usetime,
                            QuestionId = ta.questionid
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                #endregion
                #region studyOptionFill
                case "studyOptionFill":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyOptionFill)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudyOptionFill()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Golds = ta.goldCoins,
                            KcbId = ta.kcbid,
                            UseTime = ta.usetime,
                            QuestionId = ta.questionid
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                #endregion
                #region studyFill
                case "studyFill":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyFill)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudyFill()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Golds = ta.goldCoins,
                            KcbId = ta.kcbid,
                            UseTime = ta.usetime,
                            QuestionId = ta.questionid
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                #endregion
                #region studySubjective
                case "studySubjective":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudySubjective)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudySubjective()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Golds = ta.goldCoins,
                            KcbId = ta.kcbid,
                            UseTime = ta.usetime,
                            QuestionId = ta.questionid
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                #endregion
                #region studyCalligraphy
                case "studyCalligraphy":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyCalligraphy)action;
                        a = new AbhsChinese.Domain.JsonEntity.UnitStep.StudyCalligraphy()
                        {
                            Actionid = ta.actionId,
                            ActionNum = ta.actionNum,
                            Golds = ta.goldCoins,
                            KcbId = ta.kcbid,
                            UseTime = ta.usetime,
                            QuestionId = ta.questionid
                        };
                        coins += ta.goldCoins;
                    }
                    break;
                    #endregion
            }
            return a;

        }

        /// <summary>
        /// json字符串转成视图模型
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static ActionBase JsonToViewData(string json)
        {
            JObject obj = JsonConvert.DeserializeObject(json) as JObject;

            ActionBase a = null;
            #region 根据类型生成不同的对象
            switch (obj["type"].ToString())
            {
                #region setTitle
                case "setTitle":
                    {
                        a = new Models.CurriculumSetViewModel.SetTitle()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            text = obj["text"].ToString(),
                            size = obj["size"].ToString(),
                            color = obj["color"].ToString(),
                            align = obj["align"].ToString(),
                            intype = obj["intype"].ToString(),
                            x = Convert.ToDouble(obj["x"]),
                            y = Convert.ToDouble(obj["y"])
                        };
                    }
                    break;
                #endregion
                #region setBackground
                case "setBackground":
                    {
                        a = new Models.CurriculumSetViewModel.SetBackground()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            bg = obj["bg"].ToString(),
                            bgType = obj["bgType"].ToString()
                        };
                    }
                    break;
                #endregion
                #region xiaoAiSay
                case "xiaoAiSay":
                    {
                        a = new Models.CurriculumSetViewModel.XiaoAiSay()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            mediaid = obj["mediaid"].ToString()
                        };
                    }
                    break;
                #endregion
                #region xiaoAiChange
                case "xiaoAiChange":
                    {
                        a = new Models.CurriculumSetViewModel.XiaoAiChange()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            align = obj["align"].ToString(),
                            height = Convert.ToDouble(obj["height"]),
                            imgId = obj["imgId"].ToString(),
                            src = obj["src"].ToString(),
                            valign = obj["valign"].ToString(),
                            width = Convert.ToDouble(obj["width"]),
                            x = Convert.ToDouble(obj["x"]),
                            y = Convert.ToDouble(obj["y"])
                        };
                    }
                    break;
                #endregion
                #region insertImg
                case "insertImg":
                    {
                        a = new Models.CurriculumSetViewModel.InsertImg()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            align = obj["align"].ToString(),
                            height = Convert.ToDouble(obj["height"]),
                            imgId = obj["imgId"].ToString(),
                            src = obj["src"].ToString(),
                            valign = obj["valign"].ToString(),
                            width = Convert.ToDouble(obj["width"]),
                            x = Convert.ToDouble(obj["x"]),
                            y = Convert.ToDouble(obj["y"]),
                            intype = obj["intype"].ToString()
                        };
                    }
                    break;
                #endregion
                #region insertText
                case "insertText":
                    {
                        a = new Models.CurriculumSetViewModel.InsertText()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            align = obj["align"].ToString(),
                            color = obj["color"].ToString(),
                            intype = obj["intype"].ToString(),
                            text = obj["text"].ToString(),
                            size = obj["size"].ToString(),
                            x = Convert.ToDouble(obj["x"]),
                            y = Convert.ToDouble(obj["y"])
                        };
                    }
                    break;
                #endregion
                #region setWaitMillisecond
                case "setWaitMillisecond":
                    {
                        a = new Models.CurriculumSetViewModel.SetWaitMillisecond()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            stop = Convert.ToInt32(obj["stop"]),
                        };
                    }
                    break;
                #endregion
                #region moveDom
                case "moveDom":
                    {
                        a = new Models.CurriculumSetViewModel.MoveDom()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            objectId = obj["objectId"].ToString(),
                            duration = Convert.ToInt32(obj["duration"]),
                            ox = Convert.ToDouble(obj["ox"]),
                            oy = Convert.ToDouble(obj["oy"]),
                            x = Convert.ToDouble(obj["x"]),
                            y = Convert.ToDouble(obj["y"])
                        };
                    }
                    break;
                #endregion
                #region scaleDom
                case "scaleDom":
                    {
                        a = new Models.CurriculumSetViewModel.ScaleDom()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            duration = Convert.ToInt32(obj["duration"]),
                            num = Convert.ToInt32(obj["num"]),
                            objectId = obj["objectId"].ToString(),
                            ratio = Convert.ToDouble(obj["ratio"]),
                        };
                    }
                    break;
                #endregion
                #region twinkleDom
                case "twinkleDom":
                    {
                        a = new Models.CurriculumSetViewModel.TwinkleDom()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            objectId = obj["objectId"].ToString(),
                            duration = Convert.ToInt32(obj["duration"]),
                            num = Convert.ToInt32(obj["num"]),
                        };
                    }
                    break;
                #endregion
                #region hideDom
                case "hideDom":
                    {
                        a = new Models.CurriculumSetViewModel.HideDom()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            objectId = obj["objectId"].ToString(),
                            outtype = obj["outtype"].ToString()
                        };
                    }
                    break;
                #endregion
                #region studyAudio
                case "studyAudio":
                    {
                        a = new Models.CurriculumSetViewModel.StudyAudio()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            mediaid = obj["mediaid"].ToString(),
                            medianame = obj["medianame"].ToString(),
                            src = obj["src"].ToString(),
                            imgsrc = obj["imgsrc"].ToString(),
                            text = obj["text"].ToString(),
                            kcbid = obj["kcbid"].ToString(),
                            //kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            wordPosition = obj["wordPosition"].ToString(),
                            goldCoins = Convert.ToInt32(obj["goldCoins"])
                        };
                    }
                    break;
                #endregion
                #region studyVideo
                case "studyVideo":
                    {
                        a = new Models.CurriculumSetViewModel.StudyVideo()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            mediaid = obj["mediaid"].ToString(),
                            medianame = obj["medianame"].ToString(),
                            src = obj["src"].ToString(),
                            kcbid = obj["kcbid"].ToString(),
                            //kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            goldCoins = Convert.ToInt32(obj["goldCoins"])
                        };
                    }
                    break;
                #endregion
                #region studyArticle
                case "studyArticle":
                    {
                        a = new Models.CurriculumSetViewModel.StudyArticle()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            imgid = obj["imgid"].ToString(),
                            imgsrc = obj["imgsrc"].ToString(),
                            goldCoins = Convert.ToInt32(obj["goldCoins"]),
                            kcbid = obj["kcbid"].ToString(),
                            kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            textid = obj["textid"].ToString(),
                            textstr = obj["textstr"].ToString(),
                            usetime = Convert.ToInt32(obj["usetime"]),
                            wordPosition = obj["wordPosition"].ToString()
                        };
                    }
                    break;
                #endregion
                #region studyRecitation
                case "studyRecitation":
                    {
                        a = new Models.CurriculumSetViewModel.StudyRecitation()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            goldCoins = Convert.ToInt32(obj["goldCoins"]),
                            kcbid = obj["kcbid"].ToString(),
                            kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            textid = obj["textid"].ToString(),
                            textstr = obj["textstr"].ToString(),
                            usetime = Convert.ToInt32(obj["usetime"])
                        };
                    }
                    break;
                #endregion
                #region studyFastReadEasy
                case "studyFastReadEasy":
                    {
                        a = new Models.CurriculumSetViewModel.StudyFastReadEasy()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            goldCoins = Convert.ToInt32(obj["goldCoins"]),
                            kcbid = obj["kcbid"].ToString(),
                            kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            speed = Convert.ToInt32(obj["speed"]),
                            showNum = Convert.ToInt32(obj["showNum"]),
                            textid = obj["textid"].ToString(),
                            textstr = obj["textstr"].ToString(),
                        };
                    }
                    break;
                #endregion
                #region studyFastRead
                case "studyFastRead":
                    {
                        a = new Models.CurriculumSetViewModel.StudyFastRead()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            goldCoins = Convert.ToInt32(obj["goldCoins"]),
                            kcbid = obj["kcbid"].ToString(),
                            kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            speed = Convert.ToInt32(obj["speed"]),
                            showNum = Convert.ToInt32(obj["showNum"]),
                            textid = obj["textid"].ToString(),
                            textstr = obj["textstr"].ToString(),
                            showModel = obj["showModel"].ToString(),
                        };
                    }
                    break;
                #endregion
                #region studyAnnotation
                case "studyAnnotation":
                    {
                        a = new Models.CurriculumSetViewModel.StudyAnnotation()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            kcbid = obj["kcbid"].ToString(),
                            kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            goldCoins = Convert.ToInt32(obj["goldCoins"]),
                            questionid = obj["questionid"].ToString(),
                            questionname = obj["questionname"].ToString(),
                            usetime = Convert.ToInt32(obj["usetime"]),
                        };
                    }
                    break;
                #endregion
                #region studyAnnotation2
                case "studyAnnotation2":
                    {
                        a = new Models.CurriculumSetViewModel.StudyAnnotation2()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            kcbid = obj["kcbid"].ToString(),
                            kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            goldCoins = Convert.ToInt32(obj["goldCoins"]),
                            questionid = obj["questionid"].ToString(),
                            questionname = obj["questionname"].ToString(),
                            usetime = Convert.ToInt32(obj["usetime"]),
                        };
                    }
                    break;
                #endregion
                #region studyJudgment
                case "studyJudgment":
                    {
                        a = new Models.CurriculumSetViewModel.StudyJudgment()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            kcbid = obj["kcbid"].ToString(),
                            kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            goldCoins = Convert.ToInt32(obj["goldCoins"]),
                            questionid = obj["questionid"].ToString(),
                            questionname = obj["questionname"].ToString(),
                            usetime = Convert.ToInt32(obj["usetime"]),
                        };
                    }
                    break;
                #endregion
                #region studyLinking
                case "studyLinking":
                    {
                        a = new Models.CurriculumSetViewModel.StudyLinking()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            kcbid = obj["kcbid"].ToString(),
                            kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            goldCoins = Convert.ToInt32(obj["goldCoins"]),
                            questionid = obj["questionid"].ToString(),
                            questionname = obj["questionname"].ToString(),
                            usetime = Convert.ToInt32(obj["usetime"]),
                        };
                    }
                    break;
                #endregion
                #region studyOption
                case "studyOption":
                    {
                        a = new Models.CurriculumSetViewModel.StudyOption()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            kcbid = obj["kcbid"].ToString(),
                            kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            goldCoins = Convert.ToInt32(obj["goldCoins"]),
                            questionid = obj["questionid"].ToString(),
                            questionname = obj["questionname"].ToString(),
                            usetime = Convert.ToInt32(obj["usetime"]),
                        };
                    }
                    break;
                #endregion
                #region studyOptionFill
                case "studyOptionFill":
                    {
                        a = new Models.CurriculumSetViewModel.StudyOptionFill()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            kcbid = obj["kcbid"].ToString(),
                            kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            goldCoins = Convert.ToInt32(obj["goldCoins"]),
                            questionid = obj["questionid"].ToString(),
                            questionname = obj["questionname"].ToString(),
                            usetime = Convert.ToInt32(obj["usetime"]),
                        };
                    }
                    break;
                #endregion
                #region studyFill
                case "studyFill":
                    {
                        a = new Models.CurriculumSetViewModel.StudyFill()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            kcbid = obj["kcbid"].ToString(),
                            kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            goldCoins = Convert.ToInt32(obj["goldCoins"]),
                            questionid = obj["questionid"].ToString(),
                            questionname = obj["questionname"].ToString(),
                            usetime = Convert.ToInt32(obj["usetime"]),
                        };
                    }
                    break;
                #endregion
                #region studySubjective
                case "studySubjective":
                    {
                        a = new Models.CurriculumSetViewModel.StudySubjective()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            kcbid = obj["kcbid"].ToString(),
                            kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            goldCoins = Convert.ToInt32(obj["goldCoins"]),
                            questionid = obj["questionid"].ToString(),
                            questionname = obj["questionname"].ToString(),
                            usetime = Convert.ToInt32(obj["usetime"]),
                        };
                    }
                    break;
                #endregion
                #region studyCalligraphy
                case "studyCalligraphy":
                    {
                        a = new Models.CurriculumSetViewModel.StudyCalligraphy()
                        {
                            actionId = obj["actionId"].ToString(),
                            actionNum = Convert.ToInt32(obj["actionNum"]),
                            kcbid = obj["kcbid"].ToString(),
                            kcbsrc = obj["kcbsrc"].ToString(),
                            kcbtext = obj["kcbtext"].ToString(),
                            goldCoins = Convert.ToInt32(obj["goldCoins"]),
                            questionid = obj["questionid"].ToString(),
                            questionname = obj["questionname"].ToString(),
                            usetime = Convert.ToInt32(obj["usetime"]),
                        };
                    }
                    break;
                    #endregion
            }
            #endregion
            return a;
        }
        /// <summary>
        /// 填充对象媒体信息
        /// </summary>
        /// <param name="action">动作</param>
        /// <param name="media">资源字典</param>
        /// <param name="text">文本字典</param>
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
                            ta.bgUrl = media[int.Parse(ta.bg)].MediaUrl;
                            ta.bgName = media[int.Parse(ta.bg)].MediaName;
                        }
                    }
                    break;
                #endregion
                #region xiaoAiSay
                case "xiaoAiSay":
                    {
                        var ta = (Models.CurriculumSetViewModel.XiaoAiSay)action;
                        ta.medianame = media[int.Parse(ta.mediaid)].MediaName;
                        ta.src = media[int.Parse(ta.mediaid)].MediaUrl;
                    }
                    break;
                #endregion
                #region xiaoAiChange
                case "xiaoAiChange":
                    {
                        var ta = (Models.CurriculumSetViewModel.XiaoAiChange)action;
                        ta.src = media[int.Parse(ta.imgId)].MediaUrl;
                        ta.imgName = media[int.Parse(ta.imgId)].MediaName;
                    }
                    break;
                #endregion
                #region insertImg
                case "insertImg":
                    {
                        var ta = (Models.CurriculumSetViewModel.InsertImg)action;
                        ta.src = media[int.Parse(ta.imgId)].MediaUrl;
                        ta.imgName = media[int.Parse(ta.imgId)].MediaName;
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
                        var obj = media[int.Parse(ta.mediaid)];
                        ta.src = obj.MediaUrl;
                        ta.imgsrc = obj.ImgUrl;
                        ta.text = obj.TextContent;
                        ta.medianame = obj.MediaName;
                        int id = 0;
                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
                        }
                    }
                    break;
                #endregion
                #region studyVideo
                case "studyVideo":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyVideo)action;
                        var obj = media[int.Parse(ta.mediaid)];
                        ta.src = obj.MediaUrl;
                        ta.medianame = obj.MediaName;
                        int id = 0;
                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
                        }
                    }
                    break;
                #endregion
                #region studyArticle
                case "studyArticle":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyArticle)action;
                        int id = 0;

                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
                            id = 0;
                        }
                        if (int.TryParse(ta.imgid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.imgsrc = kcb.MediaUrl;
                            id = 0;
                        }
                        if (int.TryParse(ta.textid, out id) && text.ContainsKey(id))
                        {
                            var kcb = text[id];
                            ta.textstr = kcb;
                        }
                    }
                    break;
                #endregion
                #region studyRecitation
                case "studyRecitation":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyRecitation)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
                            id = 0;
                        }
                        if (int.TryParse(ta.textid, out id) && text.ContainsKey(id))
                        {
                            var kcb = text[id];
                            ta.textstr = kcb;
                        }
                    }
                    break;
                #endregion
                #region studyFastReadEasy
                case "studyFastReadEasy":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyFastReadEasy)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
                            id = 0;
                        }
                        if (int.TryParse(ta.textid, out id) && text.ContainsKey(id))
                        {
                            var kcb = text[id];
                            ta.textstr = kcb;
                        }
                    }
                    break;
                #endregion
                #region studyFastRead
                case "studyFastRead":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyFastRead)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
                            id = 0;
                        }
                        if (int.TryParse(ta.textid, out id) && text.ContainsKey(id))
                        {
                            var kcb = text[id];
                            ta.textstr = kcb;
                        }
                    }
                    break;
                #endregion
                #region studyAnnotation
                case "studyAnnotation":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyAnnotation)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
                            id = 0;
                        }
                    }
                    break;
                #endregion
                #region studyAnnotation2
                case "studyAnnotation2":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyAnnotation2)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
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
                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
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
                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
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
                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
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
                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
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
                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
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
                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
                            id = 0;
                        }
                    }
                    break;
                #endregion
                #region studyCalligraphy
                case "studyCalligraphy":
                    {
                        var ta = (Models.CurriculumSetViewModel.StudyCalligraphy)action;
                        int id = 0;
                        if (int.TryParse(ta.kcbid, out id) && media.ContainsKey(id))
                        {
                            var kcb = media[id];
                            ta.kcbtext = kcb.Description;
                            id = 0;
                        }
                    }
                    break;
                    #endregion
            }
        }
    }
}