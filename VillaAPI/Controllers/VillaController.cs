using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaAPI.data;
using VillaAPI.Models;
using VillaAPI.Models.DTO;

namespace VillaAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public VillaController(AppDbContext context , IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()

        {
            IEnumerable<Villa> villaList = await _context.Villas.ToListAsync();

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

            var villa = await _context.Villas.FirstOrDefaultAsync(u => u.Id == id);

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
            if(_context.Villas.FirstOrDefault(u => u.Name.ToLower() == villaCreateDTO.Name) != null)
            {
                ModelState.AddModelError("", "Villa Already Exists !");
                return BadRequest(ModelState);
            }
            if(villaCreateDTO == null)
            {
                return BadRequest(villaCreateDTO);
            }

            Villa model = _mapper.Map<Villa>(villaCreateDTO);

            _context.Villas.Add(model);

            await _context.SaveChangesAsync();

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

            var Villa = _context.Villas.FirstOrDefault(u => u.Id == id);
            if(Villa == null)
            {
                return NotFound();
            }
            _context.Villas.Remove(Villa);

           await _context.SaveChangesAsync();
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

            _context.Villas.Update(model);

            await _context.SaveChangesAsync();

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

            var villa = await _context.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

            if(villa == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(villaDTO , ModelState);

            Villa model = _mapper.Map<Villa>(villaDTO);

            _context.Villas.Update(model);

            await _context.SaveChangesAsync();

            if(!ModelState.IsValid)
            {
                return BadRequest();    
            }

            return NoContent();
        }
    }
}
