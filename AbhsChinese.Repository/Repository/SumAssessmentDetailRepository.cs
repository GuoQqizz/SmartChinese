using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.RepositoryBase;
using Business;
using System;
using System.Collections.Generic;

namespace AbhsChinese.Repository.Repository
{
    public class SumAssessmentDetailRepository : RepositoryBase<sum_AssessmentDetail>, ISumAssessmentDetailRepository
    {
        public SumAssessmentDetailRepository() : base("")
        {
        }

        public List<sum_AssessmentDetail> GetAssessments(int id)
        {
            return null;//dbcontext.sum_AssessmentDetails.Where(x => x.sad_StudentID == id).ToList();
        }
    }
}
