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
            Occupied = true;
            Card card = eventData.pointerDrag.GetComponent<Card>();

            if (card.Placed)
            {
                return;
            }

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

            card.Attack();
            GameController.Instance.UpdateScore();
            TurnController.Instance.TurnEnded = true;
        }
    }

    public void PlaceCard(Card card)
    {
        Occupied = true;

        if (card.Placed)
        {
            return;
        }

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

        card.Attack();
        GameController.Instance.UpdateScore();
        TurnController.Instance.TurnEnded = true;
    }
}
