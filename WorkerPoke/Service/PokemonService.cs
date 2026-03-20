using WorkerPoke.Facade;
using WorkerPoke.Repository;

namespace WorkerPoke.Service;

public class PokemonService
{
    private readonly PokemonRepository _repository;
    private readonly PokemonFacade _facade;

    public PokemonService(PokemonFacade facade, PokemonRepository repository)
    {
        _facade = facade;
        _repository = repository;
    }

    public async Task RunJob()
    {
        var offset = await _repository.BuscarQuantidadePokemon();

        Console.WriteLine($"Maior ID encontrado: {offset}");

        var pokemons = await _facade.GetPokemons(offset);

        if (pokemons.Count == 0)
        {
            Console.WriteLine("Todos os pokémons já foram cadastrados!");
            return;
        }

        foreach (var pokemon in pokemons)
        {
            await _repository.Insert(pokemon);
        }

        Console.WriteLine($"Inseridos {pokemons.Count} pokémons. Maior ID agora: {offset + pokemons.Count}");
    }
} 