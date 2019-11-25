using AbhsChinese.Domain.Entity.Subject;
using System.Collections.Generic;

namespace AbhsChinese.Domain.JsonEntity.Subject
{
    public class SubjectSelectContentObj
    {
        public int ContentType { get; set; }

        /// <summary>
        /// 选项和干扰项
        /// </summary>
        public IList<SubjectOption> Options { set; get; }

        public string Stem { set; get; }

        public int StemType { get; set; }
        public int Random { get; set; }
        public int Display { get; set; }
    }
}