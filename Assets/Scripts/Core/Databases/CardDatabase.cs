using UnityEngine;

public class CardDatabase : GenericDatabase<CardData>
{
    [SerializeField] private CardTable cardTable;

    /// <summary>
    /// First method that will be called when a script is enabled. Only called once.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        table = cardTable;
    }
}
