using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Week7.Security;

namespace Week7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = _userRepository.GetByEmail(model.Email);
                if(user != null)
                {
                    if(user.PasswordHash == model.Password)
                    {
                        var jwt = new Util(_configuration);
                        var token = jwt.GenerateJWT(user);
                        return Ok(token);
                    }
                    return BadRequest("Invalid credential for password");
                }
                return BadRequest("Invalid credential for email");

            }catch(Exception ex)
            {
                return BadRequest();
            }
        }

        
    }
}
