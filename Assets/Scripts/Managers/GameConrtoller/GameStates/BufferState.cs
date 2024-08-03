using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BufferState : GameBaseState
{
    public override void EnterState(GameController gameController)
    {
        SceneManager.LoadScene((int)Scenes.Buffer);
    }

    public override void ExitState(GameController gameController)
    {
        SceneManager.LoadScene((int)gameController.NextScene);
        gameController.TransitionToState(gameController.NextState);
    }
}
