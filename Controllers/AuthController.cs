using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using NZWalks.API.Models.DTO;
using NZWalks.API.Data;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase 
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
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
                    //Get roles for this user

                    var userRolesList = await userManager.GetRolesAsync(user); 
                    if (userRolesList != null)
                    {
                    
                        var jwtToken = tokenRepository.CreateJWTToken(user,userRolesList.ToList());
                        
                        var loginResponseDto = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        
                        return Ok(loginResponseDto);

                    }
                
                }

            }


            return BadRequest("Username or password incorrect...");

        }






    }
};