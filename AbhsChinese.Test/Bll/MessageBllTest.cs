using AbhsChinese.Bll;
using AbhsChinese.Bll.Message;
using AbhsChinese.Bll.Report;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.Message;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class MessageBllTest
    {
        [TestMethod]
        public void PublishMessageTest()
        {
            MessageBll bll = new MessageBll();

            CourseStudyTaskMessage msg = new CourseStudyTaskMessage();
            msg.AsOfDate = new DateTime(2019, 11, 11);
            //msg.AsOfDate = new DateTime(2019, 10, 31, 1, 1, 1);
            msg.CourseId = 10000;
            msg.LessonId = 10000;
            msg.StudentId = 10002;
            msg.StudySeconds = 60;
            msg.GetCoins = 10;
            msg.SubjectCount = 5;
            string msgBody = JsonConvert.SerializeObject(msg);
            bll.PublishMessage(MessageChannel.REPORT_CHANNEL, MessageTypeEnum.课程学习, msgBody);

            msg = new CourseStudyTaskMessage();
            msg.AsOfDate = new DateTime(2019, 11, 11);
            msg.CourseId = 10000;
            msg.LessonId = 10000;
            msg.StudentId = 10002;
            msg.StudySeconds = 60;
            msg.GetCoins = 10;
            msg.SubjectCount = 5;
            msgBody = JsonConvert.SerializeObject(msg);
            bll.PublishMessage(MessageChannel.REPORT_CHANNEL, MessageTypeEnum.课程学习, msgBody);

            msg = new CourseStudyTaskMessage();
            msg.AsOfDate = new DateTime(2019, 11, 11);
            msg.CourseId = 10000;
            msg.LessonId = 10000;
            msg.StudentId = 10002;
            msg.StudySeconds = 60;
            msg.GetCoins = 10;
            msg.SubjectCount = 5;
            msgBody = JsonConvert.SerializeObject(msg);
            bll.PublishMessage(MessageChannel.REPORT_CHANNEL, MessageTypeEnum.课程学习, msgBody);

            msg = new CourseStudyTaskMessage();
            msg.AsOfDate = new DateTime(2019, 11, 12);
            msg.CourseId = 10000;
            msg.LessonId = 10000;
            msg.StudentId = 10002;
            msg.StudySeconds = 60;
            msg.GetCoins = 10;
            msg.SubjectCount = 5;
            msgBody = JsonConvert.SerializeObject(msg);
            bll.PublishMessage(MessageChannel.REPORT_CHANNEL, MessageTypeEnum.课程学习, msgBody);
        }

        [TestMethod]
        public void ReceiveMessageTest()
        {
            MessageBll bll = new MessageBll();
            string msg = bll.ReceiveMessage(MessageChannel.REPORT_CHANNEL);
            SumStudentBll dailyBll = new SumStudentBll();
            dailyBll.SaveReportData(msg);
        }

        [TestMethod]
        public void BothTest()
        {
            CourseStudyTaskMessage msg = new CourseStudyTaskMessage();
            msg.AsOfDate = DateTime.Now;
            msg.CourseId = 10000;
            msg.LessonId = 10000;
            msg.StudentId = 10002;
            msg.StudySeconds = 60;
            string msgBody = JsonConvert.SerializeObject(msg);
            MessageBll bll = new MessageBll();

            bll.PublishMessage(MessageChannel.REPORT_CHANNEL, MessageTypeEnum.课程学习, msgBody);

            bll.PublishMessage(MessageChannel.ADVERTISING_CHANNEL, MessageTypeEnum.课程学习, msgBody);
            List<Task> tasks = new List<Task>();

            tasks.Add(Task.Factory.StartNew(GetMsg));
            tasks.Add(Task.Factory.StartNew(GetMsg1));

            Task.WaitAll(tasks.ToArray());
            string msgx = bll.ReceiveMessage(MessageChannel.REPORT_CHANNEL);
            //StudentDailyBll dailyBll = new StudentDailyBll();
            //dailyBll.SaveData(msgx);
        }

        private void GetMsg()
        {
            MessageBll bll = new MessageBll();
            string msgx = bll.ReceiveMessage(MessageChannel.REPORT_CHANNEL);
        }

        private void GetMsg1()
        {
            MessageBll bll = new MessageBll();
            string msgx = bll.ReceiveMessage(MessageChannel.ADVERTISING_CHANNEL);
        }

    }
}
