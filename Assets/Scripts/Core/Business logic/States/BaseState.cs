using System;
using System.Collections.Generic;
using System.Linq;

public abstract class BaseState
{
    protected TurnController turnController;
    protected Card cardSelected;
    protected Slot slotSelected;

    /// <summary>
    /// Called when a class is created.
    /// Set default values of the class.
    /// Values are inherited by the other states.
    /// </summary>
    public BaseState()
    {
        turnController = TurnController.Instance;
    }

    /// <summary>
    /// Basic artificial intelligence for the enemy.
    /// Will drop a random card in a random slot.
    /// </summary>
    public void BasicAI(Team team)
    {
        slotSelected = GameController.Instance.Board.Slots.Where(slot => !slot.Occupied).FirstOrDefault();
        cardSelected = GameController.Instance.GameCards.Where(card => !card.Placed && card.Team == team).FirstOrDefault();
    }

    /// <summary>
    /// Advanced artificial intelligence for the enemy.
    /// Will drop the best card in the best slot possible. This is decided by a point system that will add points if the card capture some enemy cards.
    /// </summary>
    public void AdvancedAI(Team team)
    {
        List<Tuple<Card, Slot, int>> cardRanking = new List<Tuple<Card, Slot, int>>();

        GameController.Instance.GameCards.Where(card => !card.Placed && card.Team == team).ToList().ForEach(card =>
        {
            GameController.Instance.Board.Slots.Where(slot => !slot.Occupied).ToList().ForEach(slot =>
            {
                cardRanking.Add(new Tuple<Card, Slot, int>(card, slot, card.AttackSimulation(slot)));
            });
        });

        cardRanking = cardRanking.OrderByDescending(tuple => tuple.Item3).ToList();
        cardSelected = cardRanking.FirstOrDefault().Item1;
        slotSelected = cardRanking.FirstOrDefault().Item2;
    }

    /// <summary>
    /// Called when a state start.
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// Called when a state finish.
    /// </summary>
    public abstract void Exit();

    /// <summary>
    /// Called by the state every frame.
    /// </summary>
    public abstract void UpdateLogic();
}
