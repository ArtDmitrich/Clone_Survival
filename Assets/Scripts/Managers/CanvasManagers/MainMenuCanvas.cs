using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    public UnityAction StartPressed;
    public List<LevelSelectingButton> LevelSelectingButtons;

    [SerializeField] private Button _start;
    [SerializeField] private Image _loadingBackground;

    private void StartGame()
    {
        _loadingBackground.gameObject.SetActive(true);

        StartPressed?.Invoke();
    }

    private void OnEnable()
    {
        _loadingBackground.gameObject.SetActive(false);

        _start.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _start.onClick.RemoveListener(StartGame);
    }
}
