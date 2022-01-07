using Microsoft.AspNetCore.Mvc;
using OSK_App.DbContexts;
using OSK_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Controllers
{
    [Route("/Search")]
    public class SearchController : Controller
    {
        private readonly ApplicationContext context;

        public SearchController(ApplicationContext context) {
            this.context = context;
        }

        [HttpGet]
        [Route("GetName")]
        public JsonResult GetName(string name) {
            List<string> persons = new List<string>();
            var names = context.users.Where(q => q.FirstName.Substring(0, name.Length) == name ||
                q.Surname.Substring(0, name.Length) == name).ToList();

            foreach (var n in names) {
                persons.Add(n.Surname + " " + n.FirstName);
            }

            return new JsonResult(persons);
        }

        [Produces("application/json")]
        [HttpGet("SearchNames")]
        //[Route]
        public async Task<IActionResult> SearchNames() {

            try {
                List<string> persons = new List<string>();
                string term = HttpContext.Request.Query["term"].ToString();
                var names = context.users.Where(q =>q.Surname.Contains(term)).
                    Select(o=>new User{ Surname = o.Surname}).ToList();
                foreach (var n in names) {
                    persons.Add(n.Surname);
                }
                return Ok(persons);
            }
            catch (Exception) {
                return BadRequest();
            }
            

            /*List<string> persons = new List<string>();
            var names = context.users.Where(q => q.FirstName.Substring(0, name.Length) == name ||
                q.Surname.Substring(0, name.Length) == name).ToList();

            foreach (var n in names) {
                persons.Add(n.Surname + " " + n.FirstName);
            }

            return new JsonResult(persons);*/
        }


    }
}
