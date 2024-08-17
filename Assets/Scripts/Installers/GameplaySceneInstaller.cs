using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private InputController _inputController;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private TimersManager _timersManager;
    [SerializeField] private PickUpItemsManager _pickUpItemsManager;
    [SerializeField] private EnemiesManager _enemiesManager;
    [SerializeField] private GameplayCanvas _gameplayCanvas;
    [SerializeField] private GameplayManager _gameplayManager;
    [SerializeField] private UpgradeSystem _upgradeSystem;
    [SerializeField] private UpgradeMenu _upgradeMenu;

    public override void InstallBindings()
    {
        Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle().NonLazy();

        Container.Bind<InputController>().FromInstance(_inputController).AsSingle().NonLazy();
        Container.Bind<ResourceManager>().FromInstance(_resourceManager).AsSingle().NonLazy();
        Container.Bind<TimersManager>().FromInstance(_timersManager).AsSingle().NonLazy();
        Container.Bind<PickUpItemsManager>().FromInstance(_pickUpItemsManager).AsSingle().NonLazy();
        Container.Bind<EnemiesManager>().FromInstance(_enemiesManager).AsSingle().NonLazy();
        Container.Bind<GameplayCanvas>().FromInstance(_gameplayCanvas).AsSingle().NonLazy();
        Container.Bind<GameplayManager>().FromInstance(_gameplayManager).AsSingle().NonLazy();
        Container.Bind<UpgradeSystem>().FromInstance(_upgradeSystem).AsSingle().NonLazy();
        Container.Bind<UpgradeMenu>().FromInstance(_upgradeMenu).AsSingle().NonLazy();
    }
}