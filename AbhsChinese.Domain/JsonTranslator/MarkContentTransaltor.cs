using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using Newtonsoft.Json;
using System;

namespace AbhsChinese.Domain.JsonTranslator
{
    public class MarkContentTransaltor : ITranslator<Yw_SubjectContent, Yw_SubjectMarkContent>
    {
        public Yw_SubjectMarkContent MakeTranslator(Yw_SubjectContent entity)
        {
            Yw_SubjectMarkContent realEntity = entity.ConvertTo<Yw_SubjectMarkContent>();
            if (!string.IsNullOrEmpty(entity.Ysc_Content))
            {
                realEntity.Ysc_Content_Obj = JsonConvert.DeserializeObject<SubjectMarkContentObj>(entity.Ysc_Content);
            }
            if (!string.IsNullOrEmpty(entity.Ysc_Answer))
            {
                realEntity.Ysc_Answer_Obj = JsonConvert.DeserializeObject<SubjectMarkAnswerObj>(entity.Ysc_Answer);
            }
            return realEntity;
        }
    }
}