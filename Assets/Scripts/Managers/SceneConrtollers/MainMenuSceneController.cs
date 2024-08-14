using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuSceneController : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    private MainMenuCanvas _mainMenuCanvas;
    private GameSettings _gameSettings;

    [Inject]
    private void Costruct(SceneLoader sceneLoader, MainMenuCanvas mainMenuCanvas, GameSettings gameSettings)
    {
        _sceneLoader = sceneLoader;
        _mainMenuCanvas = mainMenuCanvas;
        _gameSettings = gameSettings;
    }

    private void LoadSelectedLevel(bool gameModeIsEndless, WaveSettings waveSettings, Sprite background)
    {
        _gameSettings.GameModeIsEndless = gameModeIsEndless;
        _gameSettings.SpecialWaves = waveSettings;
        _gameSettings.Background = background;

        _sceneLoader.Load(Scenes.Gameplay);
    }

    private void LoadGamePlayScene()
    {
        _sceneLoader.Load(Scenes.Gameplay);
    }

    private void OnEnable()
    {
        _mainMenuCanvas.StartPressed += LoadGamePlayScene;

        var levelButtons = _mainMenuCanvas.LevelSelectingButtons;

        for (int i = 0; i < levelButtons.Count; i++)
        {
            levelButtons[i].LevelSelected += LoadSelectedLevel;
        }
    }

    private void OnDisable()
    {
        _mainMenuCanvas.StartPressed -= LoadGamePlayScene;

        var levelButtons = _mainMenuCanvas.LevelSelectingButtons;

        for (int i = 0; i < levelButtons.Count; i++)
        {
            levelButtons[i].LevelSelected -= LoadSelectedLevel;
        }
    }
}
