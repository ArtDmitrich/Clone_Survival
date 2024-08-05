using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private InputController _inputController;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private TimersManager _timersManager;
    [SerializeField] private PickUpItemsManager _pickUpItemsManager;
    [SerializeField] private EnemiesManager _enemiesManager;
    [SerializeField] private GameplayCanvas _gameplayCanvas;
    [SerializeField] private GameplayManager _gameplayManager;

    public override void InstallBindings()
    {
        Container.Bind<InputController>().FromInstance(_inputController).AsSingle().NonLazy();
        Container.Bind<ResourceManager>().FromInstance(_resourceManager).AsSingle().NonLazy();
        Container.Bind<TimersManager>().FromInstance(_timersManager).AsSingle().NonLazy();
        Container.Bind<PickUpItemsManager>().FromInstance(_pickUpItemsManager).AsSingle().NonLazy();
        Container.Bind<EnemiesManager>().FromInstance(_enemiesManager).AsSingle().NonLazy();
        Container.Bind<GameplayCanvas>().FromInstance(_gameplayCanvas).AsSingle().NonLazy();
        Container.Bind<GameplayManager>().FromInstance(_gameplayManager).AsSingle().NonLazy();
    }
}