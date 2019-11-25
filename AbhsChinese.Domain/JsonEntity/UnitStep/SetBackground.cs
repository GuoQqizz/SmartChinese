using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    public class SetBackground : StepAction
    {
        public SetBackground():base(LessonActionTypeEnum.SetBackground)
        {
        }
        /// <summary>
        /// 背景类型(image/color)
        /// </summary>
        public string BgType { get; set; }
        /// <summary>
        /// 背景内容
        /// </summary>
        public string Bg { get; set; }
    }
}
