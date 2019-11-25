using AbhsChinese.Domain.Entity.StudentWrong;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonTranslator
{
    public class StuWrongAnsTranslator : ITranslator<Yw_StudentWrongSubject, Yw_StudentWrongSubjectExt>
    {
        public Yw_StudentWrongSubjectExt MakeTranslator(Yw_StudentWrongSubject entity)
        {
            Yw_StudentWrongSubjectExt realEntity = entity.ConvertTo<Yw_StudentWrongSubjectExt>();

            if (!string.IsNullOrEmpty(entity.Yws_StudentAnswer))
            {
                realEntity.Yws_Answer_Obj = AnswerFactory.ConvertByType((SubjectTypeEnum)entity.Yws_SubjectType, entity.Yws_StudentAnswer);
            }
            return realEntity;
        }
    }
}
