using System;
using System.Collections.Generic;

[Serializable]
public struct WaveData
{
    public string Title;
    public List<ChunkWaveData> chunksOfWaves;
}
