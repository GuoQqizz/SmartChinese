using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using Newtonsoft.Json;

namespace AbhsChinese.Domain.JsonTranslator
{
    public class TrueFalseContentTranslator : ITranslator<Yw_SubjectContent, Yw_TrueFalseContent>
    {
        public Yw_TrueFalseContent MakeTranslator(Yw_SubjectContent entity)
        {
            Yw_TrueFalseContent realEntity = new Yw_TrueFalseContent();
            realEntity.Ysc_SubjectId = entity.Ysc_SubjectId;
            realEntity.Ysc_SubjectType = entity.Ysc_SubjectType;
            realEntity.Ysc_CreateTime = entity.Ysc_CreateTime;
            realEntity.Ysc_Creator = entity.Ysc_Creator;
            realEntity.Ysc_Explain = entity.Ysc_Explain;
            realEntity.Ysc_UpdateTime = entity.Ysc_UpdateTime;
            realEntity.Ysc_Editor = entity.Ysc_Editor;
            realEntity.Ysc_Answer = entity.Ysc_Answer;
            realEntity.Ysc_Content = entity.Ysc_Content;
            if (!string.IsNullOrEmpty(entity.Ysc_Content))
            {
                realEntity.Ysc_Content_Obj = JsonConvert.DeserializeObject<TrueFalseContentObj>(entity.Ysc_Content);
            }
            if (!string.IsNullOrEmpty(entity.Ysc_Answer))
            {
                realEntity.Ysc_Answer_Obj = JsonConvert.DeserializeObject<TrueFalseAnswerObj>(entity.Ysc_Answer);
            }
            return realEntity;
        }
    }
}