using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VillaAPI.data;
using VillaAPI.Models;
using VillaAPI.Models.DTO;
using VillaAPI.Rebository.Interfaces;

namespace VillaAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VillaV2Controller : ControllerBase
    {
       
        [HttpGet]
        public IEnumerable<string> GetVillasV2() { 
            return new string[] { "value1", "value2" };
        }

     

    }
}
