using UnityEngine;

public class PlayState : GameplayBaseState
{
    public override void EnterState(GamePlaySceneController gameplayScene)
    {
        Time.timeScale = 1.0f;
    }

    public override void ExitState(GamePlaySceneController gameplayScene)
    {
    }
}
