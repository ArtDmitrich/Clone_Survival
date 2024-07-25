public class GameController : Singleton<GameController>
{
    public readonly GamePlayState PlayState = new GamePlayState();
    public readonly GamePauseState PauseState = new GamePauseState();
    public readonly GameWinState GameWinState = new GameWinState();
    public readonly GameOverState GameOverState = new GameOverState();

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
