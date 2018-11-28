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
            //DBConnect.my_insert("index", "test1, test2", "'lol1', 'lol2'");
            //Response.Redirect("/login");
            //Utils.Retrive_file("dashboard.html", "text/html", "html");

            if (Utils.user_logged(Request) != true)
                Response.Redirect("/login");
            else
                return "lol";
            return "";
        }

        [Route("login")]
        [HttpGet]
        public ActionResult<string> Login()
        {
            if (Utils.user_logged(Request) == true)
                Response.Redirect("/");
            else
                return Utils.Retrive_file("login.html", "text/html", "pages");
            return "";
        }

        [Route("signup")]
        [HttpGet]
        public ActionResult<string> signup()
        {
            if (Utils.user_logged(Request) == true)
                Response.Redirect("/");
            else
                return Utils.Retrive_file("signup.html", "text/html", "pages");
            return "";
        }
    }
}