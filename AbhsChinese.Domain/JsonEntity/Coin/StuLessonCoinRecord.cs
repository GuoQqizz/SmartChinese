using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.Coin
{
    public class StuLessonCoinRecord
    {
        [JsonProperty("UTim")]
        public int UseTime
        {
            set; get;
        }

        [JsonProperty("STim")]
        public string SubmitTime
        {
            set; get;
        }


        [JsonProperty("TCon")]
        public int TotalCoins
        {
            set; get;
        }

        [JsonProperty("Part")]
        public int Part
        {
            set; get;
        }

        [JsonProperty("Cons")]
        public List<StuLessonCoinItem> CoinCollection
        {
            set; get;
        }
    }
}
