using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.JsonEntity.UnitStep;
using Newtonsoft.Json;

namespace AbhsChinese.Domain.Entity.UnitStep
{
    /// <summary>
    /// 课程步骤
    /// </summary>
    public class Yw_LessonUnitStepActions : Yw_LessonUnitStep, IJsonTranslator
    {
        /// <summary>
        /// 步骤数据
        /// </summary>
        public List<Step> Steps { get; set; }
        public void ResetJsonValue()
        {
            if (Steps != null)
            {
                this.Yls_Steps = JsonConvert.SerializeObject(Steps);
            }
            else {
                this.Yls_Steps = null;
            }

        }
    }
}
