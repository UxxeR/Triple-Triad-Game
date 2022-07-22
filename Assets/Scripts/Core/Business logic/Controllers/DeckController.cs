using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeckController : MonoBehaviour
{
    public static DeckController Instance { get; private set; }
    [SerializeField] private GameObject deckCardPrefab;
    [SerializeField] private GameObject deckPrefab;
    [SerializeField] public Transform deckContainer;
    [SerializeField] public Transform deckCardsContainer;
    [SerializeField] public Transform SelectedEditorCardsContainer;
    [SerializeField] public CanvasGroup Editor;
    public Action<string> OnDeckNameUpdated;
    public readonly int MAX_CARDS = 5;
    public readonly int MAX_DECKS = 5;
    public UIDeck currentUIDeck;
    public string currentDeckName;
    public List<string> CurrentEditorCardsSelected { get; set; } = new List<string>();

    private void Awake()
    {
        Instance = this;
        PopulateDecks();
        PopulateDeckCards();
    }

    public void UpdateDeckName()
    {
        if (OnDeckNameUpdated != null)
        {
            OnDeckNameUpdated(currentDeckName);
        }
    }

    public void CreateDeck()
    {
        if (currentUIDeck != null)
        {
            DeleteDeck(currentUIDeck);
        }

        DeckData deckData = new DeckData(currentDeckName, CurrentEditorCardsSelected.ToList(), false);
        Player.Instance.Decks.Add(deckData);
        UIDeck newDeck = Instantiate(deckPrefab, deckContainer).GetComponent<UIDeck>();
        newDeck.UpdateDeckData(deckData);
        ClearDeckEditor();
        DataController.Instance.SavePlayerData();
    }

    public void ModifyDeck(UIDeck deck)
    {
        ClearDeckEditor();
        currentUIDeck = deck;
        currentDeckName = deck.deckData.Id;
        UpdateDeckName();
        deckCardsContainer.GetComponentsInChildren<EditorCard>().Where(card => deck.deckData.CardIds.Contains(card.CardData.Id)).ToList().ForEach(card => card.SelectCard());
        UIController.Instance.ShowWindow(Editor);
        DataController.Instance.SavePlayerData();
    }

    public void DeleteDeck(UIDeck deck)
    {
        if (Player.Instance.CurrentDeck == deck.deckData)
        {
            Player.Instance.CurrentDeck = Player.Instance.Decks.FirstOrDefault();
        }

        Player.Instance.Decks.Remove(deck.deckData);
        currentUIDeck = null;
        Destroy(deck.gameObject);
        DataController.Instance.SavePlayerData();
    }

    public void UseDeck(DeckData deckData)
    {
        Player.Instance.CurrentDeck = deckData;
        DataController.Instance.SavePlayerData();
    }

    public bool CanAddCardToDeck()
    {
        return SelectedEditorCardsContainer.childCount < MAX_CARDS;
    }

    public bool CanCreateDeck()
    {
        return !CanAddCardToDeck() && currentDeckName != string.Empty && !Player.Instance.Decks.Any(deck => deck.Id == currentDeckName);
    }

    public bool CheckIfCanAddDeck()
    {
        return deckContainer.childCount < MAX_DECKS;
    }

    public void ClearDeckEditor()
    {
        foreach (EditorCard card in SelectedEditorCardsContainer.GetComponentsInChildren<EditorCard>())
        {
            card.DeselectCard();
        }
    }

    public void PopulateDeckCards()
    {
        Player.Instance.UnlockedIdCards.ForEach(id =>
        {
            EditorCard card = Instantiate(deckCardPrefab, deckCardsContainer, false).GetComponent<EditorCard>();
            card.CardData = CardDatabase.Instance.FindElementById(id);
        });
    }

    public void PopulateDecks()
    {
        Player.Instance.Decks.ForEach(deck =>
        {
            UIDeck currentDeck = Instantiate(deckPrefab, deckContainer, false).GetComponent<UIDeck>();
            currentDeck.UpdateDeckData(deck);
        });
    }
}
