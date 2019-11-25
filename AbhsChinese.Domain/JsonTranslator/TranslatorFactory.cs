using AbhsChinese.Domain.Entity.StudentLessonAnswer;
using AbhsChinese.Domain.Entity.StudentWrong;
using AbhsChinese.Domain.Entity.StudyTaskAnswer;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Entity.UnitStep;
using System;
using System.Collections.Generic;

namespace AbhsChinese.Domain.JsonTranslator
{
    public class TranslatorFactory
    {
        private static Dictionary<Type, ITranslator> dic;

        static TranslatorFactory()
        {
            dic = new Dictionary<Type, ITranslator>();
            Register();
        }

        public static T1 Translator<T, T1>(T entity) where T1 : class, new()
        {
            ITranslator translator = GetTranslator<T1>();
            if (translator is ITranslator<T, T1>)
            {
                return (translator as ITranslator<T, T1>).MakeTranslator(entity);
            }
            return null;
        }

        private static ITranslator GetTranslator<T>()
        {
            return dic[typeof(T)];
        }

        private static void Register()
        {
            dic.Add(typeof(Yw_SubjectMarkContent), new MarkContentTransaltor());
            dic.Add(typeof(Yw_SubjectMark2Content), new Mark2ContentTransaltor());
            dic.Add(typeof(Yw_SubjectFreeContent), new FreeContentTransaltor());
            dic.Add(typeof(Yw_MatchContent), new MatchContentTransaltor());
            dic.Add(typeof(MultipleChoiceFillInBlankContent), new MultipleChoiceFillInBlankContentTransaltor());
            dic.Add(typeof(Yw_FillInBlankContent), new FillInBlankContentTransaltor());
            dic.Add(typeof(Yw_SubjectSelectContent), new SelectContentTransaltor());
            dic.Add(typeof(Yw_TrueFalseContent), new TrueFalseContentTranslator());
            dic.Add(typeof(Yw_LessonUnitStepActions), new UntiStepTranslator());
            dic.Add(typeof(Yw_StudentStudyTaskAnswerExt), new StuStudyTaskAnsTranslator());
            dic.Add(typeof(Yw_StudentLessonAnswerExt), new StuLessonAnsTranslator());
            dic.Add(typeof(Yw_StudentWrongSubjectExt), new StuWrongAnsTranslator());
        }
    }
}