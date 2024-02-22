using Microsoft.AspNetCore.Mvc;
using Stories.API.Applications.ViewModels;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Stories.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        // GET: api/Vote
        [HttpGet]
        public async Task<IActionResult> GetAllVotes()
        {
            var voteDtos = await _voteService.GetAllVotesAsync();
            var viewModels = voteDtos.Select(dto => new VoteViewModel
            {
                Id = dto.Id,
                IdStory = dto.IdStory,
                IdUser = dto.IdUser,
                VoteValue = dto.VoteValue
            }).ToList();

            return Ok(viewModels);
        }

        // POST: api/Vote
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var voteDto = new VoteDTO
            {
                IdStory = viewModel.IdStory,
                IdUser = viewModel.IdUser,
                VoteValue = viewModel.VoteValue
            };

            var createdVoteDto = await _voteService.CreateVoteAsync(voteDto);

            // Retornando o voto criado com o status 201 Created
            // Note que, se sua API não suporta a recuperação de um voto específico, 
            // você pode optar por retornar NoContent() como alternativa.
            return CreatedAtAction(nameof(GetAllVotes), new { id = createdVoteDto.Id }, createdVoteDto);
        }

        // Métodos para Edit e Delete não são implementados neste exemplo devido à natureza imutável dos votos.
    }
}
