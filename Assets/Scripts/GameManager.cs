using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;

public class GameManager : MonoBehaviour
{
    // Singleton
    private static GameManager _instance = null;

    public static GameManager Instance => _instance;

    /// <summary>
    /// Gets the mask to hide screen with the seed choice.
    /// </summary>
    [field: SerializeField]
    public GameObject SeedScreenMask {  get; set; }

    /// <summary>
    /// List of all humans in the scene.
    /// </summary>
    public List<Human> HumanList { get; set; } = new ();

    /// <summary>
    /// List of all trainers in the scene.
    /// </summary>
    public List<ITrainer> TrainerList { get; set; } = new();

    /// <summary>
    /// List of all healers in the scene.
    /// </summary>
    public List<IHealer> HealerList { get; set; } = new();

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

    /// <summary>
    /// Sorts all lists by alphabetic order to be sure that it's the same order at each game.
    /// </summary>
    public void SortAllLists()
    {
        HumanList.OrderBy(human => human.Name);

        TrainerList.OrderBy(trainer => ((Human)trainer).Name);

        HealerList.OrderBy(healer => ((Human)healer).Name);
    }

    /// <summary>
    /// Called at the end of a battle to stop the game.
    /// </summary>
    public void GameOver()
    {
        // Stops the fight
        BattleManager.Instance.StopAllCoroutines();

        // Gives the possibility of restarting a fight
        SeedScreenMask.SetActive(false);
    }

    /// <summary>
    /// Clears every logs on the console.
    /// </summary>
    public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    private void OnApplicationQuit()
    {
        ClearLog();
    }
}