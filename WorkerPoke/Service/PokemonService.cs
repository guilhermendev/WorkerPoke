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
        while (true)
        {
            var maiorId = await _repository.BuscarMaiorIdPokemon();

            var pokemons = await _facade.GetPokemons(maiorId);

            if (pokemons.Count == 0)
            {
                Console.WriteLine("Todos os pokémons já foram cadastrados!");
                break;
            }

            foreach (var pokemon in pokemons)
            {
                await _repository.Insert(pokemon);
            }

            Console.WriteLine($"Inseridos {pokemons.Count} pokémons...");
        }

        Console.WriteLine("Job finalizado");
    }
}