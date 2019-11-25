using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.Dto.Request.Student;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using AbhsChinese.Domain.AbhsResource;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Web;

namespace AbhsChinese.Bll
{
    public class StudentInfoBll : BllBase
    {
        public StudentInfoBll() : base()
        {
        }

        #region Repository
        private IStudentPassportRepository studentPassportRepository;
        public IStudentPassportRepository StudentPassportRepository
        {
            get
            {
                if (studentPassportRepository == null)
                {
                    studentPassportRepository = new StudentPassportRepository();
                }
                return studentPassportRepository;
            }
        }

        private IStudentLoginRepository studentLoginRepository;
        public IStudentLoginRepository StudentLoginRepository
        {
            get
            {
                if (studentLoginRepository == null)
                {
                    studentLoginRepository = new StudentLoginRepository();
                }
                return studentLoginRepository;
            }
        }

        private ISumStudentRepository sumStudentRepository;
        public ISumStudentRepository SumStudentRepository
        {
            get
            {
                if (sumStudentRepository == null)
                {
                    sumStudentRepository = new SumStudentRepository();
                }
                return sumStudentRepository;
            }
        }
        #endregion

        #region 注册
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="register"></param>
        public int Register(DtoStudentRegister register)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    int studentId = AddStudent(register.Name, register.Grade, register.Phone, register.Source, register.OperatorId);

                    AddStudentPassport(studentId, register.PassportType, register.Phone, register.Password);

                    int studentLoginId = AddStudentLogin(studentId);

                    AddSumStudent(studentLoginId);

                    //查询学生可领取的注册券
                    CashVoucherBll cashVoucherBll = new CashVoucherBll();
                    var studentCashVoucher = cashVoucherBll.GetAvailableRegisterVoucher(studentId);
                    //领取注册券
                    foreach (var item in studentCashVoucher)
                    {
                        var studentCashVoucher1 = cashVoucherBll.TakeStudentCashVoucher(studentId, item.Ycv_Id, VoucherGotTypeEnum.注册领取, 0);
                    }
                    if(studentCashVoucher!=null && studentCashVoucher.Count>0)
                    {
                        WebHelper.WriteCookie("CashVoucher", "CashVoucher");
                    }

                    scope.Complete();

                    return studentId;
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="name"></param>
        /// <param name="grade"></param>
        /// <param name="phone"></param>
        /// <param name="source"></param>
        /// <param name="operatorId"></param>
        /// <returns>学生Id</returns>
        private int AddStudent(string name, int grade, string phone, RegisterRegSourceEnum source, int operatorId)
        {
            StudentBll studentBll = new StudentBll();
            Bas_Student student = new Bas_Student();
            student.Bst_Name = name;
            student.Bst_Grade = grade;
            student.Bst_Phone = phone;
            student.Bst_Sex = (int)SexEnum.男;
            student.Bst_RegTime = DateTime.Now;
            student.Bst_RegSource = (int)source;
            student.Bst_Status = (int)StudentAccountStatusEnum.启用;
            student.Bst_Birthday = "1900-01-01"._ToDateTime();
            student.Bst_No = studentBll.StudentRepository.GetStudentNo();
            student.Bst_UpdateTime = DateTime.Now;
            if ((int)source == 2)
            {
                student.Bst_RegOperator = operatorId;
            }
            return studentBll.StudentRepository.Add(student);
        }

        /// <summary>
        /// 添加学生账号
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="passportType"></param>
        /// <param name="passportKey"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int AddStudentPassport(int studentId, StudentAccountSourceEnum passportType, string passportKey, string password)
        {
            if (passportType == StudentAccountSourceEnum.人脸识别)
            {
                var entity = StudentPassportRepository.GetPassportByStuIdAndType(studentId, (int)passportType);
                if (entity != null)
                {
                    if (entity.Bsp_Status == (int)StudentAccountStatusEnum.禁用)
                    {
                        entity.Bsp_PassportKey = passportKey;
                        entity.Bsp_Status = (int)StudentAccountStatusEnum.启用;
                        StudentPassportRepository.Update(entity);
                    }
                    return 1;
                }
            }
            Bas_StudentPassport studentPassport = new Bas_StudentPassport()
            {
                Bsp_StudentId = studentId,
                Bsp_PassportType = (int)passportType,
                Bsp_PassportKey = passportKey,
                Bsp_Password = password,
                Bsp_Status = (int)StudentAccountStatusEnum.启用,
                Bsp_CreateTime = DateTime.Now,
                Bsp_UpdateTime = DateTime.Now
            };
            return StudentPassportRepository.Register(studentPassport);
        }

        /// <summary>
        /// 添加学员统计表
        /// </summary>
        /// <param name="studentLoginId"></param>
        private void AddSumStudent(int studentLoginId)
        {
            var studentLogin = StudentLoginRepository.Get(studentLoginId);
            Sum_Student sumStudent = new Sum_Student()
            {
                Sst_StudentId = studentLogin.Bsg_StudentId,
                Sst_FirstOrderTime = "1900-01-01"._ToDateTime(),
                Sst_LastLoginIp = studentLogin.Bsg_LoginIp,
                Sst_LastLoginArea = studentLogin.Bsg_LoginArea,
                Sst_LastLoginTime = studentLogin.Bsg_LoginTime,
                Sst_OwnCourseCount = 0,
                Sst_StudyDayCount = 0,
                Sst_ExperiencePoints = 0,
                Sst_Coins = 0,
                Sst_UpdateTime = DateTime.Now
            };
            SumStudentRepository.Add(sumStudent);
        }

        /// <summary>
        /// 判断手机号是否存在
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool IsExistPhone(string phone)
        {
            return StudentPassportRepository.IsExistPhone(phone);
        }
        #endregion

        #region 登录
        public Bas_StudentPassport Login(string passportKey, string password)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Bas_StudentPassport studentPassport = StudentPassportRepository.Login(passportKey, password);
                    if (studentPassport != null)
                    {
                        AddStudentLogin(studentPassport.Bsp_StudentId);
                        UpdateSumStudent(studentPassport.Bsp_StudentId);
                    }

                    scope.Complete();

                    return studentPassport;
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 添加登录记录
        /// </summary>
        /// <param name="studentId"></param>
        public int AddStudentLogin(int studentId)
        {
            Bas_StudentLogin studentLogin = new Bas_StudentLogin();
            studentLogin.Bsg_StudentId = studentId;
            studentLogin.Bsg_LoginTime = DateTime.Now;

            string ip = "";
            string cityName = "";
            ip = clsCommon.GetIP();
            if (ip.HasValue())
            {
                cityName = clsCommon.CityName_ByBaidu(ip);
            }
            studentLogin.Bsg_LoginIp = ip;
            studentLogin.Bsg_LoginArea = cityName;
            var device = "";
            device = clsCommon.CheckAgent();
            if (device.Contains("Windows NT"))
            {
                studentLogin.Bsg_LoginDevice = (int)LoginDeviceEnum.PC端;
            }
            else if (device.Contains("iPad"))
            {
                studentLogin.Bsg_LoginDevice = (int)LoginDeviceEnum.iPad;
            }
            return StudentLoginRepository.Add(studentLogin);
        }

        public Bas_StudentPassport GetStudentPassport(int studentId, StudentAccountSourceEnum passportType)
        {
            var passport = GetPassportByStuIdAndType(studentId, passportType);
            if (passport != null)
            {
                AddStudentLogin(studentId);
                UpdateSumStudent(studentId);
            }
            return passport;
        }

        /// <summary>
        /// 修改学生统计表
        /// </summary>
        /// <param name="studentId"></param>
        private void UpdateSumStudent(int studentId)
        {
            var sumStudent = SumStudentRepository.Get(studentId);
            WebHelper.WriteCookie("LastLoginTime", Encrypt.EncryptQueryString(sumStudent.Sst_LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss")));
            if (sumStudent != null)
            {
                var currentIP = clsCommon.GetIP();
                sumStudent.Sst_LastLoginIp = currentIP;
                sumStudent.Sst_LastLoginArea = clsCommon.CityName_ByBaidu(currentIP);
                sumStudent.Sst_LastLoginTime = DateTime.Now;
                sumStudent.Sst_UpdateTime = DateTime.Now;
                SumStudentRepository.Update(sumStudent);
            }
        }

        public Bas_StudentPassport MobileLogin(string phone)
        {
            var studentPassport = StudentPassportRepository.GetPassportByPassportAndType(phone, (int)StudentAccountSourceEnum.手机);
            if (studentPassport != null)
            {
                AddStudentLogin(studentPassport.Bsp_StudentId);
                UpdateSumStudent(studentPassport.Bsp_StudentId);
            }
            return studentPassport;
        }

        /// <summary>
        /// 忘记密码 （登录）
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="newPassword"></param>
        public void UpdatePasswordByPhone(string phone, string newPassword)
        {
            var studentPassport = StudentPassportRepository.GetPassportByPassportAndType(phone, (int)StudentAccountSourceEnum.手机);
            if (studentPassport != null)
            {
                studentPassport.Bsp_Password = Encrypt.GetMD5Pwd(newPassword);
                studentPassport.Bsp_UpdateTime = DateTime.Now;
                StudentPassportRepository.Update(studentPassport);
            }
        }

        public Bas_StudentPassport GetByPassportKey(string passportKey)
        {
            StudentBll studentBll = new StudentBll();
            return studentBll.StudentPassportRepository.GetByPassport(passportKey);
        }
        #endregion

        #region 学生信息
        /// <summary>
        /// 获取学生统计信息（页眉）
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public DtoSumStudentTip GetSumStudentTip(int studentId)
        {
            StudentBll studentBll = new StudentBll();
            return studentBll.StudentRepository.GetSumStudentTip(studentId);
        }

        /// <summary>
        /// 学生个人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DtoStudentInfo GetStudentInfo(int id)
        {
            StudentBll studentBll = new StudentBll();
            StudentApplyBll studentApplyBll = new StudentApplyBll();
            var studentInfo = studentBll.StudentRepository.GetStudentInfoById(id);
            if (studentInfo.Bst_SchoolId > 0)
            {
                return studentInfo;
            }
            DtoStudentApplySchool studentApplySchool = studentApplyBll.GetApplyByStudentId(id);
            if (studentApplySchool != null)
            {
                studentInfo.ApplyStatus = studentApplySchool.Yay_Status;
                studentInfo.ApplySchoolName = studentApplySchool.SchoolName;
            }
            return studentInfo;
        }
        /// <summary>
        /// 学生个人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DtoStudentInfo GetStudentInfoByAccount(string account)
        {
            StudentBll studentBll = new StudentBll();
            StudentApplyBll studentApplyBll = new StudentApplyBll();
            var studentInfo = studentBll.StudentRepository.GetStudentInfoByAccount(account);
            return studentInfo;
        }

        public List<DtoOwnCashVoucher> GetOwnCashVoucher(PagingObject paging, int studentId, StudentCashVoucherStatusEnum status)
        {
            CashVoucherBll cashVoucherBll = new CashVoucherBll();
            if (status == StudentCashVoucherStatusEnum.未使用)
            {
                //检查是否有过期的现金券
                cashVoucherBll.StudentCashVoucherRepository.UpdateExpiredStatusByStudentId(studentId);
            }
            return cashVoucherBll.StudentCashVoucherRepository.GetPagingOwnCashVoucher(paging, studentId, (int)status);
        }
        #endregion

        #region 安全中心
        /// <summary>
        /// 判断是否绑定人脸识别登录
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool IsBindFaceLogin(int studentId)
        {
            return StudentPassportRepository.IsBindFaceLogin(studentId);
        }

        /// <summary>
        /// 更换手机
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="phone"></param>
        public void UpdateMobile(int studentId, string phone)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    StudentBll studentBll = new StudentBll();
                    var student = studentBll.StudentRepository.Get(studentId);
                    if (student != null)
                    {
                        student.Bst_Phone = phone;
                        student.Bst_UpdateTime = DateTime.Now;
                        studentBll.StudentRepository.Update(student);
                    }
                    var studentPassport = GetPassportByStuIdAndType(studentId, StudentAccountSourceEnum.手机);
                    if (studentPassport != null)
                    {
                        studentPassport.Bsp_PassportKey = phone;
                        studentPassport.Bsp_UpdateTime = DateTime.Now;
                        StudentPassportRepository.Update(studentPassport);
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        public int UpdatePassword(int studentId, string oldPassword, string newPassword)
        {
            var studentPassport = GetPassportByStuIdAndType(studentId, StudentAccountSourceEnum.手机);

            if (studentPassport != null)
            {
                if (studentPassport.Bsp_Password != Encrypt.GetMD5Pwd(oldPassword))
                {
                    return -1;
                }

                studentPassport.Bsp_Password = Encrypt.GetMD5Pwd(newPassword);
                studentPassport.Bsp_UpdateTime = DateTime.Now;
                return StudentPassportRepository.Update(studentPassport) ? 1 : 0;
            }
            return 0;
        }

        /// <summary>
        /// 忘记密码 （个人中心）
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="newPassword"></param>
        public void FindPassword(int studentId, string newPassword)
        {
            var studentPassport = GetPassportByStuIdAndType(studentId, StudentAccountSourceEnum.手机);

            if (studentPassport != null)
            {
                studentPassport.Bsp_Password = Encrypt.GetMD5Pwd(newPassword);
                studentPassport.Bsp_UpdateTime = DateTime.Now;
                StudentPassportRepository.Update(studentPassport);
            }
        }

        public List<Bas_StudentLogin> GetStudentLogin(PagingObject paging, int studentId)
        {
            return StudentLoginRepository.GetStudentLogin(paging, studentId);
        }

        public Bas_StudentPassport GetPassportByStuIdAndType(int studentId, StudentAccountSourceEnum passportType)
        {
            StudentBll studentBll = new StudentBll();
            var studentPassport = StudentPassportRepository.GetPassportByStuIdAndType(studentId, (int)passportType);
            if (studentPassport != null && studentPassport.Bsp_Status == (int)StudentAccountStatusEnum.启用)
            {
                return studentPassport;
            }
            return null;
        }

        /// <summary>
        /// 删除人脸识别
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool UpdateStatus(int studentId, StudentAccountStatusEnum status)
        {
            var studentPassport = StudentPassportRepository.GetPassportByStuIdAndType(studentId, (int)StudentAccountSourceEnum.人脸识别);
            if (studentPassport != null)
            {
                studentPassport.Bsp_Status = (int)status;
                studentPassport.Bsp_UpdateTime = DateTime.Now;
                return StudentPassportRepository.Update(studentPassport);
            }
            return false;
        }

        /// <summary>
        /// 修改人脸识别（token）
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool UpdatePassportKey(int studentId, string passportKey)
        {
            var studentPassport = StudentPassportRepository.GetPassportByStuIdAndType(studentId, (int)StudentAccountSourceEnum.人脸识别);
            if (studentPassport != null)
            {
                studentPassport.Bsp_PassportKey = passportKey;
                studentPassport.Bsp_UpdateTime = DateTime.Now;
                return StudentPassportRepository.Update(studentPassport);
            }
            return false;
        }

        public void Update(Bas_Student student)
        {
            StudentBll studentBll = new StudentBll();
            var entity = studentBll.StudentRepository.Get(student.Bst_Id);
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(student.Bst_Avatar))
                {
                    entity.Bst_Avatar = student.Bst_Avatar;
                }

                entity.Bst_NickName = student.Bst_NickName;
                entity.Bst_Name = student.Bst_Name;
                entity.Bst_Sex = student.Bst_Sex;
                entity.Bst_Birthday = student.Bst_Birthday;
                entity.Bst_Grade = student.Bst_Grade;
                entity.Bst_StudySchool = student.Bst_StudySchool;
                entity.Bst_Province = student.Bst_Province;
                entity.Bst_City = student.Bst_City;
                entity.Bst_County = student.Bst_County;
                entity.Bst_Address = student.Bst_Address;
                entity.Bst_UpdateTime = DateTime.Now;
            }
            studentBll.StudentRepository.Update(entity);
        }
        #endregion

        /// <summary>
        /// 给学员添加金币
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="coins"></param>
        public void AddCoins(int studentId, int coins)
        {
            SumStudentRepository.AddCoins(studentId, coins);
        }

        public Sum_Student GetSumStudent(int studentId)
        {
            return SumStudentRepository.Get(studentId);
        }

        public void UpdateSumStudent(Sum_Student student)
        {
            SumStudentRepository.Update(student);
        }

        public void UpdateStudyDayCountOfStudent(int studentId, int dayCount)
        {
            Sum_Student sumStudent = GetSumStudent(studentId);
            if (sumStudent != null)
            {
                sumStudent.Sst_StudyDayCount = dayCount;
                UpdateSumStudent(sumStudent);
            }
        }

        public Bas_Student GetBySchoolId()
        {
            StudentBll studentBll = new StudentBll();
            var student = studentBll.StudentRepository.GetBySchoolId();
            AddStudentLogin(student.Bst_Id);
            UpdateSumStudent(student.Bst_Id);
            return student;
        }
    }
}
