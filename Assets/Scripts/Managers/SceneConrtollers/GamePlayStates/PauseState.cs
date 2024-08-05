using UnityEngine;

public class PauseState : GameplayBaseState
{
    public override void EnterState(GamePlaySceneController gameplayScene)
    {
        Time.timeScale = 0.0f;
    }

    public override void ExitState(GamePlaySceneController gameplayScene)
    {
        Time.timeScale = 1.0f;
    }
}
