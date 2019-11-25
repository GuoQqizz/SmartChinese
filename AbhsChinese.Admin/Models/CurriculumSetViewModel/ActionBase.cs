namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 动作基类
    /// </summary>
    public class ActionBase
    {
        public ActionBase(string type)
        {
            this.type = type;
        }
        /// <summary>
        /// 动作id(用于给界面元素添加id属性)
        /// </summary>
        public string actionId { get; set; }
        /// <summary>
        /// 动作序号
        /// </summary>
        public int actionNum { get; set; }
        /// <summary>
        /// 动作类型
        /// </summary>
        public string type { get; }
    }
}