using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new pokemon")]
public class PokemonBase : ScriptableObject
{
    /// <summary>
    /// Name of the pokemon
    /// </summary>
    [SerializeField] new string name;

    /// <summary>
    /// Type of the pokemon
    /// </summary>
    [SerializeField] PokemonType type;

    /// <summary>
    /// Maximum of HP of the pokemon
    /// </summary>
    [SerializeField] int maxHp;

    /// <summary>
    /// Attack's statistic of the pokemon
    /// </summary>
    [SerializeField] int attack;

    /// <summary>
    /// Defense's statistic of the pokemon
    /// </summary>
    [SerializeField] int defense;

    /// <summary>
    /// Speed's statistic of the pokemon
    /// </summary>
    [SerializeField] int speed;

    /// <summary>
    /// List of attacks of the pokemon
    /// </summary>
    [SerializeField] List<MoveBase> MoveBases;

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
