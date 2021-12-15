public abstract class BaseState
{
    protected TurnController turnController;

    /// <summary>
    /// Called when a class is created.
    /// Set default values of the class.
    /// Values are inherited by the other states.
    /// </summary>
    public BaseState()
    {
        turnController = TurnController.Instance;
    }

    /// <summary>
    /// Called when a state start.
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// Called when a state finish.
    /// </summary>
    public abstract void Exit();

    /// <summary>
    /// Called by the state every frame.
    /// </summary>
    public abstract void UpdateLogic();
}
