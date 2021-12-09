using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTurnState : BaseState
{
    public override void Enter()
    {
        turnController.TurnEnded = false;
        turnController.TeamTurn = Team.BLUE;
        turnController.TurnIndicator.transform.position = new Vector3(Mathf.Abs(turnController.TurnIndicator.transform.position.x),
                                                                    turnController.TurnIndicator.transform.position.y,
                                                                    turnController.TurnIndicator.transform.position.z);
        GameController.Instance.UpdateRaycastPhysics(1 << LayerMask.NameToLayer("PlayerCard"));
    }

    public override void Exit()
    {
        GameController.Instance.UpdateRaycastPhysics(1 << LayerMask.NameToLayer("Nothing"));
    }

    public override void UpdateLogic()
    {
        if (turnController.TurnEnded)
        {
            if (GameController.Instance.Board.Slots.Any(slot => !slot.Occupied))
            {
                turnController.ChangeState(new EnemyTurnState());
            }
            else
            {
                turnController.ChangeState(new EndMatchState());
            }
        }
    }
}
