using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    private static GameManager _instance = null;

    public static GameManager Instance => _instance;

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
    /// Sort all lists by alphabetic order to be sure that it's the same order at each game.
    /// </summary>
    public void SortAllLists()
    {
        HumanList.OrderBy(human => human.Name);

        TrainerList.OrderBy(trainer => ((Human)trainer).Name);

        HealerList.OrderBy(healer => ((Human)healer).Name);
    }
}