using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ImplementCors.Base.Urls;
using Microsoft.AspNetCore.Http;
using NETCore.Models;
using NETCore.ViewModel;
using Newtonsoft.Json;

namespace ImplementCors.Repositories.Data
{
    public class PersonRepository : GeneralRepository<Person, string>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        public PersonRepository(Address address, string request = "Persons/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<RegisterVM>> GetAllProfile()
        {
            List<RegisterVM> registers = new List<RegisterVM>();

            using (var response = await httpClient.GetAsync(request + "GetAllProfile"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                registers = JsonConvert.DeserializeObject<List<RegisterVM>>(apiResponse);
            }
            return registers;
        }

        public async Task<RegisterVM> GetById(string nik)
        {
            RegisterVM register = new RegisterVM();

            using (var response = await httpClient.GetAsync(request + "GetById/" + nik))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                register = JsonConvert.DeserializeObject<RegisterVM>(apiResponse);
            }
            return register;
        }

        public string Register(RegisterVM registerVM)
            {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(request + "Register", content).Result.Content.ReadAsStringAsync().Result;

            return response;
        }
    }
}
