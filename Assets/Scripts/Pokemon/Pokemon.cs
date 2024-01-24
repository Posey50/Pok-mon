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
    /// The trainer of this pokemon.
    /// </summary>
    public ITrainer TrainerOfThisPokemon { get; set; }

    /// <summary>
    /// Actual HP of the pokemon.
    /// </summary>
    public int HP { get; set; }

    /// <summary>
    /// A value indicating that the pokemon is out of his pokeball.
    /// </summary>
    public bool IsOutOfHisPokeball { get; set; }

    /// <summary>
    /// A value indicating that the pokemon is KO.
    /// </summary>
    public bool IsKO { get; set; }

    /// <summary>
    /// Initialises pokemon's values.
    /// </summary>
    /// <param name="trainOfThisPokemon"> Trainer of the pokemon. </param>
    public void Init(ITrainer trainOfThisPokemon)
    {
        this.TrainerOfThisPokemon = trainOfThisPokemon;
        HP = this.Base.MaxHP;
        IsOutOfHisPokeball = false;
        IsKO = false;
    }

    /// <summary>
    /// Chooses a random attack in the move set to do.
    /// </summary>
    /// <param name="defender"> Pokemon to attack. </param>
    /// <returns></returns>
    public IEnumerator ChooseAnAttackFor(Pokemon defender)
    {
        // Chooses the attack to do
        Move attackToDo = this.Base.Moves[Random.Range(0, this.Base.Moves.Count)];

        // Anounces the attack
        Debug.Log(((Human)TrainerOfThisPokemon).Name + "'s " + this.Base.Name + " uses " + attackToDo.Base.Name + "!");

        // Wait
        yield return new WaitForSeconds(1f);

        if (attackToDo.Base.IsHealer)
        {
            // Starts healing
            this.Heal(attackToDo.Base.Damages);
        }
        else
        {
            // Attacks the enemy pokemon
            defender.TakeDamages(attackToDo, this);
        }
    }

    /// <summary>
    /// Heals HP to the pokemon.
    /// </summary>
    /// <param name="nbrOfHPToHeal"> HP to heal. </param>
    public void Heal(int nbrOfHPToHeal)
    {
        // Increases HP
        HP += nbrOfHPToHeal;
        if (HP >= this.Base.MaxHP)
        {
            HP = this.Base.MaxHP;
        }

        // Anounces heal
        Debug.Log(((Human)TrainerOfThisPokemon).Name + "'s " + this.Base.Name + " recovers " + nbrOfHPToHeal.ToString() + "HP");

        // Anounces new amount of HP
        this.ShowHP();
    }

    /// <summary>
    /// Revive a pokemon with an amount of HP given.
    /// </summary>
    /// <param name="nbrOfHPToHeal"> HP to heal. </param>
    public void Revive(int nbrOfHPToHeal)
    {
        // The pokemon is no longer KO
        IsKO = false;

        // Heal the pokemon
        HP = nbrOfHPToHeal;

        // Anounces heal
        Debug.Log(((Human)TrainerOfThisPokemon).Name + "'s " + this.Base.Name + " is no longer KO!");

        // Anounces new amount of HP
        this.ShowHP();
    }

    /// <summary>
    /// The pokemon takes damages from an attack.
    /// </summary>
    /// <param name="move"> Move used by the enemy pokemon. </param>
    /// <param name="attacker"> The enemy pokemon who has attacked. </param>
    /// <returns></returns>
    public void TakeDamages(Move move, Pokemon attacker)
    {
        // Get the efficacity coefficient
        float effectiveness = TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type);

        // Anounces effectiveness
        switch (effectiveness)
        {
            case 0f:
                {
                    Debug.Log("It doesn't affect " + ((Human)this.TrainerOfThisPokemon).Name + "'s " + this.Base.Name);
                    break;
                }
            case 0.5f:
                {
                    Debug.Log("It's not very effective...");
                    break;
                }
            case 1f:
                {
                    break;
                }
            case 2f:
                {
                    Debug.Log("It's super effective!");
                    break;
                }
        }

        // Calculates damages inflicted
        int damage = (int)(Mathf.Round((attacker.Base.Attack) * (move.Base.Damages / 100f) * effectiveness));

        // Anounces damages
        Debug.Log("The attack inflicts " + damage.ToString() + " damage on " + ((Human)TrainerOfThisPokemon).Name + "'s " + this.Base.Name);

        // Reduces HP
        HP -= damage;

        // If HP are equals to 0, the pokemon is KO
        if (HP <= 0)
        {
            HP = 0;
            this.PokemonIsKO();
        }
    }

    /// <summary>
    /// The pokemon returns in his pokeball and is KO
    /// </summary>
    public void PokemonIsKO()
    {
        // Set the pokemon KO
        this.IsKO = true;
        this.IsOutOfHisPokeball = false;

        // Anounces that the pokemon is KO
        Debug.Log(((Human)(TrainerOfThisPokemon)).Name + "'s " + this.Base.Name + " fainted!");
    }

    /// <summary>
    /// Shows HP of the pokemon.
    /// </summary>
    public void ShowHP()
    {
        // Anounces HP of the pokemon
        if (HP > 0)
        {
            Debug.Log(((Human)(TrainerOfThisPokemon)).Name + "'s " + this.Base.Name + " has " + HP.ToString() + "HP left");
        }
    }
}