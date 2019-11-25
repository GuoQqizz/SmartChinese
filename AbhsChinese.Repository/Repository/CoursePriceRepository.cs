using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.Repository
{
    public class CoursePriceRepository : RepositoryBase<Yw_CoursePrice>, ICoursePriceRepository
    {
        public CoursePriceRepository() : base(AppSetting.ConnString)
        {
        }

        public Yw_CoursePrice GetPrice(int courseId, int schoolLevelId)
        {
            string sql = "select * from Yw_CoursePrice where Yce_CourseId=@CourseId and Yce_SchoolLevelId=@LevelId";
            return Query(sql, new { courseId = courseId, LevelId = schoolLevelId }).FirstOrDefault();
        }

        public IList<DtoCoursePriceListItem> GetEntities(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new List<DtoCoursePriceListItem>();
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT c.Yce_CourseId, c.Yce_Price, s.Bhl_Name AS Yce_SchoolLevelName, s.Bhl_Id AS Yce_SchoolLevelId");
            stringBuilder.Append(" FROM Yw_CoursePrice AS c");
            stringBuilder.Append(" INNER JOIN Bas_SchoolLevel AS s ON Bhl_Id = Yce_SchoolLevelId");
            stringBuilder.Append(" WHERE Yce_CourseId IN @ids");

            string orderbyParameters = string.Empty;
            string orderby = SqlBuilder.GenerateOrderBy(ids, "Yce_Id", out orderbyParameters);
            stringBuilder.Append(orderby);
            string sql = stringBuilder.ToString();
            return Query<DtoCoursePriceListItem>(sql, new
            {
                ids = ids,
                OrderbyParameters = orderbyParameters
            }).ToList();
        }

        public Yw_CoursePrice GetEntity(int courseId, int schoolLevel)
        {
            if (courseId != 0 && courseId < 10000)
            {
                throw new AbhsException(
                    ErrorCodeEnum.IdOutOfRange,
                    AbhsErrorMsg.IdOutOfRange);
            }
            if (courseId != 0 && courseId < 10000)
            {
                throw new AbhsException(
                    ErrorCodeEnum.IdOutOfRange,
                    AbhsErrorMsg.IdOutOfRange);
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT *");
            stringBuilder.Append(" FROM Yw_CoursePrice");
            stringBuilder.Append(" WHERE Yce_CourseId=@courseId AND Yce_SchoolLevelId=@schoolLevel");

            string sql = stringBuilder.ToString();
            return Query<Yw_CoursePrice>(sql, new
            {
                courseId = courseId,
                schoolLevel = schoolLevel
            }).FirstOrDefault();
        }

        public void Update(Yw_CoursePrice entity)
        {
            UpdateEntity(entity);
        }

        public void Insert(Yw_CoursePrice entity)
        {
            InsertEntity(entity);
        }
    }
}
