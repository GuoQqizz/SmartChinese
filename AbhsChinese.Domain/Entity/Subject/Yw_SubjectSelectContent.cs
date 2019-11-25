using AbhsChinese.Domain.JsonEntity.Subject;
using Newtonsoft.Json;


namespace AbhsChinese.Domain.Entity.Subject
{
    public class Yw_SubjectSelectContent : Yw_SubjectContent,IJsonTranslator
    {
        public SubjectSelectContentObj Ysc_Content_Obj
        {
            set; get;
        }

        public SubjectSelectAnswerObj Ysc_Answer_Obj
        {
            set; get;
        }

        public void ResetJsonValue()
        {
            if (Ysc_Content_Obj == null)
            {
                this.Ysc_Content = null;
            }
            else
            {
                this.Ysc_Content = JsonConvert.SerializeObject(Ysc_Content_Obj);
            }

            if (Ysc_Answer_Obj == null)
            {
                this.Ysc_Answer = null;
            }
            else
            {
                this.Ysc_Answer = JsonConvert.SerializeObject(Ysc_Answer_Obj);
            }
        }
    }
}
