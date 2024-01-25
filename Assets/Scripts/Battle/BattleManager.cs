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

    /// <summary>
    /// Contains all steps of a battle.
    /// </summary>
    private BattleSteps _battleSteps {  get; set; }

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

        _battleSteps = GetComponent<BattleSteps>();
    }

    /// <summary>
    /// Called to set up a battle.
    /// </summary>
    /// <returns></returns>
    public IEnumerator SetUpBattle()
    {
        // Generates battle
        _battleSteps.GenerateBattle();

        // Waits
        yield return new WaitForSeconds(1f);

        // Generates teams
        yield return StartCoroutine(_battleSteps.GenerateTeams());

        // Initialises pokemons in the team of each trainer in the battle
        _battleSteps.InitPokemons();

        // Waits
        yield return new WaitForSeconds(1f);

        // Generates the healer
        _battleSteps.GenerateHealer();

        // Waits
        yield return new WaitForSeconds(1f);

        // Starts battle
        Debug.Log("The battle starts!");
        StartCoroutine(NewBattleRound());
    }

    /// <summary>
    /// Progress of a round.
    /// </summary>
    /// <returns></returns>
    private IEnumerator NewBattleRound()
    {
        // Waits
        yield return new WaitForSeconds(1f);

        // Sends pokemons if there is any need
        _battleSteps.CheckActivePokemons();
        
        // Waits
        yield return new WaitForSeconds(1f);

        // Healer turn
        if (HealerInBattle != null)
        {
            _battleSteps.HealerTurn();
        }

        // Waits
        yield return new WaitForSeconds(1f);

        // Pokemons fight
        yield return StartCoroutine(_battleSteps.TrainersTurn());

        // Starts a new round
        StartCoroutine(NewBattleRound());
    }
}