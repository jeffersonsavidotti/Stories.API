using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;

namespace Stories.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IVoteService _voteService;

        public VotesController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        [HttpPost]
        public async Task<IActionResult> AddVote(VoteDTO voteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _voteService.AddVoteAsync(voteDTO);
            if (result)
            {
                return Ok("Voto registrado com sucesso.");
            }
            else
            {
                return BadRequest("Não foi possível registrar o voto.");
            }
        }

        // Se você quiser listar votos por história, aqui vai um exemplo:
        [HttpGet("{storyId}")]
        public async Task<IActionResult> GetVotesByStory(int storyId)
        {
            var votes = await _voteService.GetVoteByStoryAsync(storyId);
            if (votes == null)
            {
                return NotFound();
            }

            return Ok(votes);
        }
    }
}