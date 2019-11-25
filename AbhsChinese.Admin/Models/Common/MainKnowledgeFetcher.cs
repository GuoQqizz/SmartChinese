using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbhsChinese.Admin.Models.Common
{
    public static class MainKnowledgeFetcher
    {
        public static Action<Yw_SubjectKnowledge> Fetch(
            QuestionInputModel subject,
            SubjectBll bll,
            int currentUser,
            out Yw_SubjectKnowledge entity)
        {
            Check.IfNull(subject, nameof(subject));
            Check.IfNull(bll, nameof(bll));
            if (currentUser < 10000)
            {
                throw new ArgumentException(
                    "当前登录用户的ID不能小于10000",
                    nameof(currentUser));
            }

            DateTime now = DateTime.Now;
            Action<Yw_SubjectKnowledge> saveKnowledge = null;
            IEnumerable<Yw_SubjectKnowledge> knowledgeEntities =
                bll.GetKnowledgesBySubject(subject.Id);
            Yw_SubjectKnowledge mainKnowledgeEntity =
                knowledgeEntities.FirstOrDefault(k => k.Ysw_IsMain);
            if (subject.Knowledge == 0)//不在指定主知识点
            {
                saveKnowledge = bll.DeleteKnowledge;
            }
            else
            {
                if (mainKnowledgeEntity == null)
                {
                    mainKnowledgeEntity = new Yw_SubjectKnowledge();
                    mainKnowledgeEntity.Ysw_IsMain = true;
                    mainKnowledgeEntity.Ysw_CreateTime = now;
                    mainKnowledgeEntity.Ysw_Creator = currentUser;
                    saveKnowledge = bll.InsertMainKnowledge;
                }
                else
                {
                    mainKnowledgeEntity.EnableAudit();
                    saveKnowledge = bll.UpdateMainKnowledge;
                }
                mainKnowledgeEntity.Ysw_Editor = currentUser;
                mainKnowledgeEntity.Ysw_KnowledgeId = subject.Knowledge;
                mainKnowledgeEntity.Ysw_UpdateTime = now;
            }

            entity = mainKnowledgeEntity;
            return saveKnowledge;
        }
    }
}