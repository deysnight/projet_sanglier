using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chasse_serv.Controllers
{
    public class BaseDataController : Controller
    {
        [Route("accalist")]
        [HttpGet]
        public ActionResult<string> get_accaList()
        {
            return JsonConvert.SerializeObject(DBConnect.my_select("acca", "name"));
        }
    }
}