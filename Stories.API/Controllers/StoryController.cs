using Microsoft.AspNetCore.Mvc;
using Stories.API.Applications.ViewModels;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;

namespace Stories.API.Controllers
{
    public class StoryController : Controller
    {
        private readonly IStoryService _storyService;

        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        // GET: Story
        public async Task<IActionResult> Index()
        {
            var storyDtos = await _storyService.GetAllStoriesAsync();
            var viewModels = storyDtos.Select(dto => new StoryViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Department = dto.Department,
                VotesCount = dto.VotesCount
            });

            return View(viewModels);
        }

        // GET: Story/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var storyDto = await _storyService.GetStoryByIdAsync(id);
            if (storyDto == null)
            {
                return NotFound();
            }

            var viewModel = new StoryViewModel
            {
                Id = storyDto.Id,
                Title = storyDto.Title,
                Description = storyDto.Description,
                Department = storyDto.Department,
                VotesCount = storyDto.VotesCount
            };

            return View(viewModel);
        }

        // GET: Story/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Story/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var storyDto = new StoryDTO
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Department = viewModel.Department,
                    // VotesCount não é usado na criação
                };

                var createdStoryDto = await _storyService.CreateStoryAsync(storyDto);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Story/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var storyDto = await _storyService.GetStoryByIdAsync(id);
            if (storyDto == null)
            {
                return NotFound();
            }

            var viewModel = new StoryViewModel
            {
                Id = storyDto.Id,
                Title = storyDto.Title,
                Description = storyDto.Description,
                Department = storyDto.Department,
                VotesCount = storyDto.VotesCount // Pode ser omitido se não for editável
            };

            return View(viewModel);
        }

        // POST: Story/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StoryViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var storyDto = new StoryDTO
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Department = viewModel.Department,
                    // VotesCount é gerenciado separadamente e não é atualizado aqui
                };

                await _storyService.UpdateStoryAsync(id, storyDto);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Story/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var storyDto = await _storyService.GetStoryByIdAsync(id);
            if (storyDto == null)
            {
                return NotFound();
            }

            var viewModel = new StoryViewModel
            {
                Id = storyDto.Id,
                Title = storyDto.Title,
                Description = storyDto.Description,
                Department = storyDto.Department,
                VotesCount = storyDto.VotesCount
            };

            return View(viewModel);
        }

        // POST: Story/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _storyService.DeleteStoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}