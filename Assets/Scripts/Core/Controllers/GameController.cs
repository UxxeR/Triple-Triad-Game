using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    [field: SerializeField] public Board Board { get; set; }
    [field: SerializeField] public List<Card> GameCards { get; set; } = new List<Card>();
    public Action<int, int> OnSlotUpdated { get; set; }
    public Action<LayerMask> OnRaycastUpdated { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateScore();
        TurnController.Instance.ChangeState(new StartMatchState());
    }

    public void UpdateScore()
    {
        if (OnSlotUpdated != null)
        {
            int blueScore = GameCards.Where(card => card.Team == Team.BLUE).Count();
            OnSlotUpdated(blueScore, GameCards.Count - blueScore);
        }
    }

    public void UpdateRaycastPhysics(LayerMask layer)
    {
        if (OnRaycastUpdated != null)
        {
            OnRaycastUpdated(layer);
        }
    }
}
