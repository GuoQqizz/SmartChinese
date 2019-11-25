using AbhsChinese.Domain.Dto.Response.StudentPractice;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;

namespace AbhsChinese.Bll
{
    public class CourseSubjectRelBll : BllBase
    {
        private ICourseSubjectRelRepository repository;
        public ICourseSubjectRelRepository Repository
        {
            get
            {
                if (repository == null)
                {
                    repository = new CourseSubjectRelRepository();
                }
                repository.TranId = tranId;
                return repository;
            }
        }

        /// <summary>
        /// 将一组题目加入到课时，题目与课时建立直接关系，题目的关联题目与课时建立间接关系
        /// </summary>
        /// <param name="lesson"></param>
        /// <param name="subjectId"></param>
        public void AddSubjectToLesson(Yw_CourseLesson lesson, List<int> subjectIds)
        {
            List<int> newSubjectIds = subjectIds.Distinct().ToList();
            List<Yw_CourseSubjectRelation> existingRel = Repository.GetDirectRelationsOfLesson(lesson.Ycl_Id);

            List<int> existingSubIds = existingRel.Select(x => x.Ysr_SubjectId).ToList();
            List<int> deletedIds = existingSubIds.Except(newSubjectIds).ToList();
            List<int> addIds = newSubjectIds.Except(existingSubIds).ToList();

            if (addIds.Count > 0)
            {
                Dictionary<int, int[]> dic = GetGroupDictionary(addIds);
                IEnumerable<int> tempSubjectIds = GetAllItemOfDictionary(dic);

                SubjectBll bll = new SubjectBll();
                List<Yw_Subject> subjects = bll.GetByIds(tempSubjectIds.ToList());
                Dictionary<int, Yw_Subject> subDic = subjects.ToDictionary(x => x.Ysj_Id, x => x);

                DataTable table = BuildBulkTable();

                foreach (KeyValuePair<int, int[]> pair in dic)
                {
                    if (subDic.ContainsKey(pair.Key))
                    {
                        BuildBulkRow(table, lesson.Ycl_CourseId, lesson.Ycl_Id, lesson.Ycl_Index, pair.Key, subDic[pair.Key].Ysj_SubjectType, true, pair.Key);

                    }
                    foreach (int id in pair.Value)
                    {
                        if (subDic.ContainsKey(id))
                        {
                            BuildBulkRow(table, lesson.Ycl_CourseId, lesson.Ycl_Id, lesson.Ycl_Index, id, subDic[id].Ysj_SubjectType, false, pair.Key);
                        }
                    }
                }

                Repository.AddBatch(table);
            }

            if (deletedIds.Count > 0)
            {
                Repository.DeleteDirectRelationOfLesson(lesson.Ycl_Id, deletedIds);
            }
        }

        public void AddSubjectToLesson(Yw_CourseLesson lesson, int subjectId)
        {
            Yw_CourseSubjectRelation rel = Repository.GetDirectRelation(lesson.Ycl_Id, subjectId);
            if (rel == null)
            {
                SubjectBll subjectBll = new SubjectBll();
                Yw_Subject subject = subjectBll.GetSubject(subjectId);

                rel = new Yw_CourseSubjectRelation();
                rel.Ysr_CourseId = lesson.Ycl_CourseId;
                rel.Ysr_CreateTime = DateTime.Now;
                rel.Ysr_DirectSubjectId = 0;
                rel.Ysr_IsDirect = true;
                rel.Ysr_LessonId = lesson.Ycl_Id;
                rel.Ysr_LessonIndex = lesson.Ycl_Index;
                rel.Ysr_SubjectId = subjectId;
                rel.Ysr_SubjectType = subject.Ysj_SubjectType;
                Repository.Add(rel);

                SubjectGroupBll groupBll = new SubjectGroupBll();
                Yw_SubjectGroup group = groupBll.GetBySubjectId(subjectId);
                if (group != null && !string.IsNullOrEmpty(group.Ysg_RelSubjectId))
                {
                    int[] subjectIds = group.Ysg_RelSubjectId.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                    Repository.CreateInDirectRelation(rel.Ysr_Id, subjectIds);
                }
            }
        }

        /// <summary>
        /// 将题目从课时中移除，删除题目与课时的直接关系，同时删除题目的关联题目与课时建立的间接关系
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="subjectId"></param>
        public void RemoveSubjectFromLesson(int lessonId,int subjectId)
        {
            Repository.DeleteDirectRelation(lessonId, subjectId);
        }

        /// <summary>
        /// 添加题目到题目组，A,B两个题目互为主副关系。A如果与某些课时有直接关联关系，就要创建B与该课时的间接关联关系；同理B也一样。
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="targetSubjectId"></param>
        public void AddSubjectToGroup(int subjectId, int targetSubjectId)
        {
            SubjectBll subjectBll = new SubjectBll();

            Yw_Subject subject = subjectBll.GetSubject(subjectId);
            Yw_Subject targetSubject = subjectBll.GetSubject(targetSubjectId);

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Repository.AddSubjectToGroup(subjectId, subject.Ysj_SubjectType, targetSubjectId);
                    Repository.AddSubjectToGroup(targetSubjectId, targetSubject.Ysj_SubjectType, subjectId);
                    scope.Complete();
                }
                catch
                {
                    RollbackTran();
                    throw;
                }
            }
        }

        /// <summary>
        /// 题目组中移除题目，如果A与某些课时有直接关系，则移除B与该课时的间接关系（基于A创建的关系）；同理B也一样。
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="targetSubjectId"></param>
        public void RemoveSubjectFromGroup(int subjectId, int targetSubjectId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Repository.DeleteInDirectRelation(subjectId, targetSubjectId);
                    Repository.DeleteInDirectRelation(targetSubjectId, subjectId);
                    scope.Complete();
                }
                catch
                {
                    RollbackTran();
                    throw;
                }
            }
        }

        private Dictionary<int, int[]> GetGroupDictionary(List<int> subjectIds)
        {
            SubjectGroupBll groupBll = new SubjectGroupBll();
            IList<Yw_SubjectGroup> groups = groupBll.GetBySubjectIds(subjectIds);

            Dictionary<int, int[]> dic = new Dictionary<int, int[]>();

            foreach (Yw_SubjectGroup g in groups)
            {
                if (!string.IsNullOrEmpty(g.Ysg_RelSubjectId))
                {
                    string[] tempIdStrs = g.Ysg_RelSubjectId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    int[] tempIds = tempIdStrs.Select(x => Convert.ToInt32(x)).ToArray();
                    dic[g.Ysg_SubjectId] = tempIds;
                }
            }

            return dic;
        }

        private IEnumerable<int> GetAllItemOfDictionary(Dictionary<int, int[]> dic)
        {
            List<int> list = new List<int>();
            foreach (KeyValuePair<int, int[]> item in dic)
            {
                list.Add(item.Key);
                list.AddRange(item.Value.ToArray());
            }
            return list.Distinct();
        }

        private DataTable BuildBulkTable()
        {
            DataTable table = new DataTable("Yw_CourseSubjectRelation");
            table.Columns.Add("Ysr_CourseId", typeof(int));
            table.Columns.Add("Ysr_LessonId", typeof(int));
            table.Columns.Add("Ysr_LessonIndex", typeof(int));
            table.Columns.Add("Ysr_SubjectId", typeof(int));
            table.Columns.Add("Ysr_SubjectType", typeof(int));
            table.Columns.Add("Ysr_IsDirect", typeof(bool));
            table.Columns.Add("Ysr_DirectSubjectId", typeof(int));
            table.Columns.Add("Ysr_CreateTime", typeof(DateTime));
            return table;
        }

        private void BuildBulkRow(DataTable table,int courseId,int lessonId,int lessonIndex,int subjectId,int subjectType,bool isDirect,int directSubjectId)
        {
            DataRow row = table.NewRow();
            row["Ysr_CourseId"] = courseId;
            row["Ysr_LessonId"] = lessonId;
            row["Ysr_LessonIndex"] = lessonIndex;
            row["Ysr_SubjectId"] = subjectId;
            row["Ysr_SubjectType"] = subjectType;
            row["Ysr_IsDirect"] = isDirect;
            row["Ysr_DirectSubjectId"] = directSubjectId;
            row["Ysr_CreateTime"] = DateTime.Now;
            table.Rows.Add(row);
        }
        /// <summary>
        /// 获取课后任务中的某个课程每个课时有多少练习题
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="targetSubjectId"></param>
        public IList<DtoSubjectCountToPractice> GetSubjectCountOfStudyPractice(
            IEnumerable<Tuple<int,int>> courseIds)
        {
            return Repository.GetSubjectIdsOfStudyTask(courseIds);
        }
    }
}

