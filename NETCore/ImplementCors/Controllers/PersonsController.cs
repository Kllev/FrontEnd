using ImplementCors.Base.Controllers;
using ImplementCors.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementCors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : BaseController<Person, PersonRepository, string>
    {
        PersonRepository personRepository;
        public PersonsController(PersonRepository personRepository) : base(personRepository)
        {
            this.personRepository = personRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllData()
        {
            var result = await personRepository.GetAllProfile();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetById(string nik)
        {
            var result = await personRepository.GetById(nik);
            return Json(result);
        }
    }
}
