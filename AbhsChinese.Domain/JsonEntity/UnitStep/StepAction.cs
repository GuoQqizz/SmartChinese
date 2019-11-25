using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 步骤动作基类
    /// </summary>
    public class StepAction
    {
        public StepAction(LessonActionTypeEnum type)
        {
            this.Type = type;
        }
        /// <summary>
        /// 动作序号
        /// </summary>
        public int ActionNum { get; set; }
        /// <summary>
        /// 动作id
        /// </summary>
        public string Actionid { get; set; }

        /// <summary>
        /// 动作类型
        /// </summary>
        public LessonActionTypeEnum Type { get;  }
    }
}
