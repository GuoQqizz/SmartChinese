using AbhsChinese.Code.Common;
using AbhsChinese.Code.Extension;
using AbhsChinese.Domain.Dto.Request.Subject;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Dto.Response.Subject;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonTranslator;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace AbhsChinese.Bll
{
    /// <summary>
    /// 题目表业务类
    /// </summary>
    public class SubjectBll : BllBase
    {
        private ISubjectContentRepository service;

        private ISubjectIndexRepository subjectIndexService;

        private ISubjectKnowledgeRepository subjectKnowledgeService;
        private ISubjectProcessRepository subjectProcessService;
        private ISubjectRepository subjectService;

        public ISubjectContentRepository Service
        {
            get
            {
                if (service == null)
                {
                    service = new SubjectContentRepository();
                }

                service.TranId = tranId;
                return service;
            }
        }

        public ISubjectIndexRepository SubjectIndexService
        {
            get
            {
                if (subjectIndexService == null)
                {
                    subjectIndexService = new SubjectIndexRepository();
                }

                subjectIndexService.TranId = tranId;
                return subjectIndexService;
            }
        }

        public ISubjectKnowledgeRepository SubjectKnowledgeService
        {
            get
            {
                if (subjectKnowledgeService == null)
                {
                    subjectKnowledgeService = new SubjectKnowledgeRepository();
                }

                subjectKnowledgeService.TranId = tranId;
                return subjectKnowledgeService;
            }
        }

        public ISubjectProcessRepository SubjectProcessService
        {
            get
            {
                if (subjectProcessService == null)
                {
                    subjectProcessService = new SubjectProcessRepository();
                }

                subjectProcessService.TranId = tranId;
                return subjectProcessService;
            }
        }

        public ISubjectRepository SubjectService
        {
            get
            {
                if (subjectService == null)
                {
                    subjectService = new SubjectRepository();
                }

                subjectService.TranId = tranId;
                return subjectService;
            }
        }

        public void AddSubject(Yw_SubjectContent content)
        {
            Service.Insert(content);
        }

        public void DeleteKeywords(IEnumerable<int> keywordIdsToDelete)
        {
            SubjectIndexService.Delete(keywordIdsToDelete);
        }

        public void DeleteKnowledge(Yw_SubjectKnowledge knowledge)
        {
            Check.IfNull(knowledge, nameof(knowledge));

            List<int> ids = new List<int> { knowledge.Ysw_Id };
            SubjectKnowledgeService.Delete(ids);
        }

        public void DeleteSecondaryKnowledges(IEnumerable<int> idsOfknowledgeToDelete)
        {
            SubjectKnowledgeService.Delete(idsOfknowledgeToDelete);
        }

        public IEnumerable<Yw_SubjectIndex> GetKeywordsBySubject(int id)
        {
            return SubjectIndexService.GetSubjectIndexBySubject(id);
        }

        public IEnumerable<Yw_SubjectKnowledge> GetKnowledgesBySubject(int id)
        {
            return SubjectKnowledgeService.GetKnowledgesBySubject(id);
        }

        public Yw_SubjectKnowledge GetMainKnowledge(int mainKnowledgeId)
        {
            return new Yw_SubjectKnowledge { };
        }

        public Yw_SubjectProcess GetNextProcess(
            int subjectId,
            int currentUser,
            SubjectStatusEnum nextStatus,
            SubjectActionEnum action)
        {
            if (currentUser < 10000)
            {
                throw new ArgumentException(nameof(currentUser));
            }
            Yw_SubjectProcess nextProcess = new Yw_SubjectProcess();
            nextProcess.Ysp_Action = (int)action;
            nextProcess.Ysp_CreateTime = DateTime.Now;
            nextProcess.Ysp_Id = 0;
            nextProcess.Ysp_Mark = "";
            nextProcess.Ysp_Operator = currentUser;
            nextProcess.Ysp_Remark = "";
            nextProcess.Ysp_Status = (int)nextStatus;
            nextProcess.Ysp_SubjectId = subjectId;
            if (subjectId >= 10000)
            {
                Yw_SubjectProcess currentProcess = SubjectProcessService.GetCurrentProcess(
                    subjectId);
                if (currentProcess != null)
                {
                    nextProcess.Ysp_Mark = currentProcess.Ysp_Mark;
                    nextProcess.Ysp_Remark = currentProcess.Ysp_Remark;
                }
            }
            return nextProcess;
        }

        public Yw_Subject GetSubject(int id)
        {
            var entity = SubjectService.Get(id);
            if (entity != null)
            {
                entity.EnableAudit();
            }
            return entity;
        }

        public List<Yw_Subject> GetByIds(List<int> ids)
        {
            return SubjectService.GetByIds(ids);
        }

        public Yw_SubjectContent GetSubjectContent(int id)
        {
            return Service.Get(id);
        }

        public List<Yw_SubjectContent> GetSubjectContents(List<int> ids)
        {
            return Service.GetByIds(ids);
        }

        /// <summary>
        /// 获取题目的SubjectContent完整数据
        /// </summary>
        /// <param name="subjectIds"></param>
        /// <returns></returns>
        public IList<DtoSubjectContent> GetCompleteContentsOfSubject(
            IEnumerable<int> subjectIds)
        {
            List<Yw_SubjectContent> contentEntities = Service.GetByIds(subjectIds.ToList());
            List<Yw_Subject> subjectEntities = SubjectService.GetByIds(subjectIds.ToList());
            return contentEntities
                .Select(c => SubjectContentDtoBuilder.Create(c,
                        subjectEntities.FirstOrDefault(s => s.Ysj_Id == c.Ysc_SubjectId)))
                .ToList();
        }

        public void Approve(Yw_Subject subject, Yw_SubjectProcess process)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    UpdateSubject(subject);
                    MoveToNext(process);
                    scope.Complete();
                }
                catch
                {
                    RollbackTran();
                    throw;
                }
            }
        }

        public IList<Yw_Subject> GetSubjectsByIds(IEnumerable<int> ids)
        {
            return SubjectService.GetSubjectsByIds(ids);
        }

        public DtoSubject GetSubjectDetails(int id)
        {
            var subject = SubjectService.Get(id);
            var knowledges = SubjectKnowledgeService.GetKnowledgesWithNamesBySubject(id);
            //次级知识点
            var secondKnowledgeIds = knowledges.Where(k => !k.Ysw_IsMain)
                                             .Select(k => k.Ysw_KnowledgeId)
                                             .ToList();
            var secondKnowledgeNames = knowledges.Where(k => !k.Ysw_IsMain)
                                             .Select(k => k.Ykl_Name)
                                             .ToList();
            var mainKnowledge = knowledges.FirstOrDefault(k => k.Ysw_IsMain);
            int mainKnowledgeId = mainKnowledge == null ? 0 : mainKnowledge.Ysw_KnowledgeId;
            string mainKnowledgeName = mainKnowledge == null ? string.Empty : mainKnowledge.Ykl_Name;

            var content = Service.Get(id);

            string markField = SubjectProcessService.GetCurrentProcess(id)?.Ysp_Mark;
            List<string> mark = null;
            if (!string.IsNullOrWhiteSpace(markField))
            {
                mark = markField.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                .ToList();
            }

            return new DtoSubject
            {
                Id = subject.Ysj_Id,
                Difficulty = subject.Ysj_Difficulty,
                Grade = subject.Ysj_Grade,
                Keywords = subject.Ysj_Keywords,
                Knowledges = secondKnowledgeIds,
                MainKnowledge = mainKnowledgeId,
                MainKnowledgeName = mainKnowledgeName,
                KnowledgeNames = secondKnowledgeNames,
                Name = subject.Ysj_Name,
                Content = content,
                Status = subject.Ysj_Status,
                Mark = mark
            };
        }

        public DtoSubject GetSubjectDetails2(int id)
        {
            DtoSubject result = null;

            var subject = SubjectService.Get(id);
            if (subject != null)
            {
                var knowledges = SubjectKnowledgeService.GetKnowledgesWithNamesBySubject(id);
                //次级知识点
                var secondKnowledgeIds = knowledges.Where(k => !k.Ysw_IsMain)
                                                 .Select(k => k.Ysw_KnowledgeId)
                                                 .ToList();
                var secondKnowledgeNames = knowledges.Where(k => !k.Ysw_IsMain)
                                                 .Select(k => k.Ykl_Name)
                                                 .ToList();
                var mainKnowledge = knowledges.FirstOrDefault(k => k.Ysw_IsMain);
                int mainKnowledgeId = mainKnowledge == null ? 0 : mainKnowledge.Ysw_KnowledgeId;
                string mainKnowledgeName = mainKnowledge == null ? string.Empty : mainKnowledge.Ykl_Name;

                var content = Service.Get(id);

                string markField = SubjectProcessService.GetCurrentProcess(id)?.Ysp_Mark;
                List<string> mark = null;
                if (!string.IsNullOrWhiteSpace(markField))
                {
                    mark = markField.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                    .ToList();
                }

                result = new DtoSubject();
                result.Id = subject.Ysj_Id;
                result.Difficulty = subject.Ysj_Difficulty;
                result.Grade = subject.Ysj_Grade;
                result.Keywords = subject.Ysj_Keywords;
                result.Knowledges = secondKnowledgeIds;
                result.MainKnowledge = mainKnowledgeId;
                result.MainKnowledgeName = mainKnowledgeName;
                result.KnowledgeNames = secondKnowledgeNames;
                result.Name = subject.Ysj_Name;
                result.Content = content;
                result.Status = subject.Ysj_Status;
                result.Mark = mark;
            }

            return result;
        }

        public IList<Yw_Subject> GetSubjects(DtoQuestionSearch search)
        {
            if (search.Id != 0)
            {
                return SubjectService.GetSubjectsById(search);
            }

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                IList<int> subjectIds =
                    SubjectIndexService.GetSubjectIdsByKeyword(search);
                return SubjectService.GetSubjectsByIds(subjectIds);
            }

            return SubjectService.GetByPage(search);
        }

        public void InsertContent(Yw_SubjectContent content)
        {
            Service.Insert(content);
        }

        public void InsertKeywords(IEnumerable<Yw_SubjectIndex> keywordsToAdd)
        {
            SubjectIndexService.Insert(keywordsToAdd);
        }

        public void InsertMainKnowledge(Yw_SubjectKnowledge mainKnowledgeEntity)
        {
            SubjectKnowledgeService.Insert(mainKnowledgeEntity);
        }

        public void InsertSecondaryKnowledges(IEnumerable<Yw_SubjectKnowledge> knowledgesToAdd)
        {
            SubjectKnowledgeService.Insert(knowledgesToAdd);
        }

        public void InsertSubject(Yw_Subject subjectEntity)
        {
            SubjectService.Insert(subjectEntity);
        }

        public void MoveToNext(Yw_SubjectProcess process)
        {
            SubjectProcessService.Insert(process);

            IEnumerable<Yw_SubjectIndex> subjectIdnexes =
                 SubjectIndexService.GetSubjectIndexBySubject(process.Ysp_SubjectId);
            foreach (var item in subjectIdnexes)
            {
                item.EnableAudit();
                item.Ysi_Status = process.Ysp_Status;
                SubjectIndexService.Update(item);
            }
        }

        public int SaveSubject(
            Action<Yw_Subject> saveSubject, Yw_Subject subject,
            Action<Yw_SubjectContent> saveContent, Yw_SubjectContent content,
            Action<Yw_SubjectKnowledge> saveKnowledge, Yw_SubjectKnowledge knowledge,
            IEnumerable<Yw_SubjectIndex> keywordsToAdd, IEnumerable<int> keywordIdsToDelete,
            IEnumerable<Yw_SubjectKnowledge> knowledgesToAdd,
            IEnumerable<int> idsOfknowledgeToDelete,
            Yw_SubjectProcess process,
            FormSubmitButton button)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Check.IfNull(saveSubject, nameof(saveSubject));
                    Check.IfNull(saveContent, nameof(saveContent));
                    Check.IfNull(saveKnowledge, nameof(saveKnowledge));

                    //题目
                    saveSubject(subject);

                    //题目的内容
                    content.Ysc_SubjectId = subject.Ysj_Id;
                    saveContent(content);

                    //主知识点
                    if (knowledge != null)
                    {
                        knowledge.Ysw_SubjectId = subject.Ysj_Id;
                        saveKnowledge(knowledge);
                    }

                    //关键词
                    if (keywordsToAdd != null)
                    {
                        for (int i = 0; i < keywordsToAdd.Count(); i++)
                        {
                            var k = keywordsToAdd.ElementAt(i);
                            k.Ysi_SubjectId = subject.Ysj_Id;
                        }
                    }
                    InsertKeywords(keywordsToAdd);
                    DeleteKeywords(keywordIdsToDelete);

                    //次级知识点
                    if (knowledgesToAdd != null)
                    {
                        for (int i = 0; i < knowledgesToAdd.Count(); i++)
                        {
                            var k = knowledgesToAdd.ElementAt(i);
                            k.Ysw_SubjectId = subject.Ysj_Id;
                        }
                    }
                    InsertSecondaryKnowledges(knowledgesToAdd);
                    DeleteSecondaryKnowledges(idsOfknowledgeToDelete);

                    //如果是提交操作,进入审批流程
                    if (button == FormSubmitButton.Submit)
                    {
                        process.Ysp_SubjectId = subject.Ysj_Id;
                        MoveToNext(process);
                    }

                    scope.Complete();
                    return subject.Ysj_Id;
                }
                catch
                {
                    RollbackTran();
                    throw;
                }
            }
        }

        public void ReEdit(Yw_SubjectProcess process)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Yw_Subject subject = SubjectService.Get(process.Ysp_SubjectId);
                    subject.EnableAudit();
                    subject.Ysj_Status = process.Ysp_Status;
                    SubjectService.Update(subject);

                    MoveToNext(process);

                    scope.Complete();
                }
                catch
                {
                    RollbackTran();
                    throw;
                }
            }
        }

        public void SaveSubject(Yw_SubjectContent content)
        {
            Service.Update(content);
        }

        public void SubmitSubject(
            Yw_Subject subject,
            Yw_SubjectContent content,
            IEnumerable<Yw_SubjectIndex> subjectIndexToAdd,
            IEnumerable<int> subjectIndexToDelete,
            Yw_SubjectKnowledge mainKnowledge,
            IEnumerable<Yw_SubjectKnowledge> knowledgesToAdd,
            IEnumerable<int> knowledgesToDelete,
            Yw_SubjectProcess subjectProcess)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (!Saved(subject.Ysj_Id))
                    {
                        //SaveSubject(
                        //    subject,
                        //    content,
                        //    subjectIndexToAdd,
                        //    subjectIndexToDelete,
                        //    mainKnowledge,
                        //    knowledgesToAdd,
                        //    knowledgesToDelete);
                    }

                    subjectProcess.Ysp_SubjectId = subject.Ysj_Id;
                    MoveSubjectProcessToNext(subjectProcess);

                    scope.Complete();
                }
                catch
                {
                    RollbackTran();
                    throw;
                }
            }
        }

        public void UpdateContent(Yw_SubjectContent subjectEntity)
        {
            Service.Update(subjectEntity);
        }

        public void UpdateMainKnowledge(Yw_SubjectKnowledge mainKnowledgeEntity)
        {
            SubjectKnowledgeService.Update(mainKnowledgeEntity);
        }

        public void UpdateSubject(Yw_Subject subjectEntity)
        {
            SubjectService.Update(subjectEntity);
        }

        private void MoveSubjectProcessToNext(Yw_SubjectProcess entity)
        {
            SubjectProcessService.Insert(entity);
        }

        private bool Saved(int id)
        {
            return id != 0;
        }

        public List<DtoSubjectKnowledge> GetTaskSubjectsAutoForKnowledges(int lessonId, List<int> knowledgeIds)
        {
            return SubjectService.GetTaskSubjectsAutoForKnowledges(lessonId, knowledgeIds);
        }

        public List<DtoLesTaskSubject> GetLessonTaskSubject(int lessonId, List<Tuple<int, int>> errorSubjects)
        {
            return SubjectService.GetLessonTaskSubject(lessonId, errorSubjects);
        }

        /// <summary>
        /// 根据学生已学的课程的课时，随机组题排除最近学生已经练习的题目
        /// </summary>
        public List<Yw_Subject> GetSubjectForPractice(int studentId, int courseId, int lessonIndex, int returnCount)
        {
            List<Yw_Subject> result = SubjectService.GetSubjectForPractice(studentId, courseId, lessonIndex, returnCount);
            return result;
        }

        public List<Yw_Subject> GetSubjectForWrongClear(int subjectId)
        {
            var relationList = SubjectService.GetSubjectRelationBySubjectId(subjectId);
            relationList = relationList.Where(s => s.Ysj_SubjectType != (int)SubjectTypeEnum.圈点批注断句 && s.Ysj_SubjectType != (int)SubjectTypeEnum.圈点批注标色).Select(s => s).ToList();
            if (relationList == null || relationList.Count == 0)
            {
                relationList = new List<Yw_Subject>();
                relationList.Add(SubjectService.Get(subjectId));
            }
            return SelectSubjectRealation(relationList, subjectId);
        }

        public List<Yw_Subject> SelectSubjectRealation(List<Yw_Subject> relationList, int subjectId)
        {
            List<Yw_Subject> res = new List<Yw_Subject>();
            if (relationList == null || relationList.Count == 0)
            {
                res.Add(SubjectService.Get(subjectId));
            }
            else if (relationList.Count <= 5)
            {
                res = relationList;
            }
            else
            {
                //需要5道题才能消除错题。前两道题是简单题（1或2难度的），中间两道是中等题（3或4难度），最后一道是难题（5难度）的
                //var listTmp = relationList.RandomSortList();
                var listTmp = relationList;
                int easy = 2;
                int mid = 2;
                int hard = 1;
                res = res.Concat(listTmp.Where(s => s.Ysj_Difficulty <= 2).Take(easy)).ToList();
                mid += easy - res.Count();
                res = res.Concat(listTmp.Where(s => s.Ysj_Difficulty <= 4 && s.Ysj_Difficulty > 2).Take(mid)).ToList();
                hard += 4 - res.Count;
                hard = hard > 2 ? 2 : hard;
                res = res.Concat(listTmp.Where(s => s.Ysj_Difficulty == 5).Take(hard)).ToList();
                if (res.Count < 5)
                {
                    res = res.Concat(listTmp.ExceptExt(res, l => l.Ysj_Id).Take(5 - res.Count())).ToList();
                }
            }
            res.OrderBy(s => s.Ysj_Difficulty);
            return res;
        }
    }
}