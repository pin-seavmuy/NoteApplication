using Microsoft.Data.SqlClient;

private IDbConnection CreateConnection() => new SqlConnection(_connectionString);
