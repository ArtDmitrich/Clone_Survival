using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GamePlaySceneController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _background;

    private readonly GameplayBaseState _pauseState = new PauseState();
    private readonly GameplayBaseState _playState = new PlayState();

    private GameplayBaseState _currentState;

    private SceneLoader _sceneLoader;
    private GameplayCanvas _gameplayCanvas;
    private GameplayManager _gameplayManager;
    private ResourceManager _resourceManager;
    private GameSettings _gameSettings;

    [Inject]
    private void Construct(SceneLoader sceneLoader, GameplayCanvas gameplayCanvas, GameplayManager gameplayManager, ResourceManager resourceManager, GameSettings gameSettings)
    {
        _sceneLoader = sceneLoader;
        _gameplayCanvas = gameplayCanvas;
        _gameplayManager = gameplayManager;
        _resourceManager = resourceManager;
        _gameSettings = gameSettings;
    }

    private void Pause()
    {
        TransitionToState(_pauseState);
        var playerHealthInfo = _gameplayManager.PlayerHealthInfo;
        _gameplayCanvas.SetInfoValues(_resourceManager.EnemiesKilled, _resourceManager.CurrentPlayerLevel, _resourceManager.Gold,
            (int)playerHealthInfo.x, (int)playerHealthInfo.y, playerHealthInfo.z,
            _resourceManager.CurrentMana, _resourceManager.ManaToNextLevel);
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

    private void GameplayEnd(bool isPlayerWin)
    {
        Pause();
        _gameplayCanvas.CallGameplayEndMenu(isPlayerWin);
    }

    private void ChangeManaValue(float value)
    {
        _gameplayCanvas.SetManaBarValue(value);
    }

    private void ChangeHealthValue(float value)
    {
        _gameplayCanvas.SetHealthBarValue(value);
    }

    private void StartPlayerUpdating(List<Upgrade> possibleUpgrades)
    {
        TransitionToState(_pauseState);
        _gameplayCanvas.CallUpgradeMenu(possibleUpgrades);
    }

    private void GetSelectedUpgrade(Upgrade upgrade)
    {
        TransitionToState(_playState);
        _gameplayManager.UpgradePlayer(upgrade);
    }

    private void Awake()
    {
        //TODO: сделать сущность, которая хранит настройки игровой сессии (список волн, задий фон, настройки игрока (купленные за голд в магазине), бесконечный ли режим игры)
        _background.sprite = _gameSettings.Background;
        _gameplayManager.SetGameSettings(_gameSettings.GameModeIsEndless, _gameSettings.SpecialWaves);
    }

    private void Start()
    {
        TransitionToState(_playState);
        _gameplayManager.StartGameplay();
    }

    private void OnEnable()
    {
        _gameplayManager.GameplayEnded += GameplayEnd;
        _gameplayManager.PlayerHealthChanged += ChangeHealthValue;
        _gameplayManager.PlayeraLevelUpped += StartPlayerUpdating;

        _gameplayCanvas.PausePressed += Pause;
        _gameplayCanvas.ResumePressed += Resume;
        _gameplayCanvas.MainMenuPressed += BackToMainMenu;
        _gameplayCanvas.UpgradeSelected += GetSelectedUpgrade;

        _resourceManager.ManaRatioChanged += ChangeManaValue;
    }

    private void OnDisable()
    {
        _gameplayManager.GameplayEnded -= GameplayEnd;
        _gameplayManager.PlayerHealthChanged -= ChangeHealthValue;
        _gameplayManager.PlayeraLevelUpped -= StartPlayerUpdating;

        _gameplayCanvas.PausePressed -= Pause;
        _gameplayCanvas.ResumePressed -= Resume;
        _gameplayCanvas.MainMenuPressed -= BackToMainMenu;
        _gameplayCanvas.UpgradeSelected -= GetSelectedUpgrade;

        _resourceManager.ManaRatioChanged -= ChangeManaValue;
    }
}
