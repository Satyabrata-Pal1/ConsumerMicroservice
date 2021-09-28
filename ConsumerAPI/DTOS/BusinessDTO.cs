using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerAPI.DTOS
{
    public class BusinessDTO
    {
        //Consumer Information
        public int ConsumerId { get; set; }
        public string ConsumerCompany { get; set; }
        public string BusinessOverview { get; set; }
        public string ConsumerName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Pan { get; set; }

        //Buisness Information

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

        public int BusinessValue { get; set; }
    }
}
