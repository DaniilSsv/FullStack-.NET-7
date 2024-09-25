using AutoMapper;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Services.Interfaces;
using FullstackOpdracht.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FullstackOpdracht.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class StadiumController : ControllerBase
    {
        private IService<Stadium> _stadiumService;
        private readonly IMapper _mapper;

        public StadiumController(IMapper mapper, IService<Stadium> stadium)
        {
            _mapper = mapper;
            _stadiumService = stadium;
        }

        [HttpGet]
        public async Task<ActionResult<StadiumVM>> GetAll()
        {
            try
            {
                var listStadion = await _stadiumService.GetAll();
                var data = _mapper.Map<List<StadiumVM>>(listStadion);
                // Als de gegevens niet worden gevonden, retourneer een 404 Not Found-status
                if (data == null)
                {
                    return NotFound();
                }

                // Retourneer de gegevens als alles goed is verlopen
                // HTTP-statuscode 200
                return Ok(data);
            }
            catch (Exception ex)
            {
                // Als er een fout optreedt, retourneer een 500 Internal Server Error-status
                return StatusCode(500, new { error = ex.Message });
            }
        }

        
    }
}
