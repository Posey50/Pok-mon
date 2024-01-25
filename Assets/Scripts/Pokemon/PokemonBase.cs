using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new pokemon")]
public class PokemonBase : ScriptableObject
{
    /// <summary>
    /// Name of the pokemon.
    /// </summary>
    [SerializeField]
    private string _name;

    /// <summary>
    /// Type of the pokemon.
    /// </summary>
    [SerializeField]
    private PokemonType _type;

    /// <summary>
    /// Maximum of HP of the pokemon.
    /// </summary>
    [SerializeField]
    private int _maxHP;

    /// <summary>
    /// Attack statistic of the pokemon.
    /// </summary>
    [SerializeField]
    private int _attack;

    /// <summary>
    /// Defense statistic of the pokemon.
    /// </summary>
    [SerializeField]
    private int _defense;

    /// <summary>
    /// Speed statistic of the pokemon.
    /// </summary>
    [SerializeField]
    private int _speed;

    /// <summary>
    /// List of attacks of the pokemon.
    /// </summary>
    [SerializeField]
    private List<Move> _moves;

    /// <summary>
    /// Gets the name of the pokemon.
    /// </summary>
    public string Name { get { return _name; } private set { } }

    /// <summary>
    /// Gets the type of the pokemon.
    /// </summary>
    public PokemonType Type { get { return _type; } private set { } }

    /// <summary>
    /// Gets the maximum of HP of the pokemon.
    /// </summary>
    public int MaxHP { get { return _maxHP; } private set { } }

    /// <summary>
    /// Gets the attack statistic of the pokemon.
    /// </summary>
    public int Attack { get { return _attack; } private set { } }

    /// <summary>
    /// Gets the defense statistic of the pokemon.
    /// </summary>
    public int Defense { get { return _defense; } private set { } }

    /// <summary>
    /// Gets the speed statistic of the pokemon.
    /// </summary>
    public int Speed { get { return _speed; } private set { } }

    /// <summary>
    /// Gets the list of attacks of the pokemon.
    /// </summary>
    public List<Move> Moves { get { return _moves; } private set { } }
}

/// <summary>
/// All pokemon types.
/// </summary>
public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon,
    Dark,
    Steel,
    Fairy
}