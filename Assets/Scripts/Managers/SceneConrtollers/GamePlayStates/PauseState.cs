using UnityEngine;

public class PauseState : GameplayBaseState
{
    public override void EnterState(GamePlaySceneManager gameplayScene)
    {
        Time.timeScale = 0.0f;
    }

    public override void ExitState(GamePlaySceneManager gameplayScene)
    {
        Time.timeScale = 1.0f;
    }
}
