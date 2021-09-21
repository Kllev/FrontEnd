using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using NETCore.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    [Authorize(Roles = "Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<Role, RoleRepository, int>
    {
        public RolesController(RoleRepository role) : base(role)
        {

        }
    }
}
