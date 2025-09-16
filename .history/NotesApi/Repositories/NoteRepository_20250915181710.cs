using Microsoft.Data.SqlClient;

private IDbConnection CreateConnection() => new SqlConnection(_connectionString);
using MySql.Data.MySqlClient;  // <-- add this

private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);  
