    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.data;
using VillaAPI.Models;
using VillaAPI.Models.DTO;

namespace VillaAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()

        {
            return Ok(VillaStore.villaList);
        }


        [HttpGet("id",Name ="GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public ActionResult<VillaDTO> GetVilla(int id)

        {
            if(id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

            if(villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villadto)
        {
            if (villadto == null)
            {
                return BadRequest();
            }

            if(villadto.Id > 0)
            {
                return BadRequest();
            }

            if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villadto.Name.ToLower())!=null)
            {
                ModelState.AddModelError("Custom Error", "Villa Name Is Already Taken !");
                return BadRequest(ModelState);
            }
            villadto.Id = VillaStore.villaList.OrderByDescending(u=>u.Id).FirstOrDefault().Id+1;
            VillaStore.villaList.Add(villadto);
            return CreatedAtRoute("GetVilla",new {id = villadto.Id},villadto);

        }
    }
}
