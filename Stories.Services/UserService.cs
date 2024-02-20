using Microsoft.EntityFrameworkCore;
using Stories.Infrastructure.Models;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;

namespace Stories.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                // Votes são gerenciados separadamente
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            userDto.Id = user.Id; // Atualize o ID após salvar
            return userDto;
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new UserDTO(u)) // Supondo que você tenha um construtor adequado em UserDTO
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _context.Users
                .Select(u => new UserDTO(u)) // Supondo que você tenha um construtor adequado em UserDTO
                .ToListAsync();

            return users;
        }

        public async Task<UserDTO> UpdateUserAsync(int id, UserDTO userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null; // Ou lance uma exceção adequada
            }

            user.Name = userDto.Name;
            // Não atualize Votes aqui

            await _context.SaveChangesAsync();

            return userDto; // Pode querer retornar uma nova consulta ao DTO para atualizações derivadas do banco de dados
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
