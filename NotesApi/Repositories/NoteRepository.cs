using Dapper;
using NotesApi.Models;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace NotesApi.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly string _connectionString;
        public NoteRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

        public async Task<IEnumerable<Note>> GetAllAsync(int userId, string? search, string? sortBy, bool ascending)
        {
            using var db = CreateConnection();
            var sql = @"SELECT * FROM Notes WHERE UserId = @UserId";
            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND Title LIKE @Search";
            }

            string sortColumn = "CreatedAt";
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("title", StringComparison.OrdinalIgnoreCase))
                    sortColumn = "Title";
                else if (sortBy.Equals("updatedat", StringComparison.OrdinalIgnoreCase))
                    sortColumn = "UpdatedAt";
            }

            sql += $" ORDER BY {sortColumn} {(ascending ? "ASC" : "DESC")}";

            return await db.QueryAsync<Note>(sql, new
            {
                UserId = userId,
                Search = $"%{search}%"
            });
        }

        public async Task<Note?> GetByIdAsync(int id, int userId)
        {
            using var db = CreateConnection();
            string sql = "SELECT * FROM Notes WHERE Id = @Id AND UserId = @UserId";
            return await db.QueryFirstOrDefaultAsync<Note>(sql, new { Id = id, UserId = userId });
        }

        public async Task<int> CreateAsync(NoteCreateDto dto, int userId)
        {
            using var db = CreateConnection();
            string sql = @"
              INSERT INTO Notes (UserId, Title, Content, CreatedAt)
              VALUES (@UserId, @Title, @Content, CURRENT_TIMESTAMP);
              SELECT LAST_INSERT_ID();";
            int newId = await db.ExecuteScalarAsync<int>(sql, new
            {
                UserId = userId,
                Title = dto.Title,
                Content = dto.Content
            });
            return newId;
        }

        public async Task<bool> UpdateAsync(int id, NoteUpdateDto dto, int userId)
        {
            using var db = CreateConnection();
            string sql = @"
              UPDATE Notes
              SET Title = @Title, Content = @Content, UpdatedAt = CURRENT_TIMESTAMP
              WHERE Id = @Id AND UserId = @UserId";
            int rows = await db.ExecuteAsync(sql, new
            {
                Id = id,
                UserId = userId,
                Title = dto.Title,
                Content = dto.Content
            });
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            using var db = CreateConnection();
            string sql = "DELETE FROM Notes WHERE Id = @Id AND UserId = @UserId";
            int rows = await db.ExecuteAsync(sql, new { Id = id, UserId = userId });
            return rows > 0;
        }
    }
}
