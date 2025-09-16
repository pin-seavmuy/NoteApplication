using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Text;

// Models and DTOs
public class Note
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class NoteCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
}

public class NoteUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
}

// Interface
public interface INoteRepository
{
    Task<IEnumerable<Note>> GetAllAsync(int userId, string? search, string? sortBy, bool ascending);
    Task<Note?> GetByIdAsync(int id, int userId);
    Task<int> CreateAsync(NoteCreateDto dto, int userId);
    Task<bool> UpdateAsync(int id, NoteUpdateDto dto, int userId);
    Task<bool> DeleteAsync(int id, int userId);
}

// Implementation
public class NoteRepository : INoteRepository
{
    private readonly string _connectionString;

    public NoteRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

    public async Task<IEnumerable<Note>> GetAllAsync(int userId, string? search, string? sortBy, bool ascending)
    {
        using var db = CreateConnection();
        var query = new StringBuilder("SELECT * FROM Notes WHERE UserId = @UserId");

        if (!string.IsNullOrWhiteSpace(search))
        {
            query.Append(" AND Title LIKE @Search");
        }

        if (!string.IsNullOrWhiteSpace(sortBy) &&
            (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase) ||
             sortBy.Equals("CreatedAt", StringComparison.OrdinalIgnoreCase) ||
             sortBy.Equals("UpdatedAt", StringComparison.OrdinalIgnoreCase)))
        {
            query.Append($" ORDER BY {sortBy} {(ascending ? "ASC" : "DESC")}");
        }
        else
        {
            query.Append(" ORDER BY CreatedAt DESC");
        }

        return await db.QueryAsync<Note>(query.ToString(), new
        {
            UserId = userId,
            Search = $"%{search}%"
        });
    }

    public async Task<Note?> GetByIdAsync(int id, int userId)
    {
        using var db = CreateConnection();
        var query = "SELECT * FROM Notes WHERE Id = @Id AND UserId = @UserId";
        return await db.QueryFirstOrDefaultAsync<Note>(query, new { Id = id, UserId = userId });
    }

    public async Task<int> CreateAsync(NoteCreateDto dto, int userId)
    {
        using var db = CreateConnection();
        var query = @"INSERT INTO Notes (UserId, Title, Content, CreatedAt)
                      VALUES (@UserId, @Title, @Content, CURRENT_TIMESTAMP);
                      SELECT LAST_INSERT_ID();";

        return await db.ExecuteScalarAsync<int>(query, new
        {
            UserId = userId,
            Title = dto.Title,
            Content = dto.Content
        });
    }

    public async Task<bool> UpdateAsync(int id, NoteUpdateDto dto, int userId)
    {
        using var db = CreateConnection();
        var query = @"UPDATE Notes 
                      SET Title = @Title, Content = @Content, UpdatedAt = CURRENT_TIMESTAMP
                      WHERE Id = @Id AND UserId = @UserId";

        var result = await db.ExecuteAsync(query, new
        {
            Id = id,
            UserId = userId,
            Title = dto.Title,
            Content = dto.Content
        });

        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        using var db = CreateConnection();
        var query = "DELETE FROM Notes WHERE Id = @Id AND UserId = @UserId";
        var result = await db.ExecuteAsync(query, new { Id = id, UserId = userId });
        return result > 0;
    }
}
