using System.Collections.Generic;
using UnityEngine;

public class Nurse_Joy : Human, IHealer
{
    private void Start()
    {
        Name = "Nurse Joy";

        GameManager.Instance.HumanList.Add(this);
        GameManager.Instance.HealerList.Add(this);
    }

    public void Revive()
    {
        List<Pokemon> pokemonsKO = new();

        // Searches for pokemons KO
        for (int i = 0; i < BattleManager.Instance.TrainersInBattle.Count; i++)
        {
            for (int j = 0; j < BattleManager.Instance.TrainersInBattle[i].Team.Count; j++)
            {
                if (BattleManager.Instance.TrainersInBattle[i].Team[j].IsKO)
                {
                    pokemonsKO.Add(BattleManager.Instance.TrainersInBattle[i].Team[j]);
                }
            }
        }

        // Chooses a random pokemon to revive with half of their HP
        if (pokemonsKO.Count > 0)
        {
            Pokemon pokemonToRevive = pokemonsKO[Random.Range(0, pokemonsKO.Count)];

            // Anounces the action
            Debug.Log(this.Name + " uses a revive on " + ((Human)pokemonToRevive.TrainerOfThisPokemon).Name + "'s " + pokemonToRevive.Base.Name + "!");

            pokemonToRevive.Revive(pokemonToRevive.Base.MaxHP / 2);
        }
        else
        {
            // Anounces that it failed
            Debug.Log(this.Name + " wants to use a revive but there were no pokemon on which to use it...");
        }
    }

    public void MaxRevive()
    {
        List<Pokemon> pokemonsKO = new();

        // Searches for pokemons KO
        for (int i = 0; i < BattleManager.Instance.TrainersInBattle.Count; i++)
        {
            for (int j = 0; j < BattleManager.Instance.TrainersInBattle[i].Team.Count; j++)
            {
                if (BattleManager.Instance.TrainersInBattle[i].Team[j].IsKO)
                {
                    pokemonsKO.Add(BattleManager.Instance.TrainersInBattle[i].Team[j]);
                }
            }
        }

        // Chooses a random pokemon to revive with all of their HP
        if (pokemonsKO.Count > 0)
        {
            Pokemon pokemonToRevive = pokemonsKO[Random.Range(0, pokemonsKO.Count)];

            // Anounces the action
            Debug.Log(this.Name + " uses a max revive on " + ((Human)pokemonToRevive.TrainerOfThisPokemon).Name + "'s " + pokemonToRevive.Base.Name + "!");

            pokemonToRevive.Revive(pokemonToRevive.Base.MaxHP);
        }
        else
        {
            // Anounces that it failed
            Debug.Log(this.Name + " wants to use a max revive but there were no pokemon on which to use it...");
        }
    }

    public void Cook()
    {
        // Anounces the action
        Debug.Log(this.Name + " decides to cook for all pokemons still standing!");

        List<Pokemon> pokemonsToHeal = new();

        // Searches for pokemons which are not KO and which have not all their HP
        for (int i = 0; i < BattleManager.Instance.TrainersInBattle.Count; i++)
        {
            for (int j = 0; j < BattleManager.Instance.TrainersInBattle[i].Team.Count; j++)
            {
                Pokemon pokemonToCheck = BattleManager.Instance.TrainersInBattle[i].Team[j];

                if (!pokemonToCheck.IsKO && pokemonToCheck.HP < pokemonToCheck.Base.MaxHP)
                {
                    pokemonsToHeal.Add(pokemonToCheck);
                }
            }
        }

        // Cookes for all pokemons which are not KO
        if (pokemonsToHeal.Count > 0)
        {
            for (int i = 0; i < pokemonsToHeal.Count; i++)
            {
                pokemonsToHeal[i].Heal(pokemonsToHeal[i].Base.MaxHP - pokemonsToHeal[i].HP);
            }
        }
        else
        {
            // Anounces that it failed
            Debug.Log(this.Name + " wants to cook but there were no pokemon that were hungry...");
        }
    }

    public void FullHeal()
    {
        List<Pokemon> pokemonsToHeal = new();

        // Searches for pokemons which are not KO
        for (int i = 0; i < BattleManager.Instance.TrainersInBattle.Count; i++)
        {
            for (int j = 0; j < BattleManager.Instance.TrainersInBattle[i].Team.Count; j++)
            {
                if (!BattleManager.Instance.TrainersInBattle[i].Team[j].IsKO)
                {
                    pokemonsToHeal.Add(BattleManager.Instance.TrainersInBattle[i].Team[j]);
                }
            }
        }

        // Chooses a random pokemon to heal with all of their HP
        if (pokemonsToHeal.Count > 0)
        {
            Pokemon pokemonToHeal = pokemonsToHeal[Random.Range(0, pokemonsToHeal.Count)];

            // Anounces the action
            Debug.Log(this.Name + " uses a full heal on " + ((Human)pokemonToHeal.TrainerOfThisPokemon).Name + "'s " + pokemonToHeal.Base.Name + "!");

            pokemonToHeal.Heal(pokemonToHeal.Base.MaxHP - pokemonToHeal.HP);
        }
        else
        {
            // Anounces that it failed
            Debug.Log(this.Name + " wants to use a full heal but there were no pokemon on which to use it...");
        }
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
}