using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 小艾说
    /// </summary>
    public class XiaoAiSay : StepAction
    {
        public XiaoAiSay() : base(LessonActionTypeEnum.XiaoAiSay) { }
        /// <summary>
        /// 媒体id
        /// </summary>
        public string MediaId { get; set; }
    }
}
