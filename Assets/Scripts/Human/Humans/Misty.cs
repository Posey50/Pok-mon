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

    [field: SerializeField]
    public Pokemon ActivePokemon { get; set; } = new();

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
        // Clears the team
        if (Team.Count > 0)
        {
            Team.Clear();
        }

        // Pokemons available for the team
        List<Pokemon> pokemonsAvailable = new(PokemonPool);

        // Creates the team
        for (int j = 0; j < TeamSize; j++)
        {
            if (pokemonsAvailable.Count > 0)
            {
                Pokemon pokemonToAdd = pokemonsAvailable[Random.Range(0, pokemonsAvailable.Count)];

                Team.Add(pokemonToAdd);

                pokemonsAvailable.Remove(pokemonToAdd);
            }
        }
    }

    public void ChooseAPokemonToSend()
    {
        List<Pokemon> pokemonAvailableInTheTeam = new(Team);

        // Removes pokemons KO and the active pokemon if there is one
        for (int i = 0; i < Team.Count; i++)
        {
            if (Team[i].IsKO || Team[i].IsOutOfHisPokeball)
            {
                pokemonAvailableInTheTeam.Remove(Team[i]);
            }
        }

        if (pokemonAvailableInTheTeam.Count > 0)
        {
            // Chooses a random pokemon to send
            ActivePokemon = pokemonAvailableInTheTeam[Random.Range(0, pokemonAvailableInTheTeam.Count)];
            ActivePokemon.IsOutOfHisPokeball = true;

            // Anounces the pokemon
            Debug.Log(this.Name + " sent out " + this.ActivePokemon.Base.Name + "!");
        }
        else
        {
            // Anounces the defeat
            Debug.Log(this.Name + " no longer has any pokemon capable of fighting...");

            BattleManager.Instance.TrainersInBattle.Remove(this);

            Debug.Log(((Human)BattleManager.Instance.TrainersInBattle[0]).Name + " wins the fight!");

            // Stops the game
            GameManager.Instance.GameOver();
        }
    }
}
