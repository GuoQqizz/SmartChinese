using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.UnitStep
{
    public class UnitStepFactory
    {
        public static Yw_LessonUnitStep Create(Yw_LessonUnitStep entity)
        {
            return TranslatorFactory.Translator<Yw_LessonUnitStep, Yw_LessonUnitStepActions>(entity);
        }
    }
}
