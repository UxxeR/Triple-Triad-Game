using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDeck : MonoBehaviour
{
    [SerializeField] private Button deckButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text deckName;
    [SerializeField] private Toggle ActiveToggle;
    [SerializeField] public DeckData deckData;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time. Only called once.
    /// </summary>
    private void Start()
    {
        if (deckName.text == "default")
        {
            deckButton.interactable = false;
            deleteButton.gameObject.SetActive(false);
        }

        deckButton.onClick.AddListener(() =>
        {
            DeckController.Instance.ModifyDeck(this);
        });

        deleteButton.onClick.AddListener(() =>
        {
            DeckController.Instance.DeleteDeck(this);
        });

        ActiveToggle.group = this.transform.parent.GetComponent<ToggleGroup>();
        ActiveToggle.isOn = deckData.Active;

        ActiveToggle.onValueChanged.AddListener(value =>
        {
            if (value)
            {
                DeckController.Instance.UseDeck(deckData);
            }
        });
    }

    /// <summary>
    /// Update the deck data.
    /// </summary>
    /// <param name="deckData">The deck data that will be set.</param>
    public void UpdateDeckData(DeckData deckData)
    {
        this.deckData = deckData;
        deckName.text = deckData.Id;
    }
}
