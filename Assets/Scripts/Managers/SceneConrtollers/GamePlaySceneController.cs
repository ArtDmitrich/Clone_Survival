using UnityEngine;
using Zenject;

public class GamePlaySceneController : MonoBehaviour
{
    public readonly GameplayBaseState PauseState = new PauseState();
    public readonly GameplayBaseState PlayState = new PlayState();

    private GameplayBaseState _currentState;

    private SceneLoader _sceneLoader;
    private GameplayCanvas _gameplayCanvas;
    private GameplayManager _gameplayManager;

    [Inject]
    private void Construct(SceneLoader sceneLoader, GameplayCanvas gameplayCanvas, GameplayManager gameplayManager)
    {
        _sceneLoader = sceneLoader;
        _gameplayCanvas = gameplayCanvas;
        _gameplayManager = gameplayManager;
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
        TransitionToState(PlayState);
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

    private void GameplayEnd(bool isPlayerWin)
    {
        TransitionToState(PauseState);
        _gameplayCanvas.CallGameplayEndMenu(isPlayerWin);
    }


    private void Start()
    {
        TransitionToState(PlayState);
        _gameplayManager.StartGameplay();
    }

    private void OnEnable()
    {
        _gameplayManager.GameplayEnded += GameplayEnd;

        _gameplayCanvas.PausePressed += Pause;
        _gameplayCanvas.ResumePressed += Resume;
        _gameplayCanvas.MainMenuPressed += BackToMainMenu;
    }

    private void OnDisable()
    {
        _gameplayManager.GameplayEnded -= GameplayEnd;

        _gameplayCanvas.PausePressed -= Pause;
        _gameplayCanvas.ResumePressed -= Resume;
        _gameplayCanvas.MainMenuPressed -= BackToMainMenu;
    }
}
