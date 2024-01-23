using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrainer
{
    /// <summary>
    /// List of pokemons in the team of this trainer.
    /// </summary>
    public List<Pokemon> Team { get; set; }

    /// <summary>
    /// List of pokemons that the player can have in his team.
    /// </summary>
    public List<Pokemon> PokemonPool { get; set; }

    /// <summary>
    /// Number of pokemons in the team of the trainer.
    /// </summary>
    public int TeamSize { get; set; }

    /// <summary>
    /// Sort the pokemon pool by alphabetic order to be sure that it's the same order at each game.
    /// </summary>
    public void SortPokemonPool();

    /// <summary>
    /// Generate a random team with the team size given and with pokemons from the pokemon pool.
    /// </summary>
    public void GenerateTeam();
}
