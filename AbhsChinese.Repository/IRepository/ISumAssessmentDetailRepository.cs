using AbhsChinese.Repository.RepositoryBase;
using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface ISumAssessmentDetailRepository : IRepositoryBase<sum_AssessmentDetail>
    {
        List<sum_AssessmentDetail> GetAssessments(int id);
    }
}
