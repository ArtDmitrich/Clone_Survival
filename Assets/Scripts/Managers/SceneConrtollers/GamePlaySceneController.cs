using UnityEngine;
using Zenject;

public class GamePlaySceneController : MonoBehaviour
{
    public readonly GameplayBaseState PauseState = new PauseState();
    public readonly GameplayBaseState PlayState = new PlayState();

    private GameplayBaseState _currentState;
    private SceneLoader _sceneLoader;
    private GameplayCanvas _gameplayCanvas;

    [Inject]
    private void Construct(SceneLoader sceneLoader, GameplayCanvas gameplayCanvas)
    {
        _sceneLoader = sceneLoader;
        _gameplayCanvas = gameplayCanvas;
    }

    private void Pause()
    {
        TransitionToState(PauseState);
    }

    private void Resume()
    {
        TransitionToState(PlayState);
    }

    private void BackToMainMenu()
    {
        _sceneLoader.Load(Scenes.MainMenu);
    }

    private void TransitionToState(GameplayBaseState state)
    {
        if (_currentState != null)
        {
            _currentState.ExitState(this);
        }

        _currentState = state;
        _currentState.EnterState(this);
    }

    private void Start()
    {
        TransitionToState(PlayState);
    }

    private void OnEnable()
    {
        _gameplayCanvas.PausePressed += Pause;
        _gameplayCanvas.ResumePressed += Resume;
        _gameplayCanvas.MainMenuPressed += BackToMainMenu;
    }

    private void OnDisable()
    {
        _gameplayCanvas.PausePressed -= Pause;
        _gameplayCanvas.ResumePressed -= Resume;
        _gameplayCanvas.MainMenuPressed -= BackToMainMenu;
    }
}
