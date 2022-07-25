using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDeckSelector : MonoBehaviour
{
    [SerializeField] private Button createDeckButton;
    [SerializeField] private TMP_Text currentDecks;

    /// <summary>
    /// Called every frame.
    /// </summary>
    private void Update()
    {
        UpdateDecksText(0);
        createDeckButton.interactable = DeckController.Instance.CheckIfCanAddDeck();
    }

    /// <summary>
    /// Update number of decks that are created.
    /// </summary>
    /// <param name="decks">The current number of decks.</param>
    public void UpdateDecksText(int decks)
    {
        currentDecks.text = $"{DeckController.Instance.deckContainer.childCount} / {DeckController.Instance.MAX_CARDS}";
    }
}
