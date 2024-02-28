using Stories.Services.DTOs;

namespace Stories.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateUserAsync(UserDTO userDto);
        Task<UserDTO> GetUserByIdAsync(Guid id);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> UpdateUserAsync(Guid id, UserDTO userDto);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
