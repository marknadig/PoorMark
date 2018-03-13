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
        public ContentResult Get()
        {
            // simple test
            var svc = new FormatterService();
            return Content(svc.FormatTSql("Select * From em"), System.Net.Mime.MediaTypeNames.Text.Html);
        }

        // POST api/<controller> 
        // note: FromBody == json, FromForm == x-www-url-formencoded - https://andrewlock.net/model-binding-json-posts-in-asp-net-core/
        [HttpPost]
        public ContentResult Post([FromForm] FormatterService.Options options)
        {
            var svc = new FormatterService();
            return Content(svc.FormatTSqlWithOptions(options), System.Net.Mime.MediaTypeNames.Text.Html);
        }

        // PUT api/<controller> 
        [HttpPut]
        public IActionResult Put([FromForm] FormatterService.Options options)
        {
            // PUT to see how options deserialized (including defaults)
            return Json(options);
        }

    }
}
