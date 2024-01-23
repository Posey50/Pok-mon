using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pokemon
{
    /// <summary>
    /// Base of the pokemon.
    /// </summary>
    [SerializeField]
    private PokemonBase _base;

    /// <summary>
    /// Gets the base of the pokemon.
    /// </summary>
    public PokemonBase Base { get { return _base; } private set { } }

    /// <summary>
    /// Actual HP of the pokemon.
    /// </summary>
    public int HP { get; set; }
}
