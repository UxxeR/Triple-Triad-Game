using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnUI : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text teamTurn;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        TurnController.Instance.OnVisibilityUpdated += UpdateVisibility;
        TurnController.Instance.OnTurnUpdated += UpdateTurnText;
    }

    private void UpdateVisibility(float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    private void UpdateTurnText(Color backgroundColor, string turn)
    {
        background.color = backgroundColor;
        teamTurn.text = turn;
    }
}
