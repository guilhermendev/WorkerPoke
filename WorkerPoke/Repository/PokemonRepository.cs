using Dapper;
using MySql.Data.MySqlClient;
using WorkerPoke.Models;

namespace WorkerPoke.Repository;

public class PokemonRepository
{
    private readonly string _connectionString = "Server=localhost;Database=PokemonDB;Uid=root;Pwd=1234";

    public async Task Insert(Pokemon pokemon)
    {
        using var connection = new MySqlConnection(_connectionString);

        string sql = @"INSERT IGNORE INTO Pokemon (PokemonId, Nome, Url)
                       VALUES (@PokemonId, @Nome, @Url)";

        await connection.ExecuteAsync(sql, pokemon);
    }

    public async Task<int> BuscarQuantidadePokemon()
    {
        using var connection = new MySqlConnection(_connectionString);

        string sql = "SELECT COUNT(*) FROM Pokemon";

        var result = await connection.ExecuteScalarAsync<int?>(sql);

        return result ?? 0;
    }
}