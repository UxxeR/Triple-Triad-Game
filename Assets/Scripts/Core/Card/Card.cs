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
    public Vector3 startPosition;
    private Vector3 offsetToMouse;
    private float zDistanceToCamera;
    [field: SerializeField] public Team Team { get; set; }
    [field: SerializeField] public Power Power { get; set; } = Power.NORMAL;
    [field: SerializeField] public CardData CardData { get; set; }
    [field: SerializeField] public int[] OriginalPower { get; set; }
    [field: SerializeField] public bool Placed { get; set; } = false;

    /// <summary>
    /// First method that will be called when a script is enabled. Only called once.
    /// </summary>
    private void Awake()
    {
        startPosition = transform.position;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time. Only called once.
    /// </summary>
    private void Start()
    {
        CardData = Instantiate(CardData);
        OriginalPower = CardData.Power;
        UpdatePowerText(Power.NORMAL);
        UpdateTeam(this.Team);
        cardSprite.sprite = CardData.CardSprite;
        elementSprite.sprite = Resources.Load<Sprite>($"Sprites/Elements/{CardData.ElementType.ToString()}");
    }

    /// <summary>
    /// Update the layers that can be interacted.
    /// </summary>
    /// <param name="layer">The layers should be writed using bitwise.</param>
    public void UpdateRaycast(LayerMask layer)
    {
        GameController.Instance.UpdateRaycastPhysics(layer);
    }

    /// <summary>
    /// Update the team of a selected card.
    /// </summary>
    /// <param name="team">Card's new team.</param>
    public void UpdateTeam(Team team)
    {
        this.Team = team;
        this.background.color = GenericAttribute.GetAttribute<CustomColorAttribute>(team).HexadecimalToRGBColor();

        for (int i = 0; i < sides.Length; i++)
        {
            this.sides[i].Background.color = GenericAttribute.GetAttribute<CustomColorAttribute>(team).HexadecimalToRGBColor();
        }
    }

    /// <summary>
    /// Update the order in layer of the sprite renderer components of a card so it will be in front or behind other elements in the scene.
    /// </summary>
    /// <param name="layer">New layer number that will be summed.</param>
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

    /// <summary>
    /// Card will attack the adjacent cards and conquer them if their power side is lesser than yours, apply plus, same, same wall and combo rules.
    /// </summary>
    /// <param name="isCombo">Especify if the attack is from the main card (combo = false) or if the attack is from an adjacent card (combo = true).</param>
    public void Attack(bool isCombo = false)
    {
        int powerIndex = 0;
        bool activeRule = false;
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
                if (CardData.Power[i] > enemy.CardData.Power[powerIndex] && enemy.Team != this.Team)
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

        if (!isCombo)
        {
            //Gets the cards that are affected by same rule
            sameRuleCards.GroupBy(pair => pair.Value)
            .Where(group => group.Count() >= 2 && group.Key)
            .SelectMany(group => group)
            .ToList()
            .ForEach(pair =>
            {
                activeRule = true;

                if (pair.Key.Team != this.Team)
                {
                    pair.Key.UpdateTeam(Team);
                    pair.Key.Attack(true);
                }
            });

            //Gets the cards that are affected by plus rule
            plusRuleCards.GroupBy(pair => pair.Value)
            .Where(group => group.Count() >= 2)
            .SelectMany(group => group)
            .ToList()
            .ForEach(pair =>
            {
                activeRule = true;

                if (pair.Key.Team != this.Team)
                {
                    pair.Key.UpdateTeam(Team);
                    pair.Key.Attack(true);
                }
            });
        }

        capturedCards.ForEach(card =>
        {
            card.UpdateTeam(this.Team);

            if (activeRule)
            {
                card.Attack(true);
            }
        });
    }

    /// <summary>
    /// Card will simulate an attack to the adjacent cards and add points if conquer them, apply plus, same, same wall and combo rules. Is a Minimax like solution with depth 0.
    /// </summary>
    /// <param name="slot">Slot in which will simulate the attack.</param>
    /// <param name="isCombo">Especify if the attack is from the main card (combo = false) or if the attack is from an adjacent card (combo = true).</param>
    public int AttackSimulation(Slot slot, bool isCombo = false)
    {
        int powerIndex = 0;
        int simulationPoints = 0;
        bool activeRule = false;
        Slot capturedSlot;
        List<Card> capturedCards = new List<Card>();
        List<KeyValuePair<Card, bool>> sameRuleCards = new List<KeyValuePair<Card, bool>>();
        List<KeyValuePair<Card, int>> plusRuleCards = new List<KeyValuePair<Card, int>>();

        if (!isCombo)
        {
            this.transform.position = slot.transform.position;
        }

        if (CardData.ElementType == slot.ElementType)
        {
            ++simulationPoints;
        }
        else if (slot.ElementType != ElementType.NONE && CardData.ElementType != slot.ElementType)
        {
            --simulationPoints;
        }

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
                if (CardData.Power[i] > enemy.CardData.Power[powerIndex] && enemy.Team != this.Team)
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

        if (!isCombo)
        {
            //Gets the cards that are affected by same rule
            sameRuleCards.GroupBy(pair => pair.Value)
            .Where(group => group.Count() >= 2 && group.Key)
            .SelectMany(group => group)
            .ToList()
            .ForEach(pair =>
            {
                activeRule = true;

                if (pair.Key.Team != this.Team)
                {
                    capturedSlot = pair.Key.GetComponentInParent<Slot>();
                    simulationPoints += pair.Key.AttackSimulation(capturedSlot, true);
                }
            });

            //Gets the cards that are affected by plus rule
            plusRuleCards.GroupBy(pair => pair.Value)
            .Where(group => group.Count() >= 2)
            .SelectMany(group => group)
            .ToList()
            .ForEach(pair =>
            {
                activeRule = true;

                if (pair.Key.Team != this.Team)
                {
                    capturedSlot = pair.Key.GetComponentInParent<Slot>();
                    simulationPoints += pair.Key.AttackSimulation(capturedSlot, true);
                }
            });
        }

        capturedCards.ForEach(card =>
        {
            simulationPoints += 2;

            if (activeRule)
            {
                capturedSlot = card.GetComponentInParent<Slot>();
                simulationPoints += card.AttackSimulation(capturedSlot, true);
            }
        });

        if (!isCombo)
        {
            this.transform.position = startPosition;
        }

        return simulationPoints;
    }

    /// <summary>
    /// Increase the power of the card in all sides in 1.
    /// </summary>
    public void IncreasePower()
    {
        this.CardData.Power = this.CardData.Power.Select(power => power = Mathf.Clamp(power + 1, 1, 10)).ToArray();
        UpdatePowerText(Power.INCREASED);
    }

    /// <summary>
    /// Decrease the power of the card in all sides in 1.
    /// </summary>
    public void DecreasePower()
    {
        this.CardData.Power = CardData.Power.Select(power => power = Mathf.Clamp(power - 1, 1, 10)).ToArray();
        UpdatePowerText(Power.DECREASED);
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

        Power = powerType;
    }

    /// <summary>
    /// Called before a drag is started.
    /// </summary>
    /// <param name="eventData">The object that is dragged.</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (this.Placed)
        {
            return;
        }

        UpdateOrderInLayer(300);
        UpdateRaycast(1 << LayerMask.NameToLayer("Slot"));
        zDistanceToCamera = Mathf.Abs(startPosition.z - Camera.main.transform.position.z);
        offsetToMouse = startPosition - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistanceToCamera));
    }

    /// <summary>
    /// Called while is dragged.
    /// </summary>
    /// <param name="eventData">The object that is dragged.</param>
    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 1 || this.Placed)
        {
            return;
        }

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistanceToCamera)) + offsetToMouse;
    }

    /// <summary>
    /// Called after a drag has ended.
    /// </summary>
    /// <param name="eventData">The object that is dragged.</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.Placed)
        {
            return;
        }

        Slot targetSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<Slot>();

        if (targetSlot == null || (targetSlot != null && targetSlot.Occupied))
        {
            this.transform.position = startPosition;
            UpdateRaycast(1 << LayerMask.NameToLayer("PlayerCard"));
            UpdateOrderInLayer(-300);
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
