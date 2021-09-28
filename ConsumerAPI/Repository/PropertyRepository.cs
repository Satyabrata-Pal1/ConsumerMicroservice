using ConsumerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerAPI.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private List<Property> properties;
        public PropertyRepository()
        {
            properties = new List<Property>
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
            },
            new Property
            {
                PropertyId =3,
                ConsumerId=3,
                BusinessId=3,
                PropertyType = Property.PropertyTypes.Building,
                OwnershipType =Property.OwnershipTypes.Rental,
                NoOfStoreys=1,
                CostOfProperty=70000,
                SalvageValue=60000,
                UsefulLife=10,
                PropertyAge =5,
                AgentId = 1,
                PropertyValue=8
            }
            };
        }
        public bool CreateProperty(Property property)
        {
            properties.Add(property);
            return true;
        }

        public IEnumerable<Property> GetAllProperties()
        {
            return properties;
        }

        public int GetNewPropertyId()
        {
            return properties.Max(p => p.PropertyId) + 1;
        }

        public Property GetProperty(int consumerId, int propertyId)
        {
            return properties.FirstOrDefault(p => p.ConsumerId == consumerId && p.PropertyId == propertyId);
        }

        public bool PropertyExists(int propertyId)
        {
            return properties.Any(p => p.PropertyId == propertyId);
        }

        public bool UpdateProperty(Property updatedProperty)
        {
            Property property = properties.FirstOrDefault(p=>p.PropertyId == updatedProperty.PropertyId);
            property.PropertyType = updatedProperty.PropertyType;
            property.OwnershipType = updatedProperty.OwnershipType;
            property.NoOfStoreys = updatedProperty.NoOfStoreys;
            property.CostOfProperty = updatedProperty.CostOfProperty;
            property.UsefulLife = updatedProperty.UsefulLife;
            property.PropertyAge= updatedProperty.PropertyAge;

            return true;
        }
    }
}
