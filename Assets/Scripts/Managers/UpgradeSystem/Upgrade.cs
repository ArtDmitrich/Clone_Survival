using UnityEngine;

public abstract class Upgrade: ScriptableObject
{
    public string Title;

    public abstract void Activate(PlayerController player);
}
