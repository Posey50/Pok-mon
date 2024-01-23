using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Move/Create new move")]
public class MoveBase : ScriptableObject
{
    /// <summary>
    /// Name of the attack.
    /// </summary>
    [SerializeField]
    private string _name;

    /// <summary>
    /// Type of the attack.
    /// </summary>
    [SerializeField]
    private PokemonType _type;

    /// <summary>
    /// Defines if the attack is an heal attack.
    /// </summary>
    [SerializeField]
    private bool _isHealer;

    /// <summary>
    /// Damages statistic of the attack in percents.
    /// </summary>
    [SerializeField]
    private int _damages;

    /// <summary>
    /// Gets the name of the attack.
    /// </summary>
    public string Name { get { return _name; } private set { } }

    /// <summary>
    /// Gets the type of the attack.
    /// </summary>
    public PokemonType Type { get { return _type; } private set { } }

    /// <summary>
    /// Gets a value indicating if the attack is an heal attack.
    /// </summary>
    public bool IsHealer { get { return _isHealer; } private set { } }

    /// <summary>
    /// Gets the damages statistic of the attack in percents.
    /// </summary>
    public int Damages { get { return _damages; } private set { } }

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
}
