using System.Text.Json;

public class PokemonFacade
{
    public async Task<List<Pokemon>> GetPokemons(int maiorId)
    {
        var client = new HttpClient();

        // 🔥 recebe o maiorId do Service
        var numeroInicial = maiorId + 1;
        var numeroFinal = numeroInicial + 150;

        // 🔥 converte pra limit
        var quantidade = numeroFinal - numeroInicial;

        var response = await client.GetStringAsync(
            $"https://pokeapi.co/api/v2/pokemon?offset={numeroInicial}&limit={quantidade}"
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