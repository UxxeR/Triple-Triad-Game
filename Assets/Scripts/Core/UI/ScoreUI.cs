using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text blueTeamScore;
    [SerializeField] private TMP_Text redTeamScore;
    

    private void Start()
    {
        GameController.Instance.OnSlotUpdated += UpdateScore;
    }

    public void UpdateScore(int blueScore, int redScore)
    {
        blueTeamScore.text = blueScore.ToString();
        redTeamScore.text = redScore.ToString();
    }
}
