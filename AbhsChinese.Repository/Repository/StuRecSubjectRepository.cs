using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class StuRecSubjectRepository : RepositoryBase<Yw_StudentRecentSubject>, IStuRecSubjectRepository
    {
        public StuRecSubjectRepository() : base(AppSetting.ConnString) { }

        public List<Yw_StudentRecentSubject> GetByStudent(int studentId)
        {
            string sql = "select * from Yw_StudentRecentSubject where Yrs_StudentId=@StudentId order by Yrs_CreateTime";
            return Query(sql, new { StudentId = studentId }).ToList();
        }

        public void Add(Yw_StudentRecentSubject subject)
        {
            InsertEntity(subject);
        }

        public void AddBatch(List<DtoSimpleStuRecentSub> objs)
        {
            if (objs != null && objs.Count > 0)
            {
                string sql = "insert into Yw_StudentRecentSubject(Yrs_StudentId,Yrs_SubjectId,Yrs_IsLastest,Yrs_CreateTime) select item1,item2,1,getdate() from Fn_To2ColumnTable(@Items)";
                string items = string.Join(";", objs.Select(x => x.Row));
                Execute(sql, new { Items = items });
            }
        }

        public void DeleteByStudent(int studentId,List<int> subjectIds)
        {
            string sql = "delete from Yw_StudentRecentSubject where Yrs_StudentId=@StudentId and Yrs_SubjectId in @Ids";
            Execute(sql, new { StudentId = studentId, Ids = subjectIds.ToArray() });
        }

        public void UpdateToLastestforStudent(int studentId, List<int> subjectIds)
        {
            string sql = @"update Yw_StudentRecentSubject set yrs_IsLastest=0 where Yrs_StudentId=@StudentId and yrs_IsLastest=1 and Yrs_SubjectId not in @Ids;
                           update Yw_StudentRecentSubject set yrs_IsLastest=1,Yrs_CreateTime=getdate() where Yrs_StudentId=@StudentId and Yrs_SubjectId in @Ids;";
            Execute(sql, new { StudentId = studentId, Ids = subjectIds.ToArray() });
        }
    }
}