using AbhsChinese.Code.Common;
using AbhsChinese.ReportService.JobProcess;
using System;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace AbhsChinese.ReportService
{
    public partial class Service1 : ServiceBase
    {
        JobProcessBase[] JobProcesss = null;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                LogHelper.WriteLog("艾宾浩斯大语文服务开始");

                JobProcesss = new JobProcessBase[3];

                JobProcesss[0] = new ReportJobProcess();
                JobProcesss[1] = new MakeLessonTaskProcess();
                JobProcesss[2] = new CancelOrderProcess();

                Task.Factory.StartNew(JobProcesss[0].Execute);
                Task.Factory.StartNew(JobProcesss[1].Execute);
                Task.Factory.StartNew(JobProcesss[2].Execute);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        protected override void OnStop()
        {
            LogHelper.WriteLog("艾宾浩斯大语文服务结束");
            JobProcesss[0].Stop = true;
            JobProcesss[1].Stop = true;
            JobProcesss[2].Stop = true;
        }
    }
}
