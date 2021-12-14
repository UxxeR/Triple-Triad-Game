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

    /// <summary>
    /// Update the visibility of the turn UI.
    /// </summary>
    /// <param name="alpha">The new alpha of the window, [0f,1f]</param>
    public void UpdateTurnVisibility(float alpha)
    {
        if (OnVisibilityUpdated != null)
        {
            OnVisibilityUpdated(alpha);
        }
    }

    /// <summary>
    /// Update the color and text of the turn UI.
    /// </summary>
    /// <param name="backgroundColor">New color.</param>
    /// <param name="text">New text.</param>
    public void UpdateTurnWindow(Color backgroundColor, string text)
    {
        if (OnTurnUpdated != null)
        {
            OnTurnUpdated(backgroundColor, text);
        }
    }

    /// <summary>
    /// First method that will be called when a script is enabled. Only called once.
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Change the current state of the game.
    /// </summary>
    /// <param name="nextState">Next state.</param>
    public void ChangeState(BaseState nextState)
    {
        currentState?.Exit();
        currentState = nextState;
        currentState?.Enter();
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateLogic();
        }
    }
}
