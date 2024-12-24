using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaAPI.data;
using VillaAPI.Models;
using VillaAPI.Models.DTO;
using VillaAPI.Rebository.Interfaces;

namespace VillaAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVillaRepository _villaRepo;
        public VillaController(IVillaRepository villaRepo , IMapper mapper) 
        {
            _villaRepo = villaRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villaList = await _villaRepo.GetAllAsync();

            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
        }


        [HttpGet("{id:int}" , Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var villa = await _villaRepo.GetAsync(u => u.Id == id);

            if(villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaCreateDTO>> CreateVilla([FromBody] VillaCreateDTO villaCreateDTO)
        {
            if(await _villaRepo.GetAsync(u => u.Name.ToLower() == villaCreateDTO.Name) != null)
            {
                ModelState.AddModelError("", "Villa Already Exists !");
                return BadRequest(ModelState);
            }
            if(villaCreateDTO == null)
            {
                return BadRequest(villaCreateDTO);
            }

            Villa model = _mapper.Map<Villa>(villaCreateDTO);

            await _villaRepo.CreateAsync(model);

            return CreatedAtRoute("GetVilla" , new {id = model.Id }, model);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            var Villa = await _villaRepo.GetAsync(u => u.Id == id);
            if(Villa == null)
            {
                return NotFound();
            }
            await _villaRepo.RemoveAsync(Villa);

           return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateVilla(int id , [FromBody] VillaUpdateDTO villaupdateDTO)
        {
            if(villaupdateDTO == null || id != villaupdateDTO.Id)
            {
                return BadRequest();
            }

            var model = _mapper.Map<Villa>(villaupdateDTO);

            await _villaRepo.UpdateAsync(model);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePartialVilla(int id , JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await _villaRepo.GetAsync(u => u.Id == id);

            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

            if(villa == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(villaDTO , ModelState);

            Villa model = _mapper.Map<Villa>(villaDTO);

            _villaRepo.UpdateAsync(model);

            if(!ModelState.IsValid)
            {
                return BadRequest();    
            }

            return NoContent();
        }

    }
}
