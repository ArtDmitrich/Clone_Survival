using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuController : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    [Inject]
    private void Cnstruct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void LoadGamePlayScene()
    {
        Debug.Log((int)Scenes.Gameplay);
        _sceneLoader.Load((int)Scenes.Gameplay);
    }
}
