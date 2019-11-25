using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using Newtonsoft.Json;
using System;

namespace AbhsChinese.Domain.JsonTranslator
{
    public class MatchContentTransaltor : ITranslator<Yw_SubjectContent, Yw_MatchContent>
    {
        public Yw_MatchContent MakeTranslator(Yw_SubjectContent entity)
        {
            Yw_MatchContent realEntity = entity.ConvertTo<Yw_MatchContent>();

            if (!string.IsNullOrEmpty(entity.Ysc_Content))
            {
                realEntity.Ysc_Content_Obj = JsonConvert.DeserializeObject<MatchContentObj>(entity.Ysc_Content);
            }
            if (!string.IsNullOrEmpty(entity.Ysc_Answer))
            {
                realEntity.Ysc_Answer_Obj = JsonConvert.DeserializeObject<MatchAnswerObj>(entity.Ysc_Answer);
            }
            return realEntity;
        }
    }
}