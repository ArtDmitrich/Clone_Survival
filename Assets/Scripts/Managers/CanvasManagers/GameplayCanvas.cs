using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameplayCanvas : MonoBehaviour
{
    public UnityAction PausePressed;
    public UnityAction ResumePressed;
    public UnityAction MainMenuPressed;

    [SerializeField] private Button _pause;
    [SerializeField] private Button _resume;
    [SerializeField] private Button _mainMenu;
    [SerializeField] private GameObject _gameMenu;

    private void CallPause()
    {
        _gameMenu.SetActive(true);
        _pause.gameObject.SetActive(false);

        PausePressed?.Invoke();
    }

    private void ResumeGame()
    {
        _gameMenu.SetActive(false);
        _pause.gameObject.SetActive(true);

        ResumePressed?.Invoke();
    }

    private void BackToMainMenu()
    {
        MainMenuPressed?.Invoke();
    }

    private void OnEnable()
    {
        _pause.onClick.AddListener(CallPause);
        _resume.onClick.AddListener(ResumeGame);
        _mainMenu.onClick.AddListener(BackToMainMenu);
    }
    private void OnDisable()
    {
        _pause.onClick.RemoveListener(CallPause);
        _resume.onClick.RemoveListener(ResumeGame);
        _mainMenu.onClick.RemoveListener(BackToMainMenu);
    }
}
