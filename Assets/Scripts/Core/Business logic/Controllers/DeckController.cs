using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public static DeckController Instance { get; private set; }
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform collectionWindow;

    private void Awake()
    {
        Instance = this;
        CardDatabase.Instance.GetAllElement().ForEach(cardData =>
        {
            Card card = Instantiate(cardPrefab, collectionWindow, false).GetComponent<Card>();
            card.CardData = cardData;
        });
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
