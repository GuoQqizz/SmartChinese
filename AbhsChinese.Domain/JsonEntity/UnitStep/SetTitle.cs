using AbhsChinese.Domain.Entity.UnitStep;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 设置标题动作
    /// </summary>
    public class SetTitle : StepAction
    {
        public SetTitle():base(LessonActionTypeEnum.SetTitle) {
        }
        /// <summary>
        /// 标题文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 标题尺寸
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 标题颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 对齐方式
        /// </summary>
        public string Align { get; set; }
        /// <summary>
        /// 定位x
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// 定位y
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// 进入类型
        /// </summary>
        public string InType { get; set; }

    }
}
