using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Effect : MonoBehaviour
{
    private PooledItem PooledItem { get { return _pooledItem = _pooledItem ?? GetComponent<PooledItem>(); } }
    private PooledItem _pooledItem;

    private ParticleSystem ParticleSystem { get { return _partivleSystem = _partivleSystem ?? GetComponent<ParticleSystem>(); } }
    private ParticleSystem _partivleSystem;

    void Start()
    {
        var main = ParticleSystem.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        PooledItem.Release();
    }
}
