using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private SpriteRenderer cardSprite;
    [SerializeField] private SpriteRenderer elementSprite;
    [SerializeField] private Side[] sides = new Side[4];
    [SerializeField] private bool dragging = false;
    private Vector3 startPosition;
    private Vector3 offsetToMouse;
    private float zDistanceToCamera;
    [field: SerializeField] public Team Team { get; set; }
    [field: SerializeField] public CardData CardData { get; set; }
    [field: SerializeField] public bool Placed { get; set; } = false;

    private void Awake()
    {
        CardData = Instantiate(CardData);

        for (int i = 0; i < sides.Length; i++)
        {
            this.sides[i].PowerText.text = this.CardData.Power[i].ToString();
        }

        UpdateTeam(this.Team);
        cardSprite.sprite = CardData.CardSprite;
        elementSprite.sprite = Resources.Load<Sprite>($"Sprites/Elements/{CardData.ElementType.ToString()}");
        startPosition = transform.position;
        GameController.Instance.GameCards.Add(this);
    }

    public void UpdateRaycast(LayerMask layer)
    {
        GameController.Instance.UpdateRaycastPhysics(layer);
    }


    public void UpdateTeam(Team team)
    {
        this.Team = team;
        this.background.color = GenericAttribute.GetAttribute<CustomColorAttribute>(team).HexadecimalToRGBColor();

        for (int i = 0; i < sides.Length; i++)
        {
            this.sides[i].Background.color = GenericAttribute.GetAttribute<CustomColorAttribute>(team).HexadecimalToRGBColor();
        }
    }

    public void Attack()
    {
        int powerIndex;

        for (int i = 0; i < sides.Length; i++)
        {
            Card enemy = this.sides[i].GetTarget();
            powerIndex = i + 2;

            if (powerIndex >= sides.Length)
            {
                powerIndex = powerIndex % 2;
            }

            if (enemy != null && CardData.Power[i] > enemy.CardData.Power[powerIndex] && enemy.Placed)
            {
                enemy.UpdateTeam(Team);
            }
        }
    }

    public void IncreasePower()
    {
        this.CardData.Power = this.CardData.Power.Select(power => ++power).ToArray();

        for (int i = 0; i < sides.Length; i++)
        {
            this.sides[i].PowerText.text = this.CardData.Power[i].ToString();
            this.sides[i].PowerText.color = GenericAttribute.GetAttribute<CustomColorAttribute>(Power.INCREASED).HexadecimalToRGBColor();
        }
    }

    public void DecreasePower()
    {
        this.CardData.Power = CardData.Power.Select(power => --power).ToArray();

        for (int i = 0; i < sides.Length; i++)
        {
            this.sides[i].PowerText.text = this.CardData.Power[i].ToString();
            this.sides[i].PowerText.color = GenericAttribute.GetAttribute<CustomColorAttribute>(Power.DECREASED).HexadecimalToRGBColor();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (this.Placed)
        {
            return;
        }

        dragging = true;
        UpdateRaycast(1 << LayerMask.NameToLayer("Slot"));
        zDistanceToCamera = Mathf.Abs(startPosition.z - Camera.main.transform.position.z);
        offsetToMouse = startPosition - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistanceToCamera));
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 1 || this.Placed)
        {
            return;
        }

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistanceToCamera)) + offsetToMouse;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.Placed)
        {
            return;
        }

        Slot targetSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<Slot>();

        dragging = false;

        if (targetSlot == null || (targetSlot != null && targetSlot.Occupied))
        {
            this.transform.position = startPosition;
            UpdateRaycast(1 << LayerMask.NameToLayer("PlayerCard"));
        }
    }
}

public enum Team
{
    [CustomColor("#222A5E")] BLUE = 0,
    [CustomColor("#5E2228")] RED = 1
}

public enum Power
{
    [CustomColor("#FFFFFF")] NORMAL = 0,
    [CustomColor("#52F113")] INCREASED = 1,
    [CustomColor("#F1141B")] DECREASED = 2
}
