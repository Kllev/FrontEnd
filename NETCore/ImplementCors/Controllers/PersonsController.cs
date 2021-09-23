using ImplementCors.Base.Controllers;
using ImplementCors.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementCors.Controllers
{
    [Route("[controller]")]
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

        [HttpGet("GetAllData")]
        public async Task<JsonResult> GetAllData()
        {
            var result = await personRepository.GetAllProfile();
            return Json(result);
        }

        [HttpGet("GetById/{nik}")]
        public async Task<JsonResult> GetById(string nik)
        {
            var result = await personRepository.GetById(nik);
            return Json(result);
        }

        [HttpPost("RegisterData/")]
        public JsonResult RegisterData([FromBody]RegisterVM register)
        {
            var result = personRepository.Register(register);
            return Json(result);
        }
    }
}
