using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected TurnController turnController;

    public BaseState()
    {
        turnController = TurnController.Instance;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void UpdateLogic();
}
