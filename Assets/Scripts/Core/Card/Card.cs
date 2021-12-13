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
    [SerializeField] private SpriteRenderer frameSprite;
    [SerializeField] private Side[] sides = new Side[4];
    [SerializeField] private bool dragging = false;
    public Vector3 startPosition;
    private Vector3 offsetToMouse;
    private float zDistanceToCamera;
    [field: SerializeField] public Team Team { get; set; }
    [field: SerializeField] public Power Power { get; set; } = Power.NORMAL;
    [field: SerializeField] public CardData CardData { get; set; }
    [field: SerializeField] public int[] OriginalPower { get; set; }
    [field: SerializeField] public bool Placed { get; set; } = false;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Start()
    {
        CardData = Instantiate(CardData);
        OriginalPower = CardData.Power;
        UpdatePowerText(Power.NORMAL);
        UpdateTeam(this.Team);
        cardSprite.sprite = CardData.CardSprite;
        elementSprite.sprite = Resources.Load<Sprite>($"Sprites/Elements/{CardData.ElementType.ToString()}");
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

    public void UpdateOrderInLayer(int layer)
    {
        background.sortingOrder += layer;
        cardSprite.sortingOrder += layer;
        elementSprite.sortingOrder += layer;
        frameSprite.sortingOrder += layer;

        foreach (Side side in sides)
        {
            side.Background.sortingOrder += layer;
            side.Frame.sortingOrder += layer;
            side.PowerText.GetComponent<MeshRenderer>().sortingOrder += layer;
        }

    }

    public void Attack(bool isCombo)
    {
        int powerIndex = 0;
        List<Card> capturedCards = new List<Card>();
        List<KeyValuePair<Card, bool>> sameRuleCards = new List<KeyValuePair<Card, bool>>();
        List<KeyValuePair<Card, int>> plusRuleCards = new List<KeyValuePair<Card, int>>();

        for (int i = 0; i < sides.Length; i++)
        {
            //Getting opposite side of power of enemy
            Card enemy = this.sides[i].GetTarget();
            powerIndex = i + 2;

            if (powerIndex >= sides.Length)
            {
                powerIndex = powerIndex % 2;
            }

            if (enemy != null && enemy.Placed)
            {
                //Normal capture
                if (CardData.Power[i] > enemy.CardData.Power[powerIndex])
                {
                    capturedCards.Add(enemy);
                }

                //Same rule capture
                sameRuleCards.Add(new KeyValuePair<Card, bool>(enemy, CardData.Power[i] == enemy.CardData.Power[powerIndex]));


                //Plus rule capture
                if (enemy.gameObject.layer != LayerMask.NameToLayer("Wall"))
                {
                    plusRuleCards.Add(new KeyValuePair<Card, int>(enemy, CardData.Power[i] + enemy.CardData.Power[powerIndex]));
                }
            }
        }

        capturedCards.ForEach(card => card.UpdateTeam(Team));

        if (!isCombo)
        {
            sameRuleCards.GroupBy(pair => pair.Value)
            .Where(group => group.Count() >= 2 && group.Key)
            .SelectMany(group => group)
            .ToList()
            .ForEach(pair =>
            {
                pair.Key.UpdateTeam(Team);
                pair.Key.Attack(true);
            });

            plusRuleCards.GroupBy(pair => pair.Value)
            .Where(group => group.Count() >= 2)
            .SelectMany(group => group)
            .ToList()
            .ForEach(pair =>
            {
                pair.Key.UpdateTeam(Team);
                pair.Key.Attack(true);
            });
        }
    }

    public void IncreasePower()
    {
        this.CardData.Power = this.CardData.Power.Select(power => power = Mathf.Clamp(power + 1, 1, 10)).ToArray();
        UpdatePowerText(Power.INCREASED);
    }

    public void DecreasePower()
    {
        this.CardData.Power = CardData.Power.Select(power => power = Mathf.Clamp(power - 1, 1, 10)).ToArray();
        UpdatePowerText(Power.DECREASED);
    }

    public void UpdatePowerText(Power powerType)
    {
        for (int i = 0; i < sides.Length; i++)
        {
            this.sides[i].PowerText.text = this.CardData.Power[i].ToString();
            this.sides[i].PowerText.color = GenericAttribute.GetAttribute<CustomColorAttribute>(powerType).HexadecimalToRGBColor();
        }

        Power = powerType;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (this.Placed)
        {
            return;
        }

        dragging = true;
        UpdateOrderInLayer(300);
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

        UpdateOrderInLayer(-300);
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
