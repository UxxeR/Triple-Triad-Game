using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public static DeckController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void CreateDeck()
    {

    }

    public void ModifyDeck()
    {

    }

    public void DeleteDeck()
    {

    }
}
