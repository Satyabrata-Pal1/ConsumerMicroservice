using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ConsumerAPI.DTOS.PropertyDTO;

namespace ConsumerAPI.DTOS
{
    public class UpdatePropertyDTO
    {
        public int PropertyId { get; set; }
        public int ConsumerId { get; set; }
        public int BusinessId { get; set; }
        public OwnershipTypes OwnershipType { get; set; }
        public PropertyTypes PropertyType { get; set; }

        public int NoOfStoreys { get; set; }

        public decimal CostOfProperty { get; set; }

        public decimal SalvageValue { get; set; }

        public int UsefulLife { get; set; }

        public int PropertyAge { get; set; }

        public int AgentId { get; set; }
    }
}
