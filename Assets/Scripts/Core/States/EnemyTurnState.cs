using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTurnState : BaseState
{
    private float timeToAction = 0f;
    private float behaviourProbability = 0.7f;
    private float MAX_THINKING_TIME;
    private bool finished = false;
    private Slot slotSelected;
    private Card cardSelected;

    /// <summary>
    /// Called when a state start.
    /// </summary>
    public override void Enter()
    {
        finished = false;
        timeToAction = 0f;
        turnController.TeamTurn = Team.RED;
        MAX_THINKING_TIME = UnityEngine.Random.Range(0.7f, 1.2f);
        turnController.TurnIndicator.transform.position = new Vector3(Mathf.Abs(turnController.TurnIndicator.transform.position.x) * -1,
                                                                    turnController.TurnIndicator.transform.position.y,
                                                                    turnController.TurnIndicator.transform.position.z);

        if (UnityEngine.Random.Range(0f, 1f) < behaviourProbability)
        {
            AdvancedAI();
        }
        else
        {
            BasicAI();
        }
    }

    /// <summary>
    /// Called when a state finish.
    /// </summary>
    public override void Exit()
    {
        //Nothing
    }

    /// <summary>
    /// Called by the state every frame.
    /// </summary>
    public override void UpdateLogic()
    {
        timeToAction += Time.deltaTime;

        if (timeToAction >= MAX_THINKING_TIME && finished)
        {
            cardSelected.UpdateOrderInLayer(300);
            slotSelected.PlaceCard(cardSelected);

            if (GameController.Instance.Board.Slots.Any(slot => !slot.Occupied))
            {
                turnController.ChangeState(new PlayerTurnState());
            }
            else
            {
                turnController.ChangeState(new EndMatchState());
            }
        }
    }

    /// <summary>
    /// Basic artificial intelligence for the enemy.
    /// Will drop a random card in a random slot.
    /// </summary>
    public void BasicAI()
    {
        slotSelected = GameController.Instance.Board.Slots.Where(slot => !slot.Occupied).FirstOrDefault();
        cardSelected = GameController.Instance.GameCards.Where(card => !card.Placed && card.Team == Team.RED).FirstOrDefault();
        finished = true;
    }

    /// <summary>
    /// Advanced artificial intelligence for the enemy.
    /// Will drop the best card in the best slot possible. This is decided by a point system that will add points if the card capture some enemy cards.
    /// </summary>
    public void AdvancedAI()
    {
        List<Tuple<Card, Slot, int>> cardRanking = new List<Tuple<Card, Slot, int>>();

        GameController.Instance.GameCards.Where(card => !card.Placed && card.Team == Team.RED).ToList().ForEach(card =>
        {
            GameController.Instance.Board.Slots.Where(slot => !slot.Occupied).ToList().ForEach(slot =>
            {
                cardRanking.Add(new Tuple<Card, Slot, int>(card, slot, card.AttackSimulation(slot)));
            });
        });

        cardRanking = cardRanking.OrderByDescending(tuple => tuple.Item3).ToList();
        cardSelected = cardRanking.FirstOrDefault().Item1;
        slotSelected = cardRanking.FirstOrDefault().Item2;
        finished = true;
    }
}
