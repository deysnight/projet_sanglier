using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace chasse_serv.Controllers
{
    [ApiController]
    public class TestController : Controller
    {
        private IConfiguration _config;

        public TestController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("test/login")]
        public ActionResult Login()
        {
            ActionResult response = Unauthorized();
            Newtonsoft.Json.Linq.JObject login;

            using (var reader = new StreamReader(Request.Body))
            {
                var body = JsonConvert.DeserializeObject(reader.ReadToEnd());
                login = (Newtonsoft.Json.Linq.JObject)body;
            }

            if (!login.ContainsKey("username") && !login.ContainsKey("password"))
            {
                return response;
            }
            //string username = Encoding.UTF8.GetString(Convert.FromBase64String(login["username"].ToString()));
            string username = login["username"].ToString(); //temporaire
            string password = login["password"].ToString();
            
            //Newtonsoft.Json.Linq.JArray result = (Newtonsoft.Json.Linq.JArray)DBConnect.my_select("users WHERE login = '" + username + "' AND pwd = '" + Utils.Encrypt(password) + "'", "*");
            Newtonsoft.Json.Linq.JArray result = (Newtonsoft.Json.Linq.JArray)DBConnect.my_select("users WHERE login = '" + username + "' AND pwd = '" + password + "'", "*"); //temporaire
            Console.WriteLine(result);
            string userID = result[0]["id"].ToString();
            string mail = result[0]["email"].ToString();
            if (result.Count == 1)
            {
                var tokenString = GenerateJSONWebToken(username, userID, mail);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        [HttpGet("test/value")]
        [Authorize]
        public ActionResult Get()
        {
            var currentUser = HttpContext.User;

            if (currentUser.HasClaim(c => c.Type == "username") && currentUser.HasClaim(c => c.Type == "usermail") && currentUser.HasClaim(c => c.Type == "userID"))
            {
                string username = currentUser.Claims.FirstOrDefault(c => c.Type == "username").Value;
                string mail = currentUser.Claims.FirstOrDefault(c => c.Type == "usermail").Value;
                string userID = currentUser.Claims.FirstOrDefault(c => c.Type == "userID").Value;

                return Ok(new { lol = username, lol2 = mail, lol3 = userID});
            }


            //error invalid token
            return Ok(new { lol = "value1", lol2 = "value2", lol3 = new string[] { "value3", "value4", "value5" } });
        }









        private string GenerateJSONWebToken(string username, string userID, string mail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                    new Claim("username", username),
                    new Claim("usermail", mail),
                    new Claim("userID", userID),
                    //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}