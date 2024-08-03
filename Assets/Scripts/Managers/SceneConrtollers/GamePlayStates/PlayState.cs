using UnityEngine;

public class PlayState : GameplayBaseState
{
    public override void EnterState(GamePlaySceneManager gameplayScene)
    {
        Time.timeScale = 1.0f;
    }

    public override void ExitState(GamePlaySceneManager gameplayScene)
    {
    }
}
