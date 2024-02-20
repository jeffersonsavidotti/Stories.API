using Microsoft.AspNetCore.Mvc;
using Stories.API.Applications.ViewModels;
using Stories.Infrastructure.Models;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;

namespace Stories.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var userDtos = await _userService.GetAllUsersAsync();
            var viewModels = userDtos.Select(dto => new UserViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                VotesCount = dto.VotesCount
            }).ToList();

            return View(viewModels);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto == null)
            {
                return NotFound();
            }

            var viewModel = new UserViewModel
            {
                Id = userDto.Id,
                Name = userDto.Name,
                VotesCount = userDto.VotesCount
            };

            return View(viewModel);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userDto = new UserDTO
                {
                    Name = viewModel.Name,
                    // VotesCount não é usado na criação
                };

                var createdUserDto = await _userService.CreateUserAsync(userDto);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto == null)
            {
                return NotFound();
            }

            var viewModel = new UserViewModel
            {
                Id = userDto.Id,
                Name = userDto.Name,
                VotesCount = userDto.VotesCount // Pode ser omitido se não for editável
            };

            return View(viewModel);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserViewModel viewModel)
        {
            if (id != viewModel.Id || !ModelState.IsValid)
            {
                return View(viewModel);
            }

            var userDto = new UserDTO
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                // VotesCount é gerenciado separadamente e não é atualizado aqui
            };

            await _userService.UpdateUserAsync(id, userDto);
            return RedirectToAction(nameof(Index));
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto == null)
            {
                return NotFound();
            }

            var viewModel = new UserViewModel
            {
                Id = userDto.Id,
                Name = userDto.Name,
                VotesCount = userDto.VotesCount
            };

            return View(viewModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
