using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMatchState : BaseState
{
    private float timeToDecideTurn = 0f;
    private bool turnDecided = false;

    public override void Enter()
    {
        GameController.Instance.UpdateRaycastPhysics(1 << LayerMask.NameToLayer("Nothing"));
        timeToDecideTurn = 0f;
        turnDecided = false;
        turnController.UpdateTurnVisibility(1f);
    }

    public override void Exit()
    {
        turnController.UpdateTurnVisibility(0f);
    }

    public override void UpdateLogic()
    {
        timeToDecideTurn += Time.deltaTime;

        if (timeToDecideTurn >= 1f && !turnDecided)
        {
            DecideTeamTurn();
        }

        if (timeToDecideTurn >= 2f && turnDecided)
        {
            turnController.TurnIndicator.color = new CustomColorAttribute("#000000F7").HexadecimalToRGBColor();

            if (turnController.TeamTurn == Team.BLUE)
            {
                turnController.ChangeState(new PlayerTurnState());
            }
            else
            {
                turnController.ChangeState(new EnemyTurnState());
            }
        }
    }

    public void DecideTeamTurn()
    {
        turnController.TeamTurn = (Team)Random.Range(0, System.Enum.GetNames(typeof(Team)).Length);
        turnController.UpdateTurnWindow(GenericAttribute.GetAttribute<CustomColorAttribute>(turnController.TeamTurn).HexadecimalToRGBColor(), $"{turnController.TeamTurn.ToString()} TURN");
        turnDecided = true;
    }
}
