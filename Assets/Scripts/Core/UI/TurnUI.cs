using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnUI : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text teamTurn;
    [SerializeField] private Slider turnTimer;
    [SerializeField] private TMP_Text turnTimerText;
    [SerializeField] private CanvasGroup canvasGroup;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time. Only called once.
    /// </summary>
    private void Start()
    {
        TurnController.Instance.OnVisibilityUpdated += UpdateVisibility;
        TurnController.Instance.OnTurnUpdated += UpdateTurnText;
        TurnController.Instance.OnTimerUpdated += UpdateMaxTurnTimer;
        TurnController.Instance.OnTimerProgressed += UpdateTurnTimer;
    }

    /// <summary>
    /// Update the visibility of the turn window.
    /// </summary>
    /// <param name="alpha">New alpha, [0f,1f].</param>
    private void UpdateVisibility(float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    /// <summary>
    /// Update the color and text of the turn UI.
    /// </summary>
    /// <param name="backgroundColor">New color.</param>
    /// <param name="turn">New text.</param>
    private void UpdateTurnText(Color backgroundColor, string turn)
    {
        background.color = backgroundColor;
        teamTurn.text = turn;
    }

    private void UpdateTurnTimer(float turnTimerProgress)
    {
        this.turnTimer.value = turnTimerProgress;
        this.turnTimerText.text = turnTimerProgress.ToString("0.00");
    }

    private void UpdateMaxTurnTimer(float maxTurnTimer)
    {
        this.turnTimer.maxValue = maxTurnTimer;
        this.turnTimer.value = maxTurnTimer;
        this.turnTimerText.text = maxTurnTimer.ToString("0.00");
    }
}
