using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineShopDeliveryAPI.Classes;
using OnlineShopDeliveryAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopDeliveryAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<UserModel> signInManager;
        private readonly UserManager<UserModel> userManager;

        private readonly JwtBearerTokenSettings jwtBearerTokenSettings;


        public AuthController(IOptions<JwtBearerTokenSettings> jwtTokenOptions, UserManager<UserModel> userManager)
        {
            this.jwtBearerTokenSettings = jwtTokenOptions.Value;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel userDetails)
        {
            if (!ModelState.IsValid || userDetails == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }

            var identityUser = new UserModel() { UserName = userDetails.Email, Email = userDetails.Email, FirstName = userDetails.FirstName };
            var result = await userManager.CreateAsync(identityUser, userDetails.Password);
            if (!result.Succeeded)
            {
                var dictionary = new ModelStateDictionary();
                foreach (IdentityError error in result.Errors)
                {
                    dictionary.AddModelError(error.Code, error.Description);
                }

                return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
            }

            return Ok(new { Message = "User Reigstration Successful" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequestModel credentials)
        {

            UserModel identityUser;

            if (!ModelState.IsValid
                || credentials == null
                || (identityUser = await ValidateUser(credentials)) == null)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }

            var token = GenerateToken(identityUser);



            return Ok(new AuthenticateResponseModel(identityUser, token.ToString()));

        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { Message = "You are logged out" });
        }

        [HttpPost]
        [Route("Driver")]
        public async Task<ActionResult<string>> GetDriverName(string id)
        {
            var result = await userManager.FindByIdAsync(id);
            if(result == null)
            {
                return NotFound();
            }

            return (result.FirstName);
        }

        private async Task<UserModel> ValidateUser(AuthenticateRequestModel credentials)
        {
            var identityUser = await userManager.FindByNameAsync(credentials.Email);
            if (identityUser != null)
            {
                var result = userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password);
                return (UserModel)(result == PasswordVerificationResult.Failed ? null : identityUser);
            }

            return null;
        }


        private object GenerateToken(UserModel identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddSeconds(jwtBearerTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = jwtBearerTokenSettings.Audience,
                Issuer = jwtBearerTokenSettings.Issuer
            };
            Console.WriteLine("Hello");
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
