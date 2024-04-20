using CountryModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using weather_server.DTO;

namespace weather_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<WorldCitiesUser> userManager, JwtHandler jwtHandler) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            WorldCitiesUser? user = await userManager.FindByNameAsync(loginRequest.Username);
            if (user == null)
            {
                return Unauthorized("Wrong username >:(");
            }

            bool success = await userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!success)
            {
                return Unauthorized("Wrong password >:(");
            }

            JwtSecurityToken token = await jwtHandler.GetTokenAsync(user);
            string jwtAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new LoginResult
            { 
                Success = true,
                Message = "Mom loves me >:)",
                Token = jwtAsString
            });
        }
    }
}
