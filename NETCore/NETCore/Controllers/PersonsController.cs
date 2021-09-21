using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using NETCore.Repository.Data;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : BaseController<Person, PersonRepository, string>
    {
        PersonRepository repository;
        public PersonsController(PersonRepository person) : base(person) {
            this.repository = person;
        }

        [EnableCors("AllowAllOrigins")]
        [HttpPost("Register")]
        public ActionResult Register(RegisterVM register) {
            var regis = repository.Register(register);
            /*
            if (regis > 0)
            {
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    result = repository.Get(register.NIK),
                    message = "Data Berhasil Di tambah"
                });
            }
            else
            {
                return BadRequest("Data Sudah Ada");
            }*/

            try {
                if (regis == 100)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Email Sudah Terdaftar"
                    });
                }
                else if (regis == 200)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "NIK Sudah Terdaftar"
                    });
                }
                else if (regis == 300) {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Nomor Telepon Sudah Terdaftar"
                    });
                }
            } catch { 
            
            }
            return Ok(new
            {
                status = HttpStatusCode.OK,
                result = repository.Get(register.NIK),
                message = "Data Berhasil Di tambah"
            });
        }

        [EnableCors("AllowAllOrigins")]
        [HttpGet("GetAllProfile")]
        public ActionResult AllProfile() {

            var get = repository.GetAllProfile();
            if (get != null)
            {
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = repository.Get(),
                    message = "Data berhasil Di tampilkan"
                });
            }
            
                return NotFound("Tidak ada Data");
            
        }

        [HttpGet("GetById/{nik}")]
        public ActionResult GetById(string nik)
        {
            var get = repository.GetById(nik);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("Tidak Ada Data");
            }
        }
    }
}
