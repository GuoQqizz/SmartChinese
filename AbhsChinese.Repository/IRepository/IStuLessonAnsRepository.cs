using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IStuLessonAnsRepository : IRepositoryBase<Yw_StudentLessonAnswer>
    {
        Yw_StudentLessonAnswer GetByProgressId(int lessonProgressId);

        /// <summary>
        /// 添加课时答题结果
        /// </summary>
        /// <param name="lessonAnswer"></param>
        void Insert(Yw_StudentLessonAnswer lessonAnswer);

        /// <summary>
        /// 修改课时答题结果
        /// </summary>
        /// <param name="lessonAnswer"></param>
        void Update(Yw_StudentLessonAnswer lessonAnswer);
        /// <summary>
        /// 根据id修改课时答案结果
        /// </summary>
        /// <param name="id"></param>
        /// <param name="answer"></param>
        /// <param name="coins"></param>
        void Update(int id, string answer, string coins);
        Yw_StudentLessonAnswer GetStuLesAnswer(int studentId, int lessonProgressId);
    }
}
