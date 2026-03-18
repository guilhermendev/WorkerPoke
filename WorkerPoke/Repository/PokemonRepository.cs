using Dapper;
using MySql.Data.MySqlClient;

public class PokemonRepository
{
    private readonly string _connectionString = "Server=localhost;Database=PokemonDB;Uid=root;Pwd=(11)959755565;";

    public async Task Insert(Pokemon pokemon)
    {
        using var connection = new MySqlConnection(_connectionString);

        string sql = @"INSERT IGNORE INTO Pokemon (PokemonId, Nome, Url)
                       VALUES (@PokemonId, @Nome, @Url)";

        await connection.ExecuteAsync(sql, pokemon);
    }
}