using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PoorMark.Classes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PoorMark
{
    [Route("api/SQL")]
    public class SQLController : Controller
    {

        // GET api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Json( new FormatterService.Options());
        }

        // POST api/<controller> 
        // note: FromBody == json, FromForm == x-www-url-formencoded - https://andrewlock.net/model-binding-json-posts-in-asp-net-core/
        [HttpPost]
        public IActionResult Post([FromForm] FormatterService.Options options)
        {
            var svc = new FormatterService();
            return Json(svc.FormatTSqlWithOptions(options));
        }

    }
}
