using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using Newtonsoft.Json;
using System;

namespace AbhsChinese.Domain.JsonTranslator
{
    public class FreeContentTransaltor : ITranslator<Yw_SubjectContent, Yw_SubjectFreeContent>
    {
        public Yw_SubjectFreeContent MakeTranslator(Yw_SubjectContent entity)
        {
            Yw_SubjectFreeContent realEntity = entity.ConvertTo<Yw_SubjectFreeContent>();
            if (!string.IsNullOrEmpty(entity.Ysc_Content))
            {
                realEntity.Ysc_Content_Obj = JsonConvert.DeserializeObject<SubjectFreeContentObj>(entity.Ysc_Content);
            }
            if (!string.IsNullOrEmpty(entity.Ysc_Answer))
            {
                realEntity.Ysc_Answer_Obj = JsonConvert.DeserializeObject<SubjectFreeAnswerObj>(entity.Ysc_Answer);
            }
            return realEntity;
        }
    }
}