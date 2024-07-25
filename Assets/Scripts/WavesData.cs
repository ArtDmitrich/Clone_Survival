using System;
using System.Collections.Generic;

[Serializable]
public struct WavesData
{
    public string Title;
    public List<ChunkWavesData> chunksOfWaves;
}
