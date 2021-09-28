using ConsumerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerAPI.Repository
{
    public interface IPropertyRepository
    {
        IEnumerable<Property> GetAllProperties();
        Property GetProperty(int consumerId,int propertyId);
        bool CreateProperty(Property property);
        bool UpdateProperty(Property property);
        bool PropertyExists(int propertyId);
        int GetNewPropertyId();
    }
}
