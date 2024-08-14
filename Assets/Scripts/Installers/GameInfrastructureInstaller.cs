using UnityEngine;
using Zenject;

public class GameInfrastructureInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().AsSingle().NonLazy();
        Container.Bind<GameSettings>().AsSingle().NonLazy();
;    }
}