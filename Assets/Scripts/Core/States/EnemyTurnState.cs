using System.Linq;
using UnityEngine;

public class EnemyTurnState : BaseState
{
    private float timeToAction = 0f;
    private float MAX_THINKING_TIME;
    private Slot slotSelected;
    private Card cardSelected;

    /// <summary>
    /// Called when a state start.
    /// </summary>
    public override void Enter()
    {
        turnController.TeamTurn = Team.RED;
        turnController.TurnIndicator.transform.position = new Vector3(Mathf.Abs(turnController.TurnIndicator.transform.position.x) * -1,
                                                                    turnController.TurnIndicator.transform.position.y,
                                                                    turnController.TurnIndicator.transform.position.z);
        timeToAction = 0f;
        MAX_THINKING_TIME = Random.Range(0.7f, 1.2f);
        BasicAI();
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

        if (timeToAction >= MAX_THINKING_TIME)
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
    }
}
