using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BearerToken.Controllers
{
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private IConfiguration _configuraiton;
        public TokenController(IConfiguration configuration)
        {
            _configuraiton = configuration;
        }
        [HttpGet]
        public IActionResult CreateToken(string username = "admin", string password = "admin")
        {
            IActionResult objResponse = Unauthorized();

            if (username.Equals(password))
            {
                // Create JWT token here 

                var jwtToken = JwtTokenBuilder();
                objResponse = Ok(new { token = jwtToken });
            }

            return objResponse;
        }


        private string JwtTokenBuilder()
        {
            //prepare key and credentials

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuraiton["JWT:key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(issuer: _configuraiton["JWT:issuer"],
                audience: _configuraiton["JWT:audience"], signingCredentials: credential,
                expires: DateTime.Now.AddMinutes(40));

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}