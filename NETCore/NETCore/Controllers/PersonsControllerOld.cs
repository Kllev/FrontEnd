using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using NETCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsControllerOld : ControllerBase
    {
        private readonly PersonRepositoryOld personRepository;
        public PersonsControllerOld(PersonRepositoryOld personRepository) {
            this.personRepository = personRepository;
        }

        [HttpPost]
        public ActionResult Insert(Person person) {
            try
            {
                if (personRepository.Insert(person) > 0)
                {
                    //return Ok("Data Berhasil Diinput");
                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        data = personRepository.Get(person.NIK),
                        message = "Data berhasil Di Tambahkan"
                    });

                }
                else if(personRepository.Insert(person) == 0) {
                    return BadRequest(new {status = HttpStatusCode.BadRequest,
                                      message = "NIK tidak boleh kosong"
                                     });
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return BadRequest(new {status = HttpStatusCode.BadRequest,
                                   message = "Data sudah ada"
                             });
            }
            
        [HttpGet]
        public ActionResult Get() {

            return Ok(new
            {
                status = HttpStatusCode.OK,
                data = personRepository.Get(),
                message = "Data berhasil Di tampilkan"
            });
        }

        [HttpGet("{NIK}")]
        public ActionResult GetNIK(string NIK)
        {
            if (personRepository.Get(NIK) != null) {
                
                //return Ok(personRepository.Get(NIK));
                return Ok(new { status = HttpStatusCode.OK, 
                                data =  personRepository.Get(NIK),
                                message = "Data berhasil Di tampilkan"       
                         });
            }
            return NotFound(new
            {
                status = HttpStatusCode.NotFound,
                message = "Data dengan NIK tersebut Tidak Ditemukan"
                //return Ok(personRepository.Get(NIK));
            });

        }
            

        [HttpPut]
        public ActionResult Update(Person person)
        {
            try
            {
                if (personRepository.Update(person) != 0)
                {
                    //return Ok("Data Berhasil di Update");
                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        data = personRepository.Get(person.NIK),
                        message = "Data berhasil Di Update"
                    });
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return NotFound(new
            {
                status = HttpStatusCode.NotFound,
                message = "Data dengan NIK tersebut Tidak Ditemukan"

            });
         }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK) 
        {
            if (personRepository.Get(NIK) != null)
            {
                //personRepository.Delete(NIK);
                //return Ok("Data Berhasil Dihapus");  
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = personRepository.Get(NIK),
                    deletedata = personRepository.Delete(NIK),
                    message = "Data berhasil Di Hapus"
                });
            }
            else
            {
              return NotFound(new { status = HttpStatusCode.NotFound,
                                      message = "Data dengan NIK tersebut Tidak Ditemukan"
              });
            }
            
        }

    }
}
