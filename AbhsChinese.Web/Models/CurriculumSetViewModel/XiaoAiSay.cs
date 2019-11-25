namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 小艾说
    /// </summary>
    public class XiaoAiSay : ActionBase
    {
        public XiaoAiSay() : base("xiaoAiSay")
        {
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string src { get; set; } = "";
    }
}