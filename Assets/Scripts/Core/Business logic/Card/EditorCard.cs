using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorCard : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image cardSprite;
    [SerializeField] private Image elementSprite;
    [SerializeField] private Image frameSprite;
    [SerializeField] private Side[] sides = new Side[4];
    [SerializeField] private Button button;
    [SerializeField] private bool selected = false;
    private int childPosition = 0;
    [field: SerializeField] public CanvasGroup CanvasGroup { get; set; }
    [field: SerializeField] public CardData CardData { get; set; }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time. Only called once.
    /// </summary>
    private void Start()
    {
        CardData = Instantiate(CardData);
        cardSprite.sprite = CardData.CardSprite;
        UpdatePowerText(Power.NORMAL);
        elementSprite.sprite = Resources.Load<Sprite>($"Sprites/Elements/{CardData.ElementType}");
        childPosition = transform.GetSiblingIndex();
        button.onClick.AddListener(() =>
        {
            if (!selected)
            {
                SelectCard();
            }
            else
            {
                DeselectCard();
            }
        }
        );
    }

    /// <summary>
    /// Update the text and the color of the text of the sides of the card.
    /// </summary>
    /// <param name="powerType">Determines the color of the text (green if is increased or red if is decreased).</param>
    public void UpdatePowerText(Power powerType)
    {
        for (int i = 0; i < sides.Length; i++)
        {
            this.sides[i].PowerText.text = this.CardData.Power[i].ToString();
            this.sides[i].PowerText.color = GenericAttribute.GetAttribute<CustomColorAttribute>(powerType).HexadecimalToRGBColor();
        }
    }

    public void SelectCard()
    {
        if (!DeckController.Instance.CanAddCardToDeck()) return;

        selected = true;
        DeckController.Instance.CurrentEditorCardsSelected.Add(this.CardData.Id);
        this.transform.SetParent(DeckController.Instance.SelectedEditorCardsContainer, false);
        
    }

    public void DeselectCard()
    {
        selected = false;
        DeckController.Instance.CurrentEditorCardsSelected.Remove(this.CardData.Id);
        this.transform.SetParent(DeckController.Instance.deckCardsContainer, false);
        this.transform.SetSiblingIndex(childPosition);
    }
}
