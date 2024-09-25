using AutoMapper;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Services;
using FullstackOpdracht.Services.Interfaces;
using FullstackOpdracht.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FullstackOpdracht.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ExtendedMatchService _matchService;

        public MatchController(IMapper mapper, ExtendedMatchService matchService)
        {
            _matchService = matchService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<MatchVM>> GetMatchesBetweenTeams(int team1, int team2)
        {
            try
            {
                var listMatches = await _matchService.GetMatchesBetweenTeams(team1, team2);
                var data = _mapper.Map<List<MatchVM>>(listMatches);
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
