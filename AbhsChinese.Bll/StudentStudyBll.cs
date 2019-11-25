using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Request.Student;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AbhsChinese.Domain.Dto.Request;
using StudentPractice = AbhsChinese.Domain.Dto.Response.StudentPractice;
using AbhsChinese.Bll.Message;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.Message;
using Newtonsoft.Json;
using AbhsChinese.Domain.JsonEntity.Answer;
using AbhsChinese.Domain.JsonEntity.Coin;
using AbhsChinese.Domain.Dto.Request.StudentWrong;

namespace AbhsChinese.Bll
{
    public class StudentStudyBll : BllBase
    {
        #region bll
        private SchoolClassBll schoolClassBll;
        public SchoolClassBll SchoolClassBll
        {
            get
            {
                if (schoolClassBll == null)
                {
                    schoolClassBll = new SchoolClassBll();
                }
                return schoolClassBll;
            }
        }

        public IList<StudentPractice.DtoCourse> GetCoursesAttended(DtoCoursesSearch search)
        {
            return ScpRepository.GetCoursesAttended(search);
        }
        #endregion

        private IStudentCourseProgressRepository scpRepository;
        public IStudentCourseProgressRepository ScpRepository
        {
            get
            {
                if (scpRepository == null)
                {
                    scpRepository = new StudentCourseProgressRepository();
                }

                scpRepository.TranId = tranId;
                return scpRepository;
            }
        }

        private IStuLessonAnsRepository stuLessonAnsRepository;

        public IStuLessonAnsRepository StuLessonAnsRepository
        {
            get
            {
                if (stuLessonAnsRepository == null)
                {
                    stuLessonAnsRepository = new StuLessonAnsRepository();
                }

                stuLessonAnsRepository.TranId = tranId;
                return stuLessonAnsRepository;
            }
        }

        private IStuLesProgressRepository stuLesProgressRepository;

        public IStuLesProgressRepository StuLesProgressRepository
        {
            get
            {
                if (stuLesProgressRepository == null)
                {
                    stuLesProgressRepository = new StuLesProgressRepository();
                }

                stuLesProgressRepository.TranId = tranId;
                return stuLesProgressRepository;
            }
        }

        public Yw_StudentLessonProgress GetStuLessonProgressById(int id)
        {
            return StuLesProgressRepository.Get(id);
        }

        public Yw_StudentCourseProgress GetCourseProgress(int id)
        {
            return ScpRepository.Get(id);
        }

        public Yw_StudentCourseProgress GetProgressByStudentCourse(int studentId, int courseId)
        {
            return ScpRepository.GetByStudentCourse(studentId, courseId);
        }

        /// <summary>
        /// 根据学生id获取学生拥有的课时数量
        /// </summary>
        /// <param name="studentId">学生id</param>
        /// <returns>第一个值:总课程数,第二个值:未完成课程数,第三个值:完成课程数</returns>
        public Tuple<int, int, int> GetCourseCountByStudent(int studentId)
        {
            return ScpRepository.GetCourseCountByStudnet(studentId);
        }

        /// <summary>
        /// 根据学生分页条件获取学生课程信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<DtoStudentCourseInfo> GetCourseProgressByStudentSearch(DtoStudentCourseSearch search)
        {
            return ScpRepository.GetByStudentCourseSearch(search);
        }
        /// <summary>
        /// 获取学生的课程详情
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public DtoStudentCourseInfo GetCourseProgress(int studentId, int courseId)
        {
            return ScpRepository.GetByStudentCourseId(studentId, courseId);
        }
        /// <summary>
        /// 根据学生id和课程id获取课时数据
        /// </summary>
        /// <param name="studentID">学生id</param>
        /// <param name="courseID">课程id</param>
        /// <returns></returns>
        public List<DtoStudentLessonInfo> GetLessonProgressByStudent(int studentID, int courseID)
        {
            if (ScpRepository.GetByStudentCourse(studentID, courseID) != null)//如果此学生有此课程
            {
                LessonBll bll = new LessonBll();
                //获取上完的课时id
                var progress = StuLesProgressRepository.GetFinishedByLesson(studentID, courseID);
                //获取并返回(添加是否已经完成属性的)课程信息
                return bll.GetLessons(courseID).Where(s => s.Status == (int)LessonStatusEnum.合格).Select(l => new DtoStudentLessonInfo
                {
                    LessonId = l.ID,
                    LessonIndex = l.Index,
                    LessonName = l.Name,
                    IsStudyOver = progress.Contains(l.ID)
                }).ToList();
            }
            return new List<DtoStudentLessonInfo>();
        }


        /// <summary>
        /// 获取最新的课时数据,并更新学习进度秘钥
        /// </summary>
        /// <param name="studentId">学生id</param>
        /// <param name="courseId">课程id</param>
        /// <param name="lessonId">课时id</param>
        /// <returns></returns>
        public Yw_StudentLessonProgress GetLastProgressByStudent(int studentId, int courseId, int lessonId)
        {
            var course = ScpRepository.GetByStudentCourse(studentId, courseId);
            if (course != null)//如果此学生有此课程
            {
                ScpRepository.UpdateStudyTime(course.Yps_Id, DateTime.Now);//更新最后学习时间
                LessonBll bll = new LessonBll();
                var lesson = bll.GetLesson(lessonId);
                if (lesson.Ycl_CourseId != courseId || lesson.Ycl_Status != (int)LessonStatusEnum.合格) { return null; }//如果课时的课程id不一致或课时不是合格状态,则返回空
                //获取最后一次学习进度
                var progress = StuLesProgressRepository.GetLastProgress(studentId, lessonId);
                var newProgress = new Yw_StudentLessonProgress
                {
                    Yle_StudentId = studentId,
                    Yle_CourseId = courseId,
                    Yle_LessonId = lessonId,
                    Yle_LessonIndex = lesson.Ycl_Index,
                    Yle_CreateTime = DateTime.Now,
                    Yle_CoinFromUIndex = 1,
                    Yle_LastStudyTime = DateTime.Now,
                    Yle_StartStudyTime = DateTime.Now,
                    Yle_UnitIndex = 1,
                    Yle_StudySeconds = 0,
                    Yle_IsFinished = false,
                    Yle_IsLastest = true,
                    Yle_key = Guid.NewGuid().ToString("N").ToLower()
                };
                var newAnswers = new Yw_StudentLessonAnswer()
                {
                    Yla_StudentId = studentId,
                    Yla_CourseId = courseId,
                    Yla_LessonId = lessonId,
                    Yla_StudentAnswer = "",
                    Yla_StudentCoin = ""

                };
                if (progress == null)//如果没有学习进度,则重新添加一个学习进度
                {
                    StuLesProgressRepository.Insert(newProgress);
                    newAnswers.Yla_LessonProgressId = newProgress.Yle_Id;
                    StuLessonAnsRepository.Insert(newAnswers);
                    return newProgress;
                }
                else if (progress.Yle_IsFinished)//如果学习进度完成了,则新增加一个学习进度,并且修改最大金币发放单元
                {
                    newProgress.Yle_CoinFromUIndex = progress.Yle_CoinFromUIndex > progress.Yle_UnitIndex ? progress.Yle_CoinFromUIndex : progress.Yle_UnitIndex;//修改最大金币发放单元
                    StuLesProgressRepository.Insert(newProgress);
                    newAnswers.Yla_LessonProgressId = newProgress.Yle_Id;
                    StuLessonAnsRepository.Insert(newAnswers);
                    return newProgress;
                }
                else //如果学习进度没有完成
                {
                    progress.Yle_key = StuLesProgressRepository.UpdateKey(progress.Yle_Id);//更新学习进度的秘钥
                    return progress;
                }
            }
            return null;

        }

        /// <summary>
        /// 生成新的学习进度数据
        /// </summary>
        /// <param name="studentId">学生id</param>
        /// <param name="courseId">课程id</param>
        /// <param name="lessonId">课时id</param>
        /// <returns></returns>
        public Yw_StudentLessonProgress GetNewProgressByStudent(int studentId, int courseId, int lessonId)
        {
            if (ScpRepository.GetByStudentCourse(studentId, courseId) != null)//如果此学生有此课程
            {
                LessonBll bll = new LessonBll();
                var lesson = bll.GetLesson(lessonId);
                //获取最后一次学习进度
                var progress = StuLesProgressRepository.GetLastProgress(studentId, lessonId);
                //学习进度
                var newProgress = new Yw_StudentLessonProgress()
                {
                    Yle_StudentId = studentId,
                    Yle_CourseId = courseId,
                    Yle_LessonId = lessonId,
                    Yle_LessonIndex = lesson.Ycl_Index,
                    Yle_CreateTime = DateTime.Now,
                    Yle_CoinFromUIndex = 1,
                    Yle_LastStudyTime = DateTime.Now,
                    Yle_StartStudyTime = DateTime.Now,
                    Yle_UnitIndex = 1,
                    Yle_StudySeconds = 0,
                    Yle_IsFinished = false,
                    Yle_IsLastest = true,
                    Yle_key = Guid.NewGuid().ToString("N").ToLower()
                };
                //学生答题记录
                var newAnswers = new Yw_StudentLessonAnswer()
                {
                    Yla_StudentId = studentId,
                    Yla_CourseId = courseId,
                    Yla_LessonId = lessonId,
                    Yla_StudentAnswer = "",
                    Yla_StudentCoin = ""

                };
                if (progress == null)//如果没有学习进度,则重新添加一个学习进度
                {
                    StuLesProgressRepository.Insert(newProgress);
                    newAnswers.Yla_LessonProgressId = newProgress.Yle_Id;
                    StuLessonAnsRepository.Insert(newAnswers);
                }
                else //否则新增加一个学习进度,并且修改最大金币发放单元
                {
                    newProgress.Yle_CoinFromUIndex = progress.Yle_CoinFromUIndex > progress.Yle_UnitIndex ? progress.Yle_CoinFromUIndex : progress.Yle_UnitIndex;//修改最大金币发放单元
                    StuLesProgressRepository.Insert(newProgress);
                    newAnswers.Yla_LessonProgressId = newProgress.Yle_Id;
                    StuLessonAnsRepository.Insert(newAnswers);
                }
                return newProgress;
            }
            return null;
        }

        /// <summary>
        /// 提交学生学习进度数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns>0:添加成功,-1:课时进度秘钥错误,-2:数据重复提交,-3:金币数目错误</returns>
        public int SubmitStudyProgress(DtoStudnetUnitSubmit info)
        {
            ///注:这里考虑如何使用事务

            DateTime submitTime = DateTime.Now;
            //获取最后一次学习进度
            var progress = StuLesProgressRepository.GetLastProgress(info.StudnetId, info.LessonId);
            //判断课时进度秘钥是否正确
            if (progress.Yle_key == info.ProgressKey)
            {
                //如果当前进度中的当前学习单元页大于提交的单元
                if (progress.Yle_UnitIndex > info.UnitNum)
                {
                    return -2;//表示数据已经提交过,不能重复提交                    
                }
                else
                {
                    //创建答题卡对象
                    StudentAnswerCard card = null;
                    #region 填充答题卡数据
                    if (info.Answers != null && info.Answers.Count > 0)
                    {
                        card = new StudentAnswerCard();
                        card.AnswerCollection = info.Answers;//填充答案
                        card.TotalStars = info.Answers.Sum(s => s.ResultStars);//填充总星数
                        card.TotalCoins = info.Answers.Sum(s => s.ResultCoins);//填充总金币数

                        card.Part = info.UnitID;//填充单元(页码)id
                        card.SubmitTime = submitTime.ToString("yyyy-MM-dd HH:mm:ss");//填充提交时间
                        card.UseTime = info.UseTime;//填充用户学习时间
                    }
                    #endregion

                    //创建金币记录对象
                    StuLessonCoinRecord coinRecord = null;
                    #region 更新金币记录对象及答题卡部分数据
                    if (info.Coins != null && info.Coins.Count > 0)
                    {
                        coinRecord = new StuLessonCoinRecord();
                        coinRecord.CoinCollection = info.Coins;//填充金币记录
                        coinRecord.TotalCoins = info.Coins.Sum(s => s.GetCoins);//填充总金币数
                        coinRecord.Part = info.UnitID;//填充单元(页码)id
                        coinRecord.SubmitTime = submitTime.ToString("yyyy-MM-dd HH:mm:ss");//填充提交时间
                        coinRecord.UseTime = info.UseTime;//填充用户学习时间
                    }
                    #endregion

                    if (coinRecord != null && coinRecord.TotalCoins > info.AllCoin)//如果得到的金币数大于本页总金币数
                    {
                        return -3;//表示金币和大于总金币数
                    }
                    else
                    {
                        #region 修改课时进度表数据
                        progress.Yle_UnitIndex = info.UnitNum + 1;//更新开始页
                        progress.Yle_IsFinished = info.UnitNum >= info.TotalUnitNum;//更新是否结束
                        progress.Yle_LastStudyTime = DateTime.Now;//更新最后学习时间
                        progress.Yle_SubjectCount += info.Answers.Count;//更新总题目数
                        progress.Yle_RightSubjectCount += info.Answers.Where(s => s.IsRight).Count();//更新正确题目数
                        progress.Yle_GainCoins += info.Coins.Sum(s => s.GetCoins);//更新获取金币数量
                        progress.Yle_Percent = (int)(Math.Round(progress.Yle_RightSubjectCount * 1.0 / (progress.Yle_SubjectCount == 0 ? 1 : progress.Yle_SubjectCount), 2) * 100);//重新计算题目正确率
                        progress.Yle_StudySeconds += info.UseTime;//增加课时进度学习时长
                        #endregion

                        #region 事务提交
                        using (TransactionScope scope = new TransactionScope())
                        {
                            try
                            {
                                #region 提交

                                //如果学习完成,更新学生课程进度表
                                if (progress.Yle_IsFinished)
                                {
                                    progress.Yle_FinishStudyTime = DateTime.Now;//修改课时完成完成时间
                                    //获取完成的课时id
                                    var finisheds = StuLesProgressRepository.GetFinishedByLesson(info.StudnetId, info.CoruseId);
                                    if (!finisheds.Contains(info.LessonId))//如果课时没有学完过,修改课程进度表数据
                                    {
                                        LessonBll bll = new LessonBll();
                                        int lessonCount = bll.GetLessonsCountByCourse(info.CoruseId);//课程的课时数
                                        var courseProgress = ScpRepository.GetByStudentCourse(info.StudnetId, info.CoruseId);//获取课程完成进度.

                                        courseProgress.Yps_LessonFinishedCount = finisheds.Count() + 1;//完成的课时数+1(自身)
                                        courseProgress.Yps_IsFinished = courseProgress.Yps_LessonFinishedCount >= lessonCount;//如果完成的课时数大于等于总课时数,则表示完成.
                                        courseProgress.Yps_NextLessonIndex = courseProgress.Yps_NextLessonIndex > info.LessonNum ? courseProgress.Yps_NextLessonIndex : info.LessonNum + 1;//如果下一课时数大于当前课时,则不更改.否则改为当前课时的下一课时序号
                                        if (courseProgress.Yps_IsFinished && courseProgress.Yps_FinishStudyTime == new DateTime(1900, 1, 1))//如果课程进度完成,且完成时间不为初始值,则修改此时间
                                        {
                                            courseProgress.Yps_FinishStudyTime = DateTime.Now;
                                        }
                                        courseProgress.Yps_UpdateTime = DateTime.Now;//更新时间
                                        ScpRepository.Update(courseProgress);//更新课程进度
                                    }

                                }
                                StuLesProgressRepository.Update(progress);//更新课时数据

                                string cardJson = "", coinJson = "";
                                //添加错题数据
                                if (card != null)
                                {
                                    cardJson = $",{JsonConvert.SerializeObject(card)}";
                                    new StudentWrongBookBll().SaveWrongBook(new List<StudentAnswerCard> { card }, new DtoStudentWrongBook { CourseId = progress.Yle_CourseId, LessonId = progress.Yle_LessonId, LessonProgressId = progress.Yle_Id, Source = StudyWrongSourceEnum.课程学习, StudentId = progress.Yle_StudentId, StudyTaskId = 0 });

                                }
                                //需要修改学生金币数量及经验
                                if (coinRecord != null)
                                {
                                    coinJson = $",{JsonConvert.SerializeObject(coinRecord)}";
                                    new StudentInfoBll().AddCoins(info.StudnetId, coinRecord.TotalCoins);
                                }

                                //更新学生课时答题结果表
                                if (card != null || coinRecord != null)
                                {
                                    StuLessonAnsRepository.Update(info.ProgressID, cardJson, coinJson);
                                }
                                #endregion
                                scope.Complete();
                            }
                            catch (Exception ex)
                            {
                                RollbackTran();
                                throw ex;
                            }
                        }
                        #endregion

                        StudentStudyBll ssbll = new StudentStudyBll();
                        ssbll.PublishStudyMessage(progress.Yle_CourseId, progress.Yle_LessonId, progress.Yle_StudentId, info.UseTime, progress.Yle_SubjectCount, progress.Yle_GainCoins);
                        if (progress.Yle_IsFinished)
                        {
                            ssbll.PublishLessonFinishMessage(progress.Yle_StudentId, progress.Yle_Id);
                        }


                        return 0;//表示成功
                    }
                }
            }
            else
            {
                return -1;//表示课时进度秘钥错误,及在其他地方打开了这个课程学习
            }
        }
        /// <summary>
        /// 校验学习进度秘钥是否正确
        /// </summary>
        /// <param name="studentId">学生id</param>
        /// <param name="lessonId">课时id</param>
        /// <param name="key">学习进度秘钥</param>
        /// <returns></returns>
        public bool CheckProgressKey(int studentId, int lessonId, string key)
        {
            var last = StuLesProgressRepository.GetLastProgress(studentId, lessonId);//获取最后一次学习进度
            if (last != null)//如果有这个进度
            {
                return last.Yle_key == key;//校验
            }
            return false;
        }
        /// <summary>
        /// 校验学习进度秘钥是否正确
        /// </summary>
        /// <param name="progressid">学习进度id</param>
        /// <param name="key">学习进度秘钥</param>
        /// <returns></returns>
        public bool CheckProgressKey(int progressid, string key)
        {
            var progress = StuLesProgressRepository.GetProgressByID(progressid);//获取最后一次学习进度
            if (progress != null)//如果有这个进度
            {
                return progress.Yle_key == key;//校验
            }
            return false;
        }

        public Yw_StudentLessonAnswer GetLessonAnswer(int lessonProgressId)
        {
            return StuLessonAnsRepository.GetByProgressId(lessonProgressId);
        }

        /// <summary>
        /// 获取未分班学生列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<DtoSchoolNoClassStudent> GetNoClassStudent(DtoSchoolNoClassStudentSearch search)
        {
            return ScpRepository.GetNoClassStudent(search);
        }

        /// <summary>
        /// 发送学生的课程学习消息（每个课时的单元学习完成后都要发送消息，进行数据统计）
        /// </summary>
        public void PublishStudyMessage(int courseId, int lessonId, int studentId, int studySeconds, int subjectCount, int getCoins)
        {
            MessageBll bll = new MessageBll();

            CourseStudyMessage msg = new CourseStudyMessage();
            msg.AsOfDate = DateTime.Now.Date;
            msg.CourseId = courseId;
            msg.LessonId = lessonId;
            msg.StudentId = studentId;
            msg.StudySeconds = studySeconds;
            msg.SubjectCount = subjectCount;
            msg.GetCoins = getCoins;
            string msgBody = JsonConvert.SerializeObject(msg);

            bll.PublishMessage(MessageChannel.REPORT_CHANNEL, MessageTypeEnum.课程学习, msgBody);
        }

        /// <summary>
        /// 发送学生的课时完成消息
        /// </summary>
        public void PublishLessonFinishMessage(int studentId, int progressId)
        {
            MessageBll bll = new MessageBll();
            string msgBody = studentId + "," + progressId;
            bll.PublishMessage(MessageChannel.LESSONTASK_CHANNEL, MessageTypeEnum.课时学习结束, msgBody);
        }

        #region update
        /// <summary>
        /// 学生分配班级
        /// </summary>
        /// <param name="studentCourseProgressId">学生课程进度表Id</param>
        /// <param name="classId">班级Id</param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public bool AssignClass(int studentCourseProgressId, int classId, int oper)
        {
            bool result = false;
            var schoolClass = SchoolClassBll.GetSchoolClass(classId);
            if (schoolClass != null && schoolClass.Ycc_LimitStudentCount >= schoolClass.Ycc_StudentCount + 1)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        var updateClass = SchoolClassBll.IncrementStudentCount(classId, 1, oper);
                        var updateCourse = ScpRepository.UpdateClassId(studentCourseProgressId, classId);
                        result = updateClass && updateCourse;
                        if (result)
                        {
                            scope.Complete();
                        }
                        else
                        {
                            RollbackTran();
                        }
                    }
                    catch (Exception ex)
                    {
                        RollbackTran();
                        throw ex;
                    }
                }
            }

            return result;

        }
        #endregion
    }
}
