using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMatchState : BaseState
{
    private List<Card> gameCards;
    private float timeToShow = 0f;

    public override void Enter()
    {
        timeToShow = 0f;
        GameController.Instance.UpdateRaycastPhysics(1 << LayerMask.NameToLayer("Nothing"));
        turnController.TurnIndicator.color = new CustomColorAttribute("#00000000").HexadecimalToRGBColor();
        gameCards = GameController.Instance.GameCards;
        int blueScore = gameCards.Where(card => card.Team == Team.BLUE).Count();

        if (blueScore > gameCards.Count / 2)
        {
            turnController.UpdateTurnWindow(GenericAttribute.GetAttribute<CustomColorAttribute>(Team.BLUE).HexadecimalToRGBColor(), $"{Team.BLUE.ToString()} WIN");
        }
        else if (blueScore < gameCards.Count / 2)
        {
            turnController.UpdateTurnWindow(GenericAttribute.GetAttribute<CustomColorAttribute>(Team.RED).HexadecimalToRGBColor(), $"{Team.RED.ToString()} WIN");
        }
        else
        {
            turnController.UpdateTurnWindow(new CustomColorAttribute("#131313").HexadecimalToRGBColor(), $"DRAW");
        }
    }

    public override void Exit()
    {
        //Nothing
    }

    public override void UpdateLogic()
    {
        timeToShow += Time.deltaTime;

        if (timeToShow > 0.7f)
        {
            turnController.UpdateTurnVisibility(1f);
        }

        if (timeToShow > 3f)
        {
            SceneManager.LoadScene(0);
        }
    }
}
