using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITrainer : MonoBehaviour
{
    /// <summary>
    /// List of pokemons in the team of this trainer
    /// </summary>
    public List<PokemonBase> Team { get; set; }

    /// <summary>
    /// List of pokemons that the player can have in his team
    /// </summary>
    public List<PokemonBase> PokemonPool { get; set; }

    /// <summary>
    /// Number of pokemons in the team of the trainer
    /// </summary>
    public int TeamSize { get; set; }
}
