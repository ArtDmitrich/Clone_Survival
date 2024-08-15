using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuSceneController : MonoBehaviour
{
    [SerializeField] private GameLevelsSettings _gameLevelsSettings;

    private SceneLoader _sceneLoader;
    private MainMenuCanvas _mainMenuCanvas;
    private GameSettings _gameSettings;
    private GameLevelSelector _gameLevelSelector;

    [Inject]
    private void Costruct(SceneLoader sceneLoader, MainMenuCanvas mainMenuCanvas, GameSettings gameSettings, GameLevelSelector gameLevelSelector)
    {
        _sceneLoader = sceneLoader;
        _mainMenuCanvas = mainMenuCanvas;
        _gameSettings = gameSettings;
        _gameLevelSelector = gameLevelSelector;
    }

    private void LoadSelectedLevel(GameLevelInfo gameLevelInfo, bool gameModeIsEndless)
    {
        _mainMenuCanvas.ActivateLoadingBackground();

        _gameSettings.GameModeIsEndless = gameModeIsEndless;
        _gameSettings.SpecialWaves = gameLevelInfo.WaveSettings;
        _gameSettings.Background = gameLevelInfo.Background;

        _sceneLoader.Load(Scenes.Gameplay);
    }

    private void OnEnable()
    {
        _gameLevelSelector.GameLevelSelected += LoadSelectedLevel;
    }

    private void OnDisable()
    {
        _gameLevelSelector.GameLevelSelected -= LoadSelectedLevel;
    }
}
