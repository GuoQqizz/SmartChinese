using AbhsChinese.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Entity.Subject;
using System.Configuration;

namespace AbhsChinese.Bll
{
    public class ResourceBll : BllBase
    {
        public ResourceBll() : base()
        {

        }

        private ITextResourceRepository textResourceRepository;
        public ITextResourceRepository TextResourceRepository
        {
            get
            {
                if (textResourceRepository == null)
                {
                    textResourceRepository = new TextResourceRepository();
                }

                return textResourceRepository;
            }
        }

        private ITextObjectRepository textObjectRepository;
        public ITextObjectRepository TextObjectRepository
        {
            get
            {
                if (textObjectRepository == null)
                {
                    textObjectRepository = new TextObjectRepository();
                }

                return textObjectRepository;
            }
        }

        private IMediaResourceRepository mediaResourceRepository;
        public IMediaResourceRepository MediaResourceRepository
        {
            get
            {
                if (mediaResourceRepository == null)
                {
                    mediaResourceRepository = new MediaResourceRepository();
                }

                return mediaResourceRepository;
            }
        }

        private IMediaObjectRepository mediaObjectRepository;
        public IMediaObjectRepository MediaObjectRepository
        {
            get
            {
                if (mediaObjectRepository == null)
                {
                    mediaObjectRepository = new MediaObjectRepository();
                }

                return mediaObjectRepository;
            }
        }

        private IResourceIndexRepository resourceIndexRepository;
        public IResourceIndexRepository ResourceIndexRepository
        {
            get
            {
                if (resourceIndexRepository == null)
                {
                    resourceIndexRepository = new ResourceIndexRepository();
                }

                return resourceIndexRepository;
            }
        }

        private IResourceGroupRepository resourceGroupRepository;
        public IResourceGroupRepository ResourceGroupRepository
        {
            get
            {
                if (resourceGroupRepository == null)
                {
                    resourceGroupRepository = new ResourceGroupRepository();
                }

                return resourceGroupRepository;
            }
        }

        private IResourceGroupItemRepository resourceGroupItemRepository;
        public IResourceGroupItemRepository ResourceGroupItemRepository
        {
            get
            {
                if (resourceGroupItemRepository == null)
                {
                    resourceGroupItemRepository = new ResourceGroupItemRepository();
                }

                return resourceGroupItemRepository;
            }
        }

        #region 文本资源
        public void AddTextResource(DtoResourceRequest resource)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var words = GetWords(resource.Name, resource.Keyword, resource.Grade);

                    var textObjectId = AddTextObject(resource.Content, resource.Status, resource.Creator);
                    if (textObjectId > 0)
                    {
                        Yw_TextResource textResource = new Yw_TextResource()
                        {
                            Ytr_Name = resource.Name,
                            Ytr_TextType = (int)resource.TextType,
                            Ytr_CreateTime = DateTime.Now,
                            Ytr_Grade = resource.Grade,
                            Ytr_Status = resource.Status,
                            Ytr_TextObjectId = textObjectId,
                            Ytr_Keywords = resource.Keyword,
                            Ytr_UpdateTime = DateTime.Now,
                            Ytr_Creator = resource.Creator,
                            Ytr_Editor = resource.Editor
                        };
                        TextResourceRepository.Add(textResource);

                        AddResourceIndex(resource.Name, resource.Grade, (int)resource.TextType, textResource.Ytr_Id, (int)ResourceTypeEnum.文本资源, words, resource.Creator);
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw ex;
                }
            }
        }

        public int AddTextObject(string content, int status, int creator)
        {
            Yw_TextObject textObject = new Yw_TextObject()
            {
                Yxo_Content = content,
                Yxo_Status = status,
                Yxo_Creator = creator,
                Yxo_CreateTime = DateTime.Now,
                Yxo_UpdateTime = DateTime.Now,
                Yxo_Editor = creator
            };
            return TextObjectRepository.Add(textObject);
        }

        public void UpdateTextResource(DtoResourceRequest resource)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var words = GetWords(resource.Name, resource.Keyword, resource.Grade);
                    Yw_TextResource textResource = TextResourceRepository.GetTextResource(resource.Id);
                    if (textResource != null)
                    {
                        textResource.Ytr_Name = resource.Name;
                        textResource.Ytr_TextType = (int)resource.TextType;
                        textResource.Ytr_Grade = resource.Grade;
                        textResource.Ytr_Status = resource.Status;
                        textResource.Ytr_Keywords = resource.Keyword;
                        textResource.Ytr_UpdateTime = DateTime.Now;
                        textResource.Ytr_Editor = resource.Editor;
                        TextResourceRepository.Update(textResource);


                        UpdateTextObject(textResource.Ytr_TextObjectId, resource.Content, resource.Status, resource.Editor);

                        UpdateResourceIndex(resource.Name, resource.Grade, (int)resource.TextType, (int)ResourceTypeEnum.文本资源, textResource.Ytr_Id, words, resource.Creator);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw ex;
                }
            }
        }

        public void UpdateTextObject(int textObjectId, string content, int status, int editor)
        {
            Yw_TextObject textObject = TextObjectRepository.GeTextObject(textObjectId);
            if (textObject != null)
            {
                textObject.Yxo_Content = content;
                textObject.Yxo_Status = status;
                textObject.Yxo_UpdateTime = DateTime.Now;
                textObject.Yxo_Editor = editor;
                TextObjectRepository.Update(textObject);
            }
        }

        public DtoTextResource GetDtoTextResource(int id)
        {
            return TextResourceRepository.GetDtoTextResource(id);
        }

        public List<Yw_TextResource> GetPagingTextResource(PagingObject paging, int id, string nameOrkey, int grade, int textType)
        {
            List<Yw_TextResource> textList = new List<Yw_TextResource>();
            if (id > 0)
            {
                textList = TextResourceRepository.GetPagingTextList(paging, id, grade, textType, true, 0);
            }
            else
            {
                if (!string.IsNullOrEmpty(nameOrkey))
                {
                    var textIds = ResourceIndexRepository.GetResourceIndexIds(paging, grade, 0, textType, nameOrkey, ResourceTypeEnum.文本资源);
                    Dictionary<int, int> orderDic = textIds.ToOrderDic();
                    textList = TextResourceRepository.GetTextListByIds(textIds);
                    textList = textList.OrderBy(x => orderDic[x.Ytr_Id]).ToList();
                }
                else
                {
                    textList = TextResourceRepository.GetPagingTextList(paging, id, grade, textType, false, 0);
                }
            }
            return textList;
        }
        #endregion

        #region 多媒体资源
        public void AddMediaResource(DtoResourceRequest resource)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    int textObjectId = 0;
                    if (!string.IsNullOrEmpty(resource.AudioContent))
                    {
                        textObjectId = AddTextObject(resource.AudioContent, resource.Status, resource.Creator);
                    }

                    var mediaObjectId = AddMediaObject((int)resource.MediaObjectType, resource.Url, resource.ImgId, textObjectId, resource.Description, resource.Creator);

                    Yw_MediaResource mediaResource = new Yw_MediaResource()
                    {
                        Ymr_Name = resource.Name,
                        Ymr_MediaType = (int)resource.MediaType,
                        Ymr_CreateTime = DateTime.Now,
                        Ymr_Grade = resource.Grade,
                        Ymr_Status = resource.Status,
                        Ymr_MediaObjectId = mediaObjectId,
                        Ymr_Keywords = resource.Keyword,
                        Ymr_UpdateTime = DateTime.Now,
                        Ymr_Creator = resource.Creator,
                        Ymr_Editor = resource.Editor
                    };
                    MediaResourceRepository.Add(mediaResource);

                    if (resource.MediaType != MediaResourceTypeEnum.小艾变 && resource.MediaType != MediaResourceTypeEnum.开场语)
                    {
                        var words = GetWords(resource.Name, resource.Keyword, resource.Grade);
                        AddResourceIndex(resource.Name, resource.Grade, (int)resource.ResourceType, mediaResource.Ymr_Id, (int)ResourceTypeEnum.多媒体资源, words, resource.Creator);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw ex;
                }
            }
        }

        public int AddMediaObject(int objectType, string url, int imageCoverId, int textObjectId, string description, int creator)
        {
            Yw_MediaObject mediaObject = new Yw_MediaObject()
            {
                Yme_MediaType = objectType,
                Yme_Url = url,
                Yme_ImageCoverId = imageCoverId,
                Yme_TextObjectId = textObjectId,
                Yme_Description = description,
                Yme_Status = 1,
                Yme_Creator = creator,
                Yme_CreateTime = DateTime.Now,
                Yme_Editor = creator,
                Yme_UpdateTime = DateTime.Now
            };
            return MediaObjectRepository.Add(mediaObject);
        }

        public void UpdateMediaResource(DtoResourceRequest resource)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var words = GetWords(resource.Name, resource.Keyword, resource.Grade);
                    Yw_MediaResource mediaResource = MediaResourceRepository.GetMediaResource(resource.Id);
                    if (mediaResource != null)
                    {
                        mediaResource.Ymr_Name = resource.Name;
                        mediaResource.Ymr_Grade = resource.Grade;
                        mediaResource.Ymr_Status = resource.Status;
                        mediaResource.Ymr_Keywords = resource.Keyword;
                        mediaResource.Ymr_UpdateTime = DateTime.Now;
                        mediaResource.Ymr_Editor = resource.Editor;
                        MediaResourceRepository.Update(mediaResource);
                    }

                    Yw_MediaObject mediaObject = MediaObjectRepository.GetMediaObject(mediaResource.Ymr_MediaObjectId);
                    if (mediaObject != null)
                    {
                        mediaObject.Yme_Url = resource.Url;
                        mediaObject.Yme_ImageCoverId = resource.ImgId;
                        mediaObject.Yme_Description = resource.Description;
                        mediaObject.Yme_Editor = resource.Editor;
                        mediaObject.Yme_UpdateTime = DateTime.Now;
                        if (!string.IsNullOrEmpty(resource.AudioContent))
                        {
                            if (mediaObject.Yme_TextObjectId > 0)
                            {
                                Yw_TextObject textObject = TextObjectRepository.GeTextObject(mediaObject.Yme_TextObjectId);
                                textObject.Yxo_Content = resource.AudioContent;
                                textObject.Yxo_Editor = resource.Editor;
                                textObject.Yxo_UpdateTime = DateTime.Now;
                                TextObjectRepository.Update(textObject);
                            }
                            else
                            {
                                Yw_TextObject textObject = new Yw_TextObject()
                                {
                                    Yxo_Content = resource.AudioContent,
                                    Yxo_Status = 1,
                                    Yxo_CreateTime = DateTime.Now,
                                    Yxo_Creator = resource.Creator,
                                    Yxo_Editor = resource.Editor,
                                    Yxo_UpdateTime = DateTime.Now
                                };
                                int textObjectId = TextObjectRepository.Add(textObject);
                                mediaObject.Yme_TextObjectId = textObjectId;
                            }
                        }
                        MediaObjectRepository.Update(mediaObject);
                    }


                    UpdateResourceIndex(resource.Name, resource.Grade, (int)resource.MediaType, (int)ResourceTypeEnum.多媒体资源, mediaResource.Ymr_Id, words, resource.Creator);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw ex;
                }
            }
        }
        

        public DtoMediaResourceAndObject GetMediaResource(int id)
        {
            return MediaObjectRepository.GetMedia(id);
        }

        public DtoMediaResourceToCourse GetMediaResourceGroup(int id)
        {
            return MediaObjectRepository.GetMediaDetailToCourse(id);
        }

        public DtoAudioResource GetAudioMedia(int id)
        {
            return MediaResourceRepository.GetAudioMedia(id);
        }

        public List<Yw_MediaResource> GetPagingMediaResource(PagingObject paging, int id, string nameOrkey, int grade, int mediaType)
        {
            List<Yw_MediaResource> mediaList = new List<Yw_MediaResource>();
            if (id > 0)
            {
                mediaList = MediaResourceRepository.GetPagingMediaList(paging, id, grade, mediaType, true, 0);
            }
            else
            {
                if (!string.IsNullOrEmpty(nameOrkey))
                {
                    var mediaIds = ResourceIndexRepository.GetResourceIndexIds(paging, grade, mediaType, 0, nameOrkey, ResourceTypeEnum.多媒体资源);
                    Dictionary<int, int> orderDic = mediaIds.ToOrderDic();
                    mediaList = MediaResourceRepository.GetMediaListByIds(mediaIds);
                    mediaList = mediaList.OrderBy(x => orderDic[x.Ymr_Id]).ToList();
                }
                else
                {
                    mediaList = MediaResourceRepository.GetPagingMediaList(paging, id, grade, mediaType, false, 0);
                }
            }
            return mediaList;
        }

        public List<Yw_MediaResource> GetAudioAndVideo(PagingObject paging, string nameOrKey)
        {
            if (string.IsNullOrEmpty(nameOrKey))
            {
                return new List<Yw_MediaResource>();
            }
            return MediaResourceRepository.GetAudioAndVideo(paging, nameOrKey);
        }

        public List<DtoMediaResourceAndObject> GetXiaoAiBianOrPrologue(PagingObject paging, int mediaType)
        {
            return MediaResourceRepository.GetXiaoAiBianOrPrologue(paging, mediaType);
        }

        /// <summary>
        /// 音频关联图片
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="grade"></param>
        /// <param name="mediaType"></param>
        /// <param name="nameOrKey"></param>
        /// <returns></returns>
        public List<Yw_MediaResource> GetImgList(PagingObject paging, int grade, string nameOrKey)
        {
            List<Yw_MediaResource> mediaList = new List<Yw_MediaResource>();
            if (!string.IsNullOrEmpty(nameOrKey))
            {
                if (nameOrKey.IsNumberic() && nameOrKey.Length >= 5)
                {
                    mediaList = MediaObjectRepository.GetPagingImgList(paging, grade,nameOrKey._ToInt32());
                }
                else
                {
                    var resourceIds = ResourceIndexRepository.GetResourceIndexIds(paging, grade, (int)MediaResourceTypeEnum.图片, 0, nameOrKey, ResourceTypeEnum.多媒体资源);
                    Dictionary<int, int> orderDic = resourceIds.ToOrderDic();
                    mediaList = MediaResourceRepository.GetMediaListByIds(resourceIds).ToList();
                    mediaList = mediaList.OrderBy(x => orderDic[x.Ymr_Id]).ToList();
                }
            }
            else
            {
                mediaList = MediaObjectRepository.GetPagingImgList(paging, grade, nameOrKey._ToInt32());
            }
            return mediaList;
        }
        #endregion

        #region 资源索引
        public void AddResourceIndex(string name, int grade, int resourceType, int resourceId, int category, List<string> words, int creator)
        {
            List<Yw_ResourceIndex> resourceIndices = new List<Yw_ResourceIndex>();
            foreach (var word in words)
            {
                Yw_ResourceIndex resourceIndex = new Yw_ResourceIndex()
                {
                    Yrx_Keyword = word,
                    Yrx_Grade = grade,
                    Yrx_ResourceType = resourceType,
                    Yrx_ResourceId = resourceId,
                    Yrx_ResourceCategory = category,
                    Yrx_CreateTime = DateTime.Now,
                    Yrx_Creator = creator
                };
                ResourceIndexRepository.Add(resourceIndex);
            }
        }

        public void UpdateResourceIndex(string name, int grade, int resourceType, int resourceCategory, int resourceId, List<string> words, int creator)
        {
            var resourceIndexs = ResourceIndexRepository.GetResourceIndices(resourceId, resourceCategory);
            if (resourceIndexs != null && resourceIndexs.Count > 0)
            {
                var keywords = resourceIndexs.Select(s => s.Yrx_Keyword);
                var enumerable = keywords as string[] ?? keywords.ToArray();

                //删除数据库中存在的部分数据
                var resourceDel = enumerable.Except(words).ToList();
                var ids = new List<int>();
                foreach (var delWord in resourceDel)
                {
                    ids = resourceIndexs.Where(x => x.Yrx_Keyword == delWord).Select(s => s.Yrx_Id).ToList();
                }
                if (ids.Count > 0)
                {
                    ResourceIndexRepository.Delete(ids.ToArray());
                }

                //添加新的部分数据
                var resourceAdd = words.Except(enumerable).ToList();
                foreach (var addWord in resourceAdd)
                {
                    Yw_ResourceIndex resourceIndex = new Yw_ResourceIndex()
                    {
                        Yrx_Keyword = addWord,
                        Yrx_Grade = grade,
                        Yrx_ResourceType = resourceType,
                        Yrx_ResourceId = resourceId,
                        Yrx_ResourceCategory = resourceCategory,
                        Yrx_CreateTime = DateTime.Now,
                        Yrx_Creator = creator
                    };
                    ResourceIndexRepository.Add(resourceIndex);
                }

                ResourceIndexRepository.UpdateByIds(resourceIndexs.Select(s => s.Yrx_Id).ToList(), resourceType, grade);
            }
            else
            {
                foreach (var word in words)
                {
                    Yw_ResourceIndex resourceIndex = new Yw_ResourceIndex()
                    {
                        Yrx_Keyword = word,
                        Yrx_Grade = grade,
                        Yrx_ResourceType = (int)resourceType,
                        Yrx_ResourceId = resourceId,
                        Yrx_ResourceCategory = resourceCategory,
                        Yrx_CreateTime = DateTime.Now,
                        Yrx_Creator = creator
                    };
                    ResourceIndexRepository.Add(resourceIndex);
                }
            }
        }

        public List<string> GetWords(string name, string keyword, int grade)
        {
            var words = WordSplitHelper.GetSplitWordList(name.Replace("，", ","), JiebaTypeEnum.CutAll).ToList();
            foreach (var key in keyword.Replace("，", ",").Split(','))
            {
                if (!words.Contains(key))
                {
                    words.Add(key);
                }
            }
            if (!words.Contains(CustomEnumHelper.Parse(typeof(GradeEnum), grade)))
            {
                words.Add(CustomEnumHelper.Parse(typeof(GradeEnum), grade));
            }
            if (!words.Contains(name))
            {
                words.Add(name);
            }
            return words;
        }
        #endregion

        #region 资源组
        public int AddResourceGroup(Yw_ResourceGroup resourceGroup)
        {
            return ResourceGroupRepository.AddResourceGroup(resourceGroup);
        }

        public List<Yw_ResourceGroup> GetPagingResourceGroup(PagingObject paging, int id, string name, int grade, int status)
        {
            return ResourceGroupRepository.GetPagingResourceGroup(paging, id, name, grade, status);
        }

        public Yw_ResourceGroup GetResourceGroup(int id)
        {
            return ResourceGroupRepository.GetResourceGroup(id);
        }

        public string GetResourceGroupName(int id)
        {
            return GetResourceGroup(id)?.Yrg_Name;
        }

        public List<DtoResourceGroupItem> GetResourceGroupItem(PagingObject paging, int groupid, int resouceType)
        {
            List<DtoResourceGroupItem> groupItems = new List<DtoResourceGroupItem>();
            var resourceIds = ResourceGroupItemRepository.GetResourceIdByTypeAndGroupId(groupid, resouceType).Select(s => s.Ygi_ResourceId).ToList();
            if (resouceType == (int)ResourceTypeEnum.多媒体资源)
            {
                groupItems = MediaResourceRepository.GetMediaForGroupItem(paging, resourceIds);
            }
            else if (resouceType == (int)ResourceTypeEnum.文本资源)
            {
                groupItems = TextResourceRepository.GetTextForGroupItem(paging, resourceIds);
            }
            else if (resouceType == (int)ResourceTypeEnum.题目)
            {
                SubjectBll subjectBll = new SubjectBll();
                groupItems = subjectBll.SubjectService.GetSubjectForGroupItem(paging, resourceIds);
            }
            return groupItems;
        }

        /// <summary>
        /// 资源组关联多媒体列表
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="grade"></param>
        /// <param name="mediaType"></param>
        /// <param name="nameOrKey"></param>
        /// <returns></returns>
        public List<Yw_MediaResource> GetMediaList(PagingObject paging, int grade, int mediaType, string nameOrKey)
        {
            List<Yw_MediaResource> mediaList = new List<Yw_MediaResource>();
            if (!string.IsNullOrEmpty(nameOrKey))
            {
                if (nameOrKey.IsNumberic() && nameOrKey.Length >= 5)
                {
                    mediaList = MediaResourceRepository.GetMediaListForGroup(paging, nameOrKey._ToInt32(), grade, mediaType);
                }
                else
                {
                    var resourceIds = ResourceIndexRepository.GetMediaIndexIds(paging, grade, mediaType,nameOrKey);
                    Dictionary<int, int> orderDic = resourceIds.ToOrderDic();
                    mediaList = MediaResourceRepository.GetMediaListByIds(resourceIds).ToList();
                    mediaList = mediaList.OrderBy(x => orderDic[x.Ymr_Id]).ToList();
                }
            }
            else
            {
                mediaList = MediaResourceRepository.GetMediaListForGroup(paging, nameOrKey._ToInt32(), grade, mediaType);
            }
            return mediaList;
        }

        /// <summary>
        /// 资源组关联文本列表
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="grade"></param>
        /// <param name="mediaType"></param>
        /// <param name="nameOrKey"></param>
        /// <returns></returns>
        public List<Yw_TextResource> GetTextList(PagingObject paging, int grade, int textType, string nameOrKey)
        {
            List<Yw_TextResource> textList = new List<Yw_TextResource>();
            if (!string.IsNullOrEmpty(nameOrKey))
            {
                if (nameOrKey.IsNumberic() && nameOrKey.Length >= 5)
                {
                    textList = TextResourceRepository.GetPagingTextList(paging, nameOrKey._ToInt32(), grade, textType, false, 1);
                }
                else
                {
                    var resourceIds = ResourceIndexRepository.GetResourceIndexIds(paging, grade, 0, textType, nameOrKey, ResourceTypeEnum.文本资源);
                    Dictionary<int, int> orderDic = resourceIds.ToOrderDic();
                    textList = TextResourceRepository.GetTextListByIds(resourceIds).ToList();
                    textList = textList.OrderBy(x => orderDic[x.Ytr_Id]).ToList();
                }
            }
            else
            {
                textList = TextResourceRepository.GetPagingTextList(paging, nameOrKey._ToInt32(), grade, textType, false, 1);
            }
            return textList;
        }

        /// <summary>
        /// 资源组关联题目列表
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="grade"></param>
        /// <param name="mediaType"></param>
        /// <param name="nameOrKey"></param>
        /// <returns></returns>
        public List<Yw_Subject> GetSubjectList(PagingObject paging, int grade, int subjectType, string nameOrKey)
        {
            List<Yw_Subject> subjectList = new List<Yw_Subject>();
            SubjectBll subjectBll = new SubjectBll();
            if (!string.IsNullOrEmpty(nameOrKey))
            {
                if (nameOrKey.IsNumberic() && nameOrKey.Length >= 5)
                {
                    subjectList = subjectBll.SubjectService.GetPagingSubjectForResourceGroup(paging, grade, subjectType, nameOrKey._ToInt32()).ToList();
                }
                else
                {
                    var resourceIds = subjectBll.SubjectIndexService.GetPagingSubjectIds(paging, grade, subjectType, nameOrKey).ToList();
                    //Dictionary<int, int> orderDic = resourceIds.ToOrderDic();
                    subjectList = subjectBll.SubjectService.GetSubjectsByIds(resourceIds).ToList();
                    //subjectList = subjectList.OrderBy(x => orderDic[x.Ysj_Id]).ToList();
                }
            }
            else
            {
                subjectList = subjectBll.SubjectService.GetPagingSubjectForResourceGroup(paging, grade, subjectType, nameOrKey._ToInt32()).ToList();
            }
            return subjectList;
        }
        
        public void AddResourceGroupItem(Yw_ResourceGroupItem entity)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var resourceGroupItem = ResourceGroupItemRepository.GetResourceGroupItem(entity.Ygi_GroupId, entity.Ygi_ResourceType, entity.Ygi_ResourceId);
                    if (resourceGroupItem != null)
                    {
                        throw new AbhsException(ErrorCodeEnum.AlreadyExistResourceItem, AbhsErrorMsg.ConstAlreadyExistResourceItem);
                    }

                    ResourceGroupItemRepository.AddResourceGroupItem(entity);

                    var resourceGroup = ResourceGroupRepository.GetResourceGroup(entity.Ygi_GroupId);

                    if (resourceGroup != null)
                    {
                        var resourceCount = ResourceGroupItemRepository.GetResourceCount(entity.Ygi_GroupId, entity.Ygi_ResourceType);
                        if (entity.Ygi_ResourceType == (int)ResourceTypeEnum.文本资源)
                        {
                            resourceGroup.Yrg_TextCount = resourceCount;
                        }
                        else if (entity.Ygi_ResourceType == (int)ResourceTypeEnum.多媒体资源)
                        {
                            resourceGroup.Yrg_MediaCount = resourceCount;
                        }
                        else if (entity.Ygi_ResourceType == (int)ResourceTypeEnum.题目)
                        {
                            resourceGroup.Yrg_SubjectCount = resourceCount;
                        }
                        ResourceGroupRepository.Update(resourceGroup);
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw ex;
                }
            }
        }

        public void Delete(int groupId, int resourceType, int resourceId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    ResourceGroupItemRepository.Delete(groupId, resourceType, resourceId);

                    var resourceGroup = ResourceGroupRepository.GetResourceGroup(groupId);
                    if (resourceGroup != null)
                    {
                        var resourceCount = ResourceGroupItemRepository.GetResourceCount(groupId, resourceType);
                        if (resourceType == (int)ResourceTypeEnum.文本资源)
                        {
                            resourceGroup.Yrg_TextCount = resourceCount;
                        }
                        else if (resourceType == (int)ResourceTypeEnum.多媒体资源)
                        {
                            resourceGroup.Yrg_MediaCount = resourceCount;
                        }
                        else if (resourceType == (int)ResourceTypeEnum.题目)
                        {
                            resourceGroup.Yrg_SubjectCount = resourceCount;
                        }
                        ResourceGroupRepository.Update(resourceGroup);
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw ex;
                }
            }
        }

        public void UpdateStatus(int id, int status)
        {
            var resourceGroup = ResourceGroupRepository.GetResourceGroup(id);
            if (resourceGroup != null)
            {
                resourceGroup.Yrg_Status = status;
                ResourceGroupRepository.Update(resourceGroup);
            }
        }

        public int UpdatePropogue(int id, string description, string url)
        {
            return MediaObjectRepository.UpdatePropogue(id, description, url);
        }
        #endregion

        #region 课程制作查询
        public List<DtoMediaResourceToCourse> GetMediaToCourse(PagingObject paging, int courseId, int mediaType, string nameOrKey)
        {
            var mediaList = new List<DtoMediaResourceToCourse>();
            if (mediaType == (int)MediaResourceTypeEnum.小艾说 || mediaType == (int)MediaResourceTypeEnum.小艾变)
            {
                if (mediaType == (int)MediaResourceTypeEnum.小艾变)
                {
                    return MediaResourceRepository.GetXiaoAiToCourse(paging, nameOrKey, (int)MediaResourceTypeEnum.小艾变);
                }
                if (mediaType == (int)MediaResourceTypeEnum.小艾说)
                {
                    return MediaResourceRepository.GetXiaoAiToCourse(paging, nameOrKey, (int)MediaResourceTypeEnum.小艾说);
                }
            }
            else
            {
                //list = ResourceGroupItemRepository.GetGroupIdAndResourceType(courseId);
                if (!string.IsNullOrEmpty(nameOrKey))
                {
                    if (nameOrKey.IsNumberic() && nameOrKey.Length >= 5)
                    {
                        mediaList = MediaResourceRepository.GetMediaToCourseById(paging, mediaType, courseId, nameOrKey._ToInt32());
                    }
                    else
                    {
                        mediaList = MediaResourceRepository.GetMediaToCourse(paging, courseId, mediaType, nameOrKey);
                    }
                }
                else
                {
                    mediaList = MediaResourceRepository.GetMediaToCourseById(paging, mediaType, courseId, nameOrKey._ToInt32());
                }
            }
            return mediaList;
        }

        public DtoMediaResourceToCourse GetMediaDetailToCourse(int id)
        {
            return MediaObjectRepository.GetMediaDetailToCourse(id);
        }

        public List<DtoMediaResourceToCourse> GetTextToCourse(PagingObject paging, int courseId, int textType, string nameOrKey)
        {
            List<DtoMediaResourceToCourse> textList = new List<DtoMediaResourceToCourse>();
            if (!string.IsNullOrEmpty(nameOrKey))
            {
                if (nameOrKey.IsNumberic() && nameOrKey.Length >= 5)
                {
                    textList = TextResourceRepository.GetTextToCourseById(paging, textType, courseId, nameOrKey._ToInt32());
                }
                else
                {
                    textList = TextResourceRepository.GetTextToCourse(paging, courseId, textType, nameOrKey);
                }
            }
            else
            {
                textList = TextResourceRepository.GetTextToCourseById(paging, textType, courseId, 0);
            }
            return textList;
        }

        public List<DtoMediaResourceToCourse> GetSubjectToCourse(PagingObject paging, int courseId, int subjectType, string nameOrKey)
        {
            SubjectBll subjectBll = new SubjectBll();
            List<DtoMediaResourceToCourse> subjectList = new List<DtoMediaResourceToCourse>();
            if (!string.IsNullOrEmpty(nameOrKey))
            {
                if (nameOrKey.IsNumberic() && nameOrKey.Length >= 5)
                {
                    subjectList = subjectBll.SubjectService.GetSubjectToCourseById(paging, subjectType, courseId, nameOrKey._ToInt32());
                }
                else
                {
                    subjectList = subjectBll.SubjectService.GetSubjectToCourse(paging, courseId, subjectType, nameOrKey);
                }
            }
            else
            {
                subjectList = subjectBll.SubjectService.GetSubjectToCourseById(paging, subjectType, courseId, 0);
            }
            return subjectList;

            //var list = ResourceGroupItemRepository.GetGroupIdAndResourceType(courseId);

            //var subjectIds = list.Where(s => s.Ygi_ResourceType == (int)ResourceTypeEnum.题目).Select(x => x.Ygi_ResourceId).ToList();


            //return subjectBll.SubjectService.GetSubjectToCourse(paging, subjectIds, subjectType, nameOrKey);
        }


        public DtoMediaResourceToCourse GetTextDetailToCourse(int id)
        {
            return TextObjectRepository.GetTextDetailToCourse(id);
        }

        public List<DtoMediaResourceToCourse> GetPrologues(string description)
        {
            return MediaResourceRepository.GetPrologues(description);
        }

        public DtoMediaResourceToCourse GetPrologueById(int prologueId)
        {
            return MediaResourceRepository.GetPrologueById(prologueId);
        }

        public Dictionary<int, DtoMediaObject> GetMediaObjectByIdList(List<int> ids, bool isText = false)
        {
            List<DtoMediaObject> objectList = new List<DtoMediaObject>();
            if (isText)
            {
                objectList = MediaObjectRepository.GetMediaObjectByIdList(ids);
            }
            else
            {
                objectList = MediaObjectRepository.GetMediaObjectByIdListExceptText(ids);
            }
            var objectDic = objectList.ToDictionary(s => s.MediaId, v => new DtoMediaObject { MediaId = v.MediaId, MediaName = v.MediaName, MediaUrl = v.MediaUrl.ToOssPath(), ImgId = v.ImgId, ImgUrl = v.ImgUrl.ToOssPath(), TextId = v.TextId, TextContent = v.TextContent, Description = v.Description });
            return objectDic;
        }

        public Dictionary<int, string> GetTextObjectByIdList(List<int> ids)
        {
            var objectList = TextObjectRepository.GetTextObjectByIdList(ids);
            var objectDic = objectList.ToDictionary(s => s.Yxo_Id, v => v.Yxo_Content);
            return objectDic;
        }
        #endregion
    }
}
