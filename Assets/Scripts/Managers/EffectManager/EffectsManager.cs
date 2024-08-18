using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : ItemManagerSingleton<EffectsManager>
{
    public Effect GetEffect(string effectName)
    {
        return _poolManager.GetPooledItem<Effect>(effectName);
    }
}
