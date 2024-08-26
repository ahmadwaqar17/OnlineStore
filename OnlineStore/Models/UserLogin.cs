using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class UserLogin
    {
        [Key]
        [EmailAddress]
        public string Email { get; set; }



        public string Name { get; set; }


    }
}
