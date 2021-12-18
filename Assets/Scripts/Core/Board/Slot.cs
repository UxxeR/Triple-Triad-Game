using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    [Range(0f, 1f)] [SerializeField] private float elementalProbability = 0.3f;
    [SerializeField] private SpriteRenderer elementSprite;
    [field: SerializeField] public ElementType ElementType { get; set; }
    [field: SerializeField] public bool Occupied { get; set; } = false;

    /// <summary>
    /// First method that will be called when a script is enabled. Only called once.
    /// </summary>
    private void Awake()
    {
        if (Random.Range(0f, 1f) < elementalProbability)
        {
            ElementType = (ElementType)Random.Range(1, System.Enum.GetNames(typeof(ElementType)).Length);
        }

        elementSprite.sprite = Resources.Load<Sprite>($"Sprites/Elements/{ElementType.ToString()}");
    }

    /// <summary>
    /// Called before a drag is ended if the object is above.
    /// </summary>
    /// <param name="eventData">The object that is dropped.</param>
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && !Occupied)
        {
            Card card = eventData.pointerDrag.GetComponent<Card>();
            PlaceCard(card);
        }
    }

    /// <summary>
    /// Place a card in a slot and decrease or decrease his power depending if the element of the slot is the same as the card element.
    /// The card will attack the adjacent cards and update the score after that.
    /// </summary>
    /// <param name="card">The card that will me placed in the slot.</param>
    public void PlaceCard(Card card)
    {
        if (card.Placed)
        {
            return;
        }

        Occupied = true;
        card.Placed = true;
        card.UpdateOrderInLayer(-300);
        card.transform.SetParent(transform);
        card.transform.position = this.transform.position;

        if (!(ElementType == ElementType.NONE))
        {
            if (ElementType == card.CardData.ElementType)
            {
                card.IncreasePower();
            }
            else
            {
                card.DecreasePower();
            }
        }

        card.Attack();
        GameController.Instance.UpdateScore();
        TurnController.Instance.TurnEnded = true;
    }
}
