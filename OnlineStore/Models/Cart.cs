using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Pid { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        // New Property
        [Required]
        [StringLength(20)]
        public string OrderStatus { get; set; } = "Processing";
    }

}
