using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDeckEditor : MonoBehaviour
{
    [SerializeField] private TMP_InputField InputDeckName;
    [SerializeField] private Button createDeckButton;
    [SerializeField] private TMP_Text currentCardsInDeck;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time. Only called once.
    /// </summary>
    private void Start()
    {
        DeckController.Instance.OnDeckNameUpdated += UpdateDeckName;
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    private void Update()
    {
        DeckController.Instance.currentDeckName = InputDeckName.text;
        UpdateCardsSelectedText(0);
        createDeckButton.interactable = DeckController.Instance.CanCreateDeck();
    }

    /// <summary>
    /// Update the deck name.
    /// </summary>
    /// <param name="deckName">The new name of the deck.</param>
    public void UpdateDeckName(string deckName)
    {
        InputDeckName.text = deckName;
    }

    /// <summary>
    /// Update the number of cards that are in the deck container.
    /// </summary>
    /// <param name="cards">The current cards that are selected.</param>
    public void UpdateCardsSelectedText(int cards)
    {
        currentCardsInDeck.text = $"{DeckController.Instance.SelectedEditorCardsContainer.childCount} / {DeckController.Instance.MAX_CARDS}";
    }
}
