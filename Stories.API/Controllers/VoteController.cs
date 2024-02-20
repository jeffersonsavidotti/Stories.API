using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stories.API.Applications.ViewModels;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;

namespace Stories.API.Controllers
{
    public class VoteController : Controller
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        // GET: Vote
        public async Task<IActionResult> Index()
        {
            var voteDtos = await _voteService.GetAllVotesAsync();
            var viewModels = voteDtos.Select(dto => new VoteViewModel
            {
                Id = dto.Id,
                IdStory = dto.IdStory,
                IdUser = dto.IdUser,
                VoteValue = dto.VoteValue
            }).ToList();

            return View(viewModels);
        }

        // GET: Vote/Create
        public IActionResult Create()
        {
            // Pode ser necessário carregar dados adicionais para a view, como listas de histórias e usuários
            return View();
        }

        // POST: Vote/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VoteViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var voteDto = new VoteDTO
                {
                    IdStory = viewModel.IdStory,
                    IdUser = viewModel.IdUser,
                    VoteValue = viewModel.VoteValue
                };

                await _voteService.CreateVoteAsync(voteDto);
                return RedirectToAction(nameof(Index));
            }
            // Em caso de falha, retorne à view de criação com os dados já inseridos
            return View(viewModel);
        }

        // Devido à natureza dos votos, métodos para Edit e Delete não serão implementados neste exemplo.
        // Se necessário, implemente aqui seguindo lógica similar às outras controllers, ajustando conforme a lógica de negócio.
    }
}