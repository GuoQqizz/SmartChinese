using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.Message;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AbhsChinese.Bll.Report
{
    public class TaskStudyReport : ReportSubscribeBase
    {
        public TaskStudyReport(string msg) : base(msg)
        {
        }

        public override void Handle()
        {
            CourseStudyTaskMessage msgObject = JsonConvert.DeserializeObject<CourseStudyTaskMessage>(message);
            SumStudentBll bll = new SumStudentBll();
            StudentStudyBll studyBll = new StudentStudyBll();
            Yw_StudentCourseProgress progress = studyBll.GetProgressByStudentCourse(msgObject.StudentId, msgObject.CourseId);

            if (progress == null)
            {
                throw new AbhsException(ErrorCodeEnum.StudentCourseProgressNotFound, AbhsErrorMsg.ConstStudentCourseProgressNotFound, msgObject.StudentId + "," + msgObject.CourseId);
            }

            bool newDay = false;

            Sum_StudyFactDaily daily = bll.GetDailyData(msgObject.StudentId, progress.Yps_ClassId, msgObject.AsOfDate.Date);
            if (daily == null)
            {
                newDay = true;
                daily = new Sum_StudyFactDaily();
                daily.Ssd_StudentId = msgObject.StudentId;
                daily.Ssd_SchoolId = progress.Yps_SchoolId;
                daily.Ssd_ClassId = progress.Yps_ClassId;
                daily.Ssd_Date = msgObject.AsOfDate.Date;
                daily.Ssd_UpdateTime = DateTime.Now;
                daily.Ssd_StudySeconds = msgObject.StudySeconds;
                daily.Ssd_IncomeCoins = msgObject.GetCoins;
                daily.Ssd_SubjectCount = msgObject.SubjectCount;
                daily.Ssd_TaskCount = 1;
                daily.Ssd_ExperiencePoints = msgObject.GetCoins;
            }
            else
            {
                daily.Ssd_UpdateTime = DateTime.Now;
                daily.Ssd_StudySeconds = daily.Ssd_StudySeconds + msgObject.StudySeconds;
                daily.Ssd_IncomeCoins = daily.Ssd_IncomeCoins + msgObject.GetCoins;
                daily.Ssd_SubjectCount = daily.Ssd_SubjectCount + msgObject.SubjectCount;
                daily.Ssd_TaskCount = daily.Ssd_TaskCount + 1;
                daily.Ssd_ExperiencePoints = daily.Ssd_ExperiencePoints + msgObject.GetCoins;
            }
            bll.SaveDailyData(daily);

            Dictionary<DateCycleTypeEnum, Sum_StudyFactDateCycle> dic = bll.FetchOrCreateCycleData(msgObject.StudentId, progress.Yps_ClassId, msgObject.AsOfDate.Date);
            foreach (KeyValuePair<DateCycleTypeEnum, Sum_StudyFactDateCycle> item in dic)
            {
                item.Value.Sdc_StudentId = msgObject.StudentId;
                item.Value.Sdc_SchoolId = progress.Yps_SchoolId;
                item.Value.Sdc_ClassId = progress.Yps_ClassId;
                item.Value.Sdc_UpdateTime = DateTime.Now;
                item.Value.Sdc_StudySeconds = item.Value.Sdc_StudySeconds + msgObject.StudySeconds;
                item.Value.Sdc_IncomeCoins = item.Value.Sdc_IncomeCoins + msgObject.GetCoins;
                item.Value.Sdc_SubjectCount = item.Value.Sdc_SubjectCount + msgObject.SubjectCount;
                item.Value.Sdc_ExperiencePoints = item.Value.Sdc_ExperiencePoints + msgObject.GetCoins;
                item.Value.Sdc_TaskCount = item.Value.Sdc_TaskCount + 1;
                item.Value.Sdc_StudyDayCount = newDay ? item.Value.Sdc_StudyDayCount + 1 : item.Value.Sdc_StudyDayCount;
                bll.SaveCycleData(item.Value);
            }

            if (newDay)
            {
                int dayCount = bll.GetStudyDayCountOfStudent(msgObject.StudentId);
                StudentInfoBll infoBll = new StudentInfoBll();
                infoBll.UpdateStudyDayCountOfStudent(msgObject.StudentId, dayCount);
            }
        }
    }
}
