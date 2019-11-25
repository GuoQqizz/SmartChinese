using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using Newtonsoft.Json;
using System;

namespace AbhsChinese.Domain.JsonTranslator
{
    public class Mark2ContentTransaltor : ITranslator<Yw_SubjectContent, Yw_SubjectMark2Content>
    {
        public Yw_SubjectMark2Content MakeTranslator(Yw_SubjectContent entity)
        {
            Yw_SubjectMark2Content realEntity = entity.ConvertTo<Yw_SubjectMark2Content>();
            if (!string.IsNullOrEmpty(entity.Ysc_Content))
            {
                realEntity.Ysc_Content_Obj = JsonConvert.DeserializeObject<SubjectMark2ContentObj>(entity.Ysc_Content);
            }
            if (!string.IsNullOrEmpty(entity.Ysc_Answer))
            {
                realEntity.Ysc_Answer_Obj = JsonConvert.DeserializeObject<SubjectMark2AnswerObj>(entity.Ysc_Answer);
            }
            return realEntity;
        }
    }
}