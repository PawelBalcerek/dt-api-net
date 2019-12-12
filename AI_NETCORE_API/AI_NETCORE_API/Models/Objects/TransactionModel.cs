using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Models.Objects
{
    public enum TransactionTypeModel { BUY_OFFER, SELL_OFFER };
    public class TransactionModel
    {
        public int Id { get; set; }
        public int SellOfferId { get; set; }
        public int BuyOfferId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionTypeModel Type { get; set; }
        public CompanyModel Company { get; set; }
    }
}
