using ConsumerAPI.DTOS;
using ConsumerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerAPI.Repository
{
    public interface IBusinessRepository
    {
        BusinessDTO GetBusiness(int consumerId);
        IEnumerable<BusinessDTO> GetBusinesses();
        bool CreateConsumerBusiness(Consumer consumer,Business business );
        bool UpdateConsumerBusiness(Consumer consumer, Business business);
        bool BusinessExists(int businessId);
        public bool ConsumerExists(int consumerId);
        int GetNewConsumerId();
    }
}
