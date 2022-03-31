using Microsoft.AspNetCore.Identity;

namespace OnlineShopDeliveryAPI.Models
{
    public class AuthenticateResponseModel
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? Email{ get; set; }
        public string? Token { get; set; }


        public AuthenticateResponseModel(UserModel user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            Email = user.UserName;
            Token = token;
        }
    }
}
