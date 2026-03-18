using System.Text.Json;

public class PokemonFacade
{
    public async Task<List<Pokemon>> GetPokemons()
    {
        var client = new HttpClient();

        var response = await client.GetStringAsync("https://pokeapi.co/api/v2/pokemon/?offset=0&limit=151");

        var data = JsonSerializer.Deserialize<PokeApiResponse>(response);

        if (data == null || data.results == null)
            return new List<Pokemon>();

        var lista = new List<Pokemon>();
        int id = 1;

        foreach (var item in data.results)
        {
            lista.Add(new Pokemon
            {
                PokemonId = id,
                Nome = item.name,
                Url = item.url
            });

            id++;
        }

        return lista;
    }
}