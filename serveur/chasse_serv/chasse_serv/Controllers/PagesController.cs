using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chasse_serv.Controllers
{
    public class PagesController : Controller
    {
        [Route("")]
        [HttpGet]
        public ActionResult<string> Home()
        {
            DBConnect.my_insert("index", "test1, test2", "'lol1', 'lol2'");
            return "lol";//Utils.Retrive_file("dashboard.html", "text/html", "html");
        }

        [Route("login")]
        [HttpGet]
        public ActionResult<string> Login()
        {
            return Utils.Retrive_file("login.html", "text/html", "pages");
        }

        [Route("signup")]
        [HttpGet]
        public ActionResult<string> signup()
        {
            return Utils.Retrive_file("signup.html", "text/html", "pages");
        }
    }
}