namespace AbhsChinese.ReportService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.艾宾浩斯大语文数据统计服务 = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // 艾宾浩斯大语文数据统计服务
            // 
            this.艾宾浩斯大语文数据统计服务.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.艾宾浩斯大语文数据统计服务.Password = null;
            this.艾宾浩斯大语文数据统计服务.Username = null;
            // 
            // serviceInstaller1
            // 
            this.serviceInstaller1.ServiceName = "艾宾浩斯大语文数据统计服务";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.艾宾浩斯大语文数据统计服务,
            this.serviceInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller 艾宾浩斯大语文数据统计服务;
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;
    }
}