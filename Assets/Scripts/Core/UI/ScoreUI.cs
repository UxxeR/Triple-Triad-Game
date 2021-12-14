using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text blueTeamScore;
    [SerializeField] private TMP_Text redTeamScore;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time. Only called once.
    /// </summary>
    private void Start()
    {
        GameController.Instance.OnSlotUpdated += UpdateScore;
    }

    /// <summary>
    /// Update the game score.
    /// </summary>
    /// <param name="blueScore">Blue team score.</param>
    /// <param name="redScore">Red team score.</param>
    public void UpdateScore(int blueScore, int redScore)
    {
        blueTeamScore.text = blueScore.ToString();
        redTeamScore.text = redScore.ToString();
    }
}
