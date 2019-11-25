using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace AbhsChinese.Bll
{
    public class KnowledgeBll : BllBase
    {
        public KnowledgeBll() : base()
        {

        }

        private IKnowledgeRepository knowledgeRepository;
        public IKnowledgeRepository KnowledgeRepository
        {
            get
            {
                if (knowledgeRepository == null)
                {
                    knowledgeRepository = new KnowledgeRepository();
                }

                return knowledgeRepository;
            }
        }

        private IKnowledgeIndexRepository knowledgeIndexRepository;
        public IKnowledgeIndexRepository KnowledgeIndexRepository
        {
            get
            {
                if (knowledgeIndexRepository == null)
                {
                    knowledgeIndexRepository = new KnowledgeIndexRepository();
                }

                return knowledgeIndexRepository;
            }
        }

        public void Add(DtoKnowledgeRequest request)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var entity = request.ConvertTo<Yw_Knowledge>();
                    entity.Ykl_CreateTime = DateTime.Now;
                    entity.Ykl_UpdateTime = DateTime.Now;

                    if (entity.Ykl_Level == (int)KnowledgeEnum.一级知识点)
                    {
                        int knowledgeId = KnowledgeRepository.Add(entity);
                        var knowledge = KnowledgeRepository.Get(knowledgeId);
                        knowledge.Ykl_Path = "/" + knowledgeId;
                        KnowledgeRepository.Update(knowledge);
                    }
                    else
                    {
                        int knowledgeId = KnowledgeRepository.Add(entity);
                        //获取父级节点
                        var parent = KnowledgeRepository.Get(entity.Ykl_ParentId);
                        //获取当前节点
                        var knowledge = KnowledgeRepository.Get(knowledgeId);
                        knowledge.Ykl_Path = parent.Ykl_Path + "/" + knowledgeId;
                        KnowledgeRepository.Update(knowledge);

                        parent.Ykl_IsLeaf = true;
                        KnowledgeRepository.Update(parent);
                    }

                    var words = GetWords(entity.Ykl_Name, entity.Ykl_Keywords);

                    AddKnowledgeIndex(words, entity.Ykl_Id, entity.Ykl_Creator, entity.Ykl_Level);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw ex;
                }
            }
        }

        public DtoKnowledge GetKnowledge(int id)
        {
            return KnowledgeRepository.GetKnowledge(id);
        }

        public Yw_Knowledge Get(int id)
        {
            return KnowledgeRepository.Get(id);
        }

        public List<Yw_Knowledge> GetPagingKnowledge(PagingObject paging, int id, string nameOrkey, int level)
        {
            List<Yw_Knowledge> knowledgeList = new List<Yw_Knowledge>();
            if (id > 0)
            {
                knowledgeList = KnowledgeRepository.GetPagingKnowledge(paging, id, level);
            }
            else
            {
                if (!string.IsNullOrEmpty(nameOrkey))
                {
                    var knowledgeIds = KnowledgeIndexRepository.GetPagingKnowledgeIndexIds(paging, level, nameOrkey);
                    Dictionary<int, int> orderDic = knowledgeIds.ToOrderDic();
                    knowledgeList = KnowledgeRepository.GetKnowledgeByIds(knowledgeIds);
                    knowledgeList = knowledgeList.OrderBy(x => orderDic[x.Ykl_Id]).ToList();
                }
                else
                {
                    knowledgeList = KnowledgeRepository.GetPagingKnowledge(paging, id, level);
                }
            }
            return knowledgeList;
        }

        /// <summary>
        /// 录入题目时，获取知识点
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="id"></param>
        /// <param name="ids"></param>
        /// <param name="kernelKownledagePointId"></param>
        /// <param name="nameOrkey"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public IList<Yw_Knowledge> GetKnowledgeForSubject(
            DtoKnowledgeSearch search)
        {
            if (search.Ids != null && search.Ids.Count > 0)
            {
                return KnowledgeRepository.GetKnowledgeByIds(search);
            }
            else if (search.Keyword.HasValue())
            {
                var knowledgeIds = KnowledgeIndexRepository.GetPagingKnowledgeIndexIds(
                    search.Pagination,
                    (int)search.Level.Value,
                    search.Keyword,
                    search.ParentId);
                Dictionary<int, int> orderDic = knowledgeIds.ToOrderDic();
                var knowledgeList = KnowledgeRepository.GetKnowledgeByIds(knowledgeIds);
                knowledgeList = knowledgeList.OrderBy(x => orderDic[x.Ykl_Id]).ToList();
                return knowledgeList;
            }
            else
            {
                return KnowledgeRepository.GetPagingKnowledge(
                    search.Pagination,
                    0,
                    (int)search.Level.Value);
            }
        }

        public void Update(DtoKnowledgeRequest entity)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    //查询当前知识点
                    var knowledge = KnowledgeRepository.Get(entity.Ykl_Id);
                    //查询新的父知识点
                    var parent = knowledgeRepository.Get(entity.Ykl_ParentId);
                    //如果知识点等级大于1，则不是顶级节点，修改其子节点的所有path
                    if (knowledge.Ykl_Level != (int)KnowledgeEnum.一级知识点)
                    {
                        var oldPath = knowledge.Ykl_Path;
                        var newPath = parent.Ykl_Path + "/" + entity.Ykl_Id;

                        for (int i = 0; i < 100; i++)
                        {
                            if (oldPath == newPath)
                            {
                                break;
                            }
                            int result = knowledgeRepository.UpdatePath(oldPath, newPath);
                            if (result > 0)
                            {
                                Thread.Sleep(100);
                                continue;
                            }
                            break;
                        }
                    }
                    //旧的父知识点Id
                    var oldParent = knowledgeRepository.Get(knowledge.Ykl_ParentId);

                    knowledge.Ykl_Name = entity.Ykl_Name;
                    knowledge.Ykl_Keywords = entity.Ykl_Keywords;
                    knowledge.Ykl_ParentId = entity.Ykl_ParentId;
                    if (entity.Ykl_ResourceId > 0)
                    {
                        knowledge.Ykl_ResourceId = entity.Ykl_ResourceId;
                    }
                    knowledge.Ykl_UpdateTime = DateTime.Now;
                    knowledge.Ykl_Editor = entity.Ykl_Editor;
                    KnowledgeRepository.Update(knowledge);

                    if (oldParent != null && knowledge.Ykl_Level != (int)KnowledgeEnum.四级知识点)
                    {
                        //判断旧的父节点是否还存在其他子节点，如不存在则修改IsLeaf
                        var oldLeaf = KnowledgeRepository.IsLeaf(oldParent.Ykl_Id);
                        if (!oldLeaf)
                        {
                            oldParent.Ykl_IsLeaf = false;
                            KnowledgeRepository.Update(oldParent);
                        }

                        //判断新的父节点IsLeaf是否为true，不为true修改
                        if (!parent.Ykl_IsLeaf)
                        {
                            parent.Ykl_IsLeaf = true;
                            KnowledgeRepository.Update(parent);
                        }
                    }
                    var words = GetWords(entity.Ykl_Name, entity.Ykl_Keywords);
                    UpdateKnowledgeIndex(knowledge.Ykl_Id, words, entity.Ykl_Creator);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw ex;
                }
            }
        }

        public void AddKnowledgeIndex(List<string> words, int knowledgeId, int creator, int level)
        {
            foreach (var word in words)
            {
                Yw_KnowledgeIndex knowledgeIndex = new Yw_KnowledgeIndex()
                {
                    Yki_Keyword = word,
                    Yki_Level = level,
                    Yki_KnowledgeId = knowledgeId,
                    Yki_CreateTime = DateTime.Now,
                    Yki_Creator = creator
                };
                KnowledgeIndexRepository.Add(knowledgeIndex);
            }
        }

        public void UpdateKnowledgeIndex(int knowledgeId, List<string> words, int creator)
        {
            var knowledgeIndexs = KnowledgeIndexRepository.GetKnowledgeIndexs(knowledgeId);
            var keywords = knowledgeIndexs.Select(s => s.Yki_Keyword);
            var enumerable = keywords as string[] ?? keywords.ToArray();

            //删除数据库中存在的部分数据
            var knowledgeDel = enumerable.Except(words).ToList();
            var ids = new List<int>();
            foreach (var delWord in knowledgeDel)
            {
                ids = knowledgeIndexs.Where(x => x.Yki_Keyword == delWord).Select(s => s.Yki_Id).ToList();
            }
            if (ids.Count > 0)
            {
                KnowledgeIndexRepository.Delete(ids);
            }


            //添加新的部分数据
            var knowledgeAdd = words.Except(enumerable).ToList();
            foreach (var addWord in knowledgeAdd)
            {
                Yw_KnowledgeIndex knowledge = new Yw_KnowledgeIndex()
                {
                    Yki_Keyword = addWord,
                    Yki_KnowledgeId = knowledgeId,
                    Yki_CreateTime = DateTime.Now,
                    Yki_Creator = creator
                };
                KnowledgeIndexRepository.Add(knowledge);
            }
        }

        public List<string> GetWords(string name, string keyword)
        {
            var words = WordSplitHelper.GetSplitWordList(name.Replace("，", ","), JiebaTypeEnum.CutAll).ToList();
            foreach (var key in keyword.Replace("，", ",").Split(','))
            {
                if (!words.Contains(key))
                {
                    words.Add(key);
                }
            }
            return words;
        }

        public List<Yw_Knowledge> GetKnowledgeTree(int parentId, int currentId)
        {
            var knowledgeTree = new List<Yw_Knowledge>();
            if (parentId == 0)
            {
                knowledgeTree = KnowledgeRepository.GetKnowledgeTreeByParent(parentId);
                var childNode = KnowledgeRepository.GetKnowledgeTreeByParent(currentId);
                knowledgeTree.AddRange(childNode);
                return knowledgeTree.Distinct().ToList();
            }
            else
            {
                //向下获取一个子节点
                knowledgeTree = KnowledgeRepository.GetKnowledgeTreeByParent(currentId);

                var ss = KnowledgeRepository.GetKnowledgeTreeByChild(currentId);
                var ids = ss.Select(s => s.Ykl_Id).ToList();
                knowledgeTree.AddRange(KnowledgeRepository.GetKnowledgeTreeByIds(ids));
                return knowledgeTree.ToList();
            }
        }

        public List<Yw_Knowledge> GetLazyTree(int parentId)
        {
            return KnowledgeRepository.GetKnowledgeTreeByParent(parentId);
        }

        public List<Yw_Knowledge> GetKnowledgeParentNode(PagingObject paging, int level, string idOrName)
        {
            List<Yw_Knowledge> knowledgeList = new List<Yw_Knowledge>();
            if (!string.IsNullOrEmpty(idOrName))
            {
                if (idOrName.IsNumberic() && idOrName.Length >= 5)
                {
                    knowledgeList = KnowledgeRepository.GetPagingKnowledge(paging, idOrName._ToInt32(), level);
                }
                else
                {
                    var ids = KnowledgeIndexRepository.GetPagingKnowledgeIndexIds(paging, level, idOrName);
                    Dictionary<int, int> orderDic = ids.ToOrderDic();
                    knowledgeList = KnowledgeRepository.GetKnowledgeByIds(ids).ToList();
                    knowledgeList = knowledgeList.OrderBy(x => orderDic[x.Ykl_Id]).ToList();
                }
            }
            else
            {
                knowledgeList = KnowledgeRepository.GetPagingKnowledge(paging, idOrName._ToInt32(), level);
            }
            return knowledgeList;
        }

        public DtoMediaForKnowledge GetMediaByKnowledgeId(int knowledgeId)
        {
            ResourceBll resourceBll = new ResourceBll();
            return resourceBll.MediaObjectRepository.GetMediaByKnowledgeId(knowledgeId);
        }
    }
}
