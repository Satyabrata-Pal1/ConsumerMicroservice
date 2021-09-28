using ConsumerAPI.Controllers;
using ConsumerAPI.DTOS;
using ConsumerAPI.Models;
using ConsumerAPI.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using static ConsumerAPI.DTOS.BusinessDTO;

namespace ConsumerTests
{
    [TestFixture]
    public class ConsumerTests
    {
        private Mock<IBusinessRepository> businessRepositoryStub ;
        private Mock<IPropertyRepository> propertyRepositoryStub;
        [SetUp]
        public void Setup()
        {
            businessRepositoryStub = new Mock<IBusinessRepository>();
            propertyRepositoryStub = new Mock<IPropertyRepository>();
        }

        [Test]
        public void ViewAllProperties_ExistingProperties_ReturnsAllProperties()
        {
            var expectedResults = new List<Property>
            {
               new Property
            {
                PropertyId =1,
                ConsumerId=1,
                BusinessId=1,
                PropertyType = Property.PropertyTypes.Building,
                OwnershipType =Property.OwnershipTypes.Owned,
                NoOfStoreys=5,
                CostOfProperty=2000000,
                SalvageValue =1999990,
                UsefulLife=20,
                PropertyAge =1,
                AgentId = 1,
                PropertyValue= 8
            },
            new Property
            {
                PropertyId =2,
                ConsumerId=2,
                BusinessId=2,
                PropertyType = Property.PropertyTypes.Building,
                OwnershipType =Property.OwnershipTypes.Rental,
                NoOfStoreys=1,
                CostOfProperty=50000,
                SalvageValue=48000,
                UsefulLife=10,
                PropertyAge =5,
                AgentId = 1,
                PropertyValue=7
            }
            };
            propertyRepositoryStub.Setup(repo => repo.GetAllProperties()).Returns(expectedResults);
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.ViewAllProperties();
            var result = response as OkObjectResult;
            result.Value.Should().BeEquivalentTo(expectedResults, options => options.ComparingByMembers<Property>());

        }

        [Test]
        public void ViewBusinessByConsumerId_WhenConsumerExists_ReturnsTheBusinessDetail()
        {
            var expectedItem = new BusinessDTO {
                ConsumerId = 1,
                ConsumerCompany = "Charles Construction",
                BusinessOverview = "Large Scale Contract Based Construction",
                ConsumerName = "Charles Boyle",
                DateOfBirth = new DateTime(1970, 11, 25),
                Email = "charlesBoyle@gmail.com",
                Pan = "QT1237FG21",
                BusinessType = BusinessTypes.Construction,
                BuisnessTurnover = 3555682245,
                CapitalInvested = 2325567,
                TotalEmployees = 5600,
                AgentId = 1,
                BusinessValue = 8
            };

            businessRepositoryStub.Setup(repo => repo.GetBusiness(1)).Returns(expectedItem);
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.ViewConsumerBusiness(1);
            var result = response as OkObjectResult;
            result.Value.Should().BeEquivalentTo(expectedItem,
                options => options.ComparingByMembers<BusinessDTO>());


        }
        [Test]
        public void ViewBusinessByConsumerId_WhenConsumerIdIsInvalid_ReturnsBadRequest()
        {
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.ViewConsumerBusiness(-1);
            response.Should().BeOfType<BadRequestObjectResult>();
            (response as BadRequestObjectResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
        [Test]
        public void ViewBusinessByConsumerId_WhenBusinessDoesNotExists_ReturnsNotFound()
        {
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.ViewConsumerBusiness(5);
            response.Should().BeOfType<NotFoundObjectResult>();
            (response as NotFoundObjectResult).StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        [Test]
        public void CreateConsumerBusiness_BusinessIsNull_ReturnsBadRequest()
        {
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            BusinessDTO business = null;
            var response = controller.CreateConsumerBusiness(business);
            response.Should().BeOfType<BadRequestResult>();
            (response as BadRequestResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
        [Test]
        public void CreateConsumerBusiness_BusinessValidationDone_AddBusiness()
        {
            var business = new BusinessDTO
            {
                ConsumerCompany = "Opticals New Nation",
                BusinessOverview = "Sells  High Quality Glasses",
                ConsumerName = "Henry Boyle",
                DateOfBirth = new DateTime(1977, 11, 25),
                Email = "henryBoyle@gmail.com",
                Pan = "FT1296FG21",
                BusinessType = BusinessTypes.Assembly,
                BuisnessTurnover = 600000,
                CapitalInvested = 5999,
                TotalEmployees = 500,
                AgentId = 1
            };
            businessRepositoryStub.Setup(repo => repo.CreateConsumerBusiness(new Consumer
            {
                ConsumerCompany = "Opticals New Nation",
                BusinessOverview = "Sells  High Quality Glasses",
                ConsumerName = "Henry Boyle",
                DateOfBirth = new DateTime(1977, 11, 25),
                Email = "henryBoyle@gmail.com",
                Pan = "FT1296FG21",
            }, new Business
            {
                BusinessType = (Business.BusinessTypes)BusinessTypes.Assembly,
                BuisnessTurnover = 600000,
                CapitalInvested = 300000,
                TotalEmployees = 500,
                AgentId = 1
            })).Returns(true);
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.CreateConsumerBusiness(business);
            response.Should().BeOfType<NoContentResult>();
            (response as NoContentResult).StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }
        [Test]
        public void CreateConsumerBusiness_BusinessValidationFailsMinimumRequirements_ReturnsBadRequest()
        {
            var business = new BusinessDTO
            {
                ConsumerCompany = "Opticals New Nation",
                BusinessOverview = "Sells  High Quality Glasses",
                ConsumerName = "Henry Boyle",
                DateOfBirth = new DateTime(1977, 11, 25),
                Email = "henryBoyle@gmail.com",
                Pan = "FT1296FG21",
                BusinessType = BusinessTypes.Assembly,
                BuisnessTurnover = 60,
                CapitalInvested = 5999999,
                TotalEmployees = 500,
                AgentId = 1
            };
            businessRepositoryStub.Setup(repo => repo.CreateConsumerBusiness(new Consumer
            {
                ConsumerCompany = "Opticals New Nation",
                BusinessOverview = "Sells  High Quality Glasses",
                ConsumerName = "Henry Boyle",
                DateOfBirth = new DateTime(1977, 11, 25),
                Email = "henryBoyle@gmail.com",
                Pan = "FT1296FG21",
            }, new Business
            {
                BusinessType = (Business.BusinessTypes)BusinessTypes.Assembly,
                BuisnessTurnover = 60,
                CapitalInvested = 5999999,
                TotalEmployees = 500,
                AgentId = 1
            })).Returns(false);
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.CreateConsumerBusiness(business);
            response.Should().BeOfType<BadRequestObjectResult>();
            (response as BadRequestObjectResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
        [Test]
        public void UpdateConsumerBusiness_BusinessValidationDone_ReturnsNoContent()
        {
            var business = new UpdateBusinessDTO
            {
                ConsumerId = 1,
                ConsumerCompany = "Charles Construction",
                BusinessOverview = "Large Scale Contract",
                ConsumerName = "Charles Boyle",
                DateOfBirth = new DateTime(1970, 11, 25),
                Email = "charlesBoyle@gmail.com",
                Pan = "QT1237FG21",
                BusinessId=1,
                BusinessType = BusinessTypes.Construction,
                BuisnessTurnover = 3555682245,
                CapitalInvested = 2325567,
                TotalEmployees = 5600,
                AgentId = 1
            };
            businessRepositoryStub.Setup(repo => repo.UpdateConsumerBusiness(new Consumer
            {
                ConsumerId = 1,
                ConsumerCompany = "Charles Construction",
                BusinessOverview = "Large Scale Contract",
                ConsumerName = "Charles Boyle",
                DateOfBirth = new DateTime(1970, 11, 25),
                Email = "charlesBoyle@gmail.com",
                Pan = "QT1237FG21"
            }, new Business
            {
                ConsumerId=1,
                BusinessId=1,
                BusinessType = (Business.BusinessTypes)BusinessTypes.Construction,
                BuisnessTurnover = 3555682245,
                CapitalInvested = 2325567,
                TotalEmployees = 5600,
                AgentId = 1
            })).Returns(true);
            businessRepositoryStub.Setup(repo => repo.ConsumerExists(1)).Returns(true);
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.UpdateConsumerBusiness(1, business);
            response.Should().BeOfType<NoContentResult>();
            (response as NoContentResult).StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }
        [Test]
        public void UpdateConsumerBusiness_ConsumerIdDoesntMatchIdInJsonObject_ReturnsBadRequest()
        {
            var business = new UpdateBusinessDTO
            {
                ConsumerId = 1,
                ConsumerCompany = "Opticals New Nation",
                BusinessOverview = "Sells  High Quality Glasses",
                ConsumerName = "Henry Boyle",
                DateOfBirth = new DateTime(1977, 11, 25),
                Email = "henryBoyle@gmail.com",
                Pan = "FT1296FG21",
                BusinessId = 1,
                BusinessType = BusinessTypes.Assembly,
                BuisnessTurnover = 600000,
                CapitalInvested = 300000,
                TotalEmployees = 500,
                AgentId = 1
            };
            businessRepositoryStub.Setup(repo => repo.UpdateConsumerBusiness(new Consumer
            {
                ConsumerId = 1,
                ConsumerCompany = "Opticals New Nation",
                BusinessOverview = "Sells  High Quality Glasses",
                ConsumerName = "Henry Boyle",
                DateOfBirth = new DateTime(1977, 11, 25),
                Email = "henryBoyle@gmail.com",
                Pan = "FT1296FG21",
            }, new Business
            {
                BusinessId = 1,
                ConsumerId = 1,
                BusinessType = (Business.BusinessTypes)BusinessTypes.Assembly,
                BuisnessTurnover = 600000,
                CapitalInvested = 300000,
                TotalEmployees = 500,
                AgentId = 1
            })).Returns(true);
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.UpdateConsumerBusiness(2, business);
            response.Should().BeOfType<BadRequestObjectResult>();
            (response as BadRequestObjectResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
        [Test]
        public void UpdateConsumerBusiness_BusinessDoesNotExist_ReturnsNotFound()
        {
            var business = new UpdateBusinessDTO
            {   ConsumerId=1,
                ConsumerCompany = "Opticals New Nation",
                BusinessOverview = "Sells  High Quality Glasses",
                ConsumerName = "Henry Boyle",
                DateOfBirth = new DateTime(1977, 11, 25),
                Email = "henryBoyle@gmail.com",
                Pan = "FT1296FG21",
                BusinessId=1,
                BusinessType = BusinessTypes.Assembly,
                BuisnessTurnover = 600000,
                CapitalInvested = 300000,
                TotalEmployees = 500,
                AgentId = 1
            };
            businessRepositoryStub.Setup(repo => repo.UpdateConsumerBusiness(new Consumer
            {
                ConsumerId=1,
                ConsumerCompany = "Opticals New Nation",
                BusinessOverview = "Sells  High Quality Glasses",
                ConsumerName = "Henry Boyle",
                DateOfBirth = new DateTime(1977, 11, 25),
                Email = "henryBoyle@gmail.com",
                Pan = "FT1296FG21",
            }, new Business
            {
                BusinessId=1,
                ConsumerId=1,
                BusinessType = (Business.BusinessTypes)BusinessTypes.Assembly,
                BuisnessTurnover = 600000,
                CapitalInvested = 300000,
                TotalEmployees = 500,
                AgentId = 1
            })).Returns(true);
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.UpdateConsumerBusiness(1,business);
            response.Should().BeOfType<NotFoundObjectResult>();
            (response as NotFoundObjectResult).StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Test]
        public void ViewConsumerProperty_ConsumerIdAndPropertyIdDontMatch_ReturnsBadRequest()
        {
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.ViewConsumerProperty(1, 2);
            response.Should().BeOfType<BadRequestObjectResult>();
            (response as BadRequestObjectResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
        [Test]
        public void ViewConsumerProperty_PropertyDoesNotExist_ReturnsBadRequest()
        {
            Property property = null;
            propertyRepositoryStub.Setup(repo => repo.GetProperty(10, 10)).Returns(property);
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.ViewConsumerProperty(10, 10);
            response.Should().BeOfType<NotFoundObjectResult>();
            (response as NotFoundObjectResult).StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        [Test]
        public void ViewConsumerProperty_PropertyExists_ReturnsOk()
        {
            Property property = new Property
            {
                PropertyId = 1,
                ConsumerId = 1,
                BusinessId = 1,
                PropertyType = Property.PropertyTypes.Building,
                OwnershipType = Property.OwnershipTypes.Owned,
                NoOfStoreys = 5,
                CostOfProperty = 2000000,
                SalvageValue = 1999990,
                UsefulLife = 20,
                PropertyAge = 1,
                AgentId = 1,
                PropertyValue = 8
            };
            propertyRepositoryStub.Setup(repo => repo.GetProperty(1, 1)).Returns(property);
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.ViewConsumerProperty(1, 1);
            response.Should().BeOfType<OkObjectResult>();
            (response as OkObjectResult).StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public void CreateBusinessProperty_PropertyDtoIsNull_ReturnsBadRequest()
        {
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            PropertyDTO property = null;
            var response = controller.CreateBusinessProperty(property);
            response.Should().BeOfType<BadRequestResult>();
            (response as BadRequestResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
        [Test]
        public void CreateBusinessProperty_PropertyValidationDone_AddPropertyReturnsNoContent()
        {
            var property = new Property
            {
                PropertyId=4,
                ConsumerId = 4,
                BusinessId = 4,
                PropertyType = Property.PropertyTypes.Building,
                OwnershipType = Property.OwnershipTypes.Owned,
                NoOfStoreys = 5,
                CostOfProperty = 2000000,
                SalvageValue = 1999990,
                UsefulLife = 20,
                PropertyAge = 1,
                AgentId = 1,
                PropertyValue=10

            };
            propertyRepositoryStub.Setup(repo => repo.CreateProperty(property)).Returns(true);
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.CreateBusinessProperty(new PropertyDTO {
              
                ConsumerId = 4,
                BusinessId = 4,
                PropertyType = (PropertyDTO.PropertyTypes)Property.PropertyTypes.Building,
                OwnershipType = (PropertyDTO.OwnershipTypes)Property.OwnershipTypes.Owned,
                NoOfStoreys = 5,
                CostOfProperty = 2000000,
                SalvageValue = 1999990,
                UsefulLife = 20,
                PropertyAge = 1,
                AgentId = 1,
            });
            response.Should().BeOfType<NoContentResult>();
            (response as NoContentResult).StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }
        [Test]
        public void CreateBusinessProperty_PropertyValidationFailsMinimumRequirements_ReturnsBadRequest()
        {
            var propertyDTO = new PropertyDTO
            {

                ConsumerId = 4,
                BusinessId = 4,
                PropertyType = (PropertyDTO.PropertyTypes)Property.PropertyTypes.Building,
                OwnershipType = (PropertyDTO.OwnershipTypes)Property.OwnershipTypes.Owned,
                NoOfStoreys = 0,
                CostOfProperty = 2000000,
                SalvageValue = 1999990,
                UsefulLife = 20,
                PropertyAge = 1,
                AgentId = 1

            };
           
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.CreateBusinessProperty(propertyDTO);
            response.Should().BeOfType<BadRequestObjectResult>();
            (response as BadRequestObjectResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
        [Test]
        public void UpdateBusinessProperty_PropertyValidationDone_ReturnsNoContent()
        {
            var propertyDTO = new UpdatePropertyDTO
            {
                ConsumerId = 1,
                BusinessId = 1,
                PropertyId = 1,
                PropertyType = (PropertyDTO.PropertyTypes)Property.PropertyTypes.Building,
                OwnershipType = (PropertyDTO.OwnershipTypes)Property.OwnershipTypes.Owned,
                NoOfStoreys = 1,
                CostOfProperty = 2000000,
                SalvageValue = 1999990,
                UsefulLife = 15,
                PropertyAge = 1,
                AgentId = 1
                
            };
            var property = new Property
            {
                ConsumerId = 1,
                BusinessId = 1,
                PropertyId = 1,
                PropertyType = Property.PropertyTypes.Building,
                OwnershipType = Property.OwnershipTypes.Owned,
                NoOfStoreys = 1,
                CostOfProperty = 2000000,
                SalvageValue = 1999990,
                UsefulLife = 15,
                PropertyAge = 1,
                AgentId = 1,
                PropertyValue=10
            };
            propertyRepositoryStub.Setup(repo => repo.UpdateProperty(property)).Returns(true);
            propertyRepositoryStub.Setup(repo => repo.PropertyExists(1)).Returns(true);
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.UpdateBusinessProperty(1, propertyDTO);
            response.Should().BeOfType<NoContentResult>();
            (response as NoContentResult).StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }
        [Test]
        public void UpdateBusinessPropert_PropertyIdDoesntMatchIdInJsonObject_ReturnsBadRequest()
        {
            var propertyDTO = new UpdatePropertyDTO
            {
                ConsumerId = 1,
                BusinessId = 1,
                PropertyId = 1,
                PropertyType = (PropertyDTO.PropertyTypes)Property.PropertyTypes.Building,
                OwnershipType = (PropertyDTO.OwnershipTypes)Property.OwnershipTypes.Owned,
                NoOfStoreys = 1,
                CostOfProperty = 2000000,
                SalvageValue = 1999990,
                UsefulLife = 15,
                PropertyAge = 1,
                AgentId = 1

            };
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.UpdateBusinessProperty(2, propertyDTO);
            response.Should().BeOfType<BadRequestObjectResult>();
            (response as BadRequestObjectResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
        [Test]
        public void UpdateBusinessProperty_PropertyDoesNotExist_ReturnsNotFound()
        {
            var propertyDTO = new UpdatePropertyDTO
            {
                ConsumerId = 10,
                BusinessId = 10,
                PropertyId = 10,
                PropertyType = (PropertyDTO.PropertyTypes)Property.PropertyTypes.Building,
                OwnershipType = (PropertyDTO.OwnershipTypes)Property.OwnershipTypes.Owned,
                NoOfStoreys = 1,
                CostOfProperty = 2000000,
                SalvageValue = 1999990,
                UsefulLife = 15,
                PropertyAge = 1,
                AgentId = 1

            };
            propertyRepositoryStub.Setup(repo => repo.PropertyExists(10)).Returns(false);
            var controller = new ConsumersController(businessRepositoryStub.Object, propertyRepositoryStub.Object);
            var response = controller.UpdateBusinessProperty(10, propertyDTO);
            response.Should().BeOfType<NotFoundObjectResult>();
            (response as NotFoundObjectResult).StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
    }


}
