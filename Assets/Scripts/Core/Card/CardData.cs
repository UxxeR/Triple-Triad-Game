using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Create new card", order = 0)]
public class CardData : ScriptableObject, ITableElement
{
    [field: SerializeField] public string Id { get; set; }
    [field: SerializeField] public string CardName { get; set; }
    [field: SerializeField] public string CardDescription { get; set; }
    [field: Range(1, 10)] [field: Min(1)] [field: SerializeField] public int[] Power { get; set; } = new int[4];
    [field: SerializeField] public Sprite CardSprite { get; set; }
    [field: SerializeField] public ElementType ElementType { get; set; }
}

public enum ElementType
{
    NONE = 0,
    FIRE = 1,
    ICE = 2,
    WIND = 3,
    EARTH = 4,
    WATER = 5,
    POISON = 6,
    HOLY = 7,
    LIGHTNING = 8,
    DARKNESS = 9
}