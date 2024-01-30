using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using NZWalks.API.Models.DTO;
using NZWalks.API.Data;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase 
    {

        private readonly UserManager<IdentityUser> userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] NewRegistrationTemplateDto newRegistrationTemplateDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = newRegistrationTemplateDto.Username,
                Email = newRegistrationTemplateDto.Username
            };
            
            var identityResult = await userManager.CreateAsync(identityUser, newRegistrationTemplateDto.Password);

            if (identityResult.Succeeded)
            {
                //Add API-Access role to User
                if (newRegistrationTemplateDto.Roles != null && newRegistrationTemplateDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser,newRegistrationTemplateDto.Roles);
                    
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login...");
                    }

                }


            }

            return BadRequest("Something went wrong!");


        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {

            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {

                var loginMatchesExistingAccount = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (loginMatchesExistingAccount)
                {

                    return Ok();

                }

            }


            return BadRequest("Username or password incorrect...");

        }






    }
};