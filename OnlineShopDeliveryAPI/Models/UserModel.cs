using Microsoft.AspNetCore.Identity;

namespace OnlineShopDeliveryAPI.Models
{
    public class UserModel : IdentityUser
    {
        public string FirstName { get; set; }
    }
}
