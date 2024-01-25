using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSteps : MonoBehaviour
{
    /// <summary>
    /// Manager of the battle.
    /// </summary>
    private BattleManager _battleManager { get; set; }

    private void Start()
    {
        _battleManager = BattleManager.Instance;
    }

    /// <summary>
    /// Generates a battle between two trainers with a minimum of one pokemon.
    /// </summary>
    public void GenerateBattle()
    {
        // Clears the list of trainers in battle
        if (_battleManager.TrainersInBattle.Count > 0)
        {
            _battleManager.TrainersInBattle.Clear();
        }

        List<ITrainer> trainers = new(GameManager.Instance.TrainerList);

        // Removes trainers without pokemon
        for (int i = 0; i < trainers.Count; i++)
        {
            if (trainers[i].TeamSize <= 0)
            {
                trainers.Remove(trainers[i]);
            }
        }

        // Chooses two trainers for the battle
        for (int i = 0; i < 2; i++)
        {
            ITrainer trainerToAdd = trainers[Random.Range(0, trainers.Count)];
            _battleManager.TrainersInBattle.Add(trainerToAdd);
            trainers.Remove(trainerToAdd);
        }

        // Announces the duel
        Debug.Log(((Human)_battleManager.TrainersInBattle[0]).Name + " and " + ((Human)_battleManager.TrainersInBattle[1]).Name + " face each other");
    }

    /// <summary>
    /// Generates teams for each trainer in the battle
    /// </summary>
    public IEnumerator GenerateTeams()
    {
        //Generates teams
        for (int i = 0; i < _battleManager.TrainersInBattle.Count; i++)
        {
            _battleManager.TrainersInBattle[i].GenerateTeam();
        }

        // Announces the teams
        for (int i = 0; i < _battleManager.TrainersInBattle.Count; i++)
        {
            // Waits
            yield return new WaitForSeconds(1f);

            Debug.Log(((Human)_battleManager.TrainersInBattle[i]).Name + " has a team made up of :");

            for (int j = 0; j < _battleManager.TrainersInBattle[i].Team.Count; j++)
            {
                // Waits
                yield return new WaitForSeconds(1f);

                Debug.Log(_battleManager.TrainersInBattle[i].Team[j].Base.Name);
            }
        }
    }

    /// <summary>
    /// Initialises all pokemons in the teams of the trainers.
    /// </summary>
    public void InitPokemons()
    {
        for (int i = 0; i < _battleManager.TrainersInBattle.Count; i++)
        {
            for (int j = 0; j < _battleManager.TrainersInBattle[i].Team.Count; j++)
            {
                _battleManager.TrainersInBattle[i].Team[j].Init(_battleManager.TrainersInBattle[i]);
            }
        }
    }

    /// <summary>
    /// If there is healer available, it chooses a random healer who will be at the edge of the terrain.
    /// </summary>
    public void GenerateHealer()
    {
        List<IHealer> healers = new (GameManager.Instance.HealerList);

        // Removes healers who are in the battle as trainers
        if (healers.Count > 0)
        {
            for (int i = 0; i < healers.Count; i++)
            {
                if (healers[i] is ITrainer healer && _battleManager.TrainersInBattle.Contains(healer))
                {
                    healers.Remove(healers[i]);
                }
            }
        }

        if (healers.Count > 0)
        {
            // Chooses the healer
            _battleManager.HealerInBattle = healers[Random.Range(0, healers.Count)];

            // Anounces the healer
            Debug.Log(((Human)_battleManager.HealerInBattle).Name + " observes the match at the edge of the terrain with his care equipment");
        }
        else
        {
            // Anounces that there is no healer
            Debug.Log("There is nobody to observe this match");
        }
    }

    /// <summary>
    /// If there is a trainer without active pokemon, he sends a new pokemon to fight.
    /// </summary>
    public void CheckActivePokemons()
    {
        // Checks if there is an active pokemon or if he is KO in his pokeball
        for (int i = 0; i < _battleManager.TrainersInBattle.Count; i++)
        {
            Pokemon pokemonToCheck = _battleManager.TrainersInBattle[i].ActivePokemon;

            if (pokemonToCheck.Base == null ||
                (!pokemonToCheck.IsOutOfHisPokeball && pokemonToCheck.IsKO))
            {
                _battleManager.TrainersInBattle[i].ChooseAPokemonToSend();
            }
        }
    }

    /// <summary>
    /// Chooses if the healer has to intervene.
    /// </summary>
    public void HealerTurn()
    {
        // Determines if the healer intervenes or not
        switch (Random.Range(0, 10))
        {
            // He intervenes
            case 0:
                {
                    _battleManager.HealerInBattle.ChooseAnAction();
                    break;
                }
            // He doesn't intervene
            default:
                {
                    Debug.Log(((Human)_battleManager.HealerInBattle).Name + " chooses not to intervene...");
                    break;
                }
        }
    }

    /// <summary>
    /// Trainers attack one after the other.
    /// </summary>
    /// <returns></returns>
    public IEnumerator TrainersTurn()
    {
        // Gets the list of active pokemons in order of priority
        List<Pokemon> pokemonPriority = OrderPriority();

        // Anounces first pokemon to attack
        Debug.Log(((Human)pokemonPriority[0].TrainerOfThisPokemon).Name + "'s " + pokemonPriority[0].Base.Name + " attacks first");

        // Waits
        yield return new WaitForSeconds(1f);

        // The first pokemon attacks the other
        yield return StartCoroutine(pokemonPriority[0].ChooseAnAttackFor(pokemonPriority[1]));

        // Waits
        yield return new WaitForSeconds(1f);

        // Shows HP of the defender pokemon
        if (!pokemonPriority[1].IsKO && pokemonPriority[1].IsOutOfHisPokeball)
        {
            pokemonPriority[1].ShowHP();

            // Waits
            yield return new WaitForSeconds(1f);

            // Anounces second pokemon to attack
            Debug.Log("It's " + ((Human)pokemonPriority[1].TrainerOfThisPokemon).Name + "'s " + pokemonPriority[1].Base.Name + "'s turn to attack");

            // Waits
            yield return new WaitForSeconds(1f);

            // The second pokemon attacks
            yield return StartCoroutine(pokemonPriority[1].ChooseAnAttackFor(pokemonPriority[0]));

            // Waits
            yield return new WaitForSeconds(1f);

            // Shows HP of the defender pokemon
            if (!pokemonPriority[0].IsKO && pokemonPriority[1].IsOutOfHisPokeball)
            {
                pokemonPriority[0].ShowHP();
            }
        }
    }

    /// <summary>
    /// Returns the list of active pokemons in order of priority
    /// </summary>
    /// <returns></returns>
    private List<Pokemon> OrderPriority()
    {
        List<Pokemon> pokemonPriority = new();
        pokemonPriority.Add(_battleManager.TrainersInBattle[0].ActivePokemon);
        pokemonPriority.Add(_battleManager.TrainersInBattle[1].ActivePokemon);

        // Defines order of priority
        if (_battleManager.TrainersInBattle[0].ActivePokemon.Base.Speed < _battleManager.TrainersInBattle[1].ActivePokemon.Base.Speed)
        {
            pokemonPriority.Reverse();
        }
        else if (_battleManager.TrainersInBattle[0].ActivePokemon.Base.Speed == _battleManager.TrainersInBattle[1].ActivePokemon.Base.Speed)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    {
                        pokemonPriority.Reverse();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        else
        {
            return pokemonPriority;
        }

        return pokemonPriority;
    }
}