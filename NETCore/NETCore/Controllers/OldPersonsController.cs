using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using NETCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldPersonsController : ControllerBase
    {
        private readonly OldPersonsRepository personsRepository;
        public OldPersonsController(OldPersonsRepository personsRepository)
        {
            this.personsRepository = personsRepository;
        }
        [HttpPost]
        public ActionResult Insert(Person persons)
        {
            try
            {
                if(personsRepository.Insert(persons) > 0)
                {
                    return Ok("Data Berhasil Ditambahkan");
                }
                else if(personsRepository.Insert(persons) == 0)
                {
                    return BadRequest("Data Tidak Lengkap");
                }
                else
                {
                    return BadRequest("Data Sudah Ada");
                }
            }
            catch (Exception)
            {
                return BadRequest("Data Sudah Ada");
            }
            //personsRepository.Insert(persons);
            //return BadRequest("Data Sudah Ada");
        }
        [HttpGet]

        public ActionResult Get()
        {
            return Ok(personsRepository.Get());
        }

        [HttpGet("{NIK}")]

        public ActionResult Get(string NIK)
        {
            var finding = personsRepository.Get(NIK);
            if(finding == null)
            {
                return NotFound("Ga Ketemu Coy!!!!!");
            }
            return Ok(personsRepository.Get(NIK));
        }

        [HttpPut]

        public ActionResult Update(Person persons)
        {
            if (personsRepository.Update(persons) == 0)
            {
                return NotFound("Ga Ketemu Coy!!!!!");
            }
           personsRepository.Update(persons);
           return Ok("Data Berhasil Terupdate");
        }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            personsRepository.Delete(NIK);
            return Ok("Data Berhasil Dihapus");
        }
    }
}
