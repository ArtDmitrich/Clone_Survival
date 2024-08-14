using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelSelectingButton : MonoBehaviour
{
    public UnityAction<bool, WaveSettings, Sprite> LevelSelected;
    public int Level;

    [SerializeField] private bool _gameModeIsEndless;
    [SerializeField] private WaveSettings _waveSettings;
    [SerializeField] private Sprite _background;

    private Button _levelButton;

    private void SelectLevel()
    {
        LevelSelected?.Invoke(_gameModeIsEndless, _waveSettings, _background);
    }

    private void Awake()
    {
        _levelButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _levelButton.onClick.AddListener(SelectLevel);
    }

    private void OnDisable()
    {
        _levelButton.onClick.RemoveListener(SelectLevel);
    }
}
