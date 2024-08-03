using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInfrastructureInstaller : MonoInstaller
{
    [SerializeField] private SceneLoader _sceneLoader;
    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().FromInstance(_sceneLoader).AsSingle().NonLazy();
    }
}
