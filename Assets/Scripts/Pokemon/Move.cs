using UnityEngine;

[System.Serializable]
public class Move
{
    /// <summary>
    /// Base of the move.
    /// </summary>
    [SerializeField]
    private MoveBase _base;

    /// <summary>
    /// Gets the base of the move.
    /// </summary>
    public MoveBase Base { get { return _base; } private set { } }
}