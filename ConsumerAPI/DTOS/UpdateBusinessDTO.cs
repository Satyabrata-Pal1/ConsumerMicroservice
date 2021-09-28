using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ConsumerAPI.DTOS.BusinessDTO;

namespace ConsumerAPI.DTOS
{
    public class UpdateBusinessDTO
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
        public int BusinessId { get; set; }
        public BusinessTypes BusinessType { get; set; }
        public decimal BuisnessTurnover { get; set; }
        public decimal CapitalInvested { get; set; }
        public long TotalEmployees { get; set; }
        public int AgentId { get; set; }
    }
}
