using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Create new card", order = 0)]
public class CardData : ScriptableObject
{
    public string id;
    public string cardName;
    public string cardDescription;
    [Range(1, 10)] public int[] power = new int[4];
    public Sprite cardSprite;
    public ElementType elementType;
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