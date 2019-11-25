using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AbhsChinese.Bll
{
    public class SubjectGroupBll : BllBase
    {
        private SubjectGroupRepository subjectGroupRepository;
        public ISubjectGroupRepository SubjectGroupRepository
        {
            get
            {
                if (subjectGroupRepository == null)
                {
                    subjectGroupRepository = new SubjectGroupRepository();
                }

                subjectGroupRepository.TranId = tranId;
                return subjectGroupRepository;
            }
        }

        public Yw_SubjectGroup GetBySubjectId(int subjectId)
        {
            return SubjectGroupRepository.GetBySubjectId(subjectId);
        }

        public IList<Yw_SubjectGroup> GetBySubjectIds(IEnumerable<int> ids)
        {

            IList<Yw_SubjectGroup> subjectGroups =
                SubjectGroupRepository.GetBySubjectIds(ids);
            return subjectGroups;
        }

        public void CancelRelation(int subjectId, int relationSubjectId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var subjectBll = new SubjectBll();
                    var subjects = subjectBll.GetSubjectsByIds(
                        new List<int> { subjectId, relationSubjectId });
                    foreach (var subject in subjects)
                    {
                        subject.EnableAudit();
                        subject.Ysj_GroupItemCount = subject.Ysj_GroupItemCount - 1;
                        subjectBll.UpdateSubject(subject);
                    }

                    int[] ids = new int[2] { subjectId, relationSubjectId };
                    IList<Yw_SubjectGroup> subjectGroups = GetBySubjectIds(ids);
                    if (subjectGroups.Count != 2)
                    {
                        throw new AbhsException(
                            ErrorCodeEnum.RelationOutOfRange,
                            AbhsErrorMsg.RelationOutOfRange);
                    }

                    foreach (var item in subjectGroups)
                    {
                        item.EnableAudit();
                        string pattern = string.Empty;

                        string relSubjectId = item.Ysg_RelSubjectId;
                        relSubjectId = "," + relSubjectId + ",";
                        if (item.Ysg_SubjectId == subjectId)
                        {
                            pattern = "," + relationSubjectId + ",";

                        }
                        else
                        {
                            pattern = "," + subjectId + ",";
                        }
                        relSubjectId = relSubjectId.Replace(pattern, ",").Trim(',');
                        item.Ysg_RelSubjectId = relSubjectId.Replace(pattern, ",");
                        SubjectGroupRepository.Update(item);
                    }

                    scope.Complete();
                }
                catch
                {
                    RollbackTran();
                    throw;
                }
            }
            new CourseSubjectRelBll().RemoveSubjectFromGroup(subjectId, relationSubjectId);

        }
        public void AddRelation(int subjectId, int relationSubjectId, int currentUser)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (subjectId == relationSubjectId)
                    {
                        throw new AbhsException(
                            ErrorCodeEnum.CanNotAddRelationToSelf,
                            AbhsErrorMsg.CanNotAddRelationToSelf);
                    }

                    var subjectBll = new SubjectBll();
                    var subjects = subjectBll.GetSubjectsByIds(
                        new List<int> { subjectId, relationSubjectId });
                    foreach (var subject in subjects)
                    {
                        subject.EnableAudit();
                        subject.Ysj_GroupItemCount = subject.Ysj_GroupItemCount + 1;
                        subjectBll.UpdateSubject(subject);
                    }

                    var subjectGroup = SubjectGroupRepository.GetBySubjectId(subjectId);
                    if (subjectGroup == null)
                    {
                        subjectGroup = new Yw_SubjectGroup
                        {
                            Ysg_SubjectId = subjectId,
                            Ysg_RelSubjectId = relationSubjectId.ToString(),
                            Ysg_CreateTime = Clock.Now,
                            Ysg_Creator = currentUser,
                            Ysg_UpdateTime = Clock.Now,
                            Ysg_Editor = currentUser
                        };
                        SubjectGroupRepository.Insert(subjectGroup);
                    }
                    else
                    {
                        if (subjectGroup.Ysg_RelSubjectId.Contains(relationSubjectId.ToString()))
                        {
                            throw new AbhsException(
                                ErrorCodeEnum.CanNotAddRelation,
                                AbhsErrorMsg.CanNotAddRelation);
                        }
                        subjectGroup.Ysg_RelSubjectId = subjectGroup.Ysg_RelSubjectId + ","
                            + relationSubjectId.ToString();
                        SubjectGroupRepository.Update(subjectGroup);
                    }

                    var subjectGroup2 = SubjectGroupRepository.GetBySubjectId(relationSubjectId);
                    if (subjectGroup2 == null)
                    {
                        subjectGroup2 = new Yw_SubjectGroup
                        {
                            Ysg_SubjectId = relationSubjectId,
                            Ysg_RelSubjectId = subjectId.ToString(),
                            Ysg_CreateTime = Clock.Now,
                            Ysg_Creator = currentUser,
                            Ysg_UpdateTime = Clock.Now,
                            Ysg_Editor = currentUser
                        };
                        SubjectGroupRepository.Insert(subjectGroup2);
                    }
                    else
                    {
                        subjectGroup2.Ysg_RelSubjectId = subjectGroup2.Ysg_RelSubjectId + ","
                            + subjectId.ToString();
                        SubjectGroupRepository.Update(subjectGroup2);
                    }

                    scope.Complete();
                }
                catch
                {
                    RollbackTran();
                    throw;
                }
            }
            new CourseSubjectRelBll().AddSubjectToGroup(subjectId, relationSubjectId);
        }
    }
}
