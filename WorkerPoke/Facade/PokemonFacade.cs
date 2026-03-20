using System.Text.Json;
using WorkerPoke.Models;

namespace WorkerPoke.Facade;

public class PokemonFacade
{
    public async Task<List<Pokemon>> GetPokemons(int offset)
    {
        var client = new HttpClient();

        var response = await client.GetStringAsync(
            $"https://pokeapi.co/api/v2/pokemon?offset={offset}&limit=150"
        );

        var data = JsonSerializer.Deserialize<PokeApiResponse>(response);

        if (data == null || data.results == null)
            return new List<Pokemon>();

        var lista = new List<Pokemon>();

        foreach (var item in data.results)
        {
            var id = int.Parse(item.url.TrimEnd('/').Split('/').Last());

            lista.Add(new Pokemon
            {
                PokemonId = id,
                Nome = item.name,
                Url = item.url
            });
        }

        return lista;
    }
}