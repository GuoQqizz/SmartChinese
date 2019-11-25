using AbhsChinese.Code.Common;
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
using AbhsChinese.Code.Extension;
using AbhsChinese.Bll.Message;

namespace AbhsChinese.Bll
{
    public class AdvertisingBll : BllBase
    {
        public AdvertisingBll() : base()
        {
        }

        #region repository
        private IAdvertisingPosRepository advertisingPosRepository;
        public IAdvertisingPosRepository AdvertisingPosRepository
        {
            get
            {
                if (advertisingPosRepository == null)
                {
                    advertisingPosRepository = new AdvertisingPosRepository();
                }
                return advertisingPosRepository;
            }
        }

        private IAdvertisingRepository advertisingRepository;
        public IAdvertisingRepository AdvertisingRepository
        {
            get
            {
                if (advertisingRepository == null)
                {
                    advertisingRepository = new AdvertisingRepository();
                }
                return advertisingRepository;
            }
        }
        #endregion

        #region select
        /// <summary>
        /// 获取推荐楼层信息
        /// </summary>
        /// <param name="paging">分页信息</param>
        /// <param name="advPosType">广告位类型</param>
        /// <param name="advStatus">广告位详情状态 < 0 查所有</param>
        /// <returns></returns>
        public List<DtoAdvertisingPos> GetPagingAdvertisingPos(PagingObject paging, int advPosType, int advStatus)
        {
            return AdvertisingPosRepository.GetPagingAdvertisingPos(paging, advPosType, advStatus);
        }
        /// <summary>
        /// 获取推荐历史记录
        /// </summary>
        /// <param name="paging">分页信息</param>
        /// <param name="advPosType">广告位类型</param>
        /// <param name="advStatus">广告位详情状态 < 0 查所有</param>
        /// <returns></returns>
        public List<DtoAdvertisingPos> GetPagingAdvertisingHistory(PagingObject paging, int advPosId)
        {
            return AdvertisingPosRepository.GetPagingAdvertisingHistory(paging, advPosId);
        }

        public Bas_Advertising GetAdvertisingById(int badId)
        {
            return AdvertisingRepository.Get(badId);
        }
        public Bas_Advertising GetAdvertisingByPosId(int bapId)
        {
            return AdvertisingRepository.GetByPosId(bapId);
        }

        public Bas_AdvertisingPos GetAdvertisingPosById(int bap_Id)
        {
            return AdvertisingPosRepository.Get(bap_Id);
        }

        public List<DtoAdvertisingIndex> GetAdvertisingForIndex(string codePre = AdvertisingPosCodePre.Banner)
        {
            return AdvertisingPosRepository.GetPagingAdvertisingForIndex(codePre);
        }
        #endregion

        #region update
        public bool IncrementHitCount(int badId, int count = 1)
        {
            return AdvertisingRepository.IncrementHitCount(badId, count);
        }

        public bool IncrementValidCount(int badId, int count = 1)
        {
            return AdvertisingRepository.IncrementValidCount(badId, count);
        }
        #endregion

        #region insert
        public int InsertList(List<Bas_Advertising> list)
        {

            return AdvertisingRepository.InsertList(list);
        }
        #endregion

        #region save
        /// <summary>
        /// 保存广告
        /// </summary>
        /// <param name="dbModel"></param>
        /// <returns></returns>
        public bool SaveAdertising(Bas_Advertising dbModel)
        {
            bool result = false;
            if (dbModel.Bad_EndTime == DateTime.MinValue)
            {
                dbModel.Bad_EndTime = DateTimeExtensions.DefaultDateTime;
            }
            if (dbModel.Bad_CreateTime == DateTime.MinValue)
            {
                dbModel.Bad_CreateTime = DateTime.Now;

            }
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (dbModel.Bad_Id > 0)
                    {
                        var oldDbModel = GetAdvertisingById(dbModel.Bad_Id);
                        var needCreate = NeedCreatNew(dbModel, oldDbModel);
                        if (needCreate)
                        {
                            dbModel.Bad_Id = 0;
                            dbModel.Bad_CreateTime = DateTime.Now;

                            oldDbModel.Bad_EndTime = DateTime.Now;
                            oldDbModel.Bad_Status = (int)StatusEnum.无效;
                            var updateOld = AdvertisingRepository.Update(oldDbModel);

                            var insertNew = AdvertisingRepository.Insert(dbModel);
                            result = updateOld && insertNew > 0;
                        }
                        else
                        {
                            result = AdvertisingRepository.Update(dbModel);
                        }
                    }
                    else
                    {
                        var updateOld = AdvertisingRepository.UpdateStatusByBabPosId(dbModel.Bad_PosId, (int)StatusEnum.有效, (int)StatusEnum.无效, DateTime.Now);
                        var insertNew = AdvertisingRepository.Insert(dbModel);
                        result = insertNew > 0;
                    }
                    if (result)
                    {
                        scope.Complete();
                    }
                    else
                    {
                        RollbackTran();
                    }
                }
                catch (Exception ex)
                {
                    RollbackTran();
                    throw ex;
                }
            }
            return result;
        }

        public bool EditAdertisingPosStatus(int bapId, int bapStatus, int editor)
        {
            return AdvertisingPosRepository.EditAdertisingPosStatus(bapId, bapStatus, editor);
        }
        #endregion

        #region Message

        /// <summary>
        /// 发送广告点击的消息
        /// </summary>
        public void SendHitMessage(int advertisingId)
        {
            MessageBll bll = new MessageBll();
            string msgBody = advertisingId + ",1,0";
            bll.PublishMessage(MessageChannel.ADVERTISING_CHANNEL, MessageTypeEnum.广告线索, msgBody);
        }

        /// <summary>
        /// 发送广告成单的消息
        /// </summary>
        public void SendOrderMessage(int advertisingId)
        {
            MessageBll bll = new MessageBll();
            string msgBody = advertisingId + ",0,1";
            bll.PublishMessage(MessageChannel.ADVERTISING_CHANNEL, MessageTypeEnum.广告线索, msgBody);
        }
        #endregion

        #region private
        /// <summary>
        /// 判断是否需要新增
        /// </summary>
        /// <param name="newModel"></param>
        /// <param name="oldModel"></param>
        /// <returns></returns>
        private bool NeedCreatNew(Bas_Advertising newModel, Bas_Advertising oldModel)
        {
            return newModel.Bad_ReferId != oldModel.Bad_ReferId || newModel.Bad_Url != oldModel.Bad_Url;
        }
        #endregion
    }
}
