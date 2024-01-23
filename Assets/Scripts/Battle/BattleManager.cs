using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // Singleton
    private static BattleManager _instance = null;

    public static BattleManager Instance => _instance;

    /// <summary>
    /// List of trainers in the battle.
    /// </summary>
    public List<ITrainer> TrainersInBattle { get; set; } = new ();

    /// <summary>
    /// Healer in the battle who can does some actions.
    /// </summary>
    public IHealer HealerInBattle { get; set; }

    /// <summary>
    /// Manager of the game.
    /// </summary>
    private GameManager _gameManager { get; set; }

    private void Awake()
    {
        // Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    /// <summary>
    /// Generate a battle between two trainers with a minimum of one pokemon.
    /// </summary>
    public IEnumerator GenerateBattle()
    {
        // Clear the list of trainers in battle
        if (TrainersInBattle.Count > 0)
        {
            TrainersInBattle.Clear();
        }

        List<ITrainer> trainers = new (_gameManager.TrainerList);

        // Sort trainers without pokemon
        for (int i = 0; i < trainers.Count; i++)
        {
            if (trainers[i].TeamSize <= 0)
            {
                trainers.Remove(trainers[i]);
            }
        }

        // Choose two trainers for the battle
        for (int i = 0; i < 2; i++)
        {
            ITrainer trainerToAdd = trainers[Random.Range(0, trainers.Count)];
            TrainersInBattle.Add(trainerToAdd);
            trainers.Remove(trainerToAdd);
        }

        // Announces the duel
        Debug.Log(((Human)TrainersInBattle[0]).Name + " and " + ((Human)TrainersInBattle[1]).Name + " face each other");

        // Wait
        yield return new WaitForSeconds(1f);

        StartCoroutine(GenerateTeams());
    }

    /// <summary>
    /// Generates teams for each trainer in the battle
    /// </summary>
    private IEnumerator GenerateTeams()
    {
        for (int i = 0; i < TrainersInBattle.Count; i++)
        {
            TrainersInBattle[i].GenerateTeam();

            // Wait
            yield return new WaitForSeconds(0.1f);
        }

        // Announces the teams
        for (int i = 0; i < TrainersInBattle.Count; i++)
        {
            // Wait
            yield return new WaitForSeconds(1f);

            Debug.Log(((Human)TrainersInBattle[i]).Name + " has a team made up of :");

            for (int j = 0; j < TrainersInBattle[0].Team.Count; j++)
            {
                // Wait
                yield return new WaitForSeconds(0.5f);

                Debug.Log(TrainersInBattle[0].Team[j].Base.Name);
            }
        }

        // Wait
        yield return new WaitForSeconds(1f);

        StartCoroutine(GenerateHealer());
    }

    /// <summary>
    /// If there is healers available, it chooses a random healer who will be at the edge of the terrain.
    /// </summary>
    /// <returns></returns>
    public IEnumerator GenerateHealer()
    {
        if (_gameManager.HealerList.Count > 0)
        {
            HealerInBattle = _gameManager.HealerList[Random.Range(0, _gameManager.HealerList.Count)];

            // Anounces the healer
            Debug.Log(((Human)HealerInBattle).Name + " observes the match at the edge of the terrain with his care equipment");
        }
        else
        {
            // Anounces that there is no healer
            Debug.Log("There is nobody to observe this match");
        }

        // Wait
        yield return new WaitForSeconds(1f);
    }
}
