using System.Linq;
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

    public void UpdateDeckData(DeckData deckData)
    {
        this.deckData = deckData;
        deckName.text = deckData.Id;
    }
}
