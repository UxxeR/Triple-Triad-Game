using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardTable", menuName = "Database/CardTable", order = 0)]
public class CardTable : ScriptableObject, ITable<CardData>
{
    public string Id => "CardTable";
    [field: SerializeField] public List<CardData> Elements { get; set; }
}
