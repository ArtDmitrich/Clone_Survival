using System;

[Serializable]
public struct ChunkWavesData
{
    public EnemyType EnemyType;
    public int Count;
    public int TimeToNextChunk;
    public float EnemySpawnColdown;
}
