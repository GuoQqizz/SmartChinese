using AbhsChinese.Bll;
using AbhsChinese.Bll.Message;
using AbhsChinese.Code.Cache;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Message;
using System;
using System.Threading;

namespace AbhsChinese.ReportService.Jobs
{
    public class MakeLessonTaskJob : JobBase
    {
        public override string JobName()
        {
            return "系统课后任务";
        }

        public override bool IsTimeToRun(DateTime now)
        {
            return true;
        }

        public override void Run(DateTime now)
        {
            MessageBll messageBll = new MessageBll();
            StudentPracticeBll practiceBll = new StudentPracticeBll();
            int count = 1;
            while (count <= 5)
            {
                try
                {
                    MessageBody body = messageBll.ReceiveMessageBody(MessageChannel.LESSONTASK_CHANNEL);
                    if (body != null && !string.IsNullOrEmpty(body.Data))
                    {
                        string message = body.Data;
                        string[] items = message.Split(',');
                        int studentId = Convert.ToInt32(items[0]);
                        int progressId = Convert.ToInt32(items[1]);
                        Yw_StudentTask task = practiceBll.CreateTaskAutoAfterStudy(studentId, progressId);
                        if (task != null)
                        {
                            practiceBll.GenerateTaskSubjectsAutoAfterStudy(task.Yuk_TaskId, studentId);
                        }

                        Thread.Sleep(20);
                    }
                    else
                    {
                        Thread.Sleep(3000);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.ErrorLog(this.JobName(), ex);
                }
                count++;
            }
        }
    }
}

