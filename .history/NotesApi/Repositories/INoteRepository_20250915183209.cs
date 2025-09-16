using NotesApi.Models;

namespace NotesApi.Repositories
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllAsync(int userId, string? search, string? sortBy, bool ascending);
        Task<Note?> GetByIdAsync(int id, int userId);
        Task<int> CreateAsync(NoteCreateDto dto, int userId);
        Task<bool> UpdateAsync(int id, NoteUpdateDto dto, int userId);
        Task<bool> DeleteAsync(int id, int userId);
    }
}
