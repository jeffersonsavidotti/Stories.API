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
            // Para seguir as práticas RESTful mais de perto, considere retornar o recurso criado
            // e o status code 201 Created, ou 204 No Content se o recurso criado não precisar ser retornado.
            return CreatedAtAction(nameof(GetAllVotes), new { id = createdVoteDto.Id }, createdVoteDto);
            // Se você optar por não retornar o recurso, use:
            // return NoContent();
        }

        // Métodos para Edit e Delete não são implementados devido à natureza imutável dos votos.
    }
}

//using Microsoft.AspNetCore.Mvc;
//using Stories.API.Applications.ViewModels;
//using Stories.Services.DTOs;
//using Stories.Services.Interfaces;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Stories.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class VoteController : ControllerBase
//    {
//        private readonly IVoteService _voteService;

//        public VoteController(IVoteService voteService)
//        {
//            _voteService = voteService;
//        }

//        // GET: api/Vote
//        [HttpGet]
//        public async Task<IActionResult> GetAllVotes()
//        {
//            var voteDtos = await _voteService.GetAllVotesAsync();
//            var viewModels = voteDtos.Select(dto => new VoteViewModel
//            {
//                Id = dto.Id,
//                IdStory = dto.IdStory,
//                IdUser = dto.IdUser,
//                VoteValue = dto.VoteValue
//            }).ToList();

//            return Ok(viewModels);
//        }

//        // POST: api/Vote
//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] VoteViewModel viewModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var voteDto = new VoteDTO
//            {
//                IdStory = viewModel.IdStory,
//                IdUser = viewModel.IdUser,
//                VoteValue = viewModel.VoteValue
//            };

//            var createdVoteDto = await _voteService.CreateVoteAsync(voteDto);
//            // Considerando que a criação de um voto pode não necessitar de um retorno do objeto criado,
//            // uma resposta com StatusCode 201 (Created) pode ser mais apropriada se o objeto for retornado,
//            // ou 204 (No Content) se não for retornado nenhum conteúdo.
//            return NoContent(); // Ou `CreatedAtAction` se estiver retornando o voto criado
//        }

//        // Métodos para Edit e Delete não são implementados neste exemplo devido à natureza imutável dos votos.
//        // Se a lógica de negócio permitir a edição ou remoção de votos, os métodos correspondentes devem ser implementados seguindo o padrão REST.
//    }
//}
