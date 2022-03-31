using System.ComponentModel.DataAnnotations;

namespace OnlineShopDeliveryAPI.Models
{
    public class RegisterModel
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
