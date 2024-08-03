using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : GameBaseState
{
    public override void EnterState(GameController gameController)
    {
    }

    public override void ExitState(GameController gameController)
    {
        gameController.NextScene = Scenes.Gameplay;
        gameController.NextState = gameController.GameplayState;
    }
}
