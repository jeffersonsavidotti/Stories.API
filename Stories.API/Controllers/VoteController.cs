using Microsoft.AspNetCore.Mvc;
using Stories.API.Applications.ViewModels;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;

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

        /// <summary>
        /// Obtem todos os votos.
        /// </summary>
        /// <response code="200">Lista de votos retornada com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VoteViewModel>), 200)]
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

        /// <summary>
        /// Cria um novo voto.
        /// </summary>
        /// <param name="viewModel">Dados do voto a ser criado.</param>
        /// <response code="201">Voto criado com sucesso.</response>
        /// <response code="400">Dados inválidos fornecidos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(VoteDTO), 201)]
        [ProducesResponseType(400)]
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

            return CreatedAtAction(nameof(GetAllVotes), new { id = createdVoteDto.Id }, createdVoteDto);
        }
    }
}
