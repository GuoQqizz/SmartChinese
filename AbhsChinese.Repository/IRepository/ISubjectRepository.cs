using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request.Subject;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;

namespace AbhsChinese.Repository.IRepository
{
    public interface ISubjectRepository : IRepositoryBase<Yw_Subject>
    {
        Yw_Subject Get(int id);

        List<Yw_Subject> GetByIds(List<int> ids);

        IList<Yw_Subject> GetByPage(DtoQuestionSearch search);

        Yw_Subject Insert(Yw_Subject entity);

        void Update(Yw_Subject entity);

        IEnumerable<Yw_Subject> GetPagingSubjectForResourceGroup(PagingObject paging, int grade, int subjectType, int subjectId);

        List<DtoSubjectKnowledge> GetTaskSubjectsAutoForKnowledges(int lessonId, List<int> knowledgeIds);

        List<Yw_Subject> GetSubjectForPractice(int studentId, int courseId, int lessonIndex, int returnCount);

        List<DtoMediaResourceToCourse> GetSubjectToCourse(PagingObject paging, int courseId, int resourceType, string nameOrkey);
        List<DtoMediaResourceToCourse> GetSubjectToCourseById(PagingObject paging, int subjectType, int courseId, int resourceId);

        List<DtoLesTaskSubject> GetLessonTaskSubject(int lessonId, List<Tuple<int, int>> errorSubjects);
        IList<Yw_Subject> GetSubjectsById(DtoQuestionSearch search);
        IList<Yw_Subject> GetSubjectsByIds(IEnumerable<int> ids);

        List<DtoResourceGroupItem> GetSubjectForGroupItem(PagingObject paging, List<int> ids);

        List<Yw_Subject> GetSubjectRelationBySubjectId(int subjectId);
    }
}