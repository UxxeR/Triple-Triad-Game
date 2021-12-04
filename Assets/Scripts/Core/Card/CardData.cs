using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Create new card", order = 0)]
public class CardData : ScriptableObject
{
    public string id;
    public string cardName;
    public int[] power = new int[4];
    public Sprite cardSprite;
    public ElementType elementType;
}

public enum ElementType {
    NONE,
    FIRE,
    ICE
}