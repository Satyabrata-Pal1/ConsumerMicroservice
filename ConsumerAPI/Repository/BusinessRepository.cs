using ConsumerAPI.DTOS;
using ConsumerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerAPI.Repository
{
    public class BusinessRepository : IBusinessRepository
    {
        private List<Consumer> consumers;
        private List<Business> businesses;
        public BusinessRepository()
        {
            consumers = new List<Consumer>
            {
                new Consumer()
                {
                    ConsumerId=1,
                    ConsumerCompany="Charles Construction",
                    BusinessOverview="Large Scale Contract Based Construction",
                    ConsumerName="Charles Boyle",
                    DateOfBirth = new DateTime(1970,11,25),
                    Email = "charlesBoyle@gmail.com",
                    Pan="QT1237FG21",
                    IsValid=true
                },
                new Consumer()
                {
                    ConsumerId=2,
                    ConsumerCompany="Mackernin Motors",
                    BusinessOverview="Building High Performance Race Cars",
                    ConsumerName="James Mackernin",
                    DateOfBirth = new DateTime(1979,1,12),
                    Email = "jamesMack@gmail.com",
                    Pan="QT1SDG2315",
                    IsValid=true
                },
                new Consumer()
                {
                    ConsumerId=3,
                    ConsumerCompany="Farm Fresh",
                    BusinessOverview="Delivery Of Fresh Fruits and Vegetables",
                    ConsumerName="Robert Zane",
                    DateOfBirth = new DateTime(1979,1,12),
                    Email = "zanerobert@gmail.com",
                    Pan="QT1SJH5315",
                    IsValid=true
                }

            };

            businesses = new List<Business>()
            {
                new Business()
                {
                    BusinessId=1,
                    ConsumerId=1,
                    BusinessType =Business.BusinessTypes.Construction,
                    BuisnessTurnover=3555682245,
                    CapitalInvested=2325567,
                    TotalEmployees=5600,
                    AgentId = 1,
                    BusinessValue=8
                },
                new Business()
                {
                    BusinessId=2,
                    ConsumerId=2,
                    BusinessType =Business.BusinessTypes.Production,
                    BuisnessTurnover=653682245,
                    CapitalInvested=7435567,
                    TotalEmployees=20000,
                    AgentId = 1,
                    BusinessValue=10
                },
                new Business()
                {
                    BusinessId=3,
                    ConsumerId=3,
                    BusinessType =Business.BusinessTypes.Agricultire,
                    BuisnessTurnover=3948921,
                    CapitalInvested=125567,
                    TotalEmployees=200,
                    AgentId = 1,
                    BusinessValue =10
                }

            };
        }
        public bool BusinessExists(int businessId)
        {
            return businesses.Any(b => b.BusinessId == businessId);
        }
        public bool ConsumerExists(int consumerId)
        {
            return consumers.Any(c => c.ConsumerId == consumerId);
        }
        public bool CreateConsumerBusiness(Consumer consumer, Business business)
        {
            consumers.Add(consumer);
            businesses.Add(business);
            return true;
        }

        public BusinessDTO GetBusiness(int consumerId)
        {
            Consumer consumer = consumers.FirstOrDefault(c => c.ConsumerId == consumerId);
            Business business = businesses.FirstOrDefault(b=>b.ConsumerId == consumerId);
            if (consumer != null && business != null)
            {
                BusinessDTO obj = new BusinessDTO
                {
                    ConsumerId=consumer.ConsumerId,
                    ConsumerCompany = consumer.ConsumerCompany,
                    BusinessOverview = consumer.BusinessOverview,
                    ConsumerName = consumer.ConsumerName,
                    DateOfBirth = consumer.DateOfBirth,
                    Email = consumer.Email,
                    Pan = consumer.Pan,
                    BusinessType = (BusinessDTO.BusinessTypes)business.BusinessType,
                    BuisnessTurnover = business.BuisnessTurnover,
                    CapitalInvested = business.CapitalInvested,
                    TotalEmployees = business.TotalEmployees,
                    AgentId = business.AgentId,
                    BusinessValue = business.BusinessValue
                };

                return obj;
            }
            else
                return null;
            
        }

        public bool UpdateConsumerBusiness(Consumer updatedConsumer, Business updatedBusiness)
        {
                Consumer consumer = consumers.FirstOrDefault(c => c.ConsumerId == updatedConsumer.ConsumerId);
                consumer.ConsumerCompany = updatedConsumer.ConsumerCompany;
                consumer.BusinessOverview = updatedConsumer.BusinessOverview;
                consumer.ConsumerName = updatedConsumer.ConsumerName;
                consumer.DateOfBirth = updatedConsumer.DateOfBirth;
                consumer.Email = updatedConsumer.Email;
                consumer.Pan = updatedConsumer.Pan;

                Business business = businesses.FirstOrDefault(b => b.ConsumerId == updatedConsumer.ConsumerId);
                business.BusinessType = updatedBusiness.BusinessType;
                business.BuisnessTurnover = updatedBusiness.BuisnessTurnover;
                business.CapitalInvested = updatedBusiness.CapitalInvested;
                business.TotalEmployees = updatedBusiness.TotalEmployees;
                business.AgentId = updatedBusiness.AgentId;

                return true;
        }

        public int GetNewConsumerId()
        {
            return consumers.Max(c => c.ConsumerId) + 1;
        }

        public IEnumerable<BusinessDTO> GetBusinesses()
        {
            List<BusinessDTO> list = new List<BusinessDTO>();
            for (int i=1;i<=businesses.Count;i++)
            {
                Consumer consumer = consumers.FirstOrDefault(c => c.ConsumerId == i);
                Business business = businesses.FirstOrDefault(b => b.ConsumerId == i);
                if (consumer != null && business != null)
                {
                    BusinessDTO obj = new BusinessDTO
                    {
                        ConsumerId = consumer.ConsumerId,
                        ConsumerCompany = consumer.ConsumerCompany,
                        BusinessOverview = consumer.BusinessOverview,
                        ConsumerName = consumer.ConsumerName,
                        DateOfBirth = consumer.DateOfBirth,
                        Email = consumer.Email,
                        Pan = consumer.Pan,
                        BusinessType = (BusinessDTO.BusinessTypes)business.BusinessType,
                        BuisnessTurnover = business.BuisnessTurnover,
                        CapitalInvested = business.CapitalInvested,
                        TotalEmployees = business.TotalEmployees,
                        AgentId = business.AgentId,
                        BusinessValue = business.BusinessValue
                    };
                    list.Add(obj);
                    
                }
            }

            return list;
        }
    }
}
