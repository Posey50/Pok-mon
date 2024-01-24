using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Brock : Human, IHealer, ITrainer
{
    [field: SerializeField]
    public List<Pokemon> Team { get; set; } = new();

    [field: SerializeField]
    public List<Pokemon> PokemonPool { get; set; } = new();

    [field: SerializeField]
    public int TeamSize { get; set; } = new();

    [field: SerializeField]
    public Pokemon ActivePokemon { get; set; } = new();

    private void Start()
    {
        Name = "Brock";

        GameManager.Instance.HumanList.Add(this);
        GameManager.Instance.TrainerList.Add(this);
        GameManager.Instance.HealerList.Add(this);

        SortPokemonPool();
    }

    public void Revive()
    {
        Debug.Log(this.Name + " uses a Revive");
    }

    public void MaxRevive()
    {
        Debug.Log(this.Name + " uses a Max Revive");
    }

    public void Cook()
    {
        Debug.Log(this.Name + " cooks");
    }

    public void FullHeal()
    {
        Debug.Log(this.Name + " uses a Full Heal");
    }

    public void ChooseAnAction()
    {
        // Chooses what to do
        switch (Random.Range(0, 4))
        {
            case 0:
                {
                    Revive();
                    break;
                }
            case 1:
                {
                    MaxRevive();
                    break;
                }
            case 2:
                {
                    Cook();
                    break;
                }
            case 3:
                {
                    FullHeal();
                    break;
                }
        }
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

        // Pokemons available for the team
        List<Pokemon> pokemonsAvailable = new(PokemonPool);

        // Create the team
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

        // Sort pokemons KO and the active pokemon if there is one
        for (int i = 0; i < Team.Count; i++)
        {
            if (Team[i].IsKO || Team[i].IsOutOfHisPokeball)
            {
                pokemonAvailableInTheTeam.Remove(Team[i]);
            }
        }

        // Choose a random pokemon to send
        ActivePokemon = pokemonAvailableInTheTeam[Random.Range(0, pokemonAvailableInTheTeam.Count)];
        ActivePokemon.IsOutOfHisPokeball = true;
    }
}
