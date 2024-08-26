using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class DeliveryInfo
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Area { get; set; }

        public int ProductId { get; set; } // New Property for ProductId

        public DateTime OrderDate { get; set; } // New Property for Order Date
    }
}
