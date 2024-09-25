using AutoMapper;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Services.Interfaces;
using FullstackOpdracht.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FullstackOpdracht.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IService<AspNetUser> _userService;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper, IService<AspNetUser> userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<UserVM>> GetAll()
        {
            try
            {
                var listUser = await _userService.GetAll();
                var data = _mapper.Map<List<UserVM>>(listUser);
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
