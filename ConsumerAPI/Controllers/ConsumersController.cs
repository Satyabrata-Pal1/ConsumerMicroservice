using ConsumerAPI.DTOS;
using ConsumerAPI.Models;
using ConsumerAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace ConsumerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class ConsumersController : ControllerBase
    {
        private readonly IBusinessRepository repository;
        private readonly IPropertyRepository repo;

        public ConsumersController(IBusinessRepository repository, IPropertyRepository repo)
        {
            this.repository = repository;
            this.repo = repo;
        }
        [HttpGet("ViewAllBusinesses")]
        public IActionResult ViewAllBusinesses()
        {
            return Ok(repository.GetBusinesses());
        }
        [HttpGet("ViewAllProperties")]
        public IActionResult ViewAllProperties()
        {
            return Ok(repo.GetAllProperties());
        }

        [ProducesResponseType(200,Type = typeof(BusinessDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("ViewConsumerBusiness/{id}")]
        public IActionResult ViewConsumerBusiness(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id");
            }
            BusinessDTO obj = repository.GetBusiness(id);
            if(obj == null)
            {
                return NotFound("Consumer with this Id does not exist ");
                //return NotFound();
            }
            else
            {
                return Ok(obj);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("CreateConsumerBusiness")]
        public IActionResult CreateConsumerBusiness([FromBody] BusinessDTO businessDTO)
        {
            if(businessDTO == null)
            {
                return BadRequest();
            }
            //contains all permissible business rules
            BusinessMaster master = new BusinessMaster();
            if (!master.IsValidBuisness(businessDTO))
            {
                return BadRequest("Business Currently Not Eligible For Any Policies! ");
            }
            int max = repository.GetNewConsumerId();
            decimal percentage = (businessDTO.BuisnessTurnover / businessDTO.CapitalInvested)*100;
            int businessValue = GetBusinessValue(percentage);
            Consumer consumer = new Consumer()
            {
                ConsumerId = max,
                ConsumerCompany = businessDTO.ConsumerCompany,
                BusinessOverview = businessDTO.BusinessOverview,
                ConsumerName = businessDTO.ConsumerName,
                DateOfBirth = businessDTO.DateOfBirth,
                Email = businessDTO.Email,
                Pan = businessDTO.Pan,
            };
            Business business = new Business
            {
                BusinessId = max,
                ConsumerId =max,
                BusinessType = (Business.BusinessTypes)businessDTO.BusinessType,
                BuisnessTurnover = businessDTO.BuisnessTurnover,
                CapitalInvested = businessDTO.CapitalInvested,
                TotalEmployees = businessDTO.TotalEmployees,
                AgentId = businessDTO.AgentId,
                BusinessValue = businessValue
               
            };

            bool isAdded = repository.CreateConsumerBusiness(consumer, business);
            
                return NoContent();
            
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("UpdateConsumerBusiness/{id}")]
        public IActionResult UpdateConsumerBusiness(int id, [FromBody] UpdateBusinessDTO businessDTO)
        {
            if (id != businessDTO.ConsumerId)
            {
                return BadRequest("Invalid Request!");
            }
            if (!repository.ConsumerExists(id))
            {
                return NotFound("Consumer does not exist");
            }
            decimal percentage = (businessDTO.BuisnessTurnover / businessDTO.CapitalInvested) * 100;
            int businessValue = GetBusinessValue(percentage);
            Consumer consumer = new Consumer()
            {
                ConsumerId = id,
                ConsumerCompany = businessDTO.ConsumerCompany,
                BusinessOverview = businessDTO.BusinessOverview,
                ConsumerName = businessDTO.ConsumerName,
                DateOfBirth = businessDTO.DateOfBirth,
                Email = businessDTO.Email,
                Pan = businessDTO.Pan,
            };
            Business business = new Business
            {
                BusinessId = id,
                ConsumerId = id,
                BusinessType = (Business.BusinessTypes)businessDTO.BusinessType,
                BuisnessTurnover = businessDTO.BuisnessTurnover,
                CapitalInvested = businessDTO.CapitalInvested,
                TotalEmployees = businessDTO.TotalEmployees,
                AgentId = businessDTO.AgentId,
                BusinessValue = businessValue

            };
            bool isUpdated = repository.UpdateConsumerBusiness(consumer, business);
            return NoContent();

        }
        
        private int GetBusinessValue(decimal percentage)
        {
            int businessValue = 0;
            if (percentage >= 0 && percentage <= 4)
                businessValue = 0;
            else if (percentage >= 5 && percentage <= 10)
                businessValue = 1;
            else if (percentage >= 11 && percentage <= 15)
                businessValue = 2;
            else if (percentage >= 16 && percentage <= 20)
                businessValue = 3;
            else if (percentage >= 21 && percentage <= 25)
                businessValue = 4;
            else if (percentage >= 26 && percentage <= 30)
                businessValue = 5;
            else if (percentage >= 31 && percentage <= 35)
                businessValue = 6;
            else if (percentage >= 36 && percentage <= 40)
                businessValue = 7;
            else if (percentage >= 41 && percentage <= 45)
                businessValue = 8;
            else if (percentage >= 46 && percentage <= 50)
                businessValue = 9;
            else
                businessValue = 10;

            return businessValue;
        }


        [ProducesResponseType(200, Type = typeof(BusinessDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("ViewConsumerProperty/{consumerId}/{propertyId}")]
        public IActionResult ViewConsumerProperty(int consumerId, int propertyId)
        {
            if (consumerId <= 0 || propertyId <= 0 ||consumerId!=propertyId)
            {
                return BadRequest("Id(s) is/are Invalid");
            }
            Property obj = repo.GetProperty(consumerId, propertyId);
            if (obj == null)
            {
                return NotFound("Property not found ");
            }
            else
            {
                PropertyDTO propertyDTO = new PropertyDTO
                {
                    PropertyId = obj.PropertyId,
                    ConsumerId = obj.ConsumerId,
                    BusinessId = obj.BusinessId,
                    PropertyType = (PropertyDTO.PropertyTypes)obj.PropertyType,
                    OwnershipType = (PropertyDTO.OwnershipTypes)obj.OwnershipType,
                    NoOfStoreys = obj.NoOfStoreys,
                    CostOfProperty = obj.CostOfProperty,
                    SalvageValue =obj.SalvageValue,
                    UsefulLife = obj.UsefulLife,
                    PropertyAge = obj.PropertyAge,
                    AgentId = obj.AgentId,
                    PropertyValue =obj.PropertyValue
                    
                };
                return Ok(propertyDTO);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("CreateBusinessProperty")]
        public IActionResult CreateBusinessProperty([FromBody] PropertyDTO propertyDTO)
        {
            if (propertyDTO == null)
            {
                return BadRequest();
            }
            //contains all permissible business rules
            PropertyMaster master = new PropertyMaster();
            if (!master.IsValidProperty(propertyDTO))
            {
                return BadRequest("Property Currently Not Eligible For Any Policies! ");
            }
            //int max = repo.GetNewPropertyId();
            decimal annualDepreciation = (propertyDTO.CostOfProperty-propertyDTO.SalvageValue)/propertyDTO.UsefulLife;
            int propertyValue = GetPropertyValue(annualDepreciation);
            Property property = new Property
            {
                PropertyId = propertyDTO.ConsumerId,
                ConsumerId = propertyDTO.ConsumerId,
                BusinessId = propertyDTO.BusinessId,
                PropertyType = (Property.PropertyTypes)propertyDTO.PropertyType,
                OwnershipType = (Property.OwnershipTypes)propertyDTO.OwnershipType,
                NoOfStoreys = propertyDTO.NoOfStoreys,
                CostOfProperty = propertyDTO.CostOfProperty,
                SalvageValue = propertyDTO.SalvageValue,
                UsefulLife = propertyDTO.UsefulLife,
                PropertyAge = propertyDTO.PropertyAge,
                AgentId = propertyDTO.AgentId,
                PropertyValue = propertyValue
            };

            bool isAdded = repo.CreateProperty(property);
            return NoContent();
           
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("UpdateBusinessProperty/{id}")]
        public IActionResult UpdateBusinessProperty(int id, [FromBody] UpdatePropertyDTO propertyDTO)
        {
            if (id != propertyDTO.PropertyId)
            {
                return BadRequest("Invalid Request!");
            }
            if (!repo.PropertyExists(id))
            {
                return NotFound("Consumer does not exist");
            }
            decimal annualDepreciation = (propertyDTO.CostOfProperty - propertyDTO.SalvageValue) / propertyDTO.UsefulLife;
            int propertyValue = GetPropertyValue(annualDepreciation);
            Property property = new Property
            {
                PropertyId = propertyDTO.PropertyId,
                ConsumerId = propertyDTO.ConsumerId,
                BusinessId = propertyDTO.BusinessId,
                PropertyType = (Property.PropertyTypes)propertyDTO.PropertyType,
                OwnershipType = (Property.OwnershipTypes)propertyDTO.OwnershipType,
                NoOfStoreys = propertyDTO.NoOfStoreys,
                CostOfProperty = propertyDTO.CostOfProperty,
                SalvageValue = propertyDTO.SalvageValue,
                UsefulLife = propertyDTO.UsefulLife,
                PropertyAge = propertyDTO.PropertyAge,
                AgentId = propertyDTO.AgentId,
                PropertyValue = propertyValue
            };
            bool isUpdated = repo.UpdateProperty(property);
            return NoContent();
        }

        private int GetPropertyValue(decimal annualDepreciation)
        {
            int propertyValue = 0;
            if (annualDepreciation >= 0 && annualDepreciation <= 1000)
                propertyValue = 10;
            else if (annualDepreciation >= 1001 && annualDepreciation <= 2000)
                propertyValue = 9;
            else if (annualDepreciation >= 2001 && annualDepreciation <= 3000)
                propertyValue = 8;
            else if (annualDepreciation >= 3001 && annualDepreciation <= 4000)
                propertyValue = 7;
            else if (annualDepreciation >= 4001 && annualDepreciation <= 5000)
                propertyValue = 6;
            else if (annualDepreciation >= 5001 && annualDepreciation <= 6000)
                propertyValue = 5;
            else if (annualDepreciation >= 6001 && annualDepreciation <= 7000)
                propertyValue = 4;
            else if (annualDepreciation >= 7001 && annualDepreciation <= 8000)
                propertyValue = 3;
            else if (annualDepreciation >= 8001 && annualDepreciation <= 9000)
                propertyValue = 2;
            else if (annualDepreciation >= 9001 && annualDepreciation <= 10000)
                propertyValue = 1;
            else
                propertyValue = 0;

            return propertyValue;
        }
    }
}
