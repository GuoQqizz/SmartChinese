using AbhsChinese.Domain.Entity.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.School.Models
{
    public class SchoolClassInputModel
    {

        public int Ycc_Id { get; set; }

        public int Ycc_SchoolId
        {
            get; set;
        }


        public string Ycc_Name
        {
            get; set;
        }


        public int Ycc_Grade
        {
            get; set;
        }


        public int Ycc_CourseType
        {
            get; set;
        }


        public int Ycc_CourseId
        {
            get; set;
        }


        public int Ycc_ClassMaster
        {
            get; set;
        }


        public int Ycc_LimitStudentCount
        {
            get; set;
        }




        /// <summary>
        /// 
        /// </summary>
        public DateTime Ycc_StartTime
        {
            get; set;
        }


        public string Ycc_Remark
        {
            get; set;
        }
        public int Ycc_Status { get; set; }

        public List<SchoolClassScheduleInputModel> Schedules { get; set; }


        public Yw_SchoolClass ToSchoolClassDbModel(Yw_SchoolClass dbModel)
        {
            var result = this.ConvertTo<Yw_SchoolClass>(dbModel);
            return result;
        }

        public List<Yw_SchoolClassSchedule> ToSchoolClassScheduleDbModel()
        {
            List<Yw_SchoolClassSchedule> list = this.Schedules.Select(s => s.ConvertTo<Yw_SchoolClassSchedule>()).ToList();
            list.ForEach(s =>
            {
                s.Ywd_ClassId = this.Ycc_Id;
                s.Ywd_SchoolId = this.Ycc_SchoolId;
            });
            return list;
        }

        public SchoolClassInputModel FromDbModel(Yw_SchoolClass schoolClass, List<Yw_SchoolClassSchedule> list)
        {
            SchoolClassInputModel model = schoolClass.ConvertTo<SchoolClassInputModel>();
            if (model == null)
            {
                model = new SchoolClassInputModel();
            }
            model.Schedules = list.Select(s => s.ConvertTo<SchoolClassScheduleInputModel>()).ToList();
            return model;
        }
    }


    public class SchoolClassScheduleInputModel
    {
        public int Ywd_Day { get; set; }

        public TimeSpan Ywd_StartTime { get; set; }

        public TimeSpan Ywd_EndTime { get; set; }
    }
}