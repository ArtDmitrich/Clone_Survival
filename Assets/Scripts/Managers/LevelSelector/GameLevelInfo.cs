using System;
using UnityEngine;

[Serializable]
public struct GameLevelInfo
{
    public int LevelNumber;
    public Sprite Icon;
    public string Info;
    public WaveSettings WaveSettings;
    public Sprite Background;
}
