using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request.StudyTask;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.StudentLessonAnswer;
using AbhsChinese.Domain.Entity.StudyTaskAnswer;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using AbhsChinese.Domain.Dto.Response.StudentPractice;
using AbhsChinese.Domain.Message;
using Newtonsoft.Json;
using AbhsChinese.Bll.Message;
using AbhsChinese.Domain.Dto.Response.Subject;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Dto.Request.StudentWrong;

namespace AbhsChinese.Bll
{
    public class StudentPracticeBll : BllBase
    {
        private IStudentTaskRepository studentTaskRepository;
        public IStudentTaskRepository StudentTaskRepository
        {
            get
            {
                if (studentTaskRepository == null)
                {
                    studentTaskRepository = new StudentTaskRepository();
                }
                return studentTaskRepository;
            }
        }

        private IStudyTaskRepository studyTaskRepository;
        public IStudyTaskRepository StudyTaskRepository
        {
            get
            {
                if (studyTaskRepository == null)
                {
                    studyTaskRepository = new StudyTaskRepository();
                }
                return studyTaskRepository;
            }
        }

        private IStuStudyTaskAnsRepository stuStudyTaskAnsRepo;
        public IStuStudyTaskAnsRepository StuStudyTaskAnsRepo
        {
            get
            {
                if (stuStudyTaskAnsRepo == null)
                {
                    stuStudyTaskAnsRepo = new StuStudyTaskAnsRepository();
                }
                return stuStudyTaskAnsRepo;
            }
        }

        public IList<DtoStudyTaskListItem> GetStudyTasks(DtoStudyTaskSearch search)
        {
            var studyTasks = StudentTaskRepository.GetEntities(search);
            return studyTasks;
        }

        /// <summary>
        /// 学生完成课时学习后，创建课后任务
        /// </summary>
        public Yw_StudentTask CreateTaskAutoAfterStudy(int studentId, int lesProgressId)
        {
            StudentStudyBll studyBll = new StudentStudyBll();
            Yw_StudentLessonProgress lesProgress = studyBll.GetStuLessonProgressById(lesProgressId);
            if (lesProgress.Yle_Percent < 100 && lesProgress.Yle_SubjectCount > 0)//学生课时学习中，如果题目全部作对，就不创建课后任务
            {
                Yw_StudentCourseProgress progress = studyBll.GetProgressByStudentCourse(studentId, lesProgress.Yle_CourseId);
                return CreateStudyPractice(progress, studentId, lesProgress.Yle_LessonId, lesProgress.Yle_LessonIndex, lesProgressId, null, StudyTaskTypeEnum.系统课后任务);
            }
            return null;
        }

        public IList<DtoStatusTotalRecord> GetTotalRecordByStatus(DtoStudyTaskSearch search)
        {
            var result = StudentTaskRepository.GetTotalRecordByStatus(search);
            return result;
        }

        /// <summary>
        /// 学生开始课后任务，生成题目
        /// </summary>
        public void GenerateTaskSubjectsAutoAfterStudy(int studyTaskId, int studentId)
        {
            StudentStudyBll studyBll = new StudentStudyBll();
            SubjectBll subjectBll = new SubjectBll();

            Yw_StudyTask task = StudyTaskRepository.Get(studyTaskId);
            Yw_StudentTask stuTask = StudentTaskRepository.GetByStudentTask(studentId, studyTaskId);

            if (task == null || stuTask == null)
            {
                return;
            }

            Yw_StudentLessonAnswerExt answer = studyBll.GetLessonAnswer(task.Ysk_LessonProgressId) as Yw_StudentLessonAnswerExt;
            if (answer == null)
            {
                return;
            }

            List<StudentAnswerBase> problemAnswers = answer.Yla_Answer_Obj.SelectMany(x => x.AnswerCollection).Where(x => x.ResultStars < 5).ToList();
            List<Tuple<int, int>> errorSubjects = problemAnswers.GroupBy(x => x.SubjectId).
                Select(x => new Tuple<int, int>(x.Key, x.Min(y => y.ResultStars))).ToList();

            List<DtoLesTaskSubject> subjects = subjectBll.GetLessonTaskSubject(task.Ysk_LessonId, errorSubjects);

            var groups = subjects.OrderBy(x => x.Number)
                .GroupBy(x => new { ErrorSubjectId = x.ErrorSubjectId, Score = x.Score })
                .OrderBy(x => x.Key.Score);

            Dictionary<int, DtoLesTaskSubject> subjectSelected = new Dictionary<int, DtoLesTaskSubject>();

            List<WrapSubjectGroup> wrapGroups = new List<WrapSubjectGroup>();
            foreach (IGrouping<dynamic, DtoLesTaskSubject> group in groups)
            {
                wrapGroups.Add(new WrapSubjectGroup(group.Key.Score, group.ToList()));
            }

            bool hasSubject = false;
            bool reachMax = false;
            int maxSubjectCount = groups.Count() > 50 ? groups.Count() : 50;//每个错题至少推一个关联题目
            int loopTime = 0;
            while (loopTime < maxSubjectCount)
            {
                hasSubject = false;
                foreach (WrapSubjectGroup group in wrapGroups)
                {
                    bool result = group.TakeSubject(subjectSelected);
                    if (result)
                    {
                        if (subjectSelected.Count >= maxSubjectCount)
                        {
                            reachMax = true;
                            break;
                        }
                        hasSubject = true;
                    }
                }
                if (!hasSubject || reachMax)
                {
                    break;
                }
                loopTime++;
            }

            if (subjectSelected.Count > 0)
            {
                task.Ysk_SubjectIds = string.Join(",", subjectSelected.OrderBy(x => x.Value.SubjectType).Select(x => x.Value.SubjectId));
                task.Ysk_SubjectCount = subjectSelected.Count;
                task.Ysk_Score = 5 * subjectSelected.Count;
                StudyTaskRepository.Update(task);

                stuTask.Yuk_SubjectCount = subjectSelected.Count;
                StudentTaskRepository.Update(stuTask);
            }
        }

        /// <summary>
        /// 分批次返回课后任务的题目，每次返回10个题目数据
        /// </summary>
        public List<DtoSubjectContent> GetTaskSubject(int studyTaskId, int pageIndex, out int totalCount)
        {
            List<DtoSubjectContent> subjectContents = new List<DtoSubjectContent>();
            Yw_StudyTask task = StudyTaskRepository.Get(studyTaskId);
            string[] ids = task.Ysk_SubjectIds.Split(',');
            totalCount = ids.Length;
            if ((pageIndex - 1) * 10 < ids.Length)
            {
                var pageIds = ids.Skip((pageIndex - 1) * 10).Take(10).ToList();
                Dictionary<string, int> orderDic = pageIds.ToOrderDic();
                SubjectBll subjectBll = new SubjectBll();
                subjectContents = subjectBll.GetCompleteContentsOfSubject(
                    pageIds.Select(x => Convert.ToInt32(x))).ToList();
                subjectContents = subjectContents.OrderBy(x => orderDic[x.SubjectId.ToString()]).ToList();
            }

            return subjectContents;
        }

        /// <summary>
        /// 生成课后练习,根据学生已学的课程的课时，随机组题排除最近学生已经练习的题目
        /// </summary>
        public Yw_StudentTask GeneratePractice(int studentId, int courseId, int returnCount)
        {
            SubjectBll subjectBll = new SubjectBll();
            StudentStudyBll studyBll = new StudentStudyBll();

            Yw_StudentCourseProgress progress = studyBll.GetProgressByStudentCourse(studentId, courseId);
            if (progress != null && progress.Yps_NextLessonIndex > 1)
            {
                List<Yw_Subject> subjects = subjectBll.GetSubjectForPractice(studentId, courseId, progress.Yps_NextLessonIndex, returnCount);
                if (subjects.Count > 0)
                {
                    var task = CreateStudyPractice(progress, studentId, 0, progress.Yps_NextLessonIndex, 0, subjects.OrderBy(x => x.Ysj_SubjectType).Select(x => x.Ysj_Id).ToList(), StudyTaskTypeEnum.课后练习);
                    return task;
                }
            }
            return null;
        }

        public int CalcCoins(List<StudentAnswerBase> answers)
        {
            //一次课后任务或练习，所应得的金币总数
            int totalCoins = 0;
            //某道题应得的金币数
            double coinsOfAnswer = 0;

            foreach (var answer in answers)
            {
                SubjectTypeEnum type = (SubjectTypeEnum)answer.Type;
                switch (type)
                {
                    //选择题 判断题，做对给5个金币，做错给0个金币
                    case SubjectTypeEnum.选择题:
                    case SubjectTypeEnum.判断题:
                        coinsOfAnswer = answer.IsRight ? 5 : 0;
                        break;
                    //填空 选择填空 连线 圈点批注, 10个金币
                    case SubjectTypeEnum.填空题:
                    case SubjectTypeEnum.选择填空:
                    case SubjectTypeEnum.连线题:
                    case SubjectTypeEnum.圈点批注标色:
                    case SubjectTypeEnum.圈点批注断句:
                        coinsOfAnswer = 10 * (answer.ResultStars * 1.0 / 5);
                        break;
                    //首先根据答案、解析和打星规则打星，然后根据星级不同给金币,20个金币
                    case SubjectTypeEnum.主观题:
                        coinsOfAnswer = 20 * (answer.ResultStars * 1.0 / 5);
                        break;
                    default:
                        break;
                }
                answer.ResultCoins = (int)coinsOfAnswer;

                totalCoins += answer.ResultCoins;
            }

            return totalCoins;
        }

        /// <summary>
        /// 保存课后任务或者练习题答案
        /// </summary>
        public void SaveResult(int studentId, int taskId, int useTime, List<StudentAnswerBase> answers, StudyTaskTypeEnum taskType = 0, int partId = 0)
        {             
            Yw_StudentTask stk = StudentTaskRepository.GetByStudentTask(studentId, taskId);
            if (stk.Yuk_Status == (int)StudentTaskStatusEnum.已完成)
            {
                throw new AbhsException(ErrorCodeEnum.StudentTaskStatusInvalid, AbhsErrorMsg.ConstStudentTaskStatusInvalid);
            }

            int totalStars = answers.Sum(x => x.ResultStars);
            int totalCoins = CalcCoins(answers);
            int fiveStarsAnswers = answers.Count(x => x.ResultStars == 5);

            var finishTime = DateTime.Now;
            //更新学生任务记录表

            stk.Yuk_StartTime = finishTime.AddSeconds(0 - useTime);//反算时间
            stk.Yuk_FinishTime = finishTime;
            stk.Yuk_StudentScore = totalStars;
            stk.Yuk_GainCoins = totalCoins;
            stk.Yuk_RightSubjectCount = fiveStarsAnswers;

            if (answers.Count > 0)
            {
                stk.Yuk_Percent = (int)(Math.Round(fiveStarsAnswers * 1.0 / answers.Count, 2) * 100);
            }

            stk.Yuk_Status = (int)StudentTaskStatusEnum.已完成;
            StudentTaskRepository.Update(stk);

            StudentAnswerCard card = new StudentAnswerCard();
            card.UseTime = useTime;
            card.SubmitTime = finishTime.ToString("yyyy-MM-dd HH:mm:ss");
            card.Part = partId;
            card.AnswerCollection = answers;
            card.TotalStars = totalStars;
            card.TotalCoins = totalCoins;

            //学生任务答题结果
            Yw_StudentStudyTaskAnswerExt answer = new Yw_StudentStudyTaskAnswerExt();
            answer.Yta_StudentId = studentId;
            answer.Yta_StudentStudyTaskId = stk.Yuk_Id;
            answer.Yta_TaskId = taskId;
            answer.Yta_Answer_Obj = card;
            answer.Yta_CreateTime = DateTime.Now;
            StuStudyTaskAnsRepo.Add(answer);

            //学生最近答题记录
            Yw_StudyTask task = StudyTaskRepository.Get(taskId);
            StudentBll studentBll = new StudentBll();
            List<int> ids = task.Ysk_SubjectIds.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            studentBll.RefreshStudentRecentSubject(studentId, ids);

            StudentInfoBll bll = new StudentInfoBll();
            bll.AddCoins(studentId, totalCoins);

            if (taskType == StudyTaskTypeEnum.系统课后任务)
            {
                new StudentPracticeBll().PublishStudyTaskMessage(
                    task.Ysk_CourseId,
                    task.Ysk_LessonId,
                    studentId,
                    useTime,
                    answers.Count,
                    totalCoins);
            }
            else if (taskType == StudyTaskTypeEnum.课后练习)
            {
                new StudentPracticeBll().PublishStudyPractiseMessage(
                    task.Ysk_CourseId,
                    task.Ysk_LessonId,
                    studentId,
                    useTime,
                    answers.Count,
                    totalCoins);
            }

            //课程学习 = 1, 课后任务 = 2, 课后练习 = 3
            StudyWrongSourceEnum? source = null;
            switch (taskType)
            {
                case StudyTaskTypeEnum.系统课后任务:
                case StudyTaskTypeEnum.老师课后任务:
                    source = StudyWrongSourceEnum.课后任务;
                    break;
                case StudyTaskTypeEnum.课后练习:
                    source = StudyWrongSourceEnum.课后练习;
                    break;
                default:
                    source = StudyWrongSourceEnum.课后任务;
                    break;
            }
            new StudentWrongBookBll().SaveWrongBook(
                new List<StudentAnswerCard> { card },
                new DtoStudentWrongBook
                {
                    CourseId = task.Ysk_CourseId,
                    LessonId = task.Ysk_LessonId,
                    LessonProgressId = task.Ysk_LessonProgressId,
                    Source = source.Value,
                    StudentId = studentId,
                    StudyTaskId = taskId
                });
        }

        public Yw_StudentTask CreateStudyPractice(Yw_StudentCourseProgress progress, int studentId, int lessonId, int lessonIndex, int lessonProgressId, List<int> subjectIds, StudyTaskTypeEnum taskType)
        {
            Yw_StudyTask st = new Yw_StudyTask();
            st.Ysk_ClassId = progress.Yps_ClassId;
            st.Ysk_CourseId = progress.Yps_CourseId;
            st.Ysk_CreateTime = DateTime.Now;
            st.Ysk_ExpiredTime = DateTime.Now.AddDays(7);
            st.Ysk_LessonId = lessonId;
            st.Ysk_LessonIndex = lessonIndex;
            st.Ysk_LessonProgressId = lessonProgressId;
            st.Ysk_SchoolId = progress.Yps_SchoolId;
            st.Ysk_Score = 0;
            st.Ysk_Status = (int)StatusEnum.有效;
            st.Ysk_SubjectCount = subjectIds == null ? 0 : subjectIds.Count;
            st.Ysk_SubjectIds = subjectIds == null ? "" : string.Join(",", subjectIds);
            st.Ysk_TaskType = (int)taskType;
            st.Ysk_TeacherId = 0;
            StudyTaskRepository.Add(st);

            Yw_StudentTask stk = new Yw_StudentTask();
            stk.Yuk_StudentId = studentId;
            stk.Yuk_CourseId = progress.Yps_CourseId;
            stk.Yuk_CreateTime = DateTime.Now;
            stk.Yuk_StartTime = new DateTime(1900, 1, 1);
            stk.Yuk_FinishTime = new DateTime(1900, 1, 1);
            stk.Yuk_ExpiredTime = DateTime.Now.AddDays(7);
            stk.Yuk_GainCoins = 0;
            stk.Yuk_LessonId = lessonId;
            stk.Yuk_LessonIndex = lessonIndex;
            stk.Yuk_Percent = 0;
            stk.Yuk_RightSubjectCount = 0;
            stk.Yuk_SchoolId = progress.Yps_SchoolId;
            stk.Yuk_Status = (int)StudentTaskStatusEnum.未开始;
            stk.Yuk_StudentScore = 0;
            stk.Yuk_TaskId = st.Ysk_Id;
            stk.Yuk_TaskType = (int)taskType;
            stk.Yuk_SubjectCount = subjectIds == null ? 0 : subjectIds.Count;
            StudentTaskRepository.Add(stk);

            return stk;
        }

        /// <summary>
        /// 获取某个课程下每个课时练习过多少题
        /// </summary>
        /// <param name="courseIds"></param>
        /// <returns></returns>
        public IList<DtoSubjectCountToPractice> GetTotalSubjectsPracticed(
            IList<int> courseIds,
            int studentId)
        {
            return StudyTaskRepository.GetTotalSubjectsPracticed(courseIds, studentId);
        }

        /// <summary>
        /// 发送学生的课后任务消息（进行数据统计）
        /// </summary>
        public void PublishStudyTaskMessage(int courseId, int lessonId, int studentId, int studySeconds, int subjectCount, int getCoins)
        {
            MessageBll bll = new MessageBll();

            CourseStudyTaskMessage msg = new CourseStudyTaskMessage();
            msg.AsOfDate = DateTime.Now.Date;
            msg.CourseId = courseId;
            msg.LessonId = lessonId;
            msg.StudentId = studentId;
            msg.StudySeconds = studySeconds;
            msg.SubjectCount = subjectCount;
            msg.GetCoins = getCoins;
            string msgBody = JsonConvert.SerializeObject(msg);

            bll.PublishMessage(MessageChannel.REPORT_CHANNEL, MessageTypeEnum.课后任务, msgBody);
        }

        /// <summary>
        /// 发送学生的课后练习消息（进行数据统计）
        /// </summary>
        public void PublishStudyPractiseMessage(int courseId, int lessonId, int studentId, int studySeconds, int subjectCount, int getCoins)
        {
            MessageBll bll = new MessageBll();

            CourseStudyPractiseMessage msg = new CourseStudyPractiseMessage();
            msg.AsOfDate = DateTime.Now.Date;
            msg.CourseId = courseId;
            msg.LessonId = lessonId;
            msg.StudentId = studentId;
            msg.StudySeconds = studySeconds;
            msg.SubjectCount = subjectCount;
            msg.GetCoins = getCoins;
            string msgBody = JsonConvert.SerializeObject(msg);

            bll.PublishMessage(MessageChannel.REPORT_CHANNEL, MessageTypeEnum.课后练习, msgBody);
        }

        public class WrapSubjectGroup
        {
            public WrapSubjectGroup(int score, List<DtoLesTaskSubject> subjects)
            {
                Score = score;
                SeekPos = 0;
                TakenSubjectCount = 0;
                Subjects = subjects;
            }

            public int Score
            {
                set; get;
            }
            public List<DtoLesTaskSubject> Subjects
            {
                set; get;
            }

            public int SeekPos
            {
                set; get;
            }

            public int TakenSubjectCount
            {
                set; get;
            }

            public bool TakeSubject(Dictionary<int, DtoLesTaskSubject> subjectSelected)
            {
                if (!NeedTake)
                {
                    return false;
                }

                while (SeekPos < Subjects.Count)
                {
                    DtoLesTaskSubject subject = Subjects[SeekPos];
                    SeekPos++;
                    if (!subjectSelected.ContainsKey(subject.SubjectId))
                    {
                        if (subject.SubjectId != subject.ErrorSubjectId || TakenSubjectCount == 0)
                        {
                            TakenSubjectCount++;
                            subjectSelected.Add(subject.SubjectId, subject);
                            return true;
                        }
                    }
                }
                return false;
            }

            private bool NeedTake
            {
                get
                {
                    if ((Score <= 2 && TakenSubjectCount < 2) || (Score > 2 && TakenSubjectCount < 1))
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取课程和课时信息
        /// </summary>
        public DtoStudyTaskListItem GetCourseLessonNameByStudyTask(int studentTaskId)
        {
            DtoStudyTaskListItem item =
                StudyTaskRepository.GetCourseLessonByStudyTask(studentTaskId);
            return item;
        }
    }


}
