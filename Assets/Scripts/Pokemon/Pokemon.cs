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

    /// <summary>
    /// A value indicating that the pokemon is out of his pokeball.
    /// </summary>
    public bool IsOutOfHisPokeball { get; set; }

    /// <summary>
    /// A value indicating indicating that the pokemon is KO.
    /// </summary>
    public bool IsKO { get; set; }

    //public IEnumerator TakeDamage(Move move, Pokemon attacker)
    //{

    //    float effectiveness = TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type);

    //    int damage = 

    //    HP -= damage;
    //    if (HP <= 0)
    //    {
    //        HP = 0;
    //    }
    //}
}