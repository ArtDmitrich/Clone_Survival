using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameModeSelectingButton : MonoBehaviour
{
    public UnityAction<bool> GameModeSelected;

    [SerializeField] private bool _gameModeIsEndless;

    private Button _levelButton;

    private void SelectLevel()
    {
        GameModeSelected?.Invoke(_gameModeIsEndless);
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
