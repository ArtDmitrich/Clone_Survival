using UnityEngine;
using Zenject;

public class BufferSceneInstaller : MonoInstaller
{
    [SerializeField] private BufferCanvas _bufferCanvas;

    public override void InstallBindings()
    {
        Container.Bind<BufferCanvas>().FromInstance(_bufferCanvas).AsSingle().NonLazy();
    }
}