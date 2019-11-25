using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response.School
{
    public class DtoSchoolClass
    {

        public int Ycc_Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Ycc_SchoolId
        {
            get; set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Ycc_Name
        {
            get; set;
        }


        /// <summary>
        /// 
        /// </summary>
        public int Ycc_Grade
        {
            get; set;
        }
        public string GradeStr => CustomEnumHelper.Parse(typeof(GradeEnum), Ycc_Grade);


        /// <summary>
        /// 
        /// </summary>
        public int Ycc_CourseType
        {
            get; set;
        }
        public string CourseTypeStr => CustomEnumHelper.Parse(typeof(CourseCategoryEnum), Ycc_CourseType);


        /// <summary>
        /// 
        /// </summary>
        public int Ycc_CourseId
        {
            get; set;
        }
        public string CourseName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Ycc_ClassMaster
        {
            get; set;
        }
        public string ClassMasterName { get; set; }


        /// <summary>
        /// 容纳人数
        /// </summary>
        public int Ycc_LimitStudentCount
        {
            get; set;
        }


        /// <summary>
        /// 实际人数
        /// </summary>
        public int Ycc_StudentCount
        {
            get; set;
        }


        /// <summary>
        /// 开课时间
        /// </summary>
        public DateTime Ycc_StartTime
        {
            get; set;
        }
        public string StartTimeStr => Ycc_StartTime.ToString("yyyy-MM-dd");
        
        /// <summary>
        /// 
        /// </summary>
        public int Ycc_Status
        {
            get; set;
        }

        public string StatusStr => CustomEnumHelper.Parse(typeof(SchoolClassEnum), Ycc_Status);


        /// <summary>
        /// 上课计划
        /// </summary>
        public List<DtoSchoolClassSchedule> ClassSchedule { get; set; }
        /// <summary>
        /// 上课计划
        /// </summary>
        public string ClassScheduleStr => string.Join(",", ClassSchedule.Select(s => s.StudyTime));
    }

    public class DtoSchoolClassSchedule
    {
        public int Ywd_Day { get; set; }

        public string Day => CustomEnumHelper.Parse(typeof(WeekDayEnum), Ywd_Day);

        public DateTime Ywd_StartTime { get; set; }

        public string StartTime => Ywd_StartTime.ToString("hh:mm");


        public DateTime Ywd_EndTime { get; set; }

        public string EndTime => Ywd_EndTime.ToString("hh:mm");

        public string StudyTime => $"{Day}({StartTime}~{EndTime})";
    }

}
