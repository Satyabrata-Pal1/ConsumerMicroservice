using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerAPI.Models
{
    public class Consumer
    {
        public int ConsumerId { get; set; }

        [MaxLength(50)]
        [Required]
        public string ConsumerCompany { get; set; }

        [MaxLength(100)]
        [Required]
        public string BusinessOverview { get; set; }

        [MaxLength(50)]
        [Required]
        public string ConsumerName { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public string Email { get; set; }

        [MaxLength(10)]
        [Required]
        public string Pan { get; set; }

        public bool IsValid { get; set; }
    }
}
