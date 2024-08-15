using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameLevelsSettings", order = 1)]
public class GameLevelsSettings : ScriptableObject
{
    public int LevelsCount
    {
        get
        {
            return _levels.Count;
        }
    }

    [SerializeField] private List<GameLevelInfo> _levels;

    public GameLevelInfo GetGameLevelInfo(int index)
    {
        if (LevelsCount == 0)
        {
            return new GameLevelInfo();
        }

        if (index >= LevelsCount)
        {
            return _levels[^1];
        }

        return _levels[index];
    }
}
