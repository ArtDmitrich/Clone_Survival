using UnityEngine;

public class GameController : Singleton<GameController>
{
    public GameBaseState NextState;
    public Scenes NextScene;

    public readonly GameBaseState GameplayState = new GamePlayState();
    public readonly GameBaseState MainMenuState = new MainMenuState();
    public readonly GameBaseState BufferState = new BufferState();


    private GameBaseState _currentState;
    
    public void TransitionToState(GameBaseState state)
    {
        if (_currentState != null)
        {
            _currentState.ExitState(this);
        }

        _currentState = state;
        _currentState.EnterState(this);
    }
}
