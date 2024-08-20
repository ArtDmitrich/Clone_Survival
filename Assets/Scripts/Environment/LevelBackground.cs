using System.Collections.Generic;
using UnityEngine;

public class LevelBackground : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _spriteRenderers;

    public void SetBackground(Sprite sprite)
    {
        for (int i = 0; i < _spriteRenderers.Count; i++)
        {
            _spriteRenderers[i].sprite = sprite;
        }
    }
}
