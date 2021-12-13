using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    [Range(0f, 1f)] [SerializeField] private float elementalProbability = 0.3f;
    [SerializeField] private SpriteRenderer elementSprite;
    [SerializeField] private ElementType elementType;
    [field: SerializeField] public bool Occupied { get; set; } = false;

    private void Awake()
    {
        if (Random.Range(0f, 1f) < elementalProbability)
        {
            elementType = (ElementType)Random.Range(1, System.Enum.GetNames(typeof(ElementType)).Length);
        }

        elementSprite.sprite = Resources.Load<Sprite>($"Sprites/Elements/{elementType.ToString()}");
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && !Occupied)
        {
            Card card = eventData.pointerDrag.GetComponent<Card>();
            PlaceCard(card);
        }
    }

    public void PlaceCard(Card card)
    {
        if (card.Placed)
        {
            return;
        }

        Occupied = true;
        card.Placed = true;
        card.transform.SetParent(transform);
        card.transform.position = this.transform.position;

        if (!(elementType == ElementType.NONE))
        {
            if (elementType == card.CardData.ElementType)
            {
                card.IncreasePower();
            }
            else
            {
                card.DecreasePower();
            }
        }

        card.Attack(false);
        GameController.Instance.UpdateScore();
        TurnController.Instance.TurnEnded = true;
    }
}
