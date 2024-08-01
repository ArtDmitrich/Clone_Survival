using UnityEngine;
using Zenject;

public class InfarstructureInstaller : MonoInstaller
{
    [SerializeField] private InputController _inputController;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private TimersManager _timersManager;

    [SerializeField] private PickUpItemsManager _pickUpItemsManager;
    [SerializeField] private EnemiesManager EnemiesManager;


    public override void InstallBindings()
    {
        Container.Bind<InputController>().FromInstance(_inputController).AsSingle().NonLazy();
        Container.Bind<ResourceManager>().FromInstance(_resourceManager).AsSingle().NonLazy();
        Container.Bind<TimersManager>().FromInstance(_timersManager).AsSingle().NonLazy();

        Container.Bind<PickUpItemsManager>().FromInstance(_pickUpItemsManager).AsSingle().NonLazy();
        Container.Bind<EnemiesManager>().FromInstance(EnemiesManager).AsSingle().NonLazy();
    }
}