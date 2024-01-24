using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        // Wait
        yield return new WaitForSeconds(1f);

        // Generate battle
        _battleSteps.GenerateBattle();

        // Wait
        yield return new WaitForSeconds(1f);

        // Generate teams
        yield return StartCoroutine(_battleSteps.GenerateTeams());

        // Initialises pokemons in the team of each trainer in the battle
        _battleSteps.InitPokemons();

        // Wait
        yield return new WaitForSeconds(1f);

        // Generate the healer
        _battleSteps.GenerateHealer();

        //Wait
        yield return new WaitForSeconds(1f);

        // Start battle
        Debug.Log("The battle starts!");
        StartCoroutine(NewBattleRound());
    }

    /// <summary>
    /// Progress of a round.
    /// </summary>
    /// <returns></returns>
    private IEnumerator NewBattleRound()
    {
        //Wait
        yield return new WaitForSeconds(1f);

        // Send pokemons if there is any need
        _battleSteps.CheckActivePokemons();

        //Wait
        yield return new WaitForSeconds(1f);

        // Healer turn
        if (HealerInBattle != null)
        {
            _battleSteps.HealerTurn();
        }

        // Wait
        yield return new WaitForSeconds(1f);

        // Pokemons fight
        yield return StartCoroutine(_battleSteps.TrainersTurn());

        // Start a new round
        StartCoroutine(NewBattleRound());
    }
}