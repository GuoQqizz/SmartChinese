using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.Coin
{
    public class StuLessonCoinItem
    {
        [JsonProperty("AcId")]
        public string ActionId
        {
            set;get;
        }

        [JsonProperty("Type")]
        public string ActionType
        {
            set;get;
        }

        [JsonProperty("ACin")]
        public int ActionCoins
        {
            set; get;
        }

        [JsonProperty("GCin")]
        public int GetCoins
        {
            set; get;
        }
    }
}
