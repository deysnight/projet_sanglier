using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chasse_serv.Controllers
{
    public class LoginController : Controller
    {
        [Route("internal/login/{data}")]
        [HttpGet]
        public ActionResult<string> Login(string data)
        {
            return "OK LOGIN";
        }

        [Route("internal/signup/{data}")]
        [HttpGet]
        public ActionResult<string> signup(string data)
        {
            string decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(Uri.UnescapeDataString(data)));
            string[] tmp = decodedString.Split(':');
            var result = (Newtonsoft.Json.Linq.JArray)DBConnect.my_select("users WHERE login = '" + tmp[0] + "' AND email = '" + tmp[1] + "'", "*");
            
            if (result.Count == 0)
            {
                DBConnect.my_insert("users", "login, email, pwd", "'" + tmp[0] + "', '" + tmp[1] + "', '" + Utils.Encrypt(tmp[2]) + "'");

                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddHours(6);
                //option.Expires = DateTime.Now.AddMinutes(1);
                Response.Cookies.Append("dot_user", tmp[0], option);
                Response.Cookies.Append("logged_in", "true", option);
                return "OK USER REGISTER";
            }
            else
            {
                return "KO LOGIN";
            }


            return data;
        }
    }
}

/*  CREATE COOKIE
 CookieOptions option = new CookieOptions();

            int? expireTime = 2;
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append("bite", "COLOSSALE", option);
*/


/*  HEADER TO JSON
        read header as json
       var re = Request;
       var headers = re.Headers;
       Dictionary<string, string> ss = Request.Headers.ToDictionary(a => a.Key, a => string.Join(";", a.Value));
       return JsonConvert.SerializeObject(ss, Formatting.Indented);
*/

/* COOKIE TO JSON
Dictionary<string, string> ss = Request.Cookies.ToDictionary(a => a.Key, a => string.Join(";", a.Value));
            return JsonConvert.SerializeObject(ss, Formatting.Indented);
*/

/* GET COOKIE'S VALUE
            string cookieValueFromReq;
            if (Request.Cookies["bite"] != null)
                cookieValueFromReq = Request.Cookies["bite"];
            else
                cookieValueFromReq = "lol";
            return cookieValueFromReq;
*/
