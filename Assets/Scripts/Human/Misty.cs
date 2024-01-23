using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Misty : Human, ITrainer
{
    [field: SerializeField]
    public List<Pokemon> Team { get; set; } = new ();

    [field: SerializeField]
    public List<Pokemon> PokemonPool { get; set; } = new();

    [field: SerializeField]
    public int TeamSize { get; set; } = new();

    private void Start()
    {
        Name = "Misty";

        GameManager.Instance.HumanList.Add(this);
        GameManager.Instance.TrainerList.Add(this);

        SortPokemonPool();
    }

    public void SortPokemonPool()
    {
        PokemonPool.OrderBy(pokemon => pokemon.Base.Name);
    }

    public void GenerateTeam()
    {
        // Clear the team
        if (Team.Count > 0)
        {
            Team.Clear();
        }

        List<Pokemon> pokemonsAvailable = new(PokemonPool);

        for (int i = 0; i < TeamSize; i++)
        {
            if (pokemonsAvailable.Count > 0)
            {
                Pokemon pokemonToAdd = pokemonsAvailable[Random.Range(0, pokemonsAvailable.Count)];

                Team.Add(pokemonToAdd);

                pokemonsAvailable.Remove(pokemonToAdd);
            }
        }
    }
}
