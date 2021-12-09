using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public static TurnController Instance { get; private set; }
    private BaseState currentState;
    [field: SerializeField] public Team TeamTurn { get; set; }
    [field: SerializeField] public SpriteRenderer TurnIndicator { get; set; }
    [HideInInspector] public bool TurnEnded { get; set; } = false;
    public Action<float> OnVisibilityUpdated { get; set; }
    public Action<Color, string> OnTurnUpdated { get; set; }

    public void UpdateTurnVisibility(float alpha)
    {
        if (OnVisibilityUpdated != null)
        {
            OnVisibilityUpdated(alpha);
        }
    }

    public void UpdateTurnWindow(Color backgroundColor, string text)
    {
        if (OnTurnUpdated != null)
        {
            OnTurnUpdated(backgroundColor, text);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeState(BaseState nextState)
    {
        currentState?.Exit();
        currentState = nextState;
        currentState?.Enter();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateLogic();
        }
    }
}
