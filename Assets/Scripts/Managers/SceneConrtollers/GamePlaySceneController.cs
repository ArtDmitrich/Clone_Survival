using UnityEngine;
using Zenject;

public class GamePlaySceneController : MonoBehaviour
{
    [SerializeField] private LevelBackground _sceneBackground;

    private readonly GameplayBaseState _pauseState = new PauseState();
    private readonly GameplayBaseState _playState = new PlayState();

    private GameplayBaseState _currentState;

    private SceneLoader _sceneLoader;
    private GameplayCanvas _gameplayCanvas;
    private GameplayManager _gameplayManager;
    private GameSettings _gameSettings;

    [Inject]
    private void Construct(SceneLoader sceneLoader, GameplayCanvas gameplayCanvas, GameplayManager gameplayManager, GameSettings gameSettings)
    {
        _sceneLoader = sceneLoader;
        _gameplayCanvas = gameplayCanvas;
        _gameplayManager = gameplayManager;
        _gameSettings = gameSettings;
    }

    private void Pause()
    {
        TransitionToState(_pauseState);
    }

    private void Resume()
    {
        TransitionToState(_playState);
    }

    private void BackToMainMenu()
    {
        TransitionToState(_playState);
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

    private void UpdateMaxOppenedLevel(bool isPlayerWin)
    {
        if (isPlayerWin)
        {
            var maxOpennedLvl = PlayerPrefs.GetInt("MaxOpennedLevel", 1);

            if (maxOpennedLvl == _gameSettings.CurrentLevelNumber)
            {
                maxOpennedLvl++;
                PlayerPrefs.SetInt("MaxOpennedLevel", maxOpennedLvl);
            }
        }
    }

    private void Start()
    {
        _sceneBackground.SetBackground(_gameSettings.Background);
        _gameplayManager.SetGameSettings(_gameSettings.GameModeIsEndless, _gameSettings.SpecialWaves, _gameSettings.EnemyUpgradeSettings);

        TransitionToState(_playState);
        _gameplayManager.StartGameplay();
    }

    private void OnEnable()
    {
        _gameplayCanvas.Paused += Pause;
        _gameplayCanvas.Resumed += Resume;
        _gameplayCanvas.MainMenuPressed += BackToMainMenu;

        _gameplayManager.GameplayEnded += UpdateMaxOppenedLevel;
    }

    private void OnDisable()
    {
        _gameplayCanvas.Paused -= Pause;
        _gameplayCanvas.Resumed -= Resume;
        _gameplayCanvas.MainMenuPressed -= BackToMainMenu;

        _gameplayManager.GameplayEnded -= UpdateMaxOppenedLevel;
    }
}
