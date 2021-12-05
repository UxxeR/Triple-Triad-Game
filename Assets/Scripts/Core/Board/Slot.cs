using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    [Range(0f, 1f)] [SerializeField] private float elementalProbability = 0.3f;
    public SpriteRenderer elementSprite;
    public ElementType elementType;

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
        if (eventData.pointerDrag != null)
        {
            Card card = eventData.pointerDrag.GetComponent<Card>();

            if (card.placed)
            {
                return;
            }

            card.UpdateRaycast(LayerMask.NameToLayer("Everything"));
            card.placed = true;
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.transform.position = this.transform.position;
            card.Attack();
        }
    }
}
