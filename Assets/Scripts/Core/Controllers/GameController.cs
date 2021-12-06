using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public List<Card> gameCards = new List<Card>();
    public Action<int, int> OnSlotUpdated;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        if (OnSlotUpdated != null)
        {
            int blueScore = gameCards.Where(card => card.team == Team.BLUE).Count();
            OnSlotUpdated(blueScore, gameCards.Count - blueScore);
        }
    }
}
