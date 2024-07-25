using UnityEngine;
using Zenject;

public class InfarstructureInstaller : MonoInstaller
{
    [SerializeField] private InputController _inputController;
    [SerializeField] private GameplayManager _gameController;

    public override void InstallBindings()
    {
        Container.Bind<InputController>().FromInstance(_inputController).AsSingle().NonLazy();
        Container.Bind<GameplayManager>().FromInstance(_gameController).AsSingle().NonLazy();
    }
}