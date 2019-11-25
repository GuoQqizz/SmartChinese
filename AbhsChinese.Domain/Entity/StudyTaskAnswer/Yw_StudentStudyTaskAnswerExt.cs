using AbhsChinese.Domain.JsonEntity.Answer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.StudyTaskAnswer
{
    public class Yw_StudentStudyTaskAnswerExt : Yw_StudentStudyTaskAnswer, IJsonTranslator
    {

        public StudentAnswerCard Yta_Answer_Obj
        {
            set; get;
        }

        public void ResetJsonValue()
        {
            if (Yta_Answer_Obj == null)
            {
                Yta_StudentAnswer = null;
            }
            else
            {
                Yta_StudentAnswer = JsonConvert.SerializeObject(Yta_Answer_Obj);
            }
        }
    }
}
