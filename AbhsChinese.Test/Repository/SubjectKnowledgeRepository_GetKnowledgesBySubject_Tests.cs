using AbhsChinese.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AbhsChinese.Test.Repository
{
    [TestClass]
    public class SubjectKnowledgeRepository_GetKnowledgesBySubject_Tests
    {
        [TestMethod]
        public void SubjectKnowledgeRepository_GetKnowledgesBySubject_ShouldSuccess()
        {
            SubjectKnowledgeRepository repository = new SubjectKnowledgeRepository();
            var result = repository.GetKnowledgesBySubject(10006);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 2);
        }
    }
}