using ConsumerAPI.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerAPI
{
    public class PropertyMaster
    {
        private readonly List<PropertyDTO> permissibleProperties;

        public PropertyMaster()
        {
            permissibleProperties = new List<PropertyDTO>
            {
            new PropertyDTO
            {
                OwnershipType =PropertyDTO.OwnershipTypes.Rental,
                NoOfStoreys=1,
                CostOfProperty=50000
            },
            new PropertyDTO
            {
                OwnershipType =PropertyDTO.OwnershipTypes.Owned,
                NoOfStoreys=5,
                CostOfProperty=100000
            },
            new PropertyDTO
            {
                OwnershipType =PropertyDTO.OwnershipTypes.Owned,
                NoOfStoreys=10,
                CostOfProperty=1000000
            }
            };
        }

        public bool IsValidProperty(PropertyDTO property)
        {
            bool flag = false;
            foreach (PropertyDTO permissibleProperty in permissibleProperties)
            {
                if (property.OwnershipType == permissibleProperty.OwnershipType && property.NoOfStoreys >= permissibleProperty.NoOfStoreys && property.CostOfProperty >= permissibleProperty.CostOfProperty)
                {
                    flag = true;
                    break;
                }

            }

            return flag;
        }
    }
}
