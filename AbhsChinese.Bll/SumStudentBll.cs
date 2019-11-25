using AbhsChinese.Bll.Report;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.Message;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbhsChinese.Bll
{
    public class SumStudentBll : BllBase
    {
        public SumStudentBll() : base()
        {
        }

        private SumStudyFactDateCycleRepository dateCycleRepository;
        public ISumStudyFactDateCycleRepository DateCycleRepository
        {
            get
            {
                if (dateCycleRepository == null)
                {
                    dateCycleRepository = new SumStudyFactDateCycleRepository();
                }

                dateCycleRepository.TranId = tranId;
                return dateCycleRepository;
            }
        }

        private ISumStudyFactDailyRepository studentDailyRepository;

        public ISumStudyFactDailyRepository StudentDailyRepository
        {
            get
            {
                if (studentDailyRepository == null)
                {
                    studentDailyRepository = new SumStudyFactDailyRepository();
                }

                studentDailyRepository.TranId = tranId;
                return studentDailyRepository;
            }
        }

        public int GetStudyDayCountOfStudent(int studentId)
        {
            return StudentDailyRepository.GetStudyDayCountOfStudent(studentId);
        }

        public void SaveReportData(string msg)
        {
            MessageBody message = JsonConvert.DeserializeObject<MessageBody>(msg);
            ReportSubscribeBase handler = ReportHandlerFactory.Create(message);
            handler.Handle();
        }

        public Sum_StudyFactDaily GetDailyData(int studentId, int classId, DateTime asOfDate)
        {
            Sum_StudyFactDaily daily = StudentDailyRepository.GetByStudentDate(studentId, classId, asOfDate);
            return daily;
        }

        public void UpdateDailyData(Sum_StudyFactDaily daily)
        {
            StudentDailyRepository.Update(daily);
        }

        public void SaveDailyData(Sum_StudyFactDaily daily)
        {
            if (daily.Ssd_Id > 0)
            {
                StudentDailyRepository.Update(daily);
            }
            else
            {
                StudentDailyRepository.Add(daily);
            }
        }

        public Sum_StudyFactDateCycle GetCycleData(int studentId, int classId, DateTime asOfDate, DateCycleTypeEnum cycle)
        {
            Sum_StudyFactDateCycle daily = DateCycleRepository.GetByStudentDate(studentId, classId, asOfDate, cycle);
            return daily;
        }

        public List<Sum_StudyFactDateCycle> GetCycleDatasByStudent(int studentId, int classId)
        {
            return DateCycleRepository.GetByStudent(studentId, classId);
        }

        public void UpdateCycleData(Sum_StudyFactDateCycle obj)
        {
            DateCycleRepository.Update(obj);
        }

        public void SaveCycleData(Sum_StudyFactDateCycle obj)
        {
            if (obj.Sdc_Id > 0)
            {
                DateCycleRepository.Update(obj);
            }
            else
            {
                DateCycleRepository.Add(obj);
            }
        }

        public Dictionary<DateCycleTypeEnum, Sum_StudyFactDateCycle> FetchOrCreateCycleData(int studentId, int classId, DateTime asOfDate)
        {
            Dictionary<DateCycleTypeEnum, Sum_StudyFactDateCycle> dic = new Dictionary<DateCycleTypeEnum, Sum_StudyFactDateCycle>();

            SumStudentBll bll = new SumStudentBll();
            List<Sum_StudyFactDateCycle> cycles = bll.GetCycleDatasByStudent(studentId, classId);

            Sum_StudyFactDateCycle totalCycle = cycles.FirstOrDefault(x => x.Sdc_CycleType == (int)DateCycleTypeEnum.Total);
            if (totalCycle == null)
            {
                totalCycle = new Sum_StudyFactDateCycle();
                totalCycle.ForceAudit();
                totalCycle.Sdc_CycleType = (int)DateCycleTypeEnum.Total;
                totalCycle.Sdc_Date = new DateTime(1900, 1, 1);
            }
            dic[DateCycleTypeEnum.Total] = totalCycle;

            Sum_StudyFactDateCycle yearCycle = cycles.FirstOrDefault(x => x.Sdc_CycleType == (int)DateCycleTypeEnum.Year);
            if (yearCycle == null || yearCycle.Sdc_Date != DateTool.FirstDayOfYear(asOfDate))
            {
                int id = yearCycle == null ? 0 : yearCycle.Sdc_Id;
                yearCycle = new Sum_StudyFactDateCycle();
                yearCycle.ForceAudit();
                yearCycle.Sdc_Id = id;
                yearCycle.Sdc_CycleType = (int)DateCycleTypeEnum.Year;
                yearCycle.Sdc_Date = DateTool.FirstDayOfYear(asOfDate);
            }
            dic[DateCycleTypeEnum.Year] = yearCycle;

            Sum_StudyFactDateCycle monthCycle = cycles.FirstOrDefault(x => x.Sdc_CycleType == (int)DateCycleTypeEnum.Month);
            if (monthCycle == null || monthCycle.Sdc_Date != DateTool.FirstDayOfMonth(asOfDate))
            {
                int id = monthCycle == null ? 0 : monthCycle.Sdc_Id;
                monthCycle = new Sum_StudyFactDateCycle();
                monthCycle.ForceAudit();
                monthCycle.Sdc_Id = id;
                monthCycle.Sdc_CycleType = (int)DateCycleTypeEnum.Month;
                monthCycle.Sdc_Date = DateTool.FirstDayOfMonth(asOfDate);
            }
            dic[DateCycleTypeEnum.Month] = monthCycle;

            Sum_StudyFactDateCycle weekCycle = cycles.FirstOrDefault(x => x.Sdc_CycleType == (int)DateCycleTypeEnum.Week);
            if (weekCycle == null || weekCycle.Sdc_Date != DateTool.FirstDayOfWeek(asOfDate))
            {
                int id = weekCycle == null ? 0 : weekCycle.Sdc_Id;
                weekCycle = new Sum_StudyFactDateCycle();
                weekCycle.ForceAudit();
                weekCycle.Sdc_Id = id;
                weekCycle.Sdc_CycleType = (int)DateCycleTypeEnum.Week;
                weekCycle.Sdc_Date = DateTool.FirstDayOfWeek(asOfDate);
            }
            dic[DateCycleTypeEnum.Week] = weekCycle;

            return dic;
        }

        public DtoSumStudent GetSumByStuDate(int studentId)
        {
            int studySeconds = StudentDailyRepository.GetSumByStuDate(studentId, DateTime.Now.Date);
            StudentInfoBll studentInfoBll = new StudentInfoBll();
            DtoSumStudentTip sumStudent = studentInfoBll.GetSumStudentTip(studentId);
            DtoSumStudent student = new DtoSumStudent()
            {
                School = sumStudent.School,
                Avatar=sumStudent.Avatar,
                StudyMinutes = Convert.ToInt32(Math.Ceiling(studySeconds*1.0/60)),
                StudyDayCount = sumStudent.StudyDayCount,
                Coins = sumStudent.Coins,
                Salutation= SalutationEnum.书童.ToString()
            };
            return student;
        }
    }
}
