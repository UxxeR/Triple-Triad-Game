using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDeckEditor : MonoBehaviour
{
    [SerializeField] private TMP_InputField InputDeckName;
    [SerializeField] private Button createDeckButton;
    [SerializeField] private TMP_Text currentCardsInDeck;

    private void Start()
    {
        DeckController.Instance.OnDeckNameUpdated += UpdateDeckName;
    }

    private void Update()
    {
        DeckController.Instance.currentDeckName = InputDeckName.text;
        UpdateCardsSelectedText(0);
        createDeckButton.interactable = DeckController.Instance.CanCreateDeck();
    }

    public void UpdateDeckName(string deckName)
    {
        InputDeckName.text = deckName;
    }

    public void UpdateCardsSelectedText(int cards)
    {
        currentCardsInDeck.text = $"{DeckController.Instance.SelectedEditorCardsContainer.childCount} / {DeckController.Instance.MAX_CARDS}";
    }
}
