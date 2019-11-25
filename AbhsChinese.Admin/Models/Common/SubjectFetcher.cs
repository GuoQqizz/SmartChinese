using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using System;

namespace AbhsChinese.Admin.Models.Common
{
    public static class SubjectFetcher
    {
        public static Action<Yw_Subject> Fetch(
            QuestionInputModel subject,
            SubjectBll bll,
            int currentUser,
            out Yw_Subject entity)
        {
            Check.IfNull(subject, nameof(subject));
            if (currentUser < 10000)
            {
                throw new ArgumentException(
                    "当前登录用户的ID不能小于10000",
                    nameof(currentUser));
            }

            Action<Yw_Subject> saveSubject = null;
            Yw_Subject subjectEntity = null;
            if (subject.Id == 0)
            {
                subjectEntity = new Yw_Subject();
                subjectEntity.Ysj_CreateTime = DateTime.Now;
                subjectEntity.Ysj_Creator = currentUser;
                subjectEntity.Ysj_Status = (int)SubjectStatusEnum.编辑中;
                saveSubject = bll.InsertSubject;
            }
            else
            {
                subjectEntity = bll.GetSubject(subject.Id);
                saveSubject = bll.UpdateSubject;
            }
            subjectEntity.Ysj_UpdateTime = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(subject.PlainName))
            {
                string n = subject.PlainName;
                if (n.Length <= 50)
                {
                    subjectEntity.Ysj_Name = n;
                }
                else
                {
                    subjectEntity.Ysj_Name = n.Substring(0, 50);
                }
            }
            subjectEntity.Ysj_Keywords = subject.Keywords;
            subjectEntity.Ysj_Grade = subject.Grade;
            subjectEntity.Ysj_Editor = currentUser;
            subjectEntity.Ysj_Difficulty = subject.Difficulty;
            subjectEntity.Ysj_SubjectType = subject.SubjectType;
            subjectEntity.Ysj_MainKnowledgeId = subject.Knowledge;

            entity = subjectEntity;
            return saveSubject;
        }
    }
}