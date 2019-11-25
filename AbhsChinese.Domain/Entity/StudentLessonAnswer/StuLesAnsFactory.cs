using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.StudentLessonAnswer
{
    public class StuLesAnsFactory
    {
        public static Yw_StudentLessonAnswer Create(Yw_StudentLessonAnswer entity)
        {
            return TranslatorFactory.Translator<Yw_StudentLessonAnswer, Yw_StudentLessonAnswerExt>(entity);
        }
    }
}
