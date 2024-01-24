using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LaunchBattle : MonoBehaviour
{
    /// <summary>
    /// Seed of the battle.
    /// </summary>
    private int _seed { get; set; }

    /// <summary>
    /// Seed given by the player.
    /// </summary>
    [field: SerializeField]
    private TMP_InputField _inputSeed { get; set; }

    /// <summary>
    /// Manager of the game.
    /// </summary>
    private GameManager _gameManager { get; set; }

    /// <summary>
    /// Manager of the battle.
    /// </summary>
    private BattleManager _battleManager { get; set; }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _battleManager = BattleManager.Instance;
    }

    /// <summary>
    /// Called to try to launch a battle.
    /// </summary>
    public void TryToLaunchABattle()
    {
        StartCoroutine(StartBattle());
    }

    /// <summary>
    /// Start a battle by checking if there is a valid seed given.
    /// </summary>
    public IEnumerator StartBattle()
    {
        if (_inputSeed.text != "" && int.TryParse(_inputSeed.text, out int seedGiven))
        {
            // Announces the terrain
            Debug.Log("A battle start on terrain " + _inputSeed.text);

            // Init random with the seed given
            _seed = seedGiven;
            Random.InitState(_seed);

            // Sort all lists to be sure that it's the same order at each time
            _gameManager.SortAllLists();

            // Wait
            yield return new WaitForSeconds(1f);

            // Generate a battle
            StartCoroutine(_battleManager.SetUpBattle());
        }
        else
        {
            Debug.Log("Please enter a valid seed");
        }
    }
}