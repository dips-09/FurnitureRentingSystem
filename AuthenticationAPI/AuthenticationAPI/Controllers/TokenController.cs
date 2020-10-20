using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TokenController));
        private RentingContext context;

        public TokenController(IConfiguration config,RentingContext _context)
        {
            _config = config;
            context = _context;
        }

        

        [HttpPost]
        public IActionResult Login([FromBody] RentingUser login)
        {
            _log4net.Info("Login initiated!");
            IActionResult response = Unauthorized();
            //login.FullName = "user1";
            var user = AuthenticateUser(login);
            if(user == null)
            {
                return NotFound();
            }
            else
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        public string GenerateJSONWebToken(RentingUser userInfo)
        {
            _log4net.Info("Token Is Generated!");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);



            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        public RentingUser AuthenticateUser(RentingUser login)
        {
            //RentingContext context = new RentingContext();
            //RentingUser usr = null;
            _log4net.Info("Validating the Vendor!");

            

            //Validate the User Credentials 
            RentingUser usr = context.users.FirstOrDefault(c => c.UserId == login.UserId && c.Password == login.Password);


            return usr;
        }

        
    }
}
