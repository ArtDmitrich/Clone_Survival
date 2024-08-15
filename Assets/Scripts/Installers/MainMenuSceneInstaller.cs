using UnityEngine;
using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    [SerializeField] private MainMenuCanvas _mainMenuCanvas;
    [SerializeField] private GameLevelSelector _gameLevelSelector;
    public override void InstallBindings()
    {
        Container.Bind<MainMenuCanvas>().FromInstance(_mainMenuCanvas).AsSingle().NonLazy();
        Container.Bind<GameLevelSelector>().FromInstance(_gameLevelSelector).AsSingle().NonLazy();
    }
}