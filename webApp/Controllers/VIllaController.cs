using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VillaAPI.Models.DTO;
using webApp.Models;
using webApp.Services;

namespace webApp.Controllers
{
    public class VIllaController : Controller
    {
        private readonly IVillaService _villaService;

        private readonly IMapper _mapper;
        public VIllaController(IVillaService villaService , IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDTO> villaList = new();
                
            var response = await _villaService.GetAllAsync<APIResponse>();
            if(response != null && response.IsSuccess)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }

            return View(villaList);
        }
    }
}
