using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public CardData cardData;
    public SpriteRenderer background;
    public SpriteRenderer cardSprite;
    public SpriteRenderer elementSprite;
    public Side[] sides = new Side[4];
    public Team team;
    public bool dragging = false;
    public bool placed = false;
    Vector3 startPosition;
    Vector3 offsetToMouse;
    float zDistanceToCamera;

    private void Awake()
    {
        cardData = Instantiate(cardData);

        for (int i = 0; i < sides.Length; i++)
        {
            this.sides[i].powerText.text = this.cardData.power[i].ToString();
        }

        UpdateTeam(this.team);
        cardSprite.sprite = cardData.cardSprite;
        elementSprite.sprite = Resources.Load<Sprite>($"Sprites/Elements/{cardData.elementType.ToString()}");
        startPosition = transform.position;
        GameController.instance.gameCards.Add(this);
    }

    public void UpdateRaycast(LayerMask layer)
    {
        CustomPhysicsBehaviour.instance.raycaster.eventMask = layer;
    }


    public void UpdateTeam(Team team)
    {
        this.team = team;
        this.background.color = GenericAttribute.GetAttribute<CustomColorAttribute>(team).HexadecimalToRGBColor();

        for (int i = 0; i < sides.Length; i++)
        {
            this.sides[i].background.color = GenericAttribute.GetAttribute<CustomColorAttribute>(team).HexadecimalToRGBColor();
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

            if (enemy != null && cardData.power[i] > enemy.cardData.power[powerIndex] && enemy.placed)
            {
                enemy.UpdateTeam(team);
            }
        }
    }

    public void IncreasePower()
    {
        this.cardData.power = this.cardData.power.Select(power => ++power).ToArray();

        for (int i = 0; i < sides.Length; i++)
        {
            this.sides[i].powerText.text = this.cardData.power[i].ToString();
            this.sides[i].powerText.color = GenericAttribute.GetAttribute<CustomColorAttribute>(Power.INCREASED).HexadecimalToRGBColor();
        }
    }

    public void DecreasePower()
    {
        this.cardData.power = cardData.power.Select(power => --power).ToArray();

        for (int i = 0; i < sides.Length; i++)
        {
            this.sides[i].powerText.text = this.cardData.power[i].ToString();
            this.sides[i].powerText.color = GenericAttribute.GetAttribute<CustomColorAttribute>(Power.DECREASED).HexadecimalToRGBColor();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (this.placed)
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
        if (Input.touchCount > 1 || this.placed)
        {
            return;
        }

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistanceToCamera)) + offsetToMouse;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.placed)
        {
            return;
        }

        Slot targetSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<Slot>();

        dragging = false;

        if (targetSlot == null || (targetSlot != null && targetSlot.occupied))
        {

            this.transform.position = startPosition;
        }

        UpdateRaycast(LayerMask.NameToLayer("Everything"));
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
