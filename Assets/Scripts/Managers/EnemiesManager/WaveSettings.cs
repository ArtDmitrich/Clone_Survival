using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveSettings", order = 1)]
public class WaveSettings : ScriptableObject
{
    public float SurvivalTime;
    public float PickUpItemColdown;
    public float StartSpawnEnemyColdown;
    public float MinSpawnEnemyColdown;
    public float DecreaseEnemyColdownValue;
    public int StartEnemySpawnCount;
    public float SpecialWaveColdown;

    public List<WaveData> WavesDatas;
}
