using UnityEngine;
using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    [SerializeField] private MainMenuCanvas _mainMenuCanvas;
    public override void InstallBindings()
    {
        Container.Bind<MainMenuCanvas>().FromInstance(_mainMenuCanvas).AsSingle().NonLazy();
    }
}