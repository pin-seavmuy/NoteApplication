using NotesApi.Models;

namespace NotesApi.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User> CreateAsync(User user);
    }
}
