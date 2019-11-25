using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.StudyTaskAnswer
{
    public class StuTaskAnsFactory
    {
        public static Yw_StudentStudyTaskAnswer Create(Yw_StudentStudyTaskAnswer entity)
        {
            return TranslatorFactory.Translator<Yw_StudentStudyTaskAnswer, Yw_StudentStudyTaskAnswerExt>(entity);
        }
    }
}
