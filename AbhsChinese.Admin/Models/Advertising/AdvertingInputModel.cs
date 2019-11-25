using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Advertising
{
    public class AdvertingInputModel
    {
        public int Bad_PosId { get; set; }

        public string Bap_Code { get; set; }


        public int Bad_Id { get; set; }

        public int Bad_ReferId { get; set; }
        public string Bad_Url { get; set; }

        public string Bad_ImageUrl { get; set; }

        public string Bad_ImageUrlShow => Bad_ImageUrl.ToOssPath();

        public int Bad_Status { get; set; }
        

     

        public AdvertingInputModel(Bas_Advertising dbModel)
        {
            if (dbModel != null)
            {
                this.Bad_Id = dbModel.Bad_Id;
                this.Bad_ImageUrl = dbModel.Bad_ImageUrl;
                this.Bad_PosId = dbModel.Bad_PosId;
                this.Bad_ReferId = dbModel.Bad_ReferId;
                this.Bad_Status = dbModel.Bad_Status;
                this.Bad_Url = dbModel.Bad_Url;
            }
        }

        public AdvertingInputModel(int Bad_PosId)
        {
            this.Bad_PosId = Bad_PosId;
            this.Bad_Status = (int)StatusEnum.有效;
        }
        public AdvertingInputModel()
        {
            this.Bad_Status = (int)StatusEnum.有效;
        }
        public Bas_Advertising ToAdvertisingDbModel()
        {
            Bas_Advertising dbModel = new Bas_Advertising();
            
            dbModel.Bad_Id = this.Bad_Id;
            dbModel.Bad_ImageUrl = this.Bad_ImageUrl;
            dbModel.Bad_PosId = this.Bad_PosId;
            dbModel.Bad_ReferId = this.Bad_ReferId;
            dbModel.Bad_Status = this.Bad_Status;
            dbModel.Bad_Url = string.IsNullOrEmpty(this.Bad_Url) ? "" : this.Bad_Url;

            return dbModel;
        }
    }
}