using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerAPI.DTOS
{
    public class PropertyDTO
    {
        public int PropertyId { get; set; }
        public int ConsumerId { get; set; }
        public int BusinessId { get; set; }
        public enum PropertyTypes
        {
            Building=0,
            FactoryEquipment=1,
            PropertyInTransit=2,
            ColdStorage=3
        }
        public enum OwnershipTypes
        {
            Owned = 0,  
            Rental = 1
        }
        public OwnershipTypes OwnershipType { get; set; }
        public PropertyTypes PropertyType { get; set; }

        public int NoOfStoreys { get; set; }

        public decimal CostOfProperty { get; set; }

        public decimal SalvageValue { get; set; }

        public int UsefulLife { get; set; }

        public int PropertyAge { get; set; }

        public int AgentId { get; set; }

        public int PropertyValue { get; set; }

    }
}
