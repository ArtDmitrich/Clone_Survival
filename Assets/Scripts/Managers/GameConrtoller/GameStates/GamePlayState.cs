using UnityEngine.SceneManagement;

public class GamePlayState : GameBaseState
{
    public override void EnterState(GameController gameController)
    {
    }

    public override void ExitState(GameController gameController)
    {
        gameController.NextScene = Scenes.MainMenu;
        gameController.NextState = gameController.MainMenuState;
    }
}
