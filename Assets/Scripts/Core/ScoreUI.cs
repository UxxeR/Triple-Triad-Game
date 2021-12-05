using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text blueTeamScore;
    public TMP_Text redTeamScore;

    private void Start()
    {
        GameController.instance.OnSlotUpdated += UpdateScore;
    }

    public void UpdateScore(int blueScore, int redScore)
    {
        blueTeamScore.text = blueScore.ToString();
        redTeamScore.text = redScore.ToString();
    }
}
