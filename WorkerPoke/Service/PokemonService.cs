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
        var pokemons = await _facade.GetPokemons();

        foreach (var pokemon in pokemons)
        {
            await _repository.Insert(pokemon);
        }

        Console.WriteLine("Job finalizado");
    }
}