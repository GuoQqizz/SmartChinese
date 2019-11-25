using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response.Student
{
    public class DtoStudentInfo
    {
        public int Bst_Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Bst_No { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Bst_Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Bst_NickName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Bst_Avatar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Bst_Sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Bst_Birthday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Bst_Grade { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Bst_Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Bst_SchoolId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Bst_ClassTeacherId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Bst_StudySchool { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Bst_Province { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Bst_City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Bst_County { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Bst_Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Bst_RegTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Bst_RegSource { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Bst_RegOperator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Bst_Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Bst_UpdateTime { get; set; }

        public string SchoolName { get; set; }

        public int ApplyStatus { get; set; }

        public string ApplySchoolName { get; set; }
    }
}
