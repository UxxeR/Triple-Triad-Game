using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDeckSelector : MonoBehaviour
{
    [SerializeField] private Button createDeckButton;
    [SerializeField] private TMP_Text currentDecks;

    private void Update()
    {
        UpdateDecksText(0);
        createDeckButton.interactable = DeckController.Instance.CheckIfCanAddDeck();
    }

    public void UpdateDecksText(int decks)
    {
        currentDecks.text = $"{DeckController.Instance.deckContainer.childCount} / {DeckController.Instance.MAX_CARDS}";
    }
}
