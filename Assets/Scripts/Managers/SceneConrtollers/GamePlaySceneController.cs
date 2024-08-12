using System.Collections.Generic;
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
    private ResourceManager _resourceManager;

    [Inject]
    private void Construct(SceneLoader sceneLoader, GameplayCanvas gameplayCanvas, GameplayManager gameplayManager, ResourceManager resourceManager)
    {
        _sceneLoader = sceneLoader;
        _gameplayCanvas = gameplayCanvas;
        _gameplayManager = gameplayManager;
        _resourceManager = resourceManager;
    }

    private void Pause()
    {
        TransitionToState(PauseState);
        var playerHealthInfo = _gameplayManager.PlayerHealthInfo;
        _gameplayCanvas.SetInfoValues(_resourceManager.EnemiesKilled, _resourceManager.CurrentPlayerLevel, _resourceManager.Gold,
            (int)playerHealthInfo.x, (int)playerHealthInfo.y, playerHealthInfo.z,
            _resourceManager.CurrentMana, _resourceManager.ManaToNextLevel);
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
        TransitionToState(PauseState);
        _gameplayCanvas.CallUpgradeMenu(possibleUpgrades);
    }

    private void GetSelectedUpgrade(Upgrade upgrade)
    {
        TransitionToState(PlayState);
        _gameplayManager.UpgradePlayer(upgrade);
    }

    private void Start()
    {
        TransitionToState(PlayState);
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
