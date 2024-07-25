using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveSettings", order = 1)]
public class WaveSettings : ScriptableObject
{
    public List<WavesData> WavesDatas;
    public float TimeToNextWave;
}
