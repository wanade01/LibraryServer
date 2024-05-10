using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using LibraryServer.LibraryModel;
using LibraryServer.DTO;

namespace LibraryServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<LibraryUser> userManager,
        JwtHandler jwtHandler) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            LibraryUser? user = await userManager.FindByNameAsync(loginRequest.UserName);

            if (user == null)
            {
                return Unauthorized("Incorrect username or password");
            }

            bool success = await userManager.CheckPasswordAsync(user, loginRequest.Password);

            if (!success)
            {
                return Unauthorized("Incorrect username or password");
            }

            JwtSecurityToken token = await jwtHandler.GetTokenAsync(user);
            string jwtString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new LoginResult
            {
                Success = true,
                Message = "Mom loves me",
                Token = jwtString
            });
        }
    }
}
