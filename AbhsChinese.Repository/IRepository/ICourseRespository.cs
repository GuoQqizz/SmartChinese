using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System.Collections.Generic;

namespace AbhsChinese.Repository.IRepository
{
    public interface ICourseRespository : IRepositoryBase<Yw_Course>
    {
        IList<DtoCourseListItem> GetByPage(DtoCurriculumSearch search);

        Yw_Course GetCourse(int id);

        List<Yw_Course> GetCourseByIdOrName(string idOrName);

        void InsertCourse(Yw_Course entity);

        /// <summary>
        /// 选课中心课程列表，包含课程价格
        /// </summary>
        List<DtoSelectCenterCourseObject> GetCourseWithPrice(DtoCourseSelectCondition condition, PagingObject paging);

        List<Yw_Course> GetCourseByTypeAndGrade(int type, int grade);

        DtoSelectCenterCourseDetailObject GetCourseDetailWithPrice(int courseId, int schoolLevelId, bool isPreview = false);
        DtoSelectCenterCourseDetailObject GetCourseDetailWithoutPrice(int courseId, bool isPreview = false);
        
        /// <summary>
        /// 选课中心课程列表，不包含课程价格
        /// </summary>
        List<DtoSelectCenterCourseObject> GetCourseWithoutPrice(DtoCourseSelectCondition condition, PagingObject paging);

        List<Yw_Course> GetPagingCourseByIdOrName(PagingObject paging,string idOrName);
        void Update(Yw_Course entity);
    }
}