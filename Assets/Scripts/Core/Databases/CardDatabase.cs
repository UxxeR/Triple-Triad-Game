using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : GenericDatabase<CardData>
{
    [SerializeField] private CardTable cardTable;

    private void Awake()
    {
        Instance = this;
        table = cardTable;
    }
}
