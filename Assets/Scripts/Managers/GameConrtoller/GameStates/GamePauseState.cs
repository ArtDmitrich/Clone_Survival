using UnityEngine;

public class GamePauseState : GameBaseState
{
    public override void EnterState(GameController gameController)
    {
        Time.timeScale = 0.0f;
    }

    public override void ExitState(GameController gameController)
    {
        Time.timeScale = 1.0f;
    }
}
