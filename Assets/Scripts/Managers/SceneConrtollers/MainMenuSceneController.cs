using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuSceneController : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    private MainMenuCanvas _mainMenuCanvas;

    [Inject]
    private void Costruct(SceneLoader sceneLoader, MainMenuCanvas mainMenuCanvas)
    {
        _sceneLoader = sceneLoader;
        _mainMenuCanvas = mainMenuCanvas;
    }

    public void LoadGamePlayScene()
    {
        _sceneLoader.Load(Scenes.Gameplay);
    }

    private void OnEnable()
    {
        _mainMenuCanvas.StartPressed += LoadGamePlayScene;
    }

    private void OnDisable()
    {
        _mainMenuCanvas.StartPressed -= LoadGamePlayScene;
    }
}
