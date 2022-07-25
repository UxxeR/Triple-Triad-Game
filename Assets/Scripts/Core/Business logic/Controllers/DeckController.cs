using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    /// <summary>
    /// First method that will be called when a script is enabled. Only called once.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        PopulateDecks();
        PopulateDeckCards();
    }

    /// <summary>
    /// Update the deck name.
    /// </summary>
    public void UpdateDeckName()
    {
        if (OnDeckNameUpdated != null)
        {
            OnDeckNameUpdated(currentDeckName);
        }
    }

    /// <summary>
    /// Create a new deck.
    /// </summary>
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

    /// <summary>
    /// Modify an existing deck.
    /// </summary>
    /// <param name="deck">The deck that will be modified.</param>
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

    /// <summary>
    /// Delete an existing deck.
    /// </summary>
    /// <param name="deck">The deck that will be deleted.</param>
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

    /// <summary>
    /// Select a deck to be used in the next round.
    /// </summary>
    /// <param name="deckData">The data of the deck that will play the player.</param>
    public void UseDeck(DeckData deckData)
    {
        Player.Instance.CurrentDeck = deckData;
        DataController.Instance.SavePlayerData();
    }

    /// <summary>
    /// Check if can add a card to the deck that is creating.
    /// </summary>
    /// <returns>True if the deck has less than the max cards. Else false.</returns>
    public bool CanAddCardToDeck()
    {
        return SelectedEditorCardsContainer.childCount < MAX_CARDS;
    }

    /// <summary>
    /// Check if can create the deck.
    /// </summary>
    /// <returns>True if the player fulfill all the requirements to create a deck. Else false.</returns>
    public bool CanCreateDeck()
    {
        return !CanAddCardToDeck() && currentDeckName != string.Empty && !Player.Instance.Decks.Any(deck => deck.Id == currentDeckName);
    }

    /// <summary>
    /// Check if can add a new deck.
    /// </summary>
    /// <returns>True if the player has less than the max decks. Else false.</returns>
    public bool CheckIfCanAddDeck()
    {
        return deckContainer.childCount < MAX_DECKS;
    }

    /// <summary>
    /// Return the cards in the deck container to his original position.
    /// </summary>
    public void ClearDeckEditor()
    {
        foreach (EditorCard card in SelectedEditorCardsContainer.GetComponentsInChildren<EditorCard>())
        {
            card.DeselectCard();
        }
    }

    /// <summary>
    /// Populate with all the cards that the player has obtained.
    /// </summary>
    public void PopulateDeckCards()
    {
        Player.Instance.UnlockedIdCards.ForEach(id =>
        {
            EditorCard card = Instantiate(deckCardPrefab, deckCardsContainer, false).GetComponent<EditorCard>();
            card.CardData = CardDatabase.Instance.FindElementById(id);
        });
    }

    /// <summary>
    /// Populate with all the decks that the player has created.
    /// </summary>
    public void PopulateDecks()
    {
        Player.Instance.Decks.ForEach(deck =>
        {
            UIDeck currentDeck = Instantiate(deckPrefab, deckContainer, false).GetComponent<UIDeck>();
            currentDeck.UpdateDeckData(deck);
        });
    }
}
