using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chasse_serv.Controllers
{
    public class LoginController : Controller
    {
        [Route("internal/login/{data}")]
        [HttpGet]
        public ActionResult<string> Login(string data)
        {
            return data;
        }

        [Route("internal/signup/{data}")]
        [HttpGet]
        public ActionResult<string> signup(string data)
        {
            return data;
        }
    }
}