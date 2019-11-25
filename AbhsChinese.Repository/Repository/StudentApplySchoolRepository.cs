using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class StudentApplySchoolRepository : RepositoryBase<Yw_StudentApplySchool>, IStudentApplySchoolRepository
    {
        public StudentApplySchoolRepository() : base(AppSetting.ConnString)
        {
        }
        #region select
        /// <summary>
        /// 搜索申请学生列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<DtoStudentApplySchool> GetToDoApplyList(DtoApplyStudentSearch search)
        {
            if (search == null)
            {
                return null;
            }
            var strWhere = new StringBuilder();
            var fields = "ap.*,s.*,st.Yoh_Name AS TeacherName,st.Yoh_Phone AS TeacherPhone,sc.Bsl_SchoolName AS SchoolName ";
            var orderBy = "ap.Yay_ApplyTime DESC ";
            var parameters = new DynamicParameters();

            strWhere.Append($@"dbo.Yw_StudentApplySchool ap
                                 JOIN dbo.Bas_Student s ON ap.Yay_StudentId=s.Bst_Id AND s.Bst_SchoolId=0
                                 JOIN dbo.Yw_SchoolTeacher st ON ap.Yay_TeacherId=st.Yoh_Id
                                 JOIN dbo.Bas_School sc ON ap.Yay_SchoolId = sc.Bsl_Id
                                 WHERE 1=1  AND ap.Yay_Status={(int)ApplyStatusEnum.申请}");

            //if (search.Status > 0)
            //{
            //    strWhere.Append(" AND ap.Yay_Status=@Yay_Status");
            //    parameters.Add("Yay_Status", search.Status);
            //}
            if (search.SchoolId > 0)
            {
                strWhere.Append(" AND ap.Yay_SchoolId=@Yay_SchoolId");
                parameters.Add("Yay_SchoolId", search.SchoolId);
            }
            if (search.Grade > 0)
            {
                strWhere.Append(" AND s.Bst_Grade=@Grade");
                parameters.Add("Grade", search.Grade);
            }
            if (!string.IsNullOrEmpty(search.Bst_No))
            {
                strWhere.Append(" AND s.Bst_No=@Bst_No");
                parameters.Add("Bst_No", search.Bst_No);
            }
            if (!string.IsNullOrEmpty(search.SearchStr))
            {
                strWhere.Append(" AND (s.Bst_Name LIKE @Bst_Name OR s.Bst_NickName LIKE @Bst_NickName OR s.Bst_Phone LIKE @Bst_Phone)");
                parameters.Add("Bst_Name", $"%{search.SearchStr}%");
                parameters.Add("Bst_NickName", $"%{search.SearchStr}%");
                parameters.Add("Bst_Phone", $"%{search.SearchStr}%");
            }
            return base.QueryPaging<DtoStudentApplySchool>(fields, strWhere._ToString(), orderBy, search.Pagination, parameters).ToList();

        }

        public DtoStudentApplySchool GetApplyByStudentId(int studentId)
        {
            string sql = $@" SELECT TOP 1 ap.* ,
                                    s.* ,
                                    st.Yoh_Name AS TeacherName ,
                                    st.Yoh_Phone AS TeacherPhone ,
                                    sc.Bsl_SchoolName AS SchoolName
                             FROM   dbo.Yw_StudentApplySchool ap
                                    JOIN dbo.Bas_Student s ON ap.Yay_StudentId = s.Bst_Id
                                    JOIN dbo.Yw_SchoolTeacher st ON ap.Yay_TeacherId = st.Yoh_Id
                                    JOIN dbo.Bas_School sc ON ap.Yay_SchoolId = sc.Bsl_Id
                             WHERE  1 = 1
                                    AND ap.Yay_StudentId= {studentId} ORDER BY ap.Yay_ApplyTime DESC";
            return Query<DtoStudentApplySchool>(sql).FirstOrDefault();
        }
        public DtoStudentApplySchool GetById(int yayId)
        {
            string sql = $@" SELECT ap.* ,
                                    s.* ,
                                    st.Yoh_Name AS TeacherName ,
                                    st.Yoh_Phone AS TeacherPhone ,
                                    sc.Bsl_SchoolName AS SchoolName
                             FROM   dbo.Yw_StudentApplySchool ap
                                    JOIN dbo.Bas_Student s ON ap.Yay_StudentId = s.Bst_Id
                                    JOIN dbo.Yw_SchoolTeacher st ON ap.Yay_TeacherId = st.Yoh_Id
                                    JOIN dbo.Bas_School sc ON ap.Yay_SchoolId = sc.Bsl_Id
                             WHERE  1 = 1
                                    AND ap.Yay_Id={yayId}";
            return Query<DtoStudentApplySchool>(sql).FirstOrDefault();
        }
        #endregion

        #region update
        /// <summary>
        /// 更改申请状态
        /// </summary>
        /// <param name="yayId"></param>
        /// <param name="toStatus"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public bool UpdateApplyStatus(int yayId, int toStatus, int fromStatus, int oper)
        {
            string sql = "UPDATE dbo.Yw_StudentApplySchool SET Yay_Status=@toStatus,Yay_OperateTime=GETDATE(),Yay_Operator=@Yay_Operator WHERE Yay_Id=@Yay_Id AND Yay_Status=@fromStatus";

            return Execute(sql, new
            {
                toStatus = toStatus,
                fromStatus = fromStatus,
                Yay_Operator = oper,
                Yay_Id = yayId
            }) > 0;
        }

        public bool UpdateApplyStatusByStudentId(int studentId, int toStatus, int fromStatus, int oper)
        {
            string sql = "UPDATE dbo.Yw_StudentApplySchool SET Yay_Status=@toStatus,Yay_OperateTime=GETDATE(),Yay_Operator=@Yay_Operator WHERE Yay_StudentId=@Yay_StudentId  AND     Yay_Status = @fromStatus";

            return Execute(sql, new
            {
                toStatus = toStatus,
                fromStatus = fromStatus,
                Yay_Operator = oper,
                Yay_StudentId = studentId
            }) > 0;
        }


        #endregion


        #region basecrud
        public int Insert(Yw_StudentApplySchool entity)
        {
            return base.InsertEntity(entity);
        }
        #endregion
    }
}
