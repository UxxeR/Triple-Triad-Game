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
    public Collider2D cardCollider;
    public SpriteRenderer elementSprite;
    public Side[] sides = new Side[4];
    public Team team;
    public bool dragging = false;
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

    private void UpdateRaycast(LayerMask layer)
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        this.cardCollider.enabled = false;
        UpdateRaycast(1 << LayerMask.NameToLayer("Slot"));
        startPosition = transform.position;
        zDistanceToCamera = Mathf.Abs(startPosition.z - Camera.main.transform.position.z);
        offsetToMouse = startPosition - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistanceToCamera));
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 1)
            return;

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistanceToCamera)) + offsetToMouse;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;

        if (eventData.pointerCurrentRaycast.gameObject?.GetComponent<Slot>() == null)
        {
            this.cardCollider.enabled = true;
        }

        UpdateRaycast(LayerMask.NameToLayer("Everything"));
    }
}

public enum Team
{
    [CustomColor("#222A5E")] BLUE = 0,
    [CustomColor("#5E2228")] RED = 1
}
