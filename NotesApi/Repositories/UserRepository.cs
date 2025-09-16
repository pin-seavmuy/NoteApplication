using System.Data;
using Dapper;
using MySql.Data.MySqlClient; 
using Microsoft.Extensions.Configuration;
using NotesApi.Models;

namespace NotesApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        private IDbConnection CreateConnection() => new MySqlConnection(_connectionString); 

        public async Task<User?> GetByUsernameAsync(string username)
        {
            using var db = CreateConnection();
            var sql = "SELECT * FROM Users WHERE Username = @Username";
            return await db.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });
        }

        public async Task<User> CreateAsync(User user)
        {
            using var db = CreateConnection();
            var sql = @"
                INSERT INTO Users (Username, PasswordHash)
                VALUES (@Username, @PasswordHash);
                SELECT LAST_INSERT_ID();"; 
            
            var newId = await db.ExecuteScalarAsync<int>(sql, user);
            user.Id = newId;
            return user;
        }
    }
}
