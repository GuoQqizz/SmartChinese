using AbhsChinese.Domain.JsonEntity.Answer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.StudentWrong
{
    public class Yw_StudentWrongSubjectExt : Yw_StudentWrongSubject, IJsonTranslator
    {
        public StudentAnswerBase Yws_Answer_Obj
        {
            set; get;
        }
        public void ResetJsonValue()
        {
            if (Yws_Answer_Obj == null)
            {
                Yws_StudentAnswer = "";
            }
            else
            {
                Yws_StudentAnswer = JsonConvert.SerializeObject(Yws_Answer_Obj);
            }
        }
    }
}
