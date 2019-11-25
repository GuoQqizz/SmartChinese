using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbhsChinese.Admin.Models.Common
{
    public static class SecondaryKnowledgeFetcher
    {
        public static IList<Yw_SubjectKnowledge> Fetch(
            QuestionInputModel subject,
            SubjectBll bll,
            int currentUser,
            out IEnumerable<int> toDelete)
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
            IEnumerable<Yw_SubjectKnowledge> knowledgeEntities =
                bll.GetKnowledgesBySubject(subject.Id);
            IEnumerable<Yw_SubjectKnowledge> secondaryKnowledgeEntities =
                knowledgeEntities.Where(k => !k.Ysw_IsMain);
            //前台传过来的次级知识点
            IList<int> knowledges = new List<int>();
            if (subject.Knowledges.HasValue())
            {
                knowledges = subject.Knowledges.Split(',').Select(k => int.Parse(k)).ToList();
            }
            //需要添加的次级知识点
            IEnumerable<Yw_SubjectKnowledge> knowledgesToAdd = null;
            //需要删除的次级知识点
            IEnumerable<int> idsOfknowledgeToDelete = null;
            //数据库的次级知识点
            var knowledgesInDb =
                secondaryKnowledgeEntities.Select(k => k.Ysw_KnowledgeId);
            knowledgesToAdd = knowledges.Except(knowledgesInDb)
                                .Select(k => new Yw_SubjectKnowledge
                                {
                                    Ysw_KnowledgeId = k,
                                    Ysw_IsMain = false,
                                    Ysw_CreateTime = now,
                                    Ysw_Creator = currentUser,
                                    Ysw_UpdateTime = now,
                                    Ysw_Editor = currentUser
                                });
            var knowledgesToDelete = knowledgesInDb.Except(knowledges);
            idsOfknowledgeToDelete =
                secondaryKnowledgeEntities
                    .Where(k => knowledgesToDelete.Contains(k.Ysw_KnowledgeId))
                    .Select(k => k.Ysw_Id);
            toDelete = idsOfknowledgeToDelete;

            return knowledgesToAdd.ToList();
        }
    }
}