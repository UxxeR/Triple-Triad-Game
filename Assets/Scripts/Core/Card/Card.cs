using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public CardData card;
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
    public Action<LayerMask> OnRaycastUpdated;

    private void Awake()
    {
        for (int i = 0; i < sides.Length; i++)
        {
            this.sides[i].powerText.text = this.card.power[i].ToString();
        }

        UpdateTeam(this.team);

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
        for (int i = 0; i < sides.Length; i++)
        {
            Card enemy = this.sides[i].GetTarget();

            if (enemy != null && card.power[i] > enemy.card.power[Mathf.Abs((i + 2) % 2)] && enemy.placed)
            {
                enemy.UpdateTeam(team);
            }
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
        startPosition = transform.position;
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

        dragging = false;

        if (eventData.pointerCurrentRaycast.gameObject?.GetComponent<Slot>() == null)
        {

        }

        UpdateRaycast(LayerMask.NameToLayer("Everything"));
    }
}

public enum Team
{
    [CustomColor("#222A5E")] BLUE = 0,
    [CustomColor("#5E2228")] RED = 1
}
