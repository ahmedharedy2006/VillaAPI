    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.Models;

namespace VillaAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Villa> GetVillas()
        {
            return new List<Villa>()
            {
                new Villa() {Id = 1 , Name ="Pool View"},
                new Villa() {Id = 2 , Name="Beach View"}
            };
        }
    }
}
