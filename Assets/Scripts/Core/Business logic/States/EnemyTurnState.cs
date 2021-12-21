using System.Linq;
using UnityEngine;

public class EnemyTurnState : BaseState
{
    private float timeToAction = 0f;
    private float timeToCalculateCardPosition = 0f;
    private float behaviourProbability = 0.7f;
    private float MAX_THINKING_TIME;
    private bool finished = false;
    private bool calculating = false;

    /// <summary>
    /// Called when a state start.
    /// </summary>
    public override void Enter()
    {
        finished = false;
        calculating = false;
        timeToAction = 0f;
        turnController.UpdateTimer();
        turnController.TeamTurn = Team.RED;
        MAX_THINKING_TIME = UnityEngine.Random.Range(0.7f, 1.2f);
        turnController.TurnIndicator.transform.position = new Vector3(Mathf.Abs(turnController.TurnIndicator.transform.position.x) * -1,
                                                                    turnController.TurnIndicator.transform.position.y,
                                                                    turnController.TurnIndicator.transform.position.z);
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
        timeToCalculateCardPosition += Time.deltaTime;
        turnController.TurnTimer -= Time.deltaTime;
        turnController.UpdateTimerProgress();

        if (timeToCalculateCardPosition > 0.3f)
        {
            if (UnityEngine.Random.Range(0f, 1f) < behaviourProbability)
            {
                calculating = true;
                AdvancedAI(Team.RED);
                finished = true;
            }
            else
            {
                calculating = true;
                BasicAI(Team.RED);
                finished = true;
            }
        }

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


}
