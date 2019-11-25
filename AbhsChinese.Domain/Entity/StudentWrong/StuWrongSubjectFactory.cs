using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.StudentWrong
{
    public class StuWrongSubjectFactory
    {
        public static Yw_StudentWrongSubject Create(Yw_StudentWrongSubject entity)
        {
            return TranslatorFactory.Translator<Yw_StudentWrongSubject, Yw_StudentWrongSubjectExt>(entity);
        }
    }
}
