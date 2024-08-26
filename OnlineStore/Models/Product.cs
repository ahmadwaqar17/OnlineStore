using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class Product
    {
        [Key]
        public int Pid { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(2000)]
        public string ImagePath { get; set; } // URL of the image

        [Required]
        public string Category { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
