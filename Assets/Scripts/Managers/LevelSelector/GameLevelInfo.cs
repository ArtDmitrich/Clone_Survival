using System;
using UnityEngine;

[Serializable]
public struct GameLevelInfo
{
    public int LevelNumber;

    public Sprite Icon;
    public string Info;

    public Sprite Background;

    public WaveSettings WaveSettings;
    public EnemyUpgradeSettings EnemyUpgradeSettings;
}
