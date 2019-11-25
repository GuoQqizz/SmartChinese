using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll
{
    public class ReadRecordBll : BllBase
    {
        public ReadRecordBll() : base()
        {

        }

        private IReadRecordRepository readRecordRepository;
        public IReadRecordRepository ReadRecordRepository
        {
            get
            {
                if (readRecordRepository == null)
                {
                    readRecordRepository = new ReadRecordRepository();
                }

                return readRecordRepository;
            }
        }

        /// <summary>
        /// 学生朗读记录并返回排行（击败x%）
        /// </summary>
        public int SaveAndCalculateRankPercent(int studentId, int courseId, int lessonId, int unitId, string actionId, int score, string url)
        {
            if (score == 0)
            {
                return 0;
            }

            Yw_ReadRecord record = ReadRecordRepository.Get(unitId, actionId, studentId);
            if (record == null)
            {
                record = new Yw_ReadRecord();
                record.Yrr_StudentId = studentId;
                record.Yrr_CourseId = courseId;
                record.Yrr_LessonId = lessonId;
                record.Yrr_UnitId = unitId;
                record.Yrr_ActionId = actionId;
            }

            if (score > record.Yrr_Score)
            {
                record.Yrr_BestUrl = url;
                record.Yrr_BestScore = score;
            }

            record.Yrr_Score = score;
            record.Yrr_Url = url;
            record.Yrr_CreateTime = DateTime.Now;
            if (record.Yrr_Id == 0)
            {
                ReadRecordRepository.Add(record);
            }
            else
            {
                ReadRecordRepository.Update(record);
            }

            return ReadRecordRepository.CalculateRankPercent(unitId, actionId, score);
        }
    }
}
