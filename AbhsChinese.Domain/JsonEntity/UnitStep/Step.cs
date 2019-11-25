using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    public class Step
    {
        /// <summary>
        /// 步骤id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 步骤编号
        /// </summary>
        public int StepNum { get; set; }
        /// <summary>
        /// 动作集合
        /// </summary>
        public List<StepAction> Actions { get; set; }
    }
}
