using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.StudentLessonAnswer;
using AbhsChinese.Domain.Entity.StudyTaskAnswer;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbhsChinese.Bll
{
    public class StudentReportBll : BllBase
    {
        public StudentReportBll() : base()
        {

        }

        /// <summary>
        /// 任务报告和练习报告数量
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public List<Yw_StudentTask> StuTaskReportCount(int studentId)
        {
            StudentPracticeBll studentPracticeBll = new StudentPracticeBll();
            return studentPracticeBll.StudentTaskRepository.StuTaskReportCount(studentId);
        }
        /// <summary>
        /// 学习报告数量
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public int StuCourseReportCount(int studentId)
        {
            StudentStudyBll studentStudyBll = new StudentStudyBll();
            return studentStudyBll.StuLesProgressRepository.StuCourseReportCount(studentId);
        }


        /// <summary>
        /// 查询学生全部报告
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public List<DtoStudentReportList> GetStudentReport(PagingObject paging, int studentId, int reportType)
        {
            List<DtoStudentReportList> reportList = new List<DtoStudentReportList>();
            StudentStudyBll studentStudyBll = new StudentStudyBll();
            if (reportType == 0)
            {
                reportList = studentStudyBll.StuLesProgressRepository.GetStudentReport(paging, studentId);
            }
            else if (reportType == (int)CourseReportEnum.练习报告 || reportType == (int)CourseReportEnum.任务报告)
            {
                StudentPracticeBll studentPracticeBll = new StudentPracticeBll();
                reportList = studentPracticeBll.StudentTaskRepository.GetStuTaskReport(paging, studentId, reportType);

            }
            else if (reportType == (int)CourseReportEnum.学习报告)
            {
                reportList = studentStudyBll.StuLesProgressRepository.GetStuCourseReport(paging, studentId);
            }
            return reportList;
        }

        //获取任务报告课程信息部分
        public DtoStudentReportList GetStuTaskReportById(int taskId)
        {
            StudentPracticeBll studentPracticeBll = new StudentPracticeBll();
            var info = studentPracticeBll.StudentTaskRepository.GetStuTaskReportById(taskId);
            if (info.ReportType == (int)CourseReportEnum.练习报告)
            {
                info.PraticeCount = studentPracticeBll.StudentTaskRepository.GetPracticeReportCount(info.StudentId, info.CourseId, taskId);
            }
            return info;
        }

        //获取学习报告课程信息部分
        public DtoStudentReportList GetStuCourseReportById(int lessonProcessId)
        {
            StudentStudyBll studentStudyBll = new StudentStudyBll();
            var info = studentStudyBll.StuLesProgressRepository.GetStuCourseReportById(lessonProcessId);
            return info;
        }

        /// <summary>
        /// 获取学生任务报告的统计部分
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public DtoStudentReport GetByStudentTask(int studentId, int taskId)
        {
            StudentPracticeBll studentPracticeBll = new StudentPracticeBll();
            var result = studentPracticeBll.StudentTaskRepository.IsHasTaskReport(studentId, taskId);
            if (!result)
            {
                throw new AbhsException(ErrorCodeEnum.NotStudyReport, AbhsErrorMsg.ConstNotStudyReport);
            }
            //获取学生答案
            Yw_StudentStudyTaskAnswerExt answer = studentPracticeBll.StuStudyTaskAnsRepo.GetByStudentTask(studentId, taskId) as Yw_StudentStudyTaskAnswerExt;
            StudentAnswerCard answerCard = answer.Yta_Answer_Obj;

            DtoStudentReport studentReport = new DtoStudentReport();
            if (answerCard != null)
            {
                studentReport.StudyDate = answerCard.SubmitTime;
                studentReport.TotalScore = Convert.ToInt32(Math.Round(answerCard.TotalStars * 1.0 / (answerCard.AnswerCollection.Count() * 5) * 5,MidpointRounding.AwayFromZero));
                studentReport.TotalStars = answerCard.TotalStars;
                studentReport.StudyTime = Convert.ToInt32(Math.Ceiling(answerCard.UseTime * 1.0 / 60));
                studentReport.ResultCoins = answerCard.AnswerCollection.Select(s => s.ResultCoins).Sum();
                studentReport.TotalCoins = answerCard.TotalCoins;
                studentReport.SubjectCount = answerCard.AnswerCollection.Count();

                double rates = Math.Round((double)(answerCard.AnswerCollection.Where(x => x.ResultStars == 5).Count()) / answerCard.AnswerCollection.Count(), 2) * 100;
                studentReport.ExcellentRates = rates;

                List<Yw_Knowledge> knowledges = new List<Yw_Knowledge>();
                var knowledgeShow = answerCard.AnswerCollection.Where(x => x.KnowledgeId != 0).GroupBy(s => s.KnowledgeId);

                studentReport.Knowledge = GetKnowledge(knowledgeShow);
            }
            return studentReport;
        }

        /// <summary>
        /// 获取任务报告或练习报告的题目部分
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="taskId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Tuple<List<StudentAnswerBase>, Dictionary<int, Yw_SubjectContent>, Dictionary<int, Yw_Subject>, int, int> GetReportSubject(int studentId, int taskId, int pageIndex)
        {
            StudentPracticeBll studentPracticeBll = new StudentPracticeBll();
            int pageSize = 10;
            //获取学生答案
            Yw_StudentStudyTaskAnswerExt answer = studentPracticeBll.StuStudyTaskAnsRepo.GetByStudentTask(studentId, taskId) as Yw_StudentStudyTaskAnswerExt;
            List<StudentAnswerBase> answerCard = new List<StudentAnswerBase>();
            Dictionary<int, Yw_SubjectContent> subjectContent = new Dictionary<int, Yw_SubjectContent>();
            Dictionary<int, Yw_Subject> subjects = new Dictionary<int, Yw_Subject>();
            answerCard = answer.Yta_Answer_Obj.AnswerCollection;
            var subjectIds = answerCard.Select(s => s.SubjectId).ToList();
            if ((pageIndex - 1) * pageSize < subjectIds.Count)
            {
                answerCard = answerCard.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var pageIds = subjectIds.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                subjectContent = GetTaskSubject(pageIds).ToDictionary(x => x.Ysc_SubjectId, x => x);
                subjects = GetSubjects(pageIds).ToDictionary(x => x.Ysj_Id, x => x);
            }
            Tuple<List<StudentAnswerBase>, Dictionary<int, Yw_SubjectContent>, Dictionary<int, Yw_Subject>, int, int> reportTuple = new Tuple<List<StudentAnswerBase>, Dictionary<int, Yw_SubjectContent>, Dictionary<int, Yw_Subject>, int, int>(answerCard, subjectContent, subjects, pageSize, subjectIds.Count);
            return reportTuple;
        }

        /// <summary>
        /// 返回练习报告的题目
        /// </summary>
        public List<Yw_SubjectContent> GetTaskSubject(List<int> subjectIds)
        {
            List<Yw_SubjectContent> subjectContents = new List<Yw_SubjectContent>();
            SubjectBll subjectBll = new SubjectBll();
            subjectContents = subjectBll.GetSubjectContents(subjectIds);
            return subjectContents;
        }

        /// <summary>
        /// 返回题目（获取题目难度）
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<Yw_Subject> GetSubjects(List<int> ids)
        {
            List<Yw_Subject> subjects = new List<Yw_Subject>();
            SubjectBll subjectBll = new SubjectBll();
            subjects = subjectBll.GetByIds(ids);
            return subjects;
        }

        /// <summary>
        /// 获取学习报告的统计部分
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public DtoStudentReport GetByStuLesAnswer(int studentId, int lessonProgressId)
        {
            StudentStudyBll studentStudyBll = new StudentStudyBll();
            //var result = studentStudyBll.StuLesProgressRepository.IsHasStudy(lessonProgressId, studentId);
            //if (!result)
            //{
            //    throw new AbhsException(ErrorCodeEnum.NotStudyReport, AbhsErrorMsg.ConstNotStudyReport + " LessonProgressId:" + lessonProgressId);
            //}

            //演示异常使用，正式上线删除
            var lesProgress = studentStudyBll.StuLesProgressRepository.IsHasStudys(lessonProgressId, studentId);
            if (lesProgress == null)
            {
                throw new AbhsException(ErrorCodeEnum.NotStudyReport, AbhsErrorMsg.ConstNotStudyReport + " LessonProgressId:" + lessonProgressId);
            }
            if (!lesProgress.Yle_IsFinished)
            {
                var errorlog = $"操作者：{studentId}，操作时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}，课程进度Id：{lessonProgressId}，原因：课程状态未完成，开始学习时间：{lesProgress.Yle_StartStudyTime.ToString("yyyy-MM-dd HH:mm:ss")}，结束学习时间：{lesProgress.Yle_FinishStudyTime.ToString("yyyy-MM-dd HH:mm:ss")}，学习时长：{lesProgress.Yle_StudySeconds}，已答题目数：{lesProgress.Yle_SubjectCount}，数据创建时间：{lesProgress.Yle_CreateTime.ToString("yyyy-MM-dd HH:mm:ss")}，数据修改时间：{lesProgress.Yle_UpdateTime.ToString("yyyy-MM-dd HH:mm:ss")}";
                LogHelper.WriteLog(errorlog);

                //查询学习时长
                Yw_StudentLessonProgress lessonProgress = studentStudyBll.StuLesProgressRepository.GetProgressByID(lessonProgressId);
                var studyTime = Convert.ToInt32(Math.Ceiling(lessonProgress.Yle_StudySeconds * 1.0 / 60));
                //病句  选择题
                if (lesProgress.Yle_LessonId == 10000)
                {
                    return GetSelect(studyTime);
                }
                //断句
                else if (lesProgress.Yle_LessonId == 10001)
                {
                    return GetMarkCut(studyTime);
                }
                //阅读理解 填空
                else if (lesProgress.Yle_LessonId == 10002)
                {
                    return GetFillBlank(studyTime);
                }
            }
            //演示结束

            //获取学生答案
            Yw_StudentLessonAnswerExt lessonAnswer = studentStudyBll.StuLessonAnsRepository.GetStuLesAnswer(studentId, lessonProgressId) as Yw_StudentLessonAnswerExt;
            List<StudentAnswerCard> answerCard = lessonAnswer.Yla_Answer_Obj;
            DtoStudentReport studentReport = new DtoStudentReport();
            if (answerCard != null)
            {
                //查询学习时长
                Yw_StudentLessonProgress lessonProgress = studentStudyBll.StuLesProgressRepository.GetProgressByID(lessonProgressId);
                studentReport.StudyTime = Convert.ToInt32(Math.Ceiling(lessonProgress.Yle_StudySeconds * 1.0 / 60));

                studentReport.StudyDate = answerCard.OrderBy(s => s.SubmitTime).FirstOrDefault().SubmitTime;
                studentReport.TotalStars = answerCard.Sum(s => s.TotalStars);
                studentReport.ResultCoins = answerCard.Sum(s => s.AnswerCollection.Sum(x => x.ResultCoins));
                studentReport.TotalCoins = answerCard.Sum(s => s.TotalCoins);
                studentReport.TotalScore = Convert.ToInt32(Math.Round((answerCard.Sum(s => s.TotalStars) * 1.0 / (answerCard.Sum(s => s.AnswerCollection.Count) * 5.0) * 5), MidpointRounding.AwayFromZero));
                studentReport.SubjectCount = answerCard.Sum(s => s.AnswerCollection.Count());
                studentReport.KnowledgeRates = answerCard.Sum(s => s.TotalStars) * 1.0 / (answerCard.Sum(s => s.AnswerCollection.Count) * 5.0);

                double rates = Math.Round((double)(answerCard.Sum(s => s.AnswerCollection.Where(x => x.ResultStars == 5).Count())) / answerCard.Sum(s => s.AnswerCollection.Count()), 2) * 100;
                studentReport.ExcellentRates = rates;
                var answers = answerCard.Select(s => s.AnswerCollection).ToList();
                List<StudentAnswerBase> answerBase = new List<StudentAnswerBase>();
                foreach (var item in answers)
                {
                    answerBase.AddRange(item.Where(x => x.KnowledgeId != 0).ToList());

                }
                var knowledgeShow = answerBase.GroupBy(s => s.KnowledgeId);
                studentReport.Knowledge = GetKnowledge(knowledgeShow);
            }
            return studentReport;
        }

        private DtoStudentReport GetFillBlank(int studyTime)
        {
            DtoStudentReport studentReport = new DtoStudentReport();

            studentReport.StudyTime = studyTime; //学习时长 4
            studentReport.StudyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//学习时间
            studentReport.TotalCoins = 0;//总金币数
            studentReport.TotalScore = 3;//总星数
            studentReport.SubjectCount = 3;
            studentReport.KnowledgeRates = 0.34;
            studentReport.ExcellentRates = 25;
            List<StudentAnswerBase> answerBase = new List<StudentAnswerBase>()
            {
                new StuBlankAnswer() {KnowledgeId=10022,ResultStars=2},
                new StuBlankAnswer() {KnowledgeId=10024,ResultStars=3},
                new StuBlankAnswer() {KnowledgeId=10026,ResultStars=0}
            };
            var knowledgeShow = answerBase.GroupBy(s => s.KnowledgeId);
            studentReport.Knowledge = GetKnowledge(knowledgeShow);
            return studentReport;
        }

        private DtoStudentReport GetSelect(int studyTime)
        {
            DtoStudentReport studentReport = new DtoStudentReport();
            studentReport.StudyTime = studyTime; //学习时长 2
            studentReport.StudyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//学习时间
            studentReport.TotalCoins = 0;//总金币数
            studentReport.TotalScore = 2;//总星数
            studentReport.SubjectCount = 8;
            studentReport.KnowledgeRates = 0.2;
            studentReport.ExcellentRates = 12;
            List<StudentAnswerBase> answerBase = new List<StudentAnswerBase>()
            {
                new StuSelectAnswer() {KnowledgeId=10003,ResultStars=2},
                new StuSelectAnswer() {KnowledgeId=10005,ResultStars=0},
                new StuSelectAnswer() {KnowledgeId=10007,ResultStars=1},
                new StuSelectAnswer() {KnowledgeId=10009,ResultStars=0}
            };
            var knowledgeShow = answerBase.GroupBy(s => s.KnowledgeId);
            studentReport.Knowledge = GetKnowledge(knowledgeShow);
            return studentReport;
        }

        private DtoStudentReport GetMarkCut(int studyTime)
        {
            DtoStudentReport studentReport = new DtoStudentReport();
            studentReport.StudyTime = studyTime; //学习时长 19
            studentReport.StudyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//学习时间
            studentReport.TotalCoins = 0;//总金币数
            studentReport.TotalScore = 2;//总星数
            studentReport.SubjectCount = 1;
            studentReport.ExcellentRates = 0.4;
            List<StudentAnswerBase> answerBase = new List<StudentAnswerBase>()
            {
                //new StuMarkCutAnswer() {KnowledgeId=10013,ResultStars=0},//闪读训练
                //new StuMarkCutAnswer() {KnowledgeId=10016,ResultStars=0},//古诗词划分节奏
                new StuMarkCutAnswer() {KnowledgeId=10019,ResultStars=2} //文言文划分朗读节奏
            };
            var knowledgeShow = answerBase.GroupBy(s => s.KnowledgeId);
            studentReport.Knowledge = GetKnowledge(knowledgeShow);
            return studentReport;
        }

        private List<TopKnowledge> GetKnowledge(IEnumerable<IGrouping<int, StudentAnswerBase>> knowledgeShow)
        {
            List<Yw_Knowledge> knowledges = new List<Yw_Knowledge>();

            //获取所有Id不为0的知识点Id
            List<int> knowledgeIds = knowledgeShow.Select(s => s.Key).ToList();
            //根据Id集合查询所有的知识点
            KnowledgeBll knowledgeBll = new KnowledgeBll();
            var knowledgeList = knowledgeBll.KnowledgeRepository.GetKnowledgeByIds(knowledgeIds);
            //查询所有知识点的顶级知识点的path
            knowledges = knowledgeBll.KnowledgeRepository.GetTopKnowledge(knowledgeIds);
            //给返回页面对象赋值
            List<TopKnowledge> topKnowledges = knowledges.Select(s => new TopKnowledge { TopId = s.Ykl_Id, TopName = s.Ykl_Name, Path = s.Ykl_Path }).ToList();
            foreach (var item in topKnowledges)
            {
                var list = knowledgeList.Where(s => s.Ykl_Path.Contains(item.Path)).ToList();
                List<ChildKnowledge> childs = new List<ChildKnowledge>();
                foreach (var child in list)
                {
                    ChildKnowledge childKnowledge = new ChildKnowledge()
                    {
                        ChildId = child.Ykl_Id,
                        ChildName = child.Ykl_Name,
                        ResultScore = knowledgeShow.Where(s => s.Key == child.Ykl_Id).FirstOrDefault().Sum(s => s.ResultStars) / (knowledgeShow.Where(s => s.Key == child.Ykl_Id).FirstOrDefault().Count() * 5.0)
                    };
                    childs.Add(childKnowledge);
                }
                item.ChildKnowledge.AddRange(childs);
            }
            return topKnowledges;
        }
    }
}
