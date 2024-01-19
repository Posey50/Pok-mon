using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Move/Create new move")]
public class MoveBase : ScriptableObject
{
    /// <summary>
    /// Name of the pokemon
    /// </summary>
    [SerializeField] new string name;

    /// <summary>
    /// Type of the attack
    /// </summary>
    [SerializeField] PokemonType type;

    /// <summary>
    /// Defines if the attack is an heal attack
    /// </summary>
    [SerializeField] bool isHealer;

    /// <summary>
    /// Damage's statistic of the attack in percents
    /// </summary>
    [SerializeField] int damages;

    /// <summary>
    /// All pokemon types
    /// </summary>
    public enum PokemonType
    {
        None,
        Normal,
        Feu,
        Eau,
        Électrik,
        Plante,
        Glace,
        Combat,
        Poison,
        Sol,
        Vol,
        Psy,
        Insecte,
        Roche,
        Spectre,
        Dragon,
        Ténèbres,
        Acier,
        Fée
    }
}
