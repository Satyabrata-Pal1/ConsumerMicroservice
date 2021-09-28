using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerAPI.Models
{
    public class Business
    {
        public int BusinessId { get; set; }
        public int ConsumerId { get; set; }

        public enum BusinessTypes
        {
            ConsumerGoods =0,
            Production=1,
            Agricultire=2,
            Assembly=3,
            Development=4,
            Construction=5
        }
        public BusinessTypes BusinessType { get; set; }
        public decimal BuisnessTurnover { get; set; }
        public decimal CapitalInvested { get; set; }
        public long TotalEmployees { get; set; }
        public int AgentId { get; set; }
        [Range(1,10)]
        public int BusinessValue { get; set; }

    }
}
